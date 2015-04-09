namespace ScreenNotificator.Common.Models
{
	public class ScreenResolution
	{
		public const int PointsInInch = 96;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public ScreenResolution(int widthInWpfUnits, int heightInWpfUnits, int dpi)
		{
			this.Width = (int)(((float)dpi / ScreenResolution.PointsInInch) * widthInWpfUnits);
			this.Height = (int)(((float)dpi / ScreenResolution.PointsInInch) * heightInWpfUnits);
		}

		public ScreenResolution(double widthInWpfUnits, double heightInWpfUnits, int dpi)
		{
			this.Width = (int)(((double)dpi / ScreenResolution.PointsInInch) * widthInWpfUnits);
			this.Height = (int)(((double)dpi / ScreenResolution.PointsInInch) * heightInWpfUnits);
		}
	}
}