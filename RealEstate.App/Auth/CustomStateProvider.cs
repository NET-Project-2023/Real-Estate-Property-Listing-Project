using RealEstate.App.Contracts;
using RealEstate.App.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RealEstate.App.Auth
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationService authService;
        private readonly ITokenService tokenService;

        public CustomStateProvider(IAuthenticationService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    var username = await tokenService.GetUsernameFromTokenAsync();
                    var role = await tokenService.GetRoleFromTokenAsync(); // aici in tokenService am printat si rolul
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, role)
                    };
                    identity = new ClaimsIdentity(claims, "jwt");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAuthenticationStateAsync: {ex.Message}");
            }

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task Logout()
        {
            await authService.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Login(LoginViewModel loginParameters)
        {
            await authService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterViewModel registerParameters)
        {
            await authService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task DeleteUserByUsername(string username)
        {
            await authService.DeleteUserByUsername(username);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
