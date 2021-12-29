using Microsoft.AspNetCore.Identity;
using RESTapp.Dtos;
using RESTapp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTapp.Data
{
    public interface IUser
    {
        IEnumerable<UserDto> GetAllUser();
        Task Registration(CreateUserDto user);
        Task AddRole(string rolename);
        IEnumerable<CreateRoleDto> GetAllRoles();
        Task AddUserToRole(string username, string role);
        Task<List<string>> GetRolesFromUser(string username);
        Task<User> Authenticate (string username, string password);
    }
}
