using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Contacts.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }

        [Key]
        public override string Id { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public override string UserName { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public override string Email { get; set; } = null!;

        public string? Password { get; set; }

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}
