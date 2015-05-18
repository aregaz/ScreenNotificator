using System;
using System.Collections.Generic;

namespace ScreenNotificator.Calendars.Providers
{
	public class GoogleCalendarProvider : ICalendarProvider
	{
		public IEnumerable<string> GetCalendarList()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetEventsFromCalendar(IEnumerable<string> calendarIDs, DateTime startDate, DateTime? endDate)
		{
			throw new NotImplementedException();
		}
	}
}