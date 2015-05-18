namespace ScreenNotificator.Calendars
{
	public class Credentials
	{
		public string ProviderName { get; private set; }
		public string ClientSecret { get; private set; }
		public string ClientID { get; private set; }

		public Credentials(string providerName, string clientID, string clientSecret)
		{
			this.ProviderName = providerName;
			this.ClientID = clientID;
			this.ClientSecret = clientSecret;
		}
	}
}