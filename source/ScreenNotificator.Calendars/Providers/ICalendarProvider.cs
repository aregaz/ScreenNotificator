﻿using System;
using System.Collections.Generic;
using ScreenNotificator.Calendars.Models;

namespace ScreenNotificator.Calendars.Providers
{
	public interface ICalendarProvider
	{
		bool IsAuthorized();
		IEnumerable<Calendar> GetCalendarList();
		IEnumerable<Event> GetEventsFromCalendars(IEnumerable<string> calendarIDs, DateTime startDate, DateTime? endDate);
	}
}