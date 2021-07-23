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
using ControlzEx.Theming;

namespace LapsRemote
{
	/// <summary> 
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App() : base()
		{
			this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ThemeManager.Current.ChangeTheme(this, "Light.Orange");
			Settings.Initialize();
			Logger.Initialize();
			Logger.Log("App Startup", Level.Debug, DateTime.Now);

			if (Environment.OSVersion.Version.Major != 10)
			{
				Logger.Log("OS Not Windows 10", Level.Warning, DateTime.Now);
				Logger.MessageBoxLog("You Are Not Running Windows 10. UI Components might not work correctly", Level.Warning, DateTime.Now);
			}
		}

		//(https://stackoverflow.com/questions/49497090/wpf-where-i-can-catch-application-crash-event)
		void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Logger.Log(e.Exception.Message, Level.Fatal, DateTime.Now);

			MessageBox.Show("Unhandled exception occurred: \n" + e.Exception.Message, "Fatal", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}