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

			ValueToLoad = new SeriesCollection
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

			for(int i = 0; i != TemperatureList.Count; i++)
			{
				ValueToLoad[0].Values.Add(new ObservableValue(TemperatureList[i]));
			}
		}

		public ICommand SelectionChanged_Command => new RelayCommand(param => SelectionChanged_Action());
		public void SelectionChanged_Action()
		{
			
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

		private SeriesCollection _valueToLoad;
		public SeriesCollection ValueToLoad
		{
			get => _valueToLoad;
			set
			{
				if (value == _valueToLoad)
					return;
				_valueToLoad = value;
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


		public List<double> _temperatureList;
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

		public List<double> _oxyStatList;
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

		public List<double> _bpmList;
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

		public List<double> _respRateList;
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
	}
}
