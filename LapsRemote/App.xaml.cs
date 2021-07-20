using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LapsRemote.Logging;
using LapsRemote.Utilities;
using System.Threading;
using System.IO;
using System.Windows.Media;
using System.Windows.Interop;

namespace LapsRemote
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Settings.Initialize();
			Logger.Initialize();
			Logger.Log("App Startup", Level.Debug, DateTime.Now);

  			if (Environment.OSVersion.Version.Major != 10)
			{
				Logger.Log("OS Not Windows 10", Level.Warning, DateTime.Now);
				Logger.MessageBoxLog("You Are Not Running Windows 10. UI Components might not work correctly", Level.Warning, DateTime.Now);
			}
		}
	}
}
