using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RESTapp.Dtos;
using RESTapp.Helpers;
using RESTapp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RESTapp.Data
{
    public class UserDAL : IUser
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _appSettings;

        public UserDAL(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            // Add Secretkey Settings
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //Injcting the Secret key methods
            _appSettings = appSettings.Value;
        }

        public async Task AddRole(string rolename)
        {
            IdentityResult roleResult;
            try
            {
                //Checking if the role is exist or not on database
                var roleIsExist = await _roleManager.RoleExistsAsync(rolename);
                if(!roleIsExist)
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(rolename));
                else
                    throw new System.Exception($"Role {rolename} already exists");
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }

        public async Task AddUserToRole(string username, string role)
        {
           var user = await _userManager.FindByNameAsync(username);

            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception (ex.Message);
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            // Login methods
            var userFind = await _userManager.CheckPasswordAsync(await _userManager.FindByNameAsync(username), password);

            if (!userFind)
                return null;

            var user = new User
            {
                Username = username
            };

            // Claiming the username
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));


            // Getting all Role for to claiming the role into TOken
            var roles = await GetRolesFromUser(username);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Dimasukkan kedalam payload
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            // Set token to Active within 1 hours
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate the Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }


        public async Task<List<string>> GetRolesFromUser(string username)
        {
            List<string> lstRoles = new List<string>();
            var user = await _userManager.FindByEmailAsync(username); 
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                lstRoles.Add(role);
            }

            return lstRoles;


        }

        public async Task Registration(CreateUserDto user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.UserName,
                    Email = user.UserName,

                };

                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                    throw new System.Exception($"Gagal menambahkan User");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<UserDto> GetAllUser()
        {
            List<UserDto> users = new List<UserDto>();
            var results = _userManager.Users;

            foreach (var user in results)
            {
                users.Add(new UserDto { Username = user.UserName });
            }
            return users;
        }

        public IEnumerable<CreateRoleDto> GetAllRoles()
        {
            List<CreateRoleDto> lstRole = new List<CreateRoleDto>();
           var results = _roleManager.Roles;

            foreach (var role in results)
            {
                lstRole.Add(new CreateRoleDto { RoleName = role.Name });
            }

            return lstRole;
        }
    }
}
