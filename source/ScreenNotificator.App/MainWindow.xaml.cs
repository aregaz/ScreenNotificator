using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

using ScreenNotificator.Common;

namespace ScreenNotificator.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly OpenFileDialog openFileDialog;

		public MainWindow()
		{
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
			openFileDialog.FileOk += NewImageSelected;
		}

		private void SelectImageButton_Click(object sender, RoutedEventArgs e)
		{
			openFileDialog.ShowDialog();
			
		}

		private void NewImageSelected(object sender, CancelEventArgs cancelEventArgs)
		{
			var filePath = openFileDialog.FileName;

			if (!string.IsNullOrWhiteSpace(filePath))
			{
				LockScreenManager.ChangeLockScreenImage(filePath);

				var image = ImageManager.LoadImage(filePath);
				image.DrawNotificationArea();

				var imageFolder = string.Format("{0}\\Images", FileManager.GetAssemblyFolder());
				image.SaveImage(imageFolder, "testImage.jpg");
			}
		}
	}
}
