using Library.Data.Models;
using Library.ViewModels.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NuGet.DependencyResolver;

namespace Library.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this._signInManager = signInManager;
			this._userManager = userManager;
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User?.Identity?.IsAuthenticated ?? false)
			{
				RedirectToAction("All", "Books");
			}

			var loginViewModel = new LoginViewModel();

			return View(loginViewModel);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel loginModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginModel);
			}

			ApplicationUser user = await this._userManager.FindByNameAsync(loginModel.UserName);

			if (user is not null)
			{
				var result = await this._signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

				if (result.Succeeded)
				{
					return RedirectToAction("All", "Books");
				}
			}

			ModelState.AddModelError("", "Invalid username or password!");
			return View(loginModel);
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			var registerModel = new RegisterViewModel();

			return View(registerModel);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(registerViewModel);
			}

			var user = new ApplicationUser()
			{
				UserName = registerViewModel.UserName,
				Email = registerViewModel.Email,
			};

			IdentityResult result = await this._userManager.CreateAsync(user, registerViewModel.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("Login");
			}

			foreach (IdentityError error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}

			return View(registerViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await this._signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}
