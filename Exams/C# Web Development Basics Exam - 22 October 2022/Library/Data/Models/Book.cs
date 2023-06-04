using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Library.Data.ValidationConstants.Book;

namespace Library.Data.Models
{
	public class Book
	{
        public Book()
        {
			this.ApplicationUsersBooks = new HashSet<ApplicationUserBook>();
        }

        [Key]
        public int Id { get; set; }

		[Required]
		[MaxLength(BookTitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(BookAuthorMaxLength)]
		public string Author { get; set; } = null!;

		[Required]
		[MaxLength(BookDescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		public decimal Rating { get; set; }

		[ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

		public Category Category { get; set; } = null!;

        public ICollection<ApplicationUserBook> ApplicationUsersBooks{ get; set; }
    }
}
