using BusStation.Data.Models;

namespace BusStation.Services.Interfaces
{
	public interface IDataService
	{
		public Task<User?> FindUserByNameAsync(string userName);
	}
}
