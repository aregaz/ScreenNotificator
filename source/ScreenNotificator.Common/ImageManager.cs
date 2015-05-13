using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

		public static string ConvertImageToPng(string filePath, bool deleteOriginal = false)
		{
			string newFilePath;

			using (var image = Image.FromFile(filePath))
			{
				var fileName = Path.GetFileNameWithoutExtension(filePath);
				var newFileName = string.Format("{0}.png", fileName);
				newFilePath = string.Format("{0}\\{1}", Path.GetDirectoryName(filePath), newFileName);
				image.Save(newFilePath, ImageFormat.Png);
			}

			if (deleteOriginal)
			{
				File.Delete(filePath);
			}

			return newFilePath;
		}
	}
}