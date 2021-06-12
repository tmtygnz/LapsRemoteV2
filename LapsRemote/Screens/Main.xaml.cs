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
using MahApps.Metro.Controls;
using System.Windows.Shapes;
using System.Diagnostics;
using LapsRemote.Logging;

namespace LapsRemote.Screens
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
		double[] dataX;
		double[] dataY; 
		public Main()
        {
            InitializeComponent();
			dataX = new double[] { 1, 2, 3, 4, 5 };
			dataY = new double[] { 1, 4, 9, 16, 25 };
			
		}

		private void OpenRepo(object sender, RoutedEventArgs e)
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo
				{
					UseShellExecute = true,
					FileName = "https://github.com/jostimian/LapsRemoteV2"
				};
				Process.Start(psi);
			}
			catch (Exception exp)
			{
				Logger.MessageBoxLog("Cant open website", Level.Error, DateTime.Now);
				Logger.Log(exp.StackTrace, Level.Error, DateTime.Now);
			}
		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void MetroWindow_Closed(object sender, EventArgs e)
		{
			Logger.KillAll();
		}
	}
}
