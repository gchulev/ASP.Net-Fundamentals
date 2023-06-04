using System.ComponentModel.DataAnnotations;

using Library.ViewModels.Book;

using static Library.Data.ValidationConstants.Category;

namespace Library.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            this.Books = new HashSet<BookViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<BookViewModel> Books { get; set; }
    }
}
