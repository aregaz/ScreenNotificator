using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScreenNotificator.Calendars.Tests
{
	[TestClass]
	public class CredentialsManagerTests
	{
		[TestMethod]
		public void LoadCredentials_Google_NotEmpty()
		{
			var credentialsManager = new CredentialsManager();

			var credentials = credentialsManager.LoadCredentials("Google");

			Assert.IsNotNull(credentials);
			Assert.IsNotNull(credentials.ClientID);
			Assert.IsNotNull(credentials.ClientSecret);
		}
	}
}