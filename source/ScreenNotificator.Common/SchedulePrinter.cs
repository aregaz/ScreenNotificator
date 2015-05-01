using System.Drawing;
using ScreenNotificator.Common.Models;
using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.Common
{
	public class SchedulePrinter
	{
		public Font DayNameFont { get; set; }
		public Font EventTimeFont { get; set; }
		public Font EventTitleFont { get; set; }
		public Font EventDescriptionFont { get; set; }
		public Font EventLocationFont { get; set; }

		public Brush DayNameBrush { get; set; }
		public Brush EventTimeBrush { get; set; }
		public Brush EventTitleBrush { get; set; }
		public Brush EventDescriptionBrush { get; set; }
		public Brush EventLocationBrush { get; set; }

		public ThickEdge GlobalPadding { get; set; }
		public ThickEdge TitlePadding { get; set; }
		public ThickEdge EventPadding { get; set; }

		public SchedulePrinter()
			: this
				(
				new Font("Segoe UI", 17f, FontStyle.Bold),
				new Font("Segoe UI", 17f),
				new Font("Segoe UI", 17f),
				new Font("Segoe UI", 17f),
				new Font("Segoe UI", 17f),

				new SolidBrush(Color.Fuchsia),
				new SolidBrush(Color.Fuchsia),
				new SolidBrush(Color.Fuchsia),
				new SolidBrush(Color.Fuchsia),
				new SolidBrush(Color.Fuchsia)
				)
		{

		}

		public SchedulePrinter(
			Font dayNameFont,
			Font eventTimeFont,
			Font eventTitleFont,
			Font eventDescriptionFont,
			Font eventLocationFont,

			Brush dayNameBrush,
			Brush eventTimeBrush,
			Brush eventTitleBrush,
			Brush eventDescriptionBrush,
			Brush eventLocationBrush)
		{
			this.DayNameFont = dayNameFont;
			this.EventTimeFont = eventTimeFont;
			this.EventTitleFont = eventTitleFont;
			this.EventDescriptionFont = eventDescriptionFont;
			this.EventLocationFont = eventLocationFont;

			this.DayNameBrush = dayNameBrush;
			this.EventTimeBrush = eventTimeBrush;
			this.EventTitleBrush = eventTitleBrush;
			this.EventDescriptionBrush = eventDescriptionBrush;
			this.EventLocationBrush = eventLocationBrush;

			this.GlobalPadding = new ThickEdge(15);
			this.TitlePadding = new ThickEdge(left: 15, right: 15);
			this.EventPadding = new ThickEdge(left: 10);
		}

		public void Print(Image image, Schedule schedule)
		{
			using (var graphics = Graphics.FromImage(image))
			{
				graphics.DrawString(
					"Schedule",
					this.DayNameFont,
					this.DayNameBrush,
					new RectangleF(
						(float)this.GlobalPadding.Left,
						(float)this.GlobalPadding.Top,
						(float)(image.Width - this.GlobalPadding.Left),
						(float)(image.Height - this.GlobalPadding.Left))
					);
			}
		}
	}
}