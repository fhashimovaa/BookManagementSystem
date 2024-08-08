using Application.Exceptions;
using Application.Helpers;
using Application.Models.Auth;
using Application.Models.Jwt;
using Core.Entity.Concrete;
using DataAccess.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        protected readonly IUserRepository _userRepository;


        public AuthService(IUserRepository userRepository)
        { 
            _userRepository = userRepository;
        }

        public async Task<string> Login(UserLoginRequest userLogin)
        {
            User user =await _userRepository.GetByUsername(userLogin.UserName);

            if (user == null) throw new NotFoundException("bu istifadeci tapilmadi");

            bool isVerifed= HashingHelper.VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt);

            if (!isVerifed) throw new BadRequestException("sifre duzgun deyil,yeniden cehd edin!");

            PayloadRequirements payload = new PayloadRequirements() {Username= user.UserName,Email= user.Email,Id=user.Id };

            string token = JwtHelper.GenerateJwtToken(payload ,user.UserRoles.Select(e =>e.Role).ToList());

            return token;

        }
    }
}
