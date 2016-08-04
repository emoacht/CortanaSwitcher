using System;
using System.Collections.Generic;
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

namespace CortanaSwitcher
{
	public partial class MainWindow : Window
	{
		private readonly MainWindowViewModel _viewModel;

		public MainWindow()
		{
			InitializeComponent();

			_viewModel = new MainWindowViewModel();
			this.DataContext = _viewModel;
		}

		private void EnableButton_Click(object sender, RoutedEventArgs e)
		{
			_viewModel.EnableCortana();
		}

		private void DisableButton_Click(object sender, RoutedEventArgs e)
		{
			_viewModel.DisableCortana();
		}
	}
}