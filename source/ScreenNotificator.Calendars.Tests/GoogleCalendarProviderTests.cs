using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenNotificator.Calendars.Providers;

namespace ScreenNotificator.Calendars.Tests
{
	[TestClass]
	public class GoogleCalendarProviderTests
	{
		[TestMethod]
		public void Do__Success()
		{
			var provider = new GoogleCalendarProvider();
			provider.Do();
			
			Assert.IsTrue(true);
		}


		[TestMethod]
		public void GetCalendarsList__Success()
		{
			var provider = new GoogleCalendarProvider();
			var calendars = provider.GetCalendarList();

			Assert.IsTrue(calendars.Any());
		}
	}
}
