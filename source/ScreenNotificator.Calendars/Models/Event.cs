using System;

namespace ScreenNotificator.Calendars.Models
{
	public class Event
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}