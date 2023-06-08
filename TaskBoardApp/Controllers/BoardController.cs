using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.Controllers
{
	[Authorize]
	public class BoardController : Controller
	{
        private readonly IBaordService _dataService;
        public BoardController(IBaordService dataService)
        {
            this._dataService = dataService;
        }

        public async Task<IActionResult> All()
        {
            List<BoardViewModel> boards = await this._dataService.GetAllBoardsAsync();

            return View(boards);
        }
    }
}
