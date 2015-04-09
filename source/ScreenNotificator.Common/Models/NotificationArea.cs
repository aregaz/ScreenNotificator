namespace ScreenNotificator.Common.Models
{
	public class NotificationArea
	{
		private int HorizontalPadding { get; set; }
		private int VerticalPadding { get; set; }

		public int X { get; private set; }
		public int Y { get; private set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public NotificationArea(int imageWidth, int imageHeight)
		{
			var width = (double) imageWidth;
			var height = (double) imageHeight;
			
			this.HorizontalPadding = (int)(width / 2 / 5);
			this.VerticalPadding = (int) (height / 2 / 5);

			this.X = (int) (width/2 + this.HorizontalPadding);
			this.Y = (int) (this.VerticalPadding);

			this.Width = (int) (width/2 - 2*this.HorizontalPadding);
			this.Height = (int) (height - 2*this.VerticalPadding);
		}
	}
}