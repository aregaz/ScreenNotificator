using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

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
			}
		}
	}
}
