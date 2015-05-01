using System;

namespace ScreenNotificator.Common.Models.Calendar
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
}