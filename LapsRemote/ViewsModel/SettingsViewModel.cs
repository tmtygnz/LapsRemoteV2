using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LapsRemote.Logging;
using LapsRemote.Utilities;
using MahApps.Metro.Controls.Dialogs;
using Prism;
using Prism.Commands;

namespace LapsRemote.ViewsModel
{
	class SettingsViewModel : ViewModelBase
	{
		public SettingsViewModel(IDialogCoordinator _dialogCoordinatorInstance)
		{
			PollingRate = Settings.settingsModel.PollingRate;
			ScrollerThumbSize = Settings.settingsModel.ScrollerThumbSize;
			SelectedStrokeColor = Settings.settingsModel.SelectedStrokeColor;
			SelectedFillColor = Settings.settingsModel.SelectedFillColor;
			ApplicationLogPath = Settings.settingsModel.AppLicationLogPath;
			DisableAnimationReader = Settings.settingsModel.DisableAnimationReader;
			_dialogCoordinator = _dialogCoordinatorInstance;
		}

		private IDialogCoordinator _dialogCoordinator;

		public ICommand Save_Command => new DelegateCommand<Window>(param => Save_Action(param));
		public async void Save_Action(Window window)
		{
			Settings.Save();
			Logger.Log("Settings Saved", LogFrom.SettingsViewModelcs, Level.Debug, DateTime.Now);
			await _dialogCoordinator.ShowMessageAsync(this, "Warning!", 
				"Some changes that you made will only take effect after you restart the application.");
			window.Close();
		}

		public ICommand Cancel_Command => new DelegateCommand<Window>(param => Cancel_ActionAsync(param));
		public async Task Cancel_ActionAsync(Window window) {
			Logger.Log("Settings Quit", LogFrom.SettingsViewModelcs, Level.Debug, DateTime.Now);
			MessageDialogResult _dialog = await _dialogCoordinator.ShowMessageAsync(this, "Warning!",
				"You might have some unsaved changes. Clicking okay will close " +
				"the settings page and void all of the changes.", MessageDialogStyle.AffirmativeAndNegative);

			if (_dialog != MessageDialogResult.Affirmative) { return; }
			window.Close();
		}

		private string _selectedFillColor;
		public string SelectedFillColor
		{
			get => _selectedFillColor;
			set
			{
				if (value == _selectedFillColor)
					return;
				Settings.settingsModel.SelectedFillColor = value;
				_selectedFillColor = value;
				OnPropertyChanged();
			}
		}

		private string _selectedStrokeColor;
		public string SelectedStrokeColor
		{
			get => _selectedStrokeColor;
			set
			{
				if (value == _selectedStrokeColor)
					return;
				Settings.settingsModel.SelectedStrokeColor = value;
				_selectedStrokeColor = value;
				OnPropertyChanged();
			}
		}


		private int _pollingRate;
		public int PollingRate
		{
			get => _pollingRate;
			set
			{
				if (value == _pollingRate)
					return;
				Settings.settingsModel.PollingRate = value;
				_pollingRate = value;
				OnPropertyChanged();
			}
		}

		private int _scrollerThumbSize;
		public int ScrollerThumbSize
		{
			get => _scrollerThumbSize;
			set
			{
				if (value == _scrollerThumbSize)
					return;
				Settings.settingsModel.ScrollerThumbSize = value;
				_scrollerThumbSize = value;
				OnPropertyChanged();
			}
		}

		private string _applicationLogPath;
		public string ApplicationLogPath
		{
			get => _applicationLogPath;
			set
			{
				if (value == _applicationLogPath)
					return;
				Settings.settingsModel.AppLicationLogPath = value;
				_applicationLogPath = value;
				OnPropertyChanged();
			}
		}

		private bool _disableAnimationReader;
		public bool DisableAnimationReader
		{
			get => _disableAnimationReader;
			set
			{
				if (value == _disableAnimationReader) 
					return;
				Settings.settingsModel.DisableAnimationReader = value;
				_disableAnimationReader = value;
				OnPropertyChanged();
			}
		}
	}
}
