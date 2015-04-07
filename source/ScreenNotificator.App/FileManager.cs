﻿using System.IO;
using System.Reflection;

namespace ScreenNotificator.App
{
	public class FileManager
	{
		public static string CopyExternalFileToAssemblyFolder(string externalFilePath)
		{
			var originalFileName = Path.GetFileName(externalFilePath);

			var internalFolderPath = FileManager.CreateInternalFolder("Images");
			var internalFilePath = string.Format("{0}\\{1}", internalFolderPath, originalFileName);

			File.Copy(externalFilePath, internalFilePath);

			if (!File.Exists(internalFilePath))
			{
				throw new FileNotFoundException("Unable to copy external file to internal folder.");
			}

			return internalFilePath;
		}


		private static string CreateInternalFolder(string folderName)
		{
			var assembyPath = Assembly.GetExecutingAssembly().GetName().CodeBase;
			var assemblyFolder = Path.GetDirectoryName(assembyPath).Replace(@"file:\", "");

			var internalFolderPath = string.Format("{0}\\{1}", assemblyFolder, folderName);

			if (!Directory.Exists(internalFolderPath))
			{
				Directory.CreateDirectory(internalFolderPath);
			}

			return internalFolderPath;
		}
	}
}