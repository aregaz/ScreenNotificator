using System;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

using ScreenNotificator.Common;
using ScreenNotificator.Common.Models;

namespace ScreenNotificator.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private readonly OpenFileDialog openFileDialog;
		private readonly ScreenResolution screenResolution;

		public MainWindow()
		{
			InitializeComponent();

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

			if (!string.IsNullOrWhiteSpace(filePath))
			{
				try
				{
					LockScreenManager.ChangeLockScreenImage(filePath);
				}
				catch (Exception exception)
				{
					MessageBox.Show(
						exception.Message,
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
				}

				

				var image = ImageManager.LoadImage(filePath, this.screenResolution);
				image.DrawNotificationArea();

				var imageFolder = string.Format("{0}\\Images", FileManager.GetAssemblyFolder());
				image.SaveImage(imageFolder, "ScreenNotificatior.jpg");
			}
		}
	}
}
