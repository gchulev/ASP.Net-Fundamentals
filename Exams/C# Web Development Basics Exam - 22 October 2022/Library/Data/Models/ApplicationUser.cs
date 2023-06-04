using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

using static Library.Data.ValidationConstants.AppUser;

namespace Library.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
        public ApplicationUser()
        {
			this.ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }

        [Required]
		[MaxLength(AppUserUserNameMaxValue)]
		public override string UserName
		{
			get => base.UserName;
			set => base.UserName = value;
		}

		[Required]
		[MaxLength(AppUserEmailMaxValue)]
		public override string Email
		{
			get => base.Email;
			set => base.Email = value;
		}

		public ICollection<ApplicationUserBook> ApplicationUsersBooks { get; set; }
	}
}
