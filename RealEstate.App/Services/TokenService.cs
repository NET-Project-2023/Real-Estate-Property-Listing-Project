using Blazored.LocalStorage;
using RealEstate.App.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstate.App.Services
{
    public class TokenService : ITokenService
    {
        private const string TOKEN = "token";
        private readonly ILocalStorageService localStorageService;
        //private readonly IUserManager userManager;
        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
            //this.userManager = userManager;
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

        public async Task<string> GetUsernameFromTokenAsync()
        {
            var token = await GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null && jsonToken.Payload.TryGetValue("unique_name", out var uniqueName))
            {
                return uniqueName.ToString();
            }

            return null;
        }
    }


}
