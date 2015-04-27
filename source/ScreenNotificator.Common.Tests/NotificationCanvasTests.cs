using System;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	}
}
