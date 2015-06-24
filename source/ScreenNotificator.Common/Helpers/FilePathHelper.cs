using System;
using System.IO;

namespace ScreenNotificator.Common.Helpers
{
	public static class FilePathHelper
	{
		private const string ApplicationName = "ScreenNotificator";
		private const string SettingsFileName = "settings.xml";

		public static string GetSpecialFolder()
		{
			var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			folder = Path.Combine(folder, ApplicationName);
			return folder;
		}

		public static string GetSettingFilePath()
		{
			var settingFilePath = Path.Combine(FilePathHelper.GetSpecialFolder(), FilePathHelper.SettingsFileName);
			return settingFilePath;
		}
	}
}