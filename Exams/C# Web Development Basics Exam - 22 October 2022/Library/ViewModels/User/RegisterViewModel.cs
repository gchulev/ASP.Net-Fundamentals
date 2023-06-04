using System.ComponentModel.DataAnnotations;

using static Library.Data.ValidationConstants.AppUser;

namespace Library.ViewModels.User
{
	public class RegisterViewModel
	{

		[Required]
		[StringLength(AppUserUserNameMaxValue, MinimumLength = AppUserUserNameMinValue)]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[Required]
		[StringLength(AppUserPasswordMaxLength, MinimumLength = AppUserPasswordMinLength)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(AppUserEmailMaxValue, MinimumLength = AppUserEmailMinValue)]
        public string Email { get; set; }
    }
}
