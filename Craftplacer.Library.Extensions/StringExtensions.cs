namespace Craftplacer.Library.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string value)	=> string.IsNullOrWhiteSpace(value);
		public static bool IsNullOrEmpty(this string value)			=> string.IsNullOrEmpty(value);
	}
}
