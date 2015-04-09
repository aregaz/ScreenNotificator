using System.IO;
using ScreenNotificator.Common.Models;

namespace ScreenNotificator.Common
{
	public class ImageManager
	{
		public static LockScreenImage LoadImage(string filePath, ScreenResolution screenResolution)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(string.Format("Image not found: {0}", filePath));
			}

			var image = new LockScreenImage(filePath, screenResolution);
			return image;
		}
	}
}