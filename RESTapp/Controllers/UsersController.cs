using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTapp.Data;
using RESTapp.Dtos;
using RESTapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTapp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registration(CreateUserDto  user)
        {
            try
            {
                await _user.Registration(user);
                return Ok($"Registrasi user {user.UserName} Berhasil");
            }
            catch (System.Exception ex)
            {

                throw new System.Exception($"{ex.Message}");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            return Ok(_user.GetAllUser());
        }

        [HttpPost("Role")]
        public async Task<ActionResult> AddRole(CreateRoleDto roleDto)
        {
            try
            {
                await _user.AddRole(roleDto.RoleName);
                return Ok($"Tambah Role {roleDto.RoleName} berhasil!");
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("Role")]
        public ActionResult<IEnumerable<CreateRoleDto>> GetAllRoles()
        {
            return Ok(_user.GetAllRoles());
        }

        [HttpPost("UserInRole")]
        public async Task<ActionResult> AddUserToRole(string username, string role)
        {
            try
            {
                await _user.AddUserToRole(username, role);
                return Ok($"Berhasil menambahkan user {username} ke Role {role}");
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }

        }
        [HttpGet("RolesByUser/username")]
        public async Task<ActionResult<List<string>>> GetRolesFromUser(string username)
        {
            var results =  await _user.GetRolesFromUser(username);

            return Ok(results);
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<ActionResult<User>> Authentication(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _user.Authenticate(createUserDto.UserName, createUserDto.Password);
                if (user == null)
                    return BadRequest("Username / Pasword tidak tepat");

                return Ok(user);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
    }
}
