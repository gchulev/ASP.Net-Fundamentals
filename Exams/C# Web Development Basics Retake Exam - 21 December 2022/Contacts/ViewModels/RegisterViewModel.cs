using System.ComponentModel.DataAnnotations;

namespace Contacts.ViewModels
{
    public class RegisterViewModel : UserViewModel
    {
        [Required]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "Confirm password should be between 3 and 20 characters!")]
		public string ConfirmPassword { get; set; } = null!;
    }
}
