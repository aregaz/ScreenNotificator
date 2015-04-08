using System;
using System.Drawing;
using System.IO;

namespace ScreenNotificator.App
{
	public class LockScreenImage : IDisposable
	{
		public string FilePath { get; set; }
		public Image Image { get; set; }

		public LockScreenImage(string filePath)
		{
			this.FilePath = filePath;
			this.LoadImage(this.FilePath);
		}

		private void LoadImage(string filePath)
		{
			var image = Image.FromFile(filePath);
			this.Image = image;
		}

		public void DrawNotificationArea()
		{
			this.DrawNotificationArea(this.Image);
		}

		private void DrawNotificationArea(Image bmp)
		{
			var backgroundBrush = new SolidBrush(Color.FromArgb(128, 239, 241, 246));

			using (var graphics = Graphics.FromImage(bmp))
			{
				var notificationArea = new NotificationArea(bmp.Width, bmp.Height);
				var notificationAreaRectangle = new Rectangle(
					notificationArea.X,
					notificationArea.Y,
					notificationArea.Width,
					notificationArea.Height);

				graphics.FillRectangle(backgroundBrush, notificationAreaRectangle);
			}
		}

		public void SaveImage(string folder, string fileName = null)
		{
			if (fileName == null)
			{
				fileName = Path.GetFileName(this.FilePath);
			}

			var filePath = Path.Combine(folder, fileName);

			this.Image.Save(filePath);
		}

		public void Dispose()
		{
			this.Image.Dispose();
		}
	}
}