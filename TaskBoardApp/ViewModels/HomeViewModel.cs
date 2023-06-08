namespace TaskBoardApp.ViewModels
{
	public class HomeViewModel
	{
        public int AllTasksCount { get; set; }
        public ICollection<HomeBaordViewModel> BoardsWithTasksCount { get; set; } = new List<HomeBaordViewModel>();
        public int UserTasksCount { get; set; }
    }
}
