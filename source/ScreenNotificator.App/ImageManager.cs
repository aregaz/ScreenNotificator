using System.IO;

namespace ScreenNotificator.App
{
	public class ImageManager
	{
		public static LockScreenImage LoadImage(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(string.Format("Image not found: {0}", filePath));
			}

			var image = new LockScreenImage(filePath);
			return image;
		}
	}
}