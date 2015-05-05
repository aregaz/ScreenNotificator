using System;
using System.IO;
using Windows.Storage;
using Windows.System.UserProfile;
using ScreenNotificator.Common;

namespace ScreenNotificator.App
{
	public class LockScreenManager
	{
		public void ChangeLockScreenImage(string filePath)
		{
			var internalFilePath = FileManager.CopyExternalFileToAssemblyFolder(filePath);
			
			var image = StorageFile
				.GetFileFromPathAsync(internalFilePath)
				.AsTask()
				.Result;

			LockScreen.SetImageFileAsync(image).AsTask().Wait();

			var originalImage = LockScreen.OriginalImageFile;

			if (!string.Equals(
				Path.GetFullPath(originalImage.AbsolutePath),
				Path.GetFullPath(internalFilePath),
				StringComparison.CurrentCultureIgnoreCase))
			{
				// lock screen image has not been chenged
				throw new UnableToChangeLockScreenException();
			}
		}
	}
}