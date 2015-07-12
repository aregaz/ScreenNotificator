using System;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

using ScreenNotificator.Common;
using ScreenNotificator.Calendars.Providers;
using ScreenNotificator.Common.Models;
using ScreenNotificator.Common.Models.Calendar;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ScreenNotificator.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private readonly OpenFileDialog openFileDialog;
		private readonly ScreenResolution screenResolution;
		private readonly GoogleCalendarProvider googleCalendarProvider;

		public MainWindow()
		{
			InitializeComponent();

			googleCalendarProvider = new GoogleCalendarProvider();

			CheckGoogleCalendarProviderConnection();

			openFileDialog = new OpenFileDialog();
			openFileDialog.FileOk += NewImageSelected;

			screenResolution = new ScreenResolution(
				SystemParameters.PrimaryScreenWidth,
				SystemParameters.PrimaryScreenHeight,
				120);
		}

		private void SelectImageButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			openFileDialog.ShowDialog();
			
		}

		private void NewImageSelected(object sender, CancelEventArgs cancelEventArgs)
		{
			var filePath = openFileDialog.FileName;

			if (string.IsNullOrWhiteSpace(filePath)) return;

			try
			{
				//var lockScreenManager = new LockScreenManager();
				//lockScreenManager.ChangeLockScreenImage(filePath);

				var now = DateTime.Now;
				var schedule = LoadScheduleFromGoogleCalendar();
				//schedule.AddEvent(new Event()
				//{
				//	Start = now,
				//	Title = "Test 1",
				//	Description = "Test description one"
				//});
				//schedule.AddEvent(new Event()
				//{
				//	Start = now.AddHours(2),
				//	Title = "Test 2",
				//	Description = "This is long description. Lorem ipsum and further text. Bla-bla-bla. More text.",
				//	Location = "Home"
				//});
				//schedule.AddEvent(new Event()
				//{
				//	Start = now.AddDays(1),
				//	Title = "Test 3",
				//	Description = null,
				//	Location = "Office"
				//});



				var manager = new ScreenNotificatorManager(this.screenResolution);
				manager.UpdateLockScreen(filePath, schedule);
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					exception.Message,
					"Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}

			//var image = ImageManager.LoadImage(filePath, this.screenResolution);
			//image.DrawNotificationArea();

			//var imageFolder = string.Format("{0}\\Images", FileManager.GetAssemblyFolder());
			//image.SaveImage(imageFolder, "ScreenNotificatior.jpg");
		}
		
		private Schedule LoadScheduleFromGoogleCalendar()
		{
			var schedule = new Schedule();

			var calendars = googleCalendarProvider.GetCalendarList();
			var events = googleCalendarProvider.GetEventsFromCalendars(
				new[] { calendars.First().ID },
				DateTime.Now.AddDays(-1),
				DateTime.Now);

			foreach (var calendarEvent in events)
			{
				schedule.AddEvent(new Event()
				{
					Title = calendarEvent.Name,
					Start = calendarEvent.Start,
					End = calendarEvent.End,
					Description = calendarEvent.Description
				});
			}

			return schedule;
		}

		private void CheckGoogleCalendarProviderConnection()
		{
			var isConnected = googleCalendarProvider.IsAuthorized();
			var imageSoource = new Uri(
				string.Format(@"/ScreenNotificator.App;component/Resources/{0}.png", isConnected ? "Connected" : "Disconnected"),
				UriKind.Relative);			

			this.googleConnectionStatusImage.Source = new BitmapImage(imageSoource);

        }
	}
}
