using System;
using System.Drawing;
using System.Drawing.Imaging;
using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.Common.Models
{
	public class NotificationCanvas
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Image Image { get; set; }
		public ThickEdge Padding { get; set; }


		public NotificationCanvas(int width, int height, string text = null)
		{
			this.Width = width;
			this.Height = height;

			this.Image = new Bitmap(width, height);

			this.Padding = new ThickEdge();

			using (var graphics = Graphics.FromImage(this.Image))
			{
				this.DrawBackground(graphics);
				this.DrawModernBorder(graphics);

				if (!string.IsNullOrWhiteSpace(text)) this.DrawText(graphics, text);
			}
		}


		public NotificationCanvas(int width, int height, Schedule schedule)
		{
			this.Width = width;
			this.Height = height;

			this.Image = new Bitmap(width, height);

			this.Padding = new ThickEdge(15);

			using (var graphics = Graphics.FromImage(this.Image))
			{
				this.DrawBackground(graphics);
				this.DrawModernBorder(graphics);
			}

			var schedulePrinter = new SchedulePrinter();
			schedulePrinter.Print(schedule, this.Image, this.Padding);
		}


		public void SaveCanvasToFile(string filePath, ImageFormat format = null)
		{
			this.Image.Save(filePath, format ?? ImageFormat.Png);
		}


		private void DrawBackground(Graphics graphics)
		{
			//var backgroundBrush = new SolidBrush(Color.FromArgb(128, 239, 241, 246));
			var backgroundBrush = new SolidBrush(Color.FromArgb(200, 255, 255, 255));

			var background = new Rectangle(0, 0, this.Image.Width, this.Image.Height);
			graphics.FillRectangle(backgroundBrush, background);
		}


		private void DrawText(Graphics graphics, string text)
		{
			var startPoint = new PointF(10 + this.Padding.Left, 10 + this.Padding.Top);
			graphics.DrawString(
				text,
				new Font("Segoe UI", 30f),
				new SolidBrush(Color.Black),
				startPoint);
		}


		private void DrawModernBorder(Graphics graphics)
		{
			var borderBrush = new SolidBrush(Color.RoyalBlue);
			var borderWidth = (int) Math.Round(0.01*this.Width);
			graphics.FillRectangle(borderBrush, 0, 0, borderWidth, this.Height);

			this.Padding.Left += borderWidth;
		}
	}
}