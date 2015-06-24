using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ScreenNotificator.Calendars.Providers
{
	public class GoogleCalendarProvider : ICalendarProvider
	{
		static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly };
		private const string ApplicationName = "ScreenNotificator";


		public IEnumerable<string> GetCalendarList()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetEventsFromCalendar(IEnumerable<string> calendarIDs, DateTime startDate, DateTime? endDate)
		{
			throw new NotImplementedException();
		}

		public void Do()
		{
			UserCredential credential;

			using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
			{
				var credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				credPath = Path.Combine(credPath, ".credentials");

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;

				Debug.WriteLine("Credential file saved to: " + credPath);
			}

			// Create Calendar Service.
			var service = new CalendarService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			// Define parameters of request.
			var request = service.Events.List("primary");
			request.TimeMin = DateTime.Now;
			request.ShowDeleted = false;
			request.SingleEvents = true;
			request.MaxResults = 10;
			request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

			Debug.WriteLine("Upcoming events:");

			var events = request.Execute();
			if (events.Items.Count > 0)
			{
				foreach (var eventItem in events.Items)
				{
					var when = eventItem.Start.DateTime.ToString();
					if (string.IsNullOrEmpty(when))
					{
						when = eventItem.Start.Date;
					}

					Debug.WriteLine("{0} ({1})", eventItem.Summary, when);
				}
			}
			else
			{
				Debug.WriteLine("No upcoming events found.");
			}
		}

	}
}