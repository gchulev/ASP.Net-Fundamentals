using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
	public class BoardService : IBaordService
	{
		private readonly TaskBoardAppDbContext _dbContext;
		public BoardService(TaskBoardAppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<List<BoardViewModel>> GetAllBoardsAsync()
		{
			List<BoardViewModel> boardsViewModel = await this._dbContext
				.Boards
				.Select(b => new BoardViewModel()
				{
					Id = b.Id,
					Name = b.Name,
					Tasks = b
						.Tasks
						.Select(t => new TaskViewModel()
						{
							Id = t.Id,
							Description = t.Description,
							Owner = t.Owner.UserName,
							Title = t.Title
						}).ToList()
				})
				.ToListAsync();

			return boardsViewModel;
		}

		public async Task<List<TaskBoardModel>> GetBoardsAsync()
		{
			var boards = await this._dbContext
				.Boards
				.Select(b => new TaskBoardModel()
				{
					Id = b.Id,
					Name = b.Name
				})
				.ToListAsync();

			return boards;
		}
	}
}
