namespace ScreenNotificator.Calendars
{
	public class Credentials
	{
		public string ClientSecret { get; private set; }
		public string ClientID { get; private set; }

		public Credentials(string clientID, string clientSecret)
		{
			this.ClientID = clientID;
			this.ClientSecret = clientSecret;
		}
	}
}