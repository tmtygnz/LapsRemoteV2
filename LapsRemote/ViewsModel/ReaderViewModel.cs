using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LapsRemote.Utilities;
using LapsRemote.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using LiveCharts.Defaults;
using System.Windows;
using LapsRemote.Logging;
using Prism;
using Prism.Commands;

namespace LapsRemote.ViewsModel
{
	class ReaderViewModel : ViewModelBase
	{
		public ReaderViewModel()
		{
			TemperatureList = new List<double>();
			OxyStatList = new List<double>();
			BPMList = new List<double>();
			RespRateList = new List<double>();
			ValueComboBox = new ObservableCollection<string> { "Temperature", "O2Sat", "BPM", "Resperation Rate" };
			SelectedIndex = 0;
			DisableAnimation = false;
			To = Settings.settingsModel.ScrollerThumbSize;

			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

			if (openFileDialog.ShowDialog() == true)
			{
				StreamReader streamReader = new StreamReader(openFileDialog.FileName);
				VitalsRecodModel recordModel = JsonConvert.DeserializeObject<VitalsRecodModel>(streamReader.ReadToEnd());

				TemperatureList = recordModel.Temperature;
				OxyStatList = recordModel.OxyStat;
				BPMList = recordModel.BPM;
				RespRateList = recordModel.RespRate;
			}

			if(RenderCapability.Tier >> 16 < 3)
			{
				Logger.Log("Hardware Acceleration Not Supported", Level.Warning, DateTime.Now);
				Logger.MessageBoxLog($"Hardware Acceleration is not supported or not fully supported in this device [Rendering Tier: {RenderCapability.Tier >> 16}] and could " +
					"cause lag especially with big data. Disabling animations could help.", Level.Fatal, DateTime.Now);
			}

			ReaderLineSeries = new SeriesCollection
			{
				new LineSeries
				{
					Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.settingsModel.SelectedStrokeColor)),
					Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Settings.settingsModel.SelectedFillColor)),
					LineSmoothness = 0.1,
					StrokeThickness = 3,
					Opacity = 1.0,
					AreaLimit = -5,
					Values = new ChartValues<ObservableValue>(),
					PointGeometry = DefaultGeometries.None
				},
			};

			ScrollerLineSeries = new SeriesCollection
			{
				new LineSeries
				{
					LineSmoothness = 0.1,
					Fill = new SolidColorBrush(Color.FromRgb(192, 192, 192)),
					PointGeometry = DefaultGeometries.None,
					Values = new ChartValues<ObservableValue>()
				}
			};

			for (int i = 0; i != TemperatureList.Count; i++)
			{
				ReaderLineSeries[0].Values.Add(new ObservableValue(TemperatureList[i]));
				ScrollerLineSeries[0].Values.Add(new ObservableValue(TemperatureList[i]));
			}
			
		}

		public ICommand SelectionChanged_Command => new DelegateCommand(SelectionChanged_Action);
		public void SelectionChanged_Action()
		{
			ReaderLineSeries[0].Values.Clear();
			ScrollerLineSeries[0].Values.Clear();
			if (SelectedIndex == 0)
			{
				for (int i = 0; i != TemperatureList.Count; i++)
				{
					ReaderLineSeries[0].Values.Add(new ObservableValue(TemperatureList[i]));
					ScrollerLineSeries[0].Values.Add(new ObservableValue(TemperatureList[i]));
				}
			}
			
			if (SelectedIndex == 1)
			{
				for (int i = 0; i != OxyStatList.Count; i++)
				{
					ReaderLineSeries[0].Values.Add(new ObservableValue(OxyStatList[i]));
					ScrollerLineSeries[0].Values.Add(new ObservableValue(OxyStatList[i]));
				}
			}
			
			if (SelectedIndex == 2)
			{
				for (int i = 0; i != BPMList.Count; i++)
				{
					ReaderLineSeries[0].Values.Add(new ObservableValue(BPMList[i]));
					ScrollerLineSeries[0].Values.Add(new ObservableValue(BPMList[i]));
				}
			}

			if (SelectedIndex == 3)
			{
				for (int i = 0; i != RespRateList.Count; i++)
				{
					ReaderLineSeries[0].Values.Add(new ObservableValue(RespRateList[i]));
					ScrollerLineSeries[0].Values.Add(new ObservableValue(RespRateList[i]));
				}
			}
		}

		public ICommand RangeChange_Command => new DelegateCommand(RangeChange_Action);
		public void RangeChange_Action()
		{
			Console.Beep(100, 100);
			if (From <= 0) From = 0;
			if (To >= TemperatureList.Count) To = TemperatureList.Count;
		}

		public ICommand ResetScrollBar_Command => new DelegateCommand(ResetScrollbar_Action);
		public void ResetScrollbar_Action()
		{
			To = 5;
			From = 0;
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
				if (value == _selectedIndex)
					return;
				_selectedIndex = value;
				OnPropertyChanged();
			}
		}


		private SeriesCollection _readerLineSeries;
		public SeriesCollection ReaderLineSeries
		{
			get => _readerLineSeries;
			set
			{
				if (value == _readerLineSeries)
					return;
				_readerLineSeries = value;
				OnPropertyChanged();
			}
		}

		private bool _disableAnimation;
		public bool DisableAnimation
		{
			get => _disableAnimation;
			set
			{
				if (value == _disableAnimation)
					return;
				_disableAnimation = value;
				OnPropertyChanged();
			}
		}

		private SeriesCollection _scrollerLineSeries;
		public SeriesCollection ScrollerLineSeries
		{
			get => _scrollerLineSeries;
			set
			{
				if (value == _scrollerLineSeries)
					return;
				_scrollerLineSeries = value;
				OnPropertyChanged();
			}
		}

		private List<double> _temperatureList;
		public List<double> TemperatureList
		{
			get => _temperatureList;
			set
			{
				if (value == _temperatureList)
					return;
				_temperatureList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _oxyStatList;
		public List<double> OxyStatList
		{
			get => _oxyStatList;
			set
			{
				if (value == _oxyStatList)
					return;
				_oxyStatList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _bpmList;
		public List<double> BPMList
		{
			get => _bpmList;
			set
			{
				if (value == _bpmList)
					return;
				_bpmList = value;
				OnPropertyChanged();
			}
		}

		private List<double> _respRateList;
		public List<double> RespRateList
		{
			get => _respRateList;
			set
			{
				if (value == _respRateList)
					return;
				_respRateList = value;
				OnPropertyChanged();
			}
		}

		private double _from;
		public double From
		{
			get => _from;
			set
			{
				if (value == _from)
					return;
				_from = value;
				OnPropertyChanged();
			}
		}

		private double _to;
		public double To
		{
			get => _to;
			set
			{
				if (value == _to)
					return;
				_to = value;
				OnPropertyChanged();
			}
		}
	}
}
