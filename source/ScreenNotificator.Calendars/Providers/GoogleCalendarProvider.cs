using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using ScreenNotificator.Calendars.Models;
using ScreenNotificator.Common.Helpers;
using GoogleModels = Google.Apis.Calendar.v3.Data;
using ScreenNotificatiorModels = ScreenNotificator.Calendars.Models;

namespace ScreenNotificator.Calendars.Providers
{
	public class GoogleCalendarProvider : ICalendarProvider
	{
		static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly };
		private const string ApplicationName = "ScreenNotificator";

		private static CalendarService GoogleCalendatService { get; set; }


		public GoogleCalendarProvider()
		{
			this.Initialize();
		}


		public bool IsAuthorized()
		{
			return true;
		}


		public IEnumerable<Calendar> GetCalendarList()
		{
			var request = GoogleCalendarProvider.GoogleCalendatService.CalendarList.List();
			var calendars = request.Execute();
			if (!calendars.Items.Any()) return new List<Calendar>();

			var result = new List<Calendar>();
			foreach (var calendar in calendars.Items)
			{
				result.Add(new Calendar()
				{
					ID = calendar.Id,
					Name = calendar.Summary,
					Description = calendar.Description
				});
			}

			return result;
		}


		public IEnumerable<ScreenNotificatiorModels.Event> GetEventsFromCalendars(IEnumerable<string> calendarIDs, DateTime startDate, DateTime? endDate)
		{
			var events = new List<GoogleModels.Event>();

			foreach (var calendarID in calendarIDs)
			{
				var request = GoogleCalendarProvider.GoogleCalendatService.Events.List(calendarID);
				request.TimeMin = startDate;
				request.TimeMax = endDate;
				request.ShowDeleted = false;

				var calendarEvents = request.Execute();
				if (calendarEvents.Items.Count > 0)
				{
					events.AddRange(calendarEvents.Items);
				}
            }

			return events
				.Select(e => new ScreenNotificatiorModels.Event()
				{
					ID = e.Id,
					Name = e.Summary,
					Description = e.Description,
					Start = GetDateTime(e.Start),
					End = GetDateTime(e.End)
				})
				.ToList();
		}


		public void Do()
		{
			// Define parameters of request.
			var request = GoogleCalendarProvider.GoogleCalendatService.Events.List("primary");
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


		private void Initialize()
		{
			UserCredential credential;

			using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
			{
				var credPath = FilePathHelper.GetSpecialFolder();

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

			GoogleCalendarProvider.GoogleCalendatService = service;
		}


		private DateTime GetDateTime(GoogleModels.EventDateTime googleDate)
		{
			if (googleDate.DateTime.HasValue)
			{
				return googleDate.DateTime.Value;
			}

			if (!string.IsNullOrWhiteSpace(googleDate.Date))
			{
				return DateTime.Parse(googleDate.Date);
			}

			DateTime result;
			if (DateTime.TryParse(googleDate.DateTimeRaw, out result))
			{
				return result;
			}
			else
			{
				throw new InvalidCastException();
			}
		}
	}
}