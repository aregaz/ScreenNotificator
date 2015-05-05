using System;
using System.Drawing;
using System.Xml.Schema;
using ScreenNotificator.Common.Models;
using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.Common
{
	public class SchedulePrinter
	{
		private readonly SchedulePropertiesFormatter schedulePropertiesFormatter = new SchedulePropertiesFormatter();

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
		public ThickEdge EventTimePadding { get; set; }
		public ThickEdge EventDescriptionPadding { get; set; }
		public ThickEdge EventLocationPadding { get; set; }
		public ThickEdge WorkDayPadding { get; set; }

		public SchedulePrinter()
			: this
				(
				new Font("Consolas", 17f, FontStyle.Bold),
				new Font("Arial", 17f, FontStyle.Regular),
				new Font(FontFamily.GenericSansSerif, 17f),
				new Font("Segoe UI", 15f, FontStyle.Italic),
				new Font("Consolas", 15f, FontStyle.Italic),

				new SolidBrush(Color.FromArgb(255, 134, 182, 160)),
				new SolidBrush(Color.FromArgb(255, 34, 182, 160)),
				new SolidBrush(Color.FromArgb(255, 234, 82, 160)),
				new SolidBrush(Color.FromArgb(255, 234, 202, 16)),
				new SolidBrush(Color.FromArgb(255, 234, 70, 60))
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
			this.EventTimePadding = new ThickEdge(left: 35, bottom: 5);
			this.EventDescriptionPadding = new ThickEdge(left: 50);
			this.EventLocationPadding = new ThickEdge(left: 50);
			this.WorkDayPadding = new ThickEdge(bottom: 15);
		}

		public void Print(Schedule schedule, Image image, ThickEdge imagePadding = null)
		{
			if (imagePadding == null)
			{
				imagePadding = new ThickEdge();
			}

			var imageAllowedWidth = image.Width - imagePadding.Left - imagePadding.Right;
			var imageAllowedHeight = image.Height - imagePadding.Top - imagePadding.Bottom;

			using (var graphics = Graphics.FromImage(image))
			{
				var daysShift = 0;
				var eventsShift = 0;

				foreach (var workDay in schedule.Days)
				{
					eventsShift = 0;

					// draw day title:
					var dayTextToPrint = schedulePropertiesFormatter.GetDay(workDay);
					var dayPrintedSize = graphics.MeasureString(
						dayTextToPrint,
						this.DayNameFont,
						new PointF(),
						StringFormat.GenericDefault);

					var workDayX = imagePadding.Left + this.GlobalPadding.Left;
					var workDayY = imagePadding.Top + this.GlobalPadding.Top + daysShift;
					var workDayWidth = imageAllowedWidth - this.GlobalPadding.Left - this.GlobalPadding.Right;
					var workDayHeight = (int)dayPrintedSize.Height;

					graphics.DrawString(
						dayTextToPrint,
						this.DayNameFont,
						this.DayNameBrush,
						new RectangleF(workDayX, workDayY, workDayWidth, workDayHeight));

					// draw each event:
					foreach (var scheduledEvent in workDay.Events)
					{
						var eventTimeTextToPrint = schedulePropertiesFormatter.GetEventTime(scheduledEvent);
						var eventTitleTextToPrint = scheduledEvent.Title;
						var eventDescriptionTextToPrint = (scheduledEvent.Description ?? string.Empty).Trim();
						var eventLocationTextToPrint = schedulePropertiesFormatter.GetLocation(scheduledEvent);

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

						var eventTimeX = workDayX + this.EventTimePadding.Left;
						var eventTimeY = workDayY + this.EventTimePadding.Top + workDayHeight + eventsShift;
						var eventTimeWidth = (int)(workDayWidth * 0.3);
						var eventTimeHeight = (int)Math.Max(eventTimePrintedSize.Height, eventTitlePrintedSize.Height);
						var eventTitleX = eventTimeX + eventTimeWidth;
						var eventTitleY = eventTimeY;
						var eventTitleWidth = workDayWidth - eventTimeWidth;
						var eventTitleHeight = eventTimeHeight;

						var eventDescriptionX = eventTimeX + this.EventDescriptionPadding.Left;
						var eventDescriptionY = eventTimeY + eventTimeHeight;
						var eventDescritpionWidth =
							imageAllowedWidth
							- this.GlobalPadding.Left - this.GlobalPadding.Right
							- this.EventDescriptionPadding.Left;
						var eventDescriptionHeight = 0;

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

						// draw event description:
						if (!string.IsNullOrWhiteSpace(eventDescriptionTextToPrint))
						{
							var eventDescriptionPrintedSize = graphics.MeasureString(
								eventDescriptionTextToPrint,
								this.EventDescriptionFont,
								new SizeF(eventDescritpionWidth, float.MaxValue),
								StringFormat.GenericDefault);

							eventDescriptionHeight = (int)eventDescriptionPrintedSize.Height;

							graphics.DrawString(
								eventDescriptionTextToPrint,
								this.EventDescriptionFont,
								this.EventDescriptionBrush,
								new RectangleF(eventDescriptionX, eventDescriptionY, eventDescritpionWidth, eventDescriptionHeight));
						}

						var eventLocationX = eventTimeX + this.EventLocationPadding.Left;
						var eventLocationY = eventDescriptionY + eventDescriptionHeight;
						var eventLocationWidth = eventDescritpionWidth - this.EventLocationPadding.Left;
						var eventLocationHeight = 0;

						// draw event location:
						if (!string.IsNullOrWhiteSpace(eventLocationTextToPrint))
						{
							var eventLocationPrintedSize = graphics.MeasureString(
								eventLocationTextToPrint,
								this.EventLocationFont,
								new SizeF(eventLocationWidth, float.MaxValue),
								StringFormat.GenericDefault);

							eventLocationHeight = (int)eventLocationPrintedSize.Height;

							graphics.DrawString(
								eventLocationTextToPrint,
								this.EventLocationFont,
								this.EventLocationBrush,
								new RectangleF(eventLocationX, eventLocationY, eventLocationWidth, eventLocationHeight));
						}

						eventsShift += eventTimeHeight + eventDescriptionHeight + eventLocationHeight;
					}

					daysShift += workDayHeight + eventsShift + this.WorkDayPadding.Bottom;
				}
			}
		}
	}
}