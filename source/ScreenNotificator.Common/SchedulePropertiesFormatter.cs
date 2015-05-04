using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.Common
{
	internal class SchedulePropertiesFormatter
	{
		public string GetEventTime(Event scheduledEvent)
		{
			return scheduledEvent.Start.ToString("HH:mm");
		}

		public string GetDay(WorkDay workDay)
		{
			var date = workDay.Date;

			var culture = new System.Globalization.CultureInfo("uk-UA");
			var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);

			return string.Format(
				"{0} - {1}",
				date.ToString("dd MMMM"),
				day);
		}

		public string GetLocation(Event scheduledEvent)
		{
			if (string.IsNullOrWhiteSpace(scheduledEvent.Location)) return null;

			return string.Format(
				"@ {0}",
				scheduledEvent.Location);
		}
	}
}