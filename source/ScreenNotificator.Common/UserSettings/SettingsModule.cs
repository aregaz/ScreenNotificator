using System.Collections.Generic;

namespace ScreenNotificator.Common.UserSettings
{
	public class SettingsModule : ISettingsModule
	{
		public delegate void SettingUpdatedHandler(ISettingsModule updatedModule);
		public event SettingUpdatedHandler SettingsUpdated;

		internal SettingsModule(string name, Dictionary<string, string> settings)
		{
			this.ModuleName = name;
			this.Settings = settings;
		}

		public string ModuleName { get; private set; }
		public Dictionary<string, string> Settings { get; private set; }

		public void SetSetting(string settingName, string settingValue, bool postponeSaving = false)
		{
			this.Settings[settingName] = settingValue;
			if (!postponeSaving && SettingsUpdated != null) SettingsUpdated(this);
		}

		public void RemoveSetting(string settingName, bool postponeSaving = false)
		{
			this.Settings.Remove(settingName);
			if (!postponeSaving && SettingsUpdated != null) SettingsUpdated(this);
		}
	}
}