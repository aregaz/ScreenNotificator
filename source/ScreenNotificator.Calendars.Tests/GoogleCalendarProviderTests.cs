using System;
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
	}
}
