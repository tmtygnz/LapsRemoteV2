using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LapsRemote.Utilities;


namespace LapsRemote.ViewsModel
{
	class SettingsViewModel : ViewModelBase
	{

		public SettingsViewModel()
		{
			PollingRate = Settings.settingsModel.PollingRate;
			ScrollerThumbSize = Settings.settingsModel.ScrollerThumbSize;
			SelectedStrokeColor = Settings.settingsModel.SelectedStrokeColor;
			SelectedFillColor = Settings.settingsModel.SelectedFillColor;
			ApplicationLogPath = Settings.settingsModel.AppLicationLogPath;
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

		public ICommand Save_Command => new RelayCommand(param => Save_Action());
		public void Save_Action()
		{
			Settings.Save();
			MessageBox.Show("Some changes that you made will only take effect after you restart the application.", "Warning", 
				MessageBoxButton.OK,MessageBoxImage.Warning);
		}
	}
}

