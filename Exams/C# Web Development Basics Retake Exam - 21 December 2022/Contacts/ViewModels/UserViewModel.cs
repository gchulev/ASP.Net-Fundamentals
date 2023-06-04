using System.ComponentModel.DataAnnotations;

namespace Contacts.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username should be between 3 and 20 characters long!")]
        public string UserName { get; set; } = null!;

		[Required]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Password should be between 5 and 20 characters long!")]
		public string Password { get; set; } = null!;

		[Required]
		[StringLength(60, MinimumLength = 10, ErrorMessage = "Email should be between 10 and 60 characters long!")]
		public string Email { get; set; } = null!;
    }
}
