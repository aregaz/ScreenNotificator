using System;
using System.Drawing;
using System.IO;

namespace ScreenNotificator.Common.Models
{
	public class LockScreenImage : IDisposable
	{
		public string FilePath { get; set; }
		public Image Image { get; set; }

		public NotificationArea NotificationArea { get; set; }
		public NotificationCanvas NotificationCanvas { get; set; }


		public LockScreenImage(string filePath, ScreenResolution screenResolution)
		{
			this.FilePath = filePath;

			var image = Image.FromFile(filePath);
			this.Image = ImageEditor.ScaleImage(
				image,
				screenResolution.Width,
				screenResolution.Height); ;

			this.NotificationArea = new NotificationArea(this.Image.Width, this.Image.Height);
			this.NotificationCanvas = new NotificationCanvas(this.NotificationArea.Width, this.NotificationArea.Height);

			this.DrawNotificationArea();
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


		private void DrawNotificationArea()
		{
			ImageEditor.DrawNotificationArea(
				this.Image,
				this.NotificationCanvas.Image,
				new PointF(this.NotificationArea.X, this.NotificationArea.Y));
		}
	}
}