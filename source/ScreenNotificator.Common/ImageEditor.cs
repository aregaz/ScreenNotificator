using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ScreenNotificator.Common
{
	public static class ImageEditor
	{
		public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
		{
			using (image)
			{
				var ratioX = (double)maxWidth / image.Width;
				var ratioY = (double)maxHeight / image.Height;
				var ratio = Math.Min(ratioX, ratioY);

				var newWidth = (int)(image.Width * ratio);
				var newHeight = (int)(image.Height * ratio);

				var newImage = new Bitmap(newWidth, newHeight);
				using (var graphics = Graphics.FromImage(newImage))
				{
					graphics.DrawImage(image, 0, 0, newWidth, newHeight);
				}

				return newImage;
			}
		}

		public static void DrawNotificationArea(Image canvas, Image notificationAreaImage, PointF startPosition)
		{
			using (var graphics = Graphics.FromImage(canvas))
			{
				graphics.DrawImage(notificationAreaImage, startPosition);
			}
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