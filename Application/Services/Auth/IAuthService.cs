﻿using Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginRequest userLogin);
    }
}
