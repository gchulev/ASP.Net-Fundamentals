using System.ComponentModel.DataAnnotations;

using static Library.Data.ValidationConstants.AppUser;

namespace Library.ViewModels.User
{
	public class LoginViewModel
	{
        [Required]
        [StringLength(AppUserUserNameMaxValue, MinimumLength = AppUserUserNameMinValue)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(AppUserPasswordMaxLength, MinimumLength = AppUserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
