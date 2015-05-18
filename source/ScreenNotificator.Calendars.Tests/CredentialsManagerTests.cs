using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScreenNotificator.Calendars.Tests
{
	[TestClass]
	public class CredentialsManagerTests
	{
		[TestMethod]
		public void LoadCredentials_Google_Loaded()
		{
			var credentialsManager = new CredentialsManager();

			var credentials = credentialsManager.LoadCredentials("Google");

			Assert.IsNotNull(credentials);
			Assert.IsNotNull(credentials.ProviderName);
			Assert.IsNotNull(credentials.ClientID);
			Assert.IsNotNull(credentials.ClientSecret);

			Assert.AreEqual(credentials.ProviderName, "Google", "Wrong provider name");
			Assert.IsTrue(credentials.ClientID.Length > 0, "ClientID is empty");
			Assert.IsNotNull(credentials.ClientSecret.Length > 0, "ClientSecret is empty");
		}
	}
}