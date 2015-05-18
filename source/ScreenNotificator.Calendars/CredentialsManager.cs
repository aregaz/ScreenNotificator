using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

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

			var xml = XElement.Load(fileName);

			var provider = xml
				.Elements("provider")
				.SingleOrDefault(p => p.Attribute("name").Value == providerName);

			if (provider == null)
			{
				throw new ArgumentException(string.Format("OAuth credentials for provider {0} not found.", providerName), providerName);
			}
			
			var credentials = new Credentials(
				providerName,
				provider.Descendants("clientID").SingleOrDefault().Value,
				provider.Descendants("secret").SingleOrDefault().Value);
			return credentials;
		}
	}
}