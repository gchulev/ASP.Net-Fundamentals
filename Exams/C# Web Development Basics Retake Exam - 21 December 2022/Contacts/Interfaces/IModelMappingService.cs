using Contacts.Data.Models;
using Contacts.ViewModels;

namespace Contacts.Interfaces
{
	public interface IModelMappingService
	{
		public Task<ContactViewModel> MapContactToContactViewModelAsync(Contact contact);

		public Task<Contact> MapContactViewModelToContactAsync(ContactViewModel viewModel);

		public Task<ContactsViewModel> MapContactsToContactsViewModelAsync(IEnumerable<Contact> contacts);

		//public Task<ContactViewModel> MapContactToContactViewModelAsync(Contact contact);


    }
}
