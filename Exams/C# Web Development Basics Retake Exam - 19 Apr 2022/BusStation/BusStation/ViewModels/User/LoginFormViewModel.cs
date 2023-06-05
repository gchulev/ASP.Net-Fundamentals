using System.ComponentModel.DataAnnotations;

using static BusStation.Data.ValidationConstants.UserValidations;

namespace BusStation.ViewModels.User
{
	public class LoginFormViewModel
	{
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(UserPasswordMaxLength, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
