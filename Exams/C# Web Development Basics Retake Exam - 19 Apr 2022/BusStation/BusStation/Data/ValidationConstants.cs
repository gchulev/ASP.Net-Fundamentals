namespace BusStation.Data
{
	public static class ValidationConstants
	{
		public static class UserValidations
		{
			public const int UserNameMinLength = 5;
			public const int UserNameMaxLength = 20;

			public const int UserEmailMinLength = 10;
			public const int UserEmailMaxLength = 60;

			public const int UserPasswordMinLength = 5;
			public const int UserPasswordMaxLength = 20;
		}

		public static class DestinationValidations
		{
			public const int DestinationNameMinLength = 2;
			public const int DestinationNameMaxLenght = 50;

			public const int DestinationOriginMinLength = 2;
			public const int DestinationOriginMaxLength = 50;

			public const int DestinationDateMaxLength = 30;

			public const int DestinationTimeMaxLength = 30;
		}

		public static class TiketValidations
		{
			public const double TiketPriceMinValue = 10;
			public const double TiketPriceValue = 90;
		}
	}
}
