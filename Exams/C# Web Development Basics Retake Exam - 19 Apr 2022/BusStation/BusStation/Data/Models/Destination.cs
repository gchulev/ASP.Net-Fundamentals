using System.ComponentModel.DataAnnotations;

using static BusStation.Data.ValidationConstants.DestinationValidations;

namespace BusStation.Data.Models
{
	public class Destination
	{
		[Required]
		[Key]
        public int Id { get; set; }

		[Required]
		[MaxLength(DestinationNameMaxLenght)]
		public string DestinationName { get; set; } = null!;

		[Required]
		[MaxLength(DestinationOriginMaxLength)]
		public string Origin { get; set; } = null!;

		[Required]
		[MaxLength(DestinationDateMaxLength)]
		public string Date { get; set; } = null!;

		[Required]
		[MaxLength(DestinationTimeMaxLength)]
		public string Time { get; set; } = null!;

		[Required]
        public string ImageUrl { get; set; }

		public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
