using Library.Data.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			if (!User?.Identity?.IsAuthenticated ?? true)
			{
				return View();
			}

			return RedirectToAction("All", "Books");
		}
	}
}