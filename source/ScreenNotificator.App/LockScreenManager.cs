using System;
using Windows.Storage;
using Windows.System.UserProfile;
using ScreenNotificator.Common;

namespace ScreenNotificator.App
{
	public class LockScreenManager
	{
		public static void ChangeLockScreenImage(string filePath)
		{
			var internalFilePath = FileManager.CopyExternalFileToAssemblyFolder(filePath);
			
			var image = StorageFile
				.GetFileFromPathAsync(internalFilePath)
				.AsTask()
				.Result;

			LockScreen.SetImageFileAsync(image).AsTask().Wait();

			//var originalImage = LockScreen.OriginalImageFile;
		}
	}
}