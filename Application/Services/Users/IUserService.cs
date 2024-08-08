using Application.Models.User;
using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.Users
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task CreateUser(CreateUserDto createUserDto);
        Task DeleteUser(int id);
        Task<UpdateUserDto> UpdateUser(UpdateUserRequest updateUserRequest);
        Task<UserDto> GetUserById(int id);

    }
}
