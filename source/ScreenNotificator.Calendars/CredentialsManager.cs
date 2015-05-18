using System.IO;
using System.Xml;

namespace ScreenNotificator.Calendars
{
	public class CredentialsManager
	{
		private const string DefaultProviderDetailsFileName = "ProviderDetails_Default.xml";

		public Credentials LoadCredentials(string providerName)
		{
			var fileName = DefaultProviderDetailsFileName;
			if (File.Exists("ProviderDetails.xml"))
			{
				fileName = "ProviderDetails.xml";
			}

			var providerDetailsXml = new XmlDocument();
			providerDetailsXml.Load(fileName);

			var credentials = new Credentials("", "");
			return credentials;
		}
	}
}