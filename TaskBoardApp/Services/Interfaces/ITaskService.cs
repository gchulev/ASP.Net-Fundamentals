using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services.Interfaces
{
	public interface ITaskService
	{
		Task AddTaskToDbAsync(Data.Models.Task task);
		Task<TaskDetailsViewModel?> GetTaskViewModelByIdAsync(int id);
		Task<Data.Models.Task?> GetTaskByIdAsync(int id);
		Task SaveDbContextChangesAsync();
		Task RemoveFromDbAsync(Data.Models.Task task);
	}
}
