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
using LapsRemote.Views;
using System.Threading;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;
using LapsRemote.Models;
using Prism;
using Prism.Commands;

namespace LapsRemote.ViewsModel
{
	class MainViewModel : ViewModelBase
	{

		public MainViewModel()
		{
			Title = $"LAPS <{Environment.OSVersion}>";
			SelectedIndex = 0;
			_isRecording = false;
			_isUpdating = true;
			RecordingStatus = "Status: Not Recording";

			
			TemperatureRecordedList = new List<double>();
			OxyStatRecordedList = new List<double>();
			BPMRecordedList = new List<double>();
			RespRateRecordedList = new List<double>();

			gradientBrush = new LinearGradientBrush { StartPoint = new Point(0, 0), EndPoint = new Point(0, 1) };
			gradientBrush.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString(Settings.settingsModel.SelectedFillColor),0));
			gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

			ValueComboBox = new ObservableCollection<string> { "Temperature", "02Sat", "BPM", "RespRate" };
			MonitorModel = new SeriesCollection
			{
				new LineSeries
				{
					Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.settingsModel.SelectedStrokeColor)),
					Fill = gradientBrush,
					LineSmoothness = 1,
					StrokeThickness = 3,
					Opacity = 1.0,
					AreaLimit = -5,
					Values = new ChartValues<ObservableValue>(),
					PointGeometry = DefaultGeometries.None
				}
			};

			new Thread(new ThreadStart(UpdateVitals)).Start();
		}

		public bool _isUpdating;
		private bool _isRecording;
		private GradientBrush gradientBrush;

		public ICommand OpenRepositoryWebsite_Command => new DelegateCommand(OpenRepositoryWebsite_Action);
		public void OpenRepositoryWebsite_Action()
		{
			try
			{
				Logger.Log("Opening Repo Page", Level.Debug, DateTime.Now);
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

		public ICommand SubmitBug_Command => new DelegateCommand(SubmitBug_Action);
		public void SubmitBug_Action()
		{
			try
			{
				Logger.Log("Opening Issue Page", Level.Debug, DateTime.Now);
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

		public ICommand SelectionChange_Command => new DelegateCommand(SelectionChange_Action);
		public void SelectionChange_Action()
		{
			Logger.Log("Selection Change", Level.Debug, DateTime.Now);
			MonitorModel[0].Values.Clear();
		}

		public ICommand StartRecording_Command => new DelegateCommand(StartRecording_Action);
		public void StartRecording_Action()
		{
			Logger.Log("Recording Started", Level.Debug, DateTime.Now);

			_isRecording = true;
			RecordingStatus = "Status: Recording";
		}

		public ICommand StopRecording_Command => new DelegateCommand(StopRecording_Action);
		public void StopRecording_Action()
		{
			Logger.Log("Recording Stoped", Level.Debug, DateTime.Now);
			_isRecording = false;
			RecordingStatus = "Status: Not Recording";
			VitalsRecodModel ModelToSave = new VitalsRecodModel
			{
				Temperature = TemperatureRecordedList,
				OxyStat = OxyStatRecordedList,
				BPM = BPMRecordedList,
				RespRate = RespRateRecordedList
			};

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
			if (saveFileDialog.ShowDialog() == true)
			{
				string ToSave = JsonConvert.SerializeObject(ModelToSave, Formatting.Indented);
				File.WriteAllTextAsync(saveFileDialog.FileName, ToSave);
			}
		}

		public ICommand CloseApplication_Command => new DelegateCommand(CloseApplication_Action);
		public void CloseApplication_Action()
		{
			MonitorModel[0].Values.Clear();
			_isUpdating = false;
			Logger.Log("App Closed", Level.Debug, DateTime.Now);
			Logger.KillAll();
			Environment.Exit(0);
		}

		public ICommand OpenReader_Command => new DelegateCommand(OpenReader_Action);
		public void OpenReader_Action()
		{
			Logger.Log("Opening Reader", Level.Debug, DateTime.Now);
			Reader reader = new Reader();
			reader.Show();
		}

		public ICommand OpenPreferneces_Command => new DelegateCommand(OpenPreferences_Action);
		public void OpenPreferences_Action()
		{
			Logger.Log("Opening Settings", Level.Debug, DateTime.Now);
			SettingsEditor settings = new SettingsEditor();
			settings.Show();
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

		private int _selectedIndex;
		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				Logger.Log($"Selected index changed to {value}",
					Level.Debug, DateTime.Now);
				if (value == _selectedIndex)
					return;
				_selectedIndex = value;
				OnPropertyChanged();
			}
		}

		private List<double> _temperatureRecordedList;
		public List<double> TemperatureRecordedList
		{
			get => _temperatureRecordedList;
			set
			{
				if (value == _temperatureRecordedList)
					return;
				_temperatureRecordedList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _oxyStatRecordedList;
		public List<double> OxyStatRecordedList
		{
			get => _oxyStatRecordedList;
			set
			{
				if (value == _oxyStatRecordedList)
					return;
				_oxyStatRecordedList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _bpmRecordedList;
		public List<double> BPMRecordedList
		{
			get => _bpmRecordedList;
			set
			{
				if (value == _bpmRecordedList)
					return;
				_bpmRecordedList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _respRateRecordedList;
		public List<double> RespRateRecordedList
		{
			get => _respRateRecordedList;
			set
			{
				if (value == _respRateRecordedList)
					return;
				_respRateRecordedList = value;
				OnPropertyChanged();
			}
		}

		private string _recordingStatus;
		public string RecordingStatus
		{
			get => _recordingStatus;
			set
			{
				if (value == _recordingStatus)
					return;
				_recordingStatus = value;
				OnPropertyChanged();
			}
		}

		public void UpdateVitals()
		{
			Logger.Log("Update Vital Thread Started", Level.Debug, DateTime.Now);
			lock (this)
			{
				while (_isUpdating)
				{
					Thread.Sleep(Settings.settingsModel.PollingRate);
					int a = 0;
					double TemperatureValue = Temperature.RandomTemperature();
					double OxyStatValue = OxyStat.RandomOxyStat();
					double BPMValue = BPM.RandomBPM();
					double RespRateValue = RespRate.RandomRespRate();

					double ValueToShow = 0;

					#region Recording
					if (_isRecording)
					{
						TemperatureRecordedList.Add(TemperatureValue);
						OxyStatRecordedList.Add(OxyStatValue);
						BPMRecordedList.Add(BPMValue);
						RespRateRecordedList.Add(RespRateValue);
					}
					#endregion Recording

					#region CheckSelectedValueToLoad
					if (SelectedIndex == 0)
						ValueToShow = TemperatureValue;

					if (SelectedIndex == 1)
						ValueToShow = OxyStatValue;

					if (SelectedIndex == 2)
						ValueToShow = BPMValue;

					if (SelectedIndex == 3)
						ValueToShow = RespRateValue;
					#endregion CheckSelectedValueToLoad

					#region LoadValueToTheScreen
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
					#endregion LoadValueToTheScreen
				}
			}
		}
	}
}
