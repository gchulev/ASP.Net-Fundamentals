using System.ComponentModel.DataAnnotations;

namespace Contacts.ViewModels
{
	public class ContactViewModel
	{
		public int? ContactId { get; set; }

		[Required(ErrorMessage = "First name is required!")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters long!")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required!")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Last name should be between 5 and 50 characters long!")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Email is required!")]
		[StringLength(60, MinimumLength = 10, ErrorMessage = "Email should be between 10 and 60 characters long!")]
		public string Email { get; set; } = null!;
		public string Address { get; set; } = null!;

		[Required(ErrorMessage = "Field 'Website' is required!")]
		[RegularExpression(@"www.[\w\d]+(\-)?[\w\d]+.bg")]
		public string Website { get; set; } = null!;

		[Required(ErrorMessage = "Phone number is required!")]
		[StringLength(13, MinimumLength = 10, ErrorMessage = "Phone number should be between 10 and 13 characters long!")]
		[RegularExpression(@"(0|\+359)((\-?|\s?)?\d+)+")]
		public string PhoneNumber { get; set; } = null!;
	}
}
