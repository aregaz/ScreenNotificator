using System;
using System.IO;
using Windows.Storage;
using Windows.System.UserProfile;

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

			var originalImage = LockScreen.OriginalImageFile;
		}
	}
}