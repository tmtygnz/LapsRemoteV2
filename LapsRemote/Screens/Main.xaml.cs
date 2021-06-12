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
using LapsRemote.Vitals;

namespace LapsRemote.Screens
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
		public Main()
        {
            InitializeComponent();
		}

		private void MetroWindow_Activated(object sender, EventArgs e)
		{
			new Thread(() => UpdateVitalView()).Start();
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

		private void UpdateVitalView()
		{
			while (true)
			{
				Thread.Sleep(100);
				this.Dispatcher.Invoke(() =>
				{
					TemperatureTextView.Text = Temperature.RandomTemperature().ToString();
					OxyStatTextIvew.Text = OxyStat.RandomOxyStat().ToString();
					RespRateTextView.Text = RespRate.RandomRespRate().ToString();
					BPMTextView.Text = BPM.RandomBPM().ToString();
				});
			}
		}
	}
}
