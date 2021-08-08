using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LapsRemote.Logging;
using Prism;
using Prism.Commands;

namespace LapsRemote.ViewsModel
{
	class PopupWebviewViewModel : ViewModelBase
	{
		public PopupWebviewViewModel(string _websiteURI)
		{
			WebsiteURI = _websiteURI;
			Logger.Log($"Opening {WebsiteURI}", LogFrom.PopupWebviewViewModelcs, Level.Debug, DateTime.Now);
		}

		public ICommand OpenInBrowser_Command => new DelegateCommand(OpenInBrowser_Action);
		public void OpenInBrowser_Action()
		{
			try
			{
				Logger.Log("Opening Repo Page", LogFrom.PopupWebviewViewModelcs, Level.Debug, DateTime.Now);
				ProcessStartInfo startInfo = new()
				{
					UseShellExecute = true,
					FileName = WebsiteURI
				};
				Process.Start(startInfo);
			}

			catch (Exception exp)
			{
				Logger.MessageBoxLog($"Can't Open Website \n {exp.StackTrace}",
					LogFrom.PopupWebviewViewModelcs, Level.Error, DateTime.Now);
				Logger.Log(exp.StackTrace, LogFrom.MainViewModelcs, Level.Error, DateTime.Now);
			}
		}

		private string _websiteURI;
		public string WebsiteURI
		{
			get => _websiteURI;
			set
			{
				if (value == _websiteURI) { return; }
				_websiteURI = value;
				OnPropertyChanged();
			}
		}
	}
}
