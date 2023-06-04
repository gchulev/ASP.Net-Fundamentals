using Contacts.Data.Models;
using Contacts.Interfaces;
using Contacts.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			if (User.Identity is null || !User.Identity.IsAuthenticated)
			{
				return View();
			}

			return RedirectToAction("All", "Contacts");
		}
	}
}