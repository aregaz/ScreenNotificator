using System;
using System.Drawing;
using ScreenNotificator.Common.Models;

namespace ScreenNotificator.Common
{
	internal static class ImageEditor
	{
		internal static Image ScaleImage(Image image, int maxWidth, int maxHeight)
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

		internal static void DrawNotificationArea(Image bmp)
		{
			using (var graphics = Graphics.FromImage(bmp))
			{
				var notificationArea = new NotificationArea(bmp.Width, bmp.Height);
				var notificationCanvas = new NotificationCanvas(
					notificationArea.Width,
					notificationArea.Height);
				graphics.DrawImage(
					notificationCanvas.Image,
					new PointF(notificationArea.X, notificationArea.Y));
			}
		}
	}
}