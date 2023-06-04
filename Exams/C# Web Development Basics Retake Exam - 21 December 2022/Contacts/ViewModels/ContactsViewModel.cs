using Contacts.Data.Models;

namespace Contacts.ViewModels
{
	public class ContactsViewModel
	{
		public List<ContactViewModel> Contacts { get; set; } = new List<ContactViewModel>();
    }
}
