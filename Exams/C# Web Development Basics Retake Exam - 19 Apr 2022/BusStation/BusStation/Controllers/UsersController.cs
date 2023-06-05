using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;

using BusStation.Data.Models;
using BusStation.Services.Interfaces;
using BusStation.ViewModels.User;

namespace BusStation.Controllers
{
	public class UsersController : Controller
	{
		private readonly IDataService _dataService;
        public UsersController(Request request, IDataService dataService)
			: base(request)
        {
			this._dataService = dataService;
        }

		[HttpGet]
		public Response Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<Response> Login(LoginFormViewModel userLoginInfo)
		{
			User? user = await this._dataService.FindUserByNameAsync(userLoginInfo.Username);

			if(userLoginInfo != null && user != null)
			{
				this.SignIn(user.Id);

				return Redirect("/Destinations/All");
			}
			
			return this.Unauthorized();
		}

		public Response Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<Response> Register(RegisterFormViewModel registerUserViewModel)
		{
			var formValues = this.Request.Form.Values.ToList();
			
			return this.Redirect("Login");
		}
    }
}
