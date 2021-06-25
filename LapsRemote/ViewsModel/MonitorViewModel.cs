using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapsRemote.ViewsModel
{
	class MonitorViewModel : ViewModelBase
	{
		private double _timeElapsed;
		public double TimeElapsed
		{
			get => _timeElapsed;

			set
			{
				if (value == _timeElapsed)
					return;
				_timeElapsed = value;
				OnPropertyChanged();
			}
		}

		private double _currentValue;
		public double CurrentValue
		{
			get => _currentValue;

			set
			{
				if (value == _currentValue)
					return;
				_currentValue = value;
				OnPropertyChanged();
			}
		}
	}
}
