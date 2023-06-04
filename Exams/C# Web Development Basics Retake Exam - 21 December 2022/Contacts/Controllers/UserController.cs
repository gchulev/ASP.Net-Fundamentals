using Contacts.Data.Models;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public UserController(IUserService userService, SignInManager<ApplicationUser> signInManager)
		{
			this._userService = userService;
			this._signInManager = signInManager;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			var user = new ApplicationUser()
			{
				UserName = registerViewModel.UserName,
				Email = registerViewModel.Email
			};

			try
			{
				if (!await this._userService.UserFoundAsync(user.UserName).ConfigureAwait(false))
				{
					bool registeredSuccessfully = await this._userService.RegisterUserAsync(user, registerViewModel.Password, registerViewModel.ConfirmPassword);

					if (!registeredSuccessfully)
					{
						ModelState.AddModelError("", "Unable to register user!");
						return View(registerViewModel);
					}
				}

			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				return View(registerViewModel);
			}

			return RedirectToAction("Login", "User");

		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserViewModel userViewModel)
		{
			if (await this._userService.CheckForValidPassword(userViewModel.UserName, userViewModel.Password))
			{
				var user = new ApplicationUser()
				{
					UserName = userViewModel.UserName,
					Email = userViewModel.Email
				};

				await this._signInManager.SignInAsync(user, false);

				return RedirectToAction("All", "Contacts");
			}

			return View();
		}

		[HttpGet]
		public IActionResult Logout()
		{
			this._signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
