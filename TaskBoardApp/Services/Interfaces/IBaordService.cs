using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.Services.Interfaces
{
	public interface IBaordService
	{
		public Task<List<BoardViewModel>> GetAllBoardsAsync();
		public Task<List<TaskBoardModel>> GetBoardsAsync();
	}
}
