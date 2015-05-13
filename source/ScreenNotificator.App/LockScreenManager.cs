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
			if (!IsFileInAssemblyFolder(filePath))
			{
				throw new Exception("LockScreen image file is not in the assembly folder.");
			}
			
			var image = StorageFile
				.GetFileFromPathAsync(filePath)
				.AsTask()
				.Result;

			LockScreen.SetImageFileAsync(image).AsTask().Wait();

			var originalImage = LockScreen.OriginalImageFile;

			if (!string.Equals(
				Path.GetFullPath(originalImage.AbsolutePath),
				Path.GetFullPath(filePath),
				StringComparison.CurrentCultureIgnoreCase))
			{
				// lock screen image has not been chenged
				throw new UnableToChangeLockScreenException();
			}
		}

		private bool IsFileInAssemblyFolder(string filePath)
		{
			if (!File.Exists(filePath)) return false;

			var assemblyFolder = FileManager.GetAssemblyFolder();

			return filePath.StartsWith(assemblyFolder);
		}
	}
}