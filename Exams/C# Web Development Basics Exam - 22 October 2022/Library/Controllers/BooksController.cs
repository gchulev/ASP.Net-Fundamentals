using Library.Data.Models;
using Library.Models;
using Library.Services.Interfaces;
using Library.ViewModels.Book;
using Library.ViewModels.Category;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	[Authorize]
	public class BooksController : Controller
	{
		private readonly IBookService _bookService;
		private readonly UserManager<ApplicationUser> _userManager;
		public BooksController(IBookService bookService, UserManager<ApplicationUser> userManager)
		{
			this._bookService = bookService;
			this._userManager = userManager;
		}

		public async Task<IActionResult> All()
		{
			List<BookViewModel> books = await this._bookService.AllBooksAsync();
			return View(books);
		}

		public async Task<IActionResult> Add()
		{
			List<CategoryViewModel> categories = await this._bookService.GetAllCategoriesAsync();
			var bookViewModel = new BookViewModel() { Categories = categories };

			return View(bookViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(BookViewModel book)
		{
			await this._bookService.AddBookAsync(book);
			return RedirectToAction("All");
		}

		[HttpPost]
		public async Task<IActionResult> AddToCollection(int bookId)
		{
			ApplicationUser user = await this._userManager.GetUserAsync(this.User);

			try
			{
				await this._bookService.AddBookToUsersCollectionAsync(bookId, user);

				return RedirectToAction("All", "Books");
			}
			catch (Exception e)
			{
				var error = new ErrorViewModel()
				{
					RequestId = "Add a book to user's collection",
					ErrorMessage = e.Message
				};

				return View("Error", error);
			}
		}

		public async Task<IActionResult> Mine()
		{
			ApplicationUser user = await this._userManager.GetUserAsync(this.User);

			List<BookViewModel> allBooksForUser = await this._bookService.GetAllBooksForUserAsync(user);

			return View(allBooksForUser);
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromCollection(int bookId)
		{
			ApplicationUser user = await this._userManager.GetUserAsync(User);

			if (user is not null)
			{
				await this._bookService.RemoveFromCollectionAsync(bookId, user);
			}

			return RedirectToAction("Mine");
		}
	}
}
