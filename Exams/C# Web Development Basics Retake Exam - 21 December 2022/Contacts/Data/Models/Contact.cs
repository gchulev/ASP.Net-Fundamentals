using System.ComponentModel.DataAnnotations;

namespace Contacts.Data.Models
{
    public class Contact
    {
        public Contact()
        {
            this.ApplicationUsersContacts = new HashSet<ApplicationUserContact>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(13)]
        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Required]
        public string Website { get; set; } = null!;

        public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; }
    }
}
