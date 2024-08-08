using Application.Models.User;
using Application.Models.Users;
using Application.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        protected readonly IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet("/users")]

        public async Task<List<UserDto>> GetAll()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("/user/{id}")]

        public async Task<UserDto> Get(int id)
        {
            return await _userService.GetUserById(id);

        }

        [HttpPost("/user")]

        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            await _userService.CreateUser(createUserDto);

            return Ok();
        }

        [HttpDelete("/user/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpPut ("/user")]

        public async Task<UpdateUserDto> Update([FromBody]UpdateUserRequest updateUserRequest)
        {
            UpdateUserDto updateUser = await _userService.UpdateUser(updateUserRequest);
            return updateUser;
        }

        //public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUser)
        //{
        //     updateUser=await _userService.UpdateUser(updateUser);
        //    return Ok(updateUser);

        //}
    }
}
