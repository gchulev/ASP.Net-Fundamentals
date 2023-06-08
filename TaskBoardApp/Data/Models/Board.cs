using System.ComponentModel.DataAnnotations;

using static TaskBoardApp.Data.ValidationConstants.Board;

namespace TaskBoardApp.Data.Models
{
	public class Board
	{
        public Board()
        {
            this.Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Task> Tasks { get; set; }
    }
}
