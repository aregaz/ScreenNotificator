using System;
using System.Collections.Generic;

namespace ScreenNotificator.Calendars.Providers
{
	public interface ICalendarProvider
	{
		IEnumerable<string> GetCalendarList();
		IEnumerable<string> GetEventsFromCalendar(IEnumerable<string> calendarIDs, DateTime startDate, DateTime? endDate);
	}
}