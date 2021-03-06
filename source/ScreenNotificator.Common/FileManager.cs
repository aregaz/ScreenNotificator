﻿using System;
using System.IO;
using System.Reflection;

namespace ScreenNotificator.Common
{
	public class FileManager
	{
		public static string CopyExternalFileToAssemblyFolder(
			string externalFilePath,
			string internalFolder = null,
			string newFileName = null)
		{
			var originalFileName = Path.GetFileNameWithoutExtension(externalFilePath);
			var originalFileExtension = FileManager.GetExtensionFromFilePath(externalFilePath);

			if (string.IsNullOrWhiteSpace(newFileName))
			{
				newFileName = originalFileName;
			}

			var internalFolderPath = internalFolder == null
				? FileManager.GetAssemblyFolder()
				: FileManager.CreateInternalFolder(internalFolder);
			var internalFilePath = string.Format("{0}\\{1}.{2}", internalFolderPath, newFileName, originalFileExtension);

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

		public static string CopyFile(string filePath, string newFileName, bool deleteDestinationFileIfExists = true)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("Unable to copy file. File not found.");
			}

			var originalFileDirectory = Path.GetDirectoryName(filePath);
			var originalFileExtension = FileManager.GetExtensionFromFilePath(filePath);

			var destinationFilePath = string.Format(
				"{0}\\{1}.{2}",
				originalFileDirectory,
				newFileName,
				originalFileExtension);

			if (File.Exists(destinationFilePath))
			{
				if (deleteDestinationFileIfExists)
				{
					File.Delete(destinationFilePath);
				}
				else
				{
					throw new Exception("Unable to copy file. Destination file already exists");
				}
			}
			File.Copy(filePath, destinationFilePath);

			return destinationFilePath;
		}

		public static void DeleteFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			else
			{
				throw new Exception("Unable to copy file. Destination file already exists");
			}
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

		private static string GetExtensionFromFilePath(string filePath)
		{
			return
				File.Exists(filePath)
					? Path.GetExtension(filePath).Replace(".", string.Empty)
					: string.Empty;
		}
	}
}