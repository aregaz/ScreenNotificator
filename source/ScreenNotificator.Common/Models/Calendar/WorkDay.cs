using System;
using System.Collections.Generic;

namespace ScreenNotificator.Common.Models.Calendar
{
	public class WorkDay
	{
		private List<Event> events;

		public IEnumerable<Event> Events { get { return events; } }
		public DateTime Date { get; set; }

		public WorkDay(DateTime date)
		{
			this.Date = date.Date;
			this.events = new List<Event>();
		}

		public void AddEvent(Event newEvent)
		{
			if (!DateTime.Equals(newEvent.Start.Date, this.Date))
			{
				throw new ArgumentOutOfRangeException(
					"newEvent",
					string.Format(
						"Event started on {0} cannot be added to workday of {1}",
						newEvent.Start.Date.ToShortDateString(),
						this.Date.ToShortDateString()));
			}

			this.events.Add(newEvent);
		}
	}
}