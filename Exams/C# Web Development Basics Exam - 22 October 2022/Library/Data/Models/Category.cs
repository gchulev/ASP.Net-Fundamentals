using System.ComponentModel.DataAnnotations;

using static Library.Data.ValidationConstants.Category;

namespace Library.Data.Models
{
	public class Category
	{
        public Category()
        {
            this.Books = new HashSet<Book>(); 
        }

        [Key]
        public int Id { get; set; }

		[Required]
		[StringLength(CategoryNameMaxLength)]
		public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}
