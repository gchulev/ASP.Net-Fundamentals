using System.ComponentModel.DataAnnotations;

using static BusStation.Data.ValidationConstants.UserValidations;

namespace BusStation.ViewModels.User
{
	public class RegisterFormViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
		public string Username { get; set; } = null!;

		[Required]
		[StringLength(UserEmailMaxLength, MinimumLength = UserEmailMinLength)]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[StringLength(UserPasswordMaxLength, MinimumLength = UserPasswordMinLength)]
		public string Password { get; set; } = null!;


		[Required]
		[StringLength(UserPasswordMaxLength, MinimumLength = UserPasswordMinLength)]
		[Compare(nameof(Password))]
		public string Repeatpassword { get; set; } = null!;
	}
}
