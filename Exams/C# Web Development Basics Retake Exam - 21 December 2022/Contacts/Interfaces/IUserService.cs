using System.Security.Claims;

using Contacts.Data.Models;

using Microsoft.AspNetCore.Identity;

namespace Contacts.Interfaces
{
    public interface IUserService
    {
        public Task<bool> UserFoundAsync(string? userNamed);
        public Task<bool> RegisterUserAsync(ApplicationUser user, string password, string confirmPassword);
        public Task<bool> CheckForValidPassword(string userName, string password);
        public Task<ApplicationUser> FindUserByClaimsPrincipalAsync(ClaimsPrincipal user);
        public Task<ApplicationUser?> FindUserByNameAsync(string userName);
    }
}
