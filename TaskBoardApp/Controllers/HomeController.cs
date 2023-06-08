using System.Diagnostics;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data;
using TaskBoardApp.ViewModels;
using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly TaskBoardAppDbContext _dbContext;
		public HomeController(TaskBoardAppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public async Task<IActionResult> Index()
		{
			var taskBoards = this._dbContext.Boards
				.Select(b => b.Name)
				.Distinct();

			var tasksCount = new List<HomeBaordViewModel>();

			foreach (var boardName in taskBoards)
			{
				int tasksInBoard = this._dbContext.Tasks
					.Where(t => t.Board.Name == boardName)
					.Count();

				tasksCount.Add(new HomeBaordViewModel()
				{
					BoardName = boardName,
					TasksCount = tasksInBoard
				});
			}

			int userTasksCount = -1;

			if (User.Identity.IsAuthenticated)
			{
				var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				userTasksCount = await this._dbContext.Tasks.Where(t => t.Owner.Id == currentUserId).CountAsync();
			}

			var homeModel = new HomeViewModel()
			{
				AllTasksCount = this._dbContext.Tasks.Count(),
				BoardsWithTasksCount = tasksCount,
				UserTasksCount = userTasksCount
			};

			return View(homeModel);
		}
	}
}