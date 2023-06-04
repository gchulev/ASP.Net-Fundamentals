using System.ComponentModel.DataAnnotations;

using Library.ViewModels.Category;

using static Library.Data.ValidationConstants.Book;

namespace Library.ViewModels.Book
{
    public class BookViewModel
	{
        public BookViewModel()
        {
            this.Categories = new HashSet<CategoryViewModel>();
        }
        public int Id { get; set; }

		[Required]
		[StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(BookAuthorMaxLength, MinimumLength = BookAuthoMinLength)]
		public string Author { get; set; } = null!;

		[Required]
		[StringLength(BookDescriptionMaxLength, MinimumLength = BookDescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(BookRatingMinValue, BookRatingMaxValue)]
		public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

		[Required]
		public CategoryViewModel Category { get; set; } = null!;

        public ICollection<CategoryViewModel> Categories { get; set; }
	}
}
