namespace ScreenNotificator.Common.Models
{
	public class ThickEdge
	{
		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }

		public ThickEdge(int left = 0, int top = 0, int right = 0, int bottom = 0)
		{
			this.Left = left;
			this.Right = right;
			this.Top = top;
			this.Bottom = bottom;
		}

		public ThickEdge(int padding)
			: this(padding, padding, padding, padding)
		{

		}

		public ThickEdge()
			: this(0, 0, 0, 0)
		{

		}
	}
}