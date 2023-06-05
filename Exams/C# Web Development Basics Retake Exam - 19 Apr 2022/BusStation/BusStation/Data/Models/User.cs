using System.ComponentModel.DataAnnotations;

using static BusStation.Data.ValidationConstants.UserValidations;

namespace BusStation.Data.Models
{
	public class User
	{
		[Required]
		[Key]
		public string Id { get; set; } = null!;

		[Required]
		[MaxLength(UserNameMaxLength)]
		public string Username { get; set; } = null!;

		[Required]
		[MaxLength(UserEmailMaxLength)]
		public string Email { get; set; } = null!;

		[Required]
		[MaxLength(UserPasswordMaxLength)]
		public string Password { get; set; } = null!;

		public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
	}
}
