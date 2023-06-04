using Contacts.Data;
using Contacts.Data.Models;
using Contacts.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public class DataService : IDataService
    {
        private readonly ContactsDbContext _dbContext;

        public DataService(ContactsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<Contact>> GetAllContactsAsync()
        {
            var contactsTasks = await this._dbContext.Contacts.ToListAsync();

            return contactsTasks;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            if ((await this._dbContext.Contacts.FirstOrDefaultAsync(c => (c.FirstName + c.LastName + c.Email) == (contact.FirstName + contact.LastName + contact.Email))) != null)
            {
                return null!;
            }

            var result = await this._dbContext.Contacts.AddAsync(contact);

            await this._dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Contact> GetContactByIdAsync(int? id)
        {
            Contact? contact = await this._dbContext.Contacts
                .FindAsync(id);

            return contact!;
        }

        public async Task<Contact> EditContactAsync(Contact contact)
        {
            Contact? existingContact = await this._dbContext.Contacts.FindAsync(contact.Id).ConfigureAwait(false);

            existingContact!.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            existingContact.Address = contact.Address;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Website = contact.Website;

            await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

            return existingContact;
        }

        public async Task<bool> AddContactToTeamAsync(int id, ApplicationUser user)
        {
            Contact? selectedContact = await this._dbContext.Contacts
                .FindAsync(id)
                .ConfigureAwait(false);

            int entitiesSavedInDbNum = 0;

            if (selectedContact != null)
            {
                ApplicationUser? selectedUser = await this._dbContext.Users
                    .Include(u => u.ApplicationUsersContacts)
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (selectedUser is not null && !selectedUser.ApplicationUsersContacts.Any(c => c.ContactId == id))
                {
                    var userContact = new ApplicationUserContact()
                    {
                        //ApplicationUser = selectedUser!,
                        //Contact = selectedContact,
                        //ApplicationUserId = selectedUser!.Id,
                        ContactId = selectedContact.Id
                    };

                    selectedUser.ApplicationUsersContacts.Add(userContact);
                    entitiesSavedInDbNum = await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
                }

            }

            return entitiesSavedInDbNum != 0;
        }

        public async Task<ApplicationUser> FindUserByUsername(string username)
        {
            ApplicationUser? user = await this._dbContext.Users
                .Include(u => u.ApplicationUsersContacts)
                .ThenInclude(au => au.Contact)
                .FirstOrDefaultAsync(u => u.UserName == username);

            return user!;
        }

        public async Task RemoveFromTeam(int contactId, ApplicationUser user)
        {
            ApplicationUser? foundUser = await this._dbContext.Users
                .Include(u => u.ApplicationUsersContacts)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            ApplicationUserContact? userContact = foundUser.ApplicationUsersContacts.FirstOrDefault(c => c.ContactId == contactId);

            if (foundUser != null && userContact != null)
            {
                foundUser.ApplicationUsersContacts.Remove(userContact);
                await this._dbContext.SaveChangesAsync();
            }
        }
    }
}
