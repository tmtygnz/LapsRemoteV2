using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using LapsRemote.Logging;
using System.Windows.Input;
using LapsRemote.Utilities;
using LapsRemote.Vitals;
using System.Threading;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace LapsRemote.ViewsModel
{
	class MainViewModel : ViewModelBase
	{

		public MainViewModel()
		{
			Title = $"LAPS <{Environment.OSVersion}>";
			ValueComboBox = new ObservableCollection<string> {"Temperature", "02Stat", "BPM", "RespRate"};
			SelectedIndex = 0;
			DangerousLevel = 37.5;
			MonitorModel = new SeriesCollection
			{
				new LineSeries
				{
					Stroke = new SolidColorBrush(Color.FromRgb(116, 156, 117)),
					Fill = new SolidColorBrush(Color.FromArgb(50, 148, 179, 148)),
					LineSmoothness = 0.1,
					StrokeThickness = 3,
					Opacity = 1.0,
					AreaLimit = -5,
					Values = new ChartValues<ObservableValue>(),
					PointGeometry = DefaultGeometries.None
				}
			};

			_isUpdating = true;
			new Thread(new ThreadStart(UpdateVitals)).Start();
		}

		public bool _isUpdating;

		public ICommand OpenRepositoryWebsite_Command => new RelayCommand(param => OpenRepositoryWebsite_Action());
		public void OpenRepositoryWebsite_Action()
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					UseShellExecute = true,
					FileName = "https://github.com/jostimian/LapsRemoteV2"
				};
				Process.Start(startInfo);
			}

			catch (Exception exp)
			{
				Logger.MessageBoxLog($"Can't Open Website \n {exp.StackTrace}",
					Level.Error, DateTime.Now);
				Logger.Log(exp.StackTrace, Level.Error, DateTime.Now);
			}
		}

		public ICommand SubmitBug_Command => new RelayCommand(param => SubmitBug_Action());
		public void SubmitBug_Action()
		{
			try
			{
				ProcessStartInfo startInfo = new ProcessStartInfo()
				{
					UseShellExecute = true,
					FileName = "https://github.com/jostimian/LapsRemoteV2/issues"
				};
				Process.Start(startInfo);
			}

			catch (Exception exp)
			{
				Logger.MessageBoxLog($"Can't Open Issues Page \n {exp.StackTrace}",
					Level.Error, DateTime.Now);
				Logger.Log(exp.StackTrace, Level.Error, DateTime.Now);
			}
		}

		public ICommand SelectionChange_Command => new RelayCommand(param => SelectionChange_Action());
		public void SelectionChange_Action()
		{
			MonitorModel[0].Values.Clear();
		}

		private string _temperatureString;
		public string TemperatureString 
		{
			get => _temperatureString;
			set
			{
				if (value == _temperatureString)
					return;
				_temperatureString = value;
				OnPropertyChanged();
			}
		}

		private string _oxyStatString;
		public string OxyStatString
		{
			get => _oxyStatString;
			set
			{
				if (value == _oxyStatString)
					return;
				_oxyStatString = value;
				OnPropertyChanged();
			}
		}

		private string _bpmString;
		public string BPMString
		{
			get => _bpmString;
			set
			{
				if (value == _bpmString)
					return;
				_bpmString = value;
				OnPropertyChanged();
			}
		}

		private string _respRateString;
		public string RespRateString
		{
			get => _respRateString;
			set
			{
				if (value == _respRateString)
					return;
				_respRateString = value;
				OnPropertyChanged();
			}
		}

		private string _title;
		public string Title
		{
			get => _title;

			set 
			{
				if (value == _title)
					return;
				_title = value;
				OnPropertyChanged();
			}
		}

		private SeriesCollection _monitorModel;
		public SeriesCollection MonitorModel
		{
			get => _monitorModel;
			set
			{
				if (value == _monitorModel)
					return;
				_monitorModel = value;
				OnPropertyChanged();
			}
		}

		private double _maxY;
		public double MaxY
		{
			get => _maxY;
			set
			{
				if (value == _maxY)
					return;
				_maxY = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<string> _valueComboBox;
		public ObservableCollection<string> ValueComboBox
		{
			get => _valueComboBox;
			set
			{
				if (value == _valueComboBox)
					return;
				_valueComboBox = value;
				OnPropertyChanged();
			}
		}

		private double _dangerousLevel;
		public double DangerousLevel
		{
			get => _dangerousLevel;
			set
			{
				if (value == _dangerousLevel)
					return;
				_dangerousLevel = value;
				OnPropertyChanged();
			}
		}

		private int _selectedValue;
		public int SelectedIndex
		{
			get => _selectedValue;
			set
			{
				if (value == _selectedValue)
					return;
				_selectedValue = value;
				OnPropertyChanged();
			}
		}

		public void UpdateVitals()
		{
			lock (this)
			{
				while (_isUpdating)
				{
					Thread.Sleep(400);

					double TemperatureValue = Temperature.RandomTemperature();
					double OxyStatValue = OxyStat.RandomOxyStat();
					double BPMValue = BPM.RandomBPM();
					double RespRateValue = RespRate.RandomRespRate();

					double ValueToShow = 0;

					if (SelectedIndex == 0)
						ValueToShow = TemperatureValue;

					if (SelectedIndex == 1)
						ValueToShow = OxyStatValue;

					if (SelectedIndex == 2)
						ValueToShow = BPMValue;

					if (SelectedIndex == 3)
						ValueToShow = RespRateValue;

					TemperatureString = TemperatureValue.ToString();
					OxyStatString = OxyStatValue.ToString();
					BPMString = BPMValue.ToString();
					RespRateString = RespRateValue.ToString();


					if (double.Parse(BPMString) < 100 )
						MaxY = 100;
					else
						MaxY = double.NaN;

					MonitorModel[0].Values.Add(new ObservableValue(ValueToShow));

					if (MonitorModel[0].Values.Count > 19)
						MonitorModel[0].Values.RemoveAt(0);
				}
			}
		}
	}
}
