using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Controllers
{
	[Authorize]
	public class TaskController : Controller
	{
		private readonly IBaordService _boardService;
		private readonly ITaskService _taskService;

		public TaskController(IBaordService boardService, ITaskService taskService)
		{
			this._boardService = boardService;
			this._taskService = taskService;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			TaskFormModel taskModel = new TaskFormModel()
			{
				Boards = await this._boardService.GetBoardsAsync()
			};

			return View(taskModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(TaskFormModel taskFormViewModel)
		{
			var boardsModel = await this._boardService.GetBoardsAsync();

			if (!boardsModel.Any(b => b.Id == taskFormViewModel.BoardId))
			{
				ModelState.AddModelError(nameof(taskFormViewModel.BoardId), "Board does not exist.");
			}

			string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var task = new Data.Models.Task()
			{
				Title = taskFormViewModel.Title,
				Description = taskFormViewModel.Description,
				CreatedOn = DateTime.UtcNow,
				BoardId = taskFormViewModel.BoardId,
				OwnerId = currentUserId
			};

			try
			{
				await this._taskService.AddTaskToDbAsync(task);

				return RedirectToAction("All", "Board");
			}
			catch (Exception)
			{
				var error = new ErrorViewModel()
				{
					ErrorMessage = $"Error while adding taskId: {task.Id} to the database"
				};

				return View("error", error);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			TaskDetailsViewModel? taskDetailsViewModel = await this._taskService.GetTaskViewModelByIdAsync(id);

			if (taskDetailsViewModel is null)
			{
				var error = new ErrorViewModel()
				{
					RequestId = id.ToString(),
					ErrorMessage = $"Task with {id} does not exist in the database!"
				};
				return View("Error", error);
			}

			return View(taskDetailsViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			TaskDetailsViewModel? taskDetails = await this._taskService.GetTaskViewModelByIdAsync(id);

			if (taskDetails is null)
			{
				var error = new ErrorViewModel()
				{
					RequestId = id.ToString(),
					ErrorMessage = $"Task with id: {id} does not exist!"
				};

				return View("Error", error);
			}

			var taskFormModel = new TaskFormModel()
			{
				Title = taskDetails.Title,
				Description = taskDetails.Description,
				Boards = (await this._boardService.GetAllBoardsAsync())
					.Select(t => new TaskBoardModel()
					{
						Id = t.Id,
						Name = t.Name
					})
					.ToList()
			};

			return View(taskFormModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, TaskFormModel taskFormModel)
		{
			Data.Models.Task? task = await this._taskService.GetTaskByIdAsync(id);

			if (task is null)
			{
				var error = new ErrorViewModel()
				{
					RequestId = id.ToString(),
					ErrorMessage = $"Task with id: {id} does not exist!"
				};

				return View("Error", error);
			}

			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			var boards = await this._boardService.GetAllBoardsAsync();

			if (!boards.Any(b => b.Id == task.BoardId))
			{
				var error = new ErrorViewModel()
				{
					RequestId = nameof(task.BoardId),
					ErrorMessage = $"Board with Id {task.BoardId} does not exist!"
				};

				return View("Error", error);
			}

			task.Title = taskFormModel.Title;
			task.BoardId = taskFormModel.BoardId;
			task.Description = taskFormModel.Description;

			try
			{
				await this._taskService.SaveDbContextChangesAsync();

				return RedirectToAction("All", "Board");
			}
			catch (Exception ex)
			{
				var error = new ErrorViewModel()
				{
					ErrorMessage = ex.Message
				};

				return View("Error", error);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var task = await this._taskService.GetTaskByIdAsync(id);

			if (task == null)
			{
				var error = new ErrorViewModel()
				{
					ErrorMessage = $"Task with id: {id} not found in the database!"
				};

				return View("Error", error);
			}

			var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (currentUserId != task.OwnerId)
			{
				return Unauthorized();
			}

			var taskViewModel = new TaskViewModel()
			{
				Title = task.Title,
				Description = task.Description,
				Owner = task.Owner.UserName
			};

			return View(taskViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(TaskViewModel taskModel)
		{
			Data.Models.Task? task = await this._taskService.GetTaskByIdAsync(taskModel.Id);

			if (task == null)
			{
				var error = new ErrorViewModel()
				{
					ErrorMessage = $"Task with id: {taskModel.Id} does not exist in the database!"
				};

				return View("Error", error);
			}
			
			try
			{
				await this._taskService.RemoveFromDbAsync(task);

				return RedirectToAction("All", "Board");
			}
			catch (Exception)
			{
				var error = new ErrorViewModel()
				{
					ErrorMessage = $"Error while removing task with id: {taskModel.Id}"
				};

				return View("Error", error);
			}
		}
	}
}
