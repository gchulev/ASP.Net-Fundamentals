using System.Security.Claims;

using Contacts.Data.Models;
using Contacts.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Contacts.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<bool> UserFoundAsync(string? userName)
        {
            ApplicationUser foundUser = await this._userManager.FindByNameAsync(userName);
            return foundUser != null;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new Exception("Password and confirm password do not match!");
            }

            IdentityResult result = await this._userManager.CreateAsync(user, password).ConfigureAwait(false);

            return result.Succeeded;
        }

		public async Task<bool> CheckForValidPassword(string userName, string password)
		{
            ApplicationUser user = await this._userManager.FindByNameAsync(userName);

            return user is not null && await this._userManager.CheckPasswordAsync(user, password);
		}

        public async Task<ApplicationUser> FindUserByClaimsPrincipalAsync(ClaimsPrincipal user)
        {
            ApplicationUser? currentUser = await this._userManager.FindByNameAsync(user.Identity!.Name);

            return currentUser;
        }

        public async Task<ApplicationUser?> FindUserByNameAsync(string userName)
        {
            ApplicationUser user = await this._userManager.FindByNameAsync(userName);
            return user ?? null;
        }
    }
}
