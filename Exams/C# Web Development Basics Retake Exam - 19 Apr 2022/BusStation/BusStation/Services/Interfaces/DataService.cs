
using BusStation.Data;
using BusStation.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace BusStation.Services.Interfaces
{
	public class DataService : IDataService
	{
		private readonly BusStationDbContext _dbContext;
        public DataService(BusStationDbContext dbContext)
        {
			this._dbContext = dbContext;
        }
        public async Task<User?> FindUserByNameAsync(string userName)
		{
			User? user = await this._dbContext.Users
				.FirstOrDefaultAsync(u => u.Username == userName);

			return user;
		}
	}
}
