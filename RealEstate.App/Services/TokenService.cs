﻿using Blazored.LocalStorage;
using RealEstate.App.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstate.App.Services
{
    public class TokenService : ITokenService
    {
        private const string TOKEN = "token";
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetTokenAsync(string token)
        {
            await localStorageService.SetItemAsync(TOKEN, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await localStorageService.GetItemAsync<string>(TOKEN);
        }

        public async Task RemoveTokenAsync()
        {
            await localStorageService.RemoveItemAsync(TOKEN);
        }

        public async Task<Guid?> GetUserIdFromTokenAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null && jsonToken.Payload.TryGetValue("sub", out var sub))
            {
                // Return the 'sub' claim as a Guid
                return Guid.Parse(sub.ToString());
            }


            return null;
        }
    }
}
