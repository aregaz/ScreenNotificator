using System;
using System.Collections.Generic;
using System.Linq;

namespace ScreenNotificator.Common.Models
{
	public class Event
	{
		public string Title { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
		public bool WholeDay { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
	}

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

	public class DaysSequence
	{
		private List<WorkDay> days;
		public IEnumerable<WorkDay> Days { get { return days.OrderBy(d => d.Date); } }

		public WorkDay this[DateTime date]
		{
			get
			{
				var theDay = this.days.SingleOrDefault(d => DateTime.Equals(d.Date.Date, date.Date));
				//if (theDay == null)
				//{
				//	this.days.Add(new WorkDay(date));
				//}

				//theDay = days.SingleOrDefault(d => DateTime.Equals(d.Date.Date, date.Date));
				return theDay;
			}

			set
			{
				var theDay = this.days.SingleOrDefault(d => DateTime.Equals(d.Date.Date, date.Date));
				if (theDay == null)
				{
					this.days.Add(value);
				}
				else
				{
					foreach (var eventToAdd in value.Events)
					{
						theDay.AddEvent(eventToAdd);
					}
				}
			}
		}

		public override string ToString()
		{
			var text = string.Empty;

			// TODO: finish this next time

			return text;
		}
	}
}