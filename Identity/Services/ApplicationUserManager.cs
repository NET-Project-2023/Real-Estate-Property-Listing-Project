using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Real_estate.Application.Features.Listings;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Common;

namespace Identity.Services
{
    public class ApplicationUserManager : IUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Result<UserDto>> FindByIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result<UserDto>.Failure($"User with id {userId} not found");
            }
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }
        public async Task<Result<UserDto>> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<UserDto>.Failure($"User with email {email} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }
        public async Task<Result<UserDto>> FindByUsernameAsync(string username)
        {

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return Result<UserDto>.Failure($"User with username {username} not found");
            var userDtos = MapToUserDto(user);
            var roles = await userManager.GetRolesAsync(user);
            userDtos.Roles = roles.ToList();
            return Result<UserDto>.Success(userDtos);
        }


        public async Task<Result<List<UserDto>>> GetAllAsync()
        {
            var users = userManager.Users.ToList();
            var userDtos = users.Select(u => MapToUserDto(u)).ToList();

            foreach (var userDto in userDtos)
            {
                var appUser = await userManager.FindByIdAsync(userDto.UserId.ToString());
                if (appUser != null)
                {
                    var roles = await userManager.GetRolesAsync(appUser);
                    userDto.Roles = roles.ToList();
                }
            }

            return Result<List<UserDto>>.Success(userDtos);
        }

        public async Task<Result<UserDto>> DeleteAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Result<UserDto>.Failure($"User with id {userId} not found");

            var deleteResult = await userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
                return Result<UserDto>.Failure($"User with id {userId} not deleted");

            return Result<UserDto>.Success(MapToUserDto(user));
        }


        public async Task<Result<UserDto>> UpdateAsync(UserDto userDto)
        {
            var userToUpdate = await userManager.FindByNameAsync(userDto.UserName.ToString());
            if (userToUpdate == null)
                return Result<UserDto>.Failure($"User with id {userDto.UserName} not found");

            UpdateUserProperties(userToUpdate, userDto);

            var updateResult = await userManager.UpdateAsync(userToUpdate);
            return updateResult.Succeeded ? Result<UserDto>.Success(MapToUserDto(userToUpdate)) : Result<UserDto>.Failure($"User with name {userDto.UserName} not updated");
        }

        //public async Task<bool> UserExistsAsync(string userId) 
        //{ 
        //    return await ExistsAsync(u => u.UserId == userId); 
        //}


        private void UpdateUserProperties(ApplicationUser user, UserDto userDto)
        {
            user.Name = userDto.Name;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
        }
        private UserDto MapToUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                UserId = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
