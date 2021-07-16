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

			ValueComboBox = new ObservableCollection<string> { "Temperature", "02Stat", "BPM", "RespRate" };
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

			new Thread(new ThreadStart(UpdateVitals)).Start();
		}

		public bool _isUpdating;
		private bool _isRecording;

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

		public ICommand StartRecording_Command => new RelayCommand(param => StartRecording_Action());
		public void StartRecording_Action()
		{
			_isRecording = true;
			RecordingStatus = "Status: Recording";
		}

		public ICommand StopRecording_Command => new RelayCommand(param => StopRecording_Action());
		public void StopRecording_Action()
		{
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
			if (saveFileDialog.ShowDialog() == true)
			{
				string ToSave = JsonConvert.SerializeObject(ModelToSave, Formatting.Indented);
				File.WriteAllTextAsync(saveFileDialog.FileName, ToSave);
			}
		}

		public ICommand CloseApplication_Command => new RelayCommand(param => CloseApplication_Action());
		public void CloseApplication_Action()
		{
			MonitorModel[0].Values.Clear();
			_isUpdating = false;
			Logger.Log("App Closed", Level.Debug, DateTime.Now);
			Logger.KillAll();
			Environment.Exit(0);
		}

		public ICommand OpenReader_Command => new RelayCommand(param => OpenReader_Action());
		public void OpenReader_Action()
		{
			Reader reader = new Reader();
			reader.Show();
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
			lock (this)
			{
				while (_isUpdating)
				{
					Thread.Sleep(500);
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
