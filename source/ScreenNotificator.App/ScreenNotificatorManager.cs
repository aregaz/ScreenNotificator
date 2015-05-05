using System;
using System.Drawing;
using System.IO;
using ScreenNotificator.Common;
using ScreenNotificator.Common.Models;
using ScreenNotificator.Common.Models.Calendar;

namespace ScreenNotificator.App
{
	public class ScreenNotificatorManager
	{
		private readonly NotificationArea notificationArea;
		private readonly ScreenResolution screenResolution; // TODO: get rid of ScreenResolution reference. Use int x int parameters
		private readonly LockScreenManager lockScreenManager;

		public ScreenNotificatorManager(ScreenResolution screenResolution)
		{
			this.screenResolution = screenResolution;
			this.notificationArea = new NotificationArea(screenResolution.Width, screenResolution.Height);

			this.lockScreenManager = new LockScreenManager();
		}

		public void UpdateLockScreen(string filePath, Schedule schedule)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("File not found.");
			}

			// 1) copy external image to internal folder. Name it as "original.ext"
			var internalFilePath = FileManager.CopyExternalFileToAssemblyFolder(filePath, "Images", "original");

			// 1.5) convert it to PNG - "original.png"
			internalFilePath = ImageManager.ConvertImageToPng(internalFilePath, true);
			
			// 2) add NotificationCanvas on internal message. Name result as "screen.ext"
			var lockScreenImageFilePath = FileManager.CopyFile(internalFilePath, "lockScreen");
			var lockScreenImage = ImageManager.LoadImage(lockScreenImageFilePath, this.screenResolution);

			// 2.5) draw NotificationArea based on schedule
			////lockScreenImage.DrawNotificationArea();
			var notificationAreaImage = this.GetPrintedSchedule(schedule);
			using (var graphics = Graphics.FromImage(lockScreenImage.Image))
			{
				graphics.DrawImage(
					notificationAreaImage,
					this.notificationArea.X,
					this.notificationArea.Y);
			}
			lockScreenImage.SaveImage(Path.GetDirectoryName(lockScreenImageFilePath));

			// 3) set "screen.ext" as LockScreen image
			this.lockScreenManager.ChangeLockScreenImage(lockScreenImageFilePath);
		}

		private Image GetPrintedSchedule(Schedule schedule)
		{
			var image = new Bitmap(this.notificationArea.Width, this.notificationArea.Height) as Image;
			var schedulePrinter = new SchedulePrinter();
			schedulePrinter.Print(schedule, image);
			return image;
		}
	}
}