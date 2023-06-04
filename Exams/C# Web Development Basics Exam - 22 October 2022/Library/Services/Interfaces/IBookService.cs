using Library.Data.Models;
using Library.ViewModels.Book;
using Library.ViewModels.Category;

namespace Library.Services.Interfaces
{
    public interface IBookService
    {
        public Task<List<BookViewModel>> AllBooksAsync();
        public Task AddBookAsync(BookViewModel bookViewModel);
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        /// <summary>
        /// Finds the book by id.
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <returns>Returns the "id" of the book or null if not found.</returns>
        public Task<Book?> FindBookByIdAsync(int id);

        /// <summary>
        /// Finds the book by its "id" and adds it to the "user" collection of books "ApplicationUserBooks" and applies changes to the Db.
        /// Throws and exception if book is not found!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task AddBookToUsersCollectionAsync(int id, ApplicationUser user);

        /// <summary>
        /// Finds all books for the provided user. This method uses eager loading in order to load the books collection after it found the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>"List<BookViewModel>" all books related to the user. </returns>
        public Task<List<BookViewModel>> GetAllBooksForUserAsync(ApplicationUser user);

        /// <summary>
        /// Removes the book with the provided "bookId" from the user's collection if it exists.
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>If "bookId" is not found the moethod does nothing.</returns>
        public Task RemoveFromCollectionAsync(int bookId, ApplicationUser user);
	}
}
