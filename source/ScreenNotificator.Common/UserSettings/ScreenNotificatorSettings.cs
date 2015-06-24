using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ScreenNotificator.Common.UserSettings
{
	public class ScreenNotificatorSettings : IDisposable
	{
		private string settingsFilePath;

		public List<ISettingsModule> SettingsModules { get; private set; }
		
		public ScreenNotificatorSettings(string settingsFilePath)
		{
			this.settingsFilePath = settingsFilePath;
			var settingsXml = this.LoadSettingsFile(settingsFilePath);
			this.ParseSettings(settingsXml);

			foreach (var settingsModule in this.SettingsModules)
			{
				settingsModule.SettingsUpdated += OnSettingsUpdated;
			}
		}

		private XElement LoadSettingsFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				// settings file does not exists yet
				File.Create(filePath);
			}

			using (var settingsFileStream = new FileStream(filePath, FileMode.Open))
			{
				var xml = XElement.Load(settingsFileStream);
				return xml;
			}
		}

		private void ParseSettings(XElement settingsXml)
		{
			if (settingsXml.IsEmpty)
			{
				// settings is empty
				this.SettingsModules = new List<ISettingsModule>();
				return;
			}

			this.SettingsModules = new List<ISettingsModule>();
			foreach (var moduleXml in settingsXml.Elements("Module"))
			{
				var moduleName = moduleXml.Attribute("Name").Value;
				var settings = moduleXml.Elements("Setting")
					.Select(e => new {Key = e.Attribute("SettingName").Value, Value = e.Value})
					.ToDictionary(s => s.Key, s => s.Value);

				var module = new SettingsModule(moduleName, settings);
				this.SettingsModules.Add(module);
			}
		}

		private void SaveSettingsToFile()
		{
			throw new NotImplementedException();
		}

		private void OnSettingsUpdated(ISettingsModule updatedModule)
		{
			SaveSettingsToFile();
		}

		public void Dispose()
		{
			foreach (var settingsModule in this.SettingsModules)
			{
				settingsModule.SettingsUpdated -= OnSettingsUpdated;
			}
		}
	}
}