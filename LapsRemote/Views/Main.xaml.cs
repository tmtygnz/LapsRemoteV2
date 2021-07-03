using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using System.Windows.Shapes;
using System.Diagnostics;
using LapsRemote.Logging;
using LapsRemote.ViewsModel;

namespace LapsRemote.Screens
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {

		MainViewModel _viewModel;

		public Main()
        {
            InitializeComponent();
			_viewModel = new MainViewModel();
			DataContext = _viewModel;
		}

		private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Logger.Log("Closing App", Level.Debug, DateTime.Now);
			_viewModel._isUpdating = false;
			Logger.KillAll();
			App.Current.Shutdown();
		}
	}
}
