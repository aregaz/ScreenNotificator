using System;
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
		public ThickEdge DayTitlePadding { get; set; }
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
			this.DayTitlePadding = new ThickEdge(bottom: 25);
			this.EventPadding = new ThickEdge(left: 15, bottom: 5);
		}

		public void Print(Schedule schedule, Image image, ThickEdge imagePadding = null)
		{
			if (imagePadding == null)
			{
				imagePadding = new ThickEdge();
			}

			using (var graphics = Graphics.FromImage(image))
			{
				var daysShift = 0;
				var eventsShift = 0;

				foreach (var workDay in schedule.Days)
				{
					eventsShift = 0;

					// draw day title:
					var dayTextToPrint = workDay.Date.ToLongDateString();
					var dayPrintedSize = graphics.MeasureString(
						dayTextToPrint,
						this.DayNameFont,
						new PointF(),
						StringFormat.GenericDefault);

					var workDayX = imagePadding.Left + this.GlobalPadding.Left;
					var workDayY = imagePadding.Top + this.GlobalPadding.Top + daysShift;
					var workDayWidth = image.Width
					                   - imagePadding.Left - imagePadding.Right
					                   - this.GlobalPadding.Left - this.GlobalPadding.Right;
					var workDayHeight = (int)dayPrintedSize.Height;
										//image.Height
										//- imagePadding.Top - imagePadding.Bottom
										//- this.GlobalPadding.Top - this.GlobalPadding.Bottom;

					graphics.DrawString(
						dayTextToPrint,
						this.DayNameFont,
						this.DayNameBrush,
						new RectangleF(workDayX, workDayY, workDayWidth, workDayHeight));

					// draw each event:
					foreach (var scheduledEvent in workDay.Events)
					{
						var eventTimeTextToPrint = scheduledEvent.Start.ToShortTimeString();
						var eventTitleTextToPrint = scheduledEvent.Title;

						var eventTimePrintedSize = graphics.MeasureString(
							eventTimeTextToPrint,
							this.EventTimeFont,
							new PointF(),
							StringFormat.GenericDefault);
						var eventTitlePrintedSize = graphics.MeasureString(
							eventTitleTextToPrint,
							this.EventTitleFont,
							new PointF(),
							StringFormat.GenericDefault);

						var eventTimeX = workDayX + this.EventPadding.Left;
						var eventTimeY = workDayY + this.EventPadding.Top + workDayHeight + eventsShift;
						var eventTimeWidth = (int)(workDayWidth * 0.3);
						var eventTimeHeight = (int)Math.Max(eventTimePrintedSize.Height, eventTitlePrintedSize.Height);
						var eventTitleX = eventTimeX + eventTimeWidth;
						var eventTitleY = eventTimeY;
						var eventTitleWidth = workDayWidth - eventTimeWidth;
						var eventTitleHeight = eventTimeHeight;

						// draw event time:
						graphics.DrawString(
							eventTimeTextToPrint,
							this.EventTimeFont,
							this.EventTimeBrush,
							new RectangleF(eventTimeX, eventTimeY, eventTimeWidth, eventTimeHeight));
						
						// draw event title:
						graphics.DrawString(
							eventTitleTextToPrint,
							this.EventTitleFont,
							this.EventTitleBrush,
							new RectangleF(eventTitleX, eventTitleY, eventTitleWidth, eventTitleHeight));

						eventsShift += eventTimeHeight + eventTimeHeight;
					}

					daysShift += workDayHeight + eventsShift;
				}
				
			}
		}
	}
}