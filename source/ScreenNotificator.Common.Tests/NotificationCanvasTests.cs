using System;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.Common.Tests
{
	[TestClass]
	public class NotificationCanvasTests
	{
		[TestMethod]
		public void SaveToFile_NoFormat_Success()
		{
			var canvas = new NotificationCanvas(1200, 900, "Цей текст створенно з теста.");
			canvas.SaveCanvasToFile("image.png", ImageFormat.Png);
			
			Assert.IsNotNull(canvas.Image);
		}


		[TestMethod]
		public void SchedulePrinter__Success()
		{
			var now = DateTime.Now;

			var schedule = new Schedule();
			schedule.AddEvent(new Event()
			{
				Start = now,
				Title = "Test 1",
				Description = "Test description one"
			});
			schedule.AddEvent(new Event()
			{
				Start = now.AddHours(2),
				Title = "Test 2",
				Description = "This is long description. Lorem ipsum and further text. Bla-bla-bla. More text.",
				Location = "Home"
			});
			schedule.AddEvent(new Event()
			{
				Start = now.AddDays(1),
				Title = "Test 3",
				Description = null,
				Location = "Office"
			});

			var canvas = new NotificationCanvas(520, 900, schedule);
			canvas.SaveCanvasToFile("image.png", ImageFormat.Png);
			
			Assert.IsNotNull(canvas.Image);
		}
	}
}
