using System.Collections.Generic;

namespace ScreenNotificator.Common.UserSettings
{
	public interface ISettingsModule
	{
		event SettingsModule.SettingUpdatedHandler SettingsUpdated;
		string ModuleName { get; }
		Dictionary<string, string> Settings { get; }

		/// <summary>
		/// Adds or sets new setting with the value.
		/// </summary>
		/// <param name="settingName">Unique name of a setting in the Module.</param>
		/// <param name="settingValue">Value of the setting.</param>
		/// <param name="postponeSaving">Indicates that SettingUpdated event should not be fired. Gives an option
		/// to update settings in bulk, but in this case you should not forgot to fire SettingsUpdated event
		/// by passing <paramref name="postponeSaving"/>=true</param>
		void SetSetting(string settingName, string settingValue, bool postponeSaving = false);

		/// <summary>
		/// Removes setting.
		/// </summary>
		/// <param name="settingName">Unique name of a setting in the Module.</param>
		/// <param name="postponeSaving">Indicates that SettingUpdated event should not be fired. Gives an option
		/// to update settings in bulk, but in this case you should not forgot to fire SettingsUpdated event
		/// by passing <paramref name="postponeSaving"/>=true</param>
		void RemoveSetting(string settingName, bool postponeSaving = false);
	}
}