﻿namespace RealEstate.App.Contracts
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
        Task SetTokenAsync(string token);
        Task<string?> GetUsernameFromTokenAsync();
        //Task<string?> GetUsernameFromTokenAsync();
        Task<string?> GetRoleFromTokenAsync();

        //Task<string?> GetUserIdFromTokenAsync();
    }
}
