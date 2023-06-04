using Contacts.Data.Models;
using Contacts.Interfaces;
using Contacts.ViewModels;

namespace Contacts.Services
{
    public class ModelMappingService : IModelMappingService
    {
        public async Task<ContactsViewModel> MapContactsToContactsViewModelAsync(IEnumerable<Contact> contacts)
        {
            var contactsViewModel = new ContactsViewModel()
            {
                Contacts = (await Task.WhenAll(contacts.Select(c => this.MapContactToContactViewModelAsync(c)))).ToList()
            };

            return contactsViewModel;
        }

        public async Task<ContactViewModel> MapContactToContactViewModelAsync(Contact contact)
        {
            //This is very unnecessary to make it separate task to run on a different thread since it is not CPU heavy task
            //but doing this for learning purposes at the moment to understand how async programming works
            var contactViewModel = await Task.Run(() =>
            {
                return new ContactViewModel()
                {
                    ContactId = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    Address = contact.Address,
                    PhoneNumber = contact.PhoneNumber,
                    Website = contact.Website
                };
            });

            return contactViewModel;
        }

        public Task<Contact> MapContactViewModelToContactAsync(ContactViewModel viewModel)
        {
            return Task.Run(() =>
            {
                var contact = new Contact()
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    Address = viewModel.Address,
                    PhoneNumber = viewModel.PhoneNumber,
                    Website = viewModel.Website
                };

                if (viewModel.ContactId != null)
                {
                    contact.Id = (int)viewModel.ContactId;
                }

                return contact;
            });
        }

        //public async Task<ContactViewModel> MapContactToContactViewModelAsync(Contact contact)
        //{
        //    var contactViewModel = await Task.Run(() =>
        //    {
        //         return new ContactViewModel()
        //        {
        //            ContactId = contact.Id,
        //            FirstName = contact.FirstName,
        //            LastName = contact.LastName,
        //            Email = contact.Email,
        //            Address = contact.Address,
        //            PhoneNumber = contact.PhoneNumber,
        //            Website = contact.Website
        //        };
        //    });

        //    return contactViewModel;
        //}
    }
}
