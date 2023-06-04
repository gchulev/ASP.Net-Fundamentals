using Library.Data;
using Library.Data.Models;
using Library.Services.Interfaces;
using Library.ViewModels.Book;
using Library.ViewModels.Category;

using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
	public class BookService : IBookService
	{
		private readonly LibraryDbContext _dbContext;
		public BookService(LibraryDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<List<BookViewModel>> AllBooksAsync()
		{
			List<Book> books = await this._dbContext.Books
				.ToListAsync();

			var booksToShow = books.Select(b => new BookViewModel()
			{
				Title = b.Title,
				Author = b.Author,
				CategoryId = b.CategoryId,
				Id = b.Id,
				ImageUrl = b.ImageUrl,
				Description = b.Description,
				Rating = b.Rating
			}).ToList();

			return booksToShow;
		}

		public async Task AddBookAsync(BookViewModel bookViewModel)
		{
			var book = new Book()
			{
				Title = bookViewModel.Title,
				Author = bookViewModel.Author,
				Description = bookViewModel.Description,
				ImageUrl = bookViewModel.ImageUrl,
				Rating = bookViewModel.Rating,
				CategoryId = bookViewModel.CategoryId,
			};

			await this._dbContext.Books.AddAsync(book);
			await this._dbContext.SaveChangesAsync();
		}

		public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
		{
			List<Category> categories = await this._dbContext.Categories.ToListAsync();
			var categoriesViewModel = categories.Select(c => new CategoryViewModel()
			{
				Id = c.Id,
				Name = c.Name
			}).ToList();

			return categoriesViewModel;
		}

		/// <summary>
		/// Finds the book by id.
		/// </summary>
		/// <param name="id">Book id.</param>
		/// <returns>Returns the "id" of the book or null if not found.</returns>
		public async Task<Book?> FindBookByIdAsync(int id)
		{
			return await this._dbContext.Books.FindAsync(id);
		}

		/// <summary>
		/// Finds the book by its "id" and adds it to the "user" collection of books "ApplicationUserBooks" and applies changes to the Db.
		/// Throws and exception if book is not found!
		/// </summary>
		/// <param name="id"></param>
		/// <param name="user"></param>
		/// <returns></returns
		public async Task AddBookToUsersCollectionAsync(int id, ApplicationUser user)
		{
			Book? book = await this._dbContext.Books.Include(b => b.ApplicationUsersBooks).FirstOrDefaultAsync(b => b.Id == id);

			if (book is null)
			{
				throw new Exception("Book can not be found in the database!");
			}

			var userBook = new ApplicationUserBook()
			{
				BookId = book.Id,
				Book = book
			};

			bool bookAlreadyAdded = user.ApplicationUsersBooks.Any(b => b.Book.Id == book.Id);

			if (!bookAlreadyAdded)
			{
				user.ApplicationUsersBooks.Add(userBook);
				await this._dbContext.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Finds all books for the provided user. This method uses eager loading in order to load the books collection after it found the user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns>"List<BookViewModel>" all books related to the user. </returns>
		public async Task<List<BookViewModel>> GetAllBooksForUserAsync(ApplicationUser user)
		{
			List<ApplicationUserBook> applicationUsersBooks = await this._dbContext
				.ApplicationUserBooks
				.Include(au => au.Book)
				.ThenInclude(b => b.Category)
				.Where(au => au.ApplicationUser.Id == user.Id)
				.ToListAsync();


			List<BookViewModel> books = applicationUsersBooks
				.Select(au => new BookViewModel()
				{
					Title = au.Book.Title,
					Author = au.Book.Author,
					Description = au.Book.Description,
					ImageUrl = au.Book.ImageUrl,
					Category = new CategoryViewModel()
					{
						Id = au.Book.Category.Id,
						Name = au.Book.Category.Name
					}
				})
				.ToList();

			return books;
		}

		/// <summary>
		/// Removes the book with the provided "bookId" from the user's collection if it exists.
		/// </summary>
		/// <param name="bookId"></param>
		/// <returns>If "bookId" is not found the moethod does nothing.</returns>
		public async Task RemoveFromCollectionAsync(int bookId, ApplicationUser user)
		{
			await this._dbContext
				.Entry(user)
				.Collection(u => u.ApplicationUsersBooks)
				.LoadAsync();

			
			ApplicationUserBook? bookToRemove = await this._dbContext
				.ApplicationUserBooks
				.FirstOrDefaultAsync(au => au.ApplicationUser.Id == user.Id);
			
			if (bookToRemove != null)
			{
				user.ApplicationUsersBooks.Remove(bookToRemove);

				await this._dbContext.SaveChangesAsync();
			}
		}
	}
}
