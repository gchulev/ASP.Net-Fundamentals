using Library.Data.Models;

namespace Library.Data
{
	public class ValidationConstants
	{
		public static class Book
		{
			public const int BookTitleMinLength = 10;
			public const int BookTitleMaxLength = 50;

			public const int BookAuthoMinLength = 5;
			public const int BookAuthorMaxLength = 50;

			public const int BookDescriptionMinLength = 5;
			public const int BookDescriptionMaxLength = 5000;

			public const double BookRatingMinValue = 0.00;
			public const double BookRatingMaxValue = 10.00;
		}

		public static class AppUser
		{
			public const int AppUserUserNameMinValue = 5;
			public const int AppUserUserNameMaxValue = 20;

			public const int AppUserEmailMinValue = 10;
			public const int AppUserEmailMaxValue = 60;

			public const int AppUserPasswordMinLength = 5;
			public const int AppUserPasswordMaxLength = 20;
		}

		public static class Category
		{
			public const int CategoryNameMinLength = 5;
			public const int CategoryNameMaxLength = 50;
		}
	}
}
