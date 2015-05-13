namespace ScreenNotificator.Common.Extensions
{
	public static class FileExtensions
	{
		public static string ExtensionOnly(this string fileExtension)
		{
			return fileExtension.Replace(".", string.Empty);
		}
	}
}