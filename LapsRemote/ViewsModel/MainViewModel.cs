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
using OxyPlot;
using OxyPlot.Series;

namespace LapsRemote.ViewsModel
{
	class MainViewModel : ViewModelBase
	{

		public MainViewModel()
		{
			Title = $"LAPS {Environment.OSVersion}";
			Model = new PlotModel { Title = "Hello World" };
			_isUpdating = true;
			new Thread(new ThreadStart(UpdateVitals)).Start();
		}

		public bool _isUpdating { get; set; }

		private PlotModel model;
		public PlotModel Model
		{
			get => model;

			set
			{
				if (value == model)
					return;
				model = value;
				OnPropertyChanged();
			}
		}

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

		public void UpdateVitals()
		{
			lock (this)
			{
				while (_isUpdating)
				{
					Thread.Sleep(1000);
					TemperatureString = Temperature.RandomTemperature().ToString();
					OxyStatString = OxyStat.RandomOxyStat().ToString();
					BPMString = BPM.RandomBPM().ToString();
					RespRateString = RespRate.RandomRespRate().ToString();
					Model.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
				}
			}
		}
	}
}
