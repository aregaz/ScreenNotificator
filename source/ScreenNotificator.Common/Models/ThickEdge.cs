namespace ScreenNotificator.Common.Models
{
	public class ThickEdge
	{
		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }

		public ThickEdge(int left, int top, int right, int bottom)
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