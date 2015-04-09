using System.IO;
using System.Reflection;

namespace ScreenNotificator.Common
{
	public class FileManager
	{
		public static string CopyExternalFileToAssemblyFolder(string externalFilePath)
		{
			var originalFileName = Path.GetFileName(externalFilePath);
			var originalFileExtension = Path.GetExtension(externalFilePath);

			var internalFolderPath = FileManager.CreateInternalFolder("Images");
			var internalFilePath = string.Format("{0}\\original.{1}", internalFolderPath, originalFileExtension);

			if (File.Exists(internalFilePath))
			{
				File.Delete(internalFilePath);
			}

			File.Copy(externalFilePath, internalFilePath);

			if (!File.Exists(internalFilePath))
			{
				throw new FileNotFoundException("Unable to copy external file to internal folder.");
			}

			return internalFilePath;
		}

		public static string GetAssemblyFolder()
		{
			var assembyPath = Assembly.GetExecutingAssembly().GetName().CodeBase;
			var assemblyFolder = Path.GetDirectoryName(assembyPath).Replace(@"file:\", "");

			return assemblyFolder;
		}

		private static string CreateInternalFolder(string folderName)
		{
			var assemblyFolder = FileManager.GetAssemblyFolder();

			var internalFolderPath = string.Format("{0}\\{1}", assemblyFolder, folderName);

			if (!Directory.Exists(internalFolderPath))
			{
				Directory.CreateDirectory(internalFolderPath);
			}

			return internalFolderPath;
		}
	}
}