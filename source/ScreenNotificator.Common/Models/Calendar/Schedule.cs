using System;
using System.Collections.Generic;
using System.Linq;

namespace ScreenNotificator.Common.Models.Calendar
{
	public class Schedule
	{
		private readonly List<WorkDay> days = new List<WorkDay>();
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

		public void AddEvent(Event newEventToSchedule)
		{
			var theDay = this[newEventToSchedule.Start];
			if (theDay == null)
			{
				this.days.Add(new WorkDay(newEventToSchedule.Start));
				theDay = this[newEventToSchedule.Start];
			}

			theDay.AddEvent(newEventToSchedule);
		}
	}
}