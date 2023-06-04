
using System.Security.Claims;

using Contacts.Data.Models;
using Contacts.ViewModels;

using Microsoft.AspNetCore.Identity;

namespace Contacts.Interfaces
{
	public interface IDataService
	{
		public Task<List<Contact>> GetAllContactsAsync();
		public Task<Contact> AddContactAsync(Contact contact);
		public Task<Contact> GetContactByIdAsync(int? id);
		public Task<Contact> EditContactAsync(Contact contact);
		public Task<bool> AddContactToTeamAsync(int id, ApplicationUser user);
		public Task<ApplicationUser> FindUserByUsername(string username);
		public Task RemoveFromTeam(int contactId, ApplicationUser user);

    }
}
