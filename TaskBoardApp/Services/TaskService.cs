using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
	public class TaskService : ITaskService
	{
		private readonly TaskBoardAppDbContext _dbContext;

        public TaskService(TaskBoardAppDbContext dbContext)
        {
			this._dbContext = dbContext;
        }
        public async Task AddTaskToDbAsync(Data.Models.Task task)
		{
			await this._dbContext.Tasks.AddAsync(task);
			await this._dbContext.SaveChangesAsync();
		}

		public async Task<Data.Models.Task?> GetTaskByIdAsync(int id)
		{
			return await this._dbContext.Tasks
				.Include(t => t.Owner)
				.Include(t => t.Board)
				.FirstOrDefaultAsync(t => t.Id == id);
		}

		public async Task<TaskDetailsViewModel?> GetTaskViewModelByIdAsync(int id)
		{
			Data.Models.Task? task = await this._dbContext.Tasks
				.Include(t => t.Owner)
				.Include(t => t.Board)
				.AsNoTracking()
				.FirstOrDefaultAsync(t => t.Id == id);

			if (task is not null)
			{
				var taskViewModel = new TaskDetailsViewModel()
				{
					Id = task.Id,
					Title = task.Title,
					Description = task.Description,
					Owner = task.Owner.UserName,
					Board = task.Board.Name,
					CreatedOn = task.CreatedOn.ToString("d")
				};

				return taskViewModel;
			}

			return null;
		}

		public async Task RemoveFromDbAsync(Data.Models.Task task)
		{
			this._dbContext.Tasks.Remove(task);
			await this._dbContext.SaveChangesAsync();
		}

		public async Task SaveDbContextChangesAsync()
		{
			await this._dbContext.SaveChangesAsync();
		}
	}
}
