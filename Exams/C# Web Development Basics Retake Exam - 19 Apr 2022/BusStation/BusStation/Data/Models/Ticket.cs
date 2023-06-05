using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusStation.Data.Models
{
	public class Ticket
	{
		[Required]
		[Key]
		public int Id { get; set; }

		[Required]
		[Column(TypeName = "DECIMAL(6,2)")]
		public decimal Price { get; set; }

		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public int DestinationId { get; set; }
	}
}
