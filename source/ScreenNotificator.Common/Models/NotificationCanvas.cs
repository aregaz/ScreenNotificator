using System.Drawing;

namespace ScreenNotificator.Common.Models
{
	public class NotificationCanvas
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Image Image { get; set; }

		public NotificationCanvas(int width, int height)
		{
			this.Width = width;
			this.Height = height;

			this.Image = new Bitmap(width, height);

			using (var graphics = Graphics.FromImage(this.Image))
			{
				this.DrawBackground(graphics);
				this.DrawText(graphics);
			}
		}

		private void DrawBackground(Graphics graphics)
		{
			var backgroundBrush = new SolidBrush(Color.FromArgb(128, 239, 241, 246));

			var background = new Rectangle(0, 0, this.Image.Width, this.Image.Height);
			graphics.FillRectangle(backgroundBrush, background);
		}

		private void DrawText(Graphics graphics)
		{
			graphics.DrawString(
				"Тут будуть ваші події для нагадування",
				new Font("Segoe UI", 30f),
				new SolidBrush(Color.Black),
				new PointF(10f, 10f));
		}
	}
}