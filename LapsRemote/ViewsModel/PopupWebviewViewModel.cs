using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
			Title = $"{WebsiteURI}";
			Logger.Log($"Opening {WebsiteURI}", LogFrom.PopupWebviewViewModelcs, Level.Debug, DateTime.Now);
			IsLoading = true;
			IsVisible = Visibility.Collapsed;
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

		public ICommand ContentLoading_Command => new DelegateCommand(ContentLoading_Action);
		public void ContentLoading_Action()
		{
			Title = $"{WebsiteURI} - Loading";
			IsLoading = true;
			IsVisible = Visibility.Visible;
		}

		public ICommand ContentLoaded_Command => new DelegateCommand(ContentLoaded_Action);
		public void ContentLoaded_Action()
		{
			Title = $"{WebsiteURI} - Loaded";
			IsLoading = false;
			IsVisible = Visibility.Collapsed;
		}

		private Visibility _isVisible;
		public Visibility IsVisible
		{
			get => _isVisible;
			set
			{
				if (value == _isVisible) { return; }
				_isVisible = value;
				OnPropertyChanged();
			}
		}

		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set
			{
				if (value == _isLoading) { return; }
				_isLoading = value;
				OnPropertyChanged();
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				if (value == _title) { return; }
				_title = value;
				OnPropertyChanged();
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
