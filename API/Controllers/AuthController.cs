using Application.Models.Auth;
using Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class AuthController : BaseController
    {
        protected readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            var result = await _authService.Login(userLogin);
            return Ok(result);
        }
        
    }
}
