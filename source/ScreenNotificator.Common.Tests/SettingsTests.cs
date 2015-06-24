using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScreenNotificator.Common.Helpers;
using ScreenNotificator.Common.UserSettings;

namespace ScreenNotificator.Common.Tests
{
	[TestClass]
	public class SettingsTests
	{
		[TestMethod]
		public void ReadSettings__Success()
		{
			var settingsFilePath = FilePathHelper.GetSettingFilePath();
			var settings = new ScreenNotificatorSettings(settingsFilePath);

			Assert.IsTrue(settings.SettingsModules.Any());
			Assert.IsNotNull(settings.SettingsModules.FirstOrDefault(m => m.ModuleName == "Calendar"));
			Assert.AreEqual(settings.SettingsModules.FirstOrDefault(m => m.ModuleName == "Calendar").Settings["Setting1"], "Value1");
		}
	}
}