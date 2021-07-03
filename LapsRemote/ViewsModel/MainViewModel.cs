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

namespace LapsRemote.ViewsModel
{
	class MainViewModel : ViewModelBase
	{

		public MainViewModel()
		{
			Title = $"LAPS {Environment.OSVersion}";
			MonitorModel = new SeriesCollection
			{
				new LineSeries
				{
					Stroke = new SolidColorBrush(Color.FromRgb(204, 255, 189)),
					Fill = new SolidColorBrush(Color.FromArgb (0,0,0,0)),
					LineSmoothness = 0.1,
					StrokeThickness = 10,
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

		public void UpdateVitals()
		{
			lock (this)
			{
				while (_isUpdating)
				{
					Thread.Sleep(400);
					TemperatureString = Temperature.RandomTemperature().ToString();
					OxyStatString = OxyStat.RandomOxyStat().ToString();
					BPMString = BPM.RandomBPM().ToString();
					RespRateString = RespRate.RandomRespRate().ToString();

					if (double.Parse(BPMString) < 100 )
						MaxY = 100;
					else
						MaxY = double.NaN;

					MonitorModel[0].Values.Add(new ObservableValue(double.Parse(BPMString)));

					if (MonitorModel[0].Values.Count > 19)
						MonitorModel[0].Values.RemoveAt(0);
				}
			}
		}
	}
}
