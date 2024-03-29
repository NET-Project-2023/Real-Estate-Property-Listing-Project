﻿using RealEstate.App.ViewModels;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstate.App.Contracts
{
    public interface IAuthenticationService
    {
        Task Login(LoginViewModel loginRequest);
        Task Register(RegisterViewModel registerRequest);
        Task Logout(); // Logout deoarece stergem local storage-ul din browser; la urmatoarea logare obtinem alt token
        Task DeleteUserByUsername(string username);
        Task<string> DecodeUsernameFromTokenAsync(string token);
    }
}
