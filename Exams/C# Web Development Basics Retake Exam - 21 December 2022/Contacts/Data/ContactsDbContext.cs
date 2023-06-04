using Contacts.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactsDbContext : IdentityDbContext<ApplicationUser>
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
            /* builder
                .Entity<Contact>()
                .HasData(new Contact()
                {
                    Id = 1,
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    PhoneNumber = "+359881223344",
                    Address = "Gotham City",
                    Email = "imbatman@batman.com",
                    Website = "www.batman.com"
                });
            */
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ApplicationUserContact> ApplicationUserContacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserContact>(entity =>
            {
                entity.HasKey(k => new { k.ContactId, k.ApplicationUserId });

                entity.HasOne(au => au.ApplicationUser)
                .WithMany(auc => auc.ApplicationUsersContacts)
                .HasForeignKey(au => au.ApplicationUserId);

                entity.HasOne(au => au.Contact)
                .WithMany(auc => auc.ApplicationUsersContacts)
                .HasForeignKey(au => au.ContactId);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(k => new { k.LoginProvider, k.ProviderKey });
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(k => new { k.UserId, k.RoleId });
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(k => new { k.UserId, k.LoginProvider, k.Name });
            });
        }
    }
}
