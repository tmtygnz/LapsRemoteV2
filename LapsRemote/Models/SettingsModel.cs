using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapsRemote.Models
{
	struct SettingsModel
	{
		private int? _pollingRate;

		public int PollingRate
		{
			get => _pollingRate ?? 500;
			set
			{
				if (value == _pollingRate)
					return;
				_pollingRate = value;
			}
		}

		private int? _scrollerThumbSize;

		public int ScrollerThumbSize
		{
			get => _scrollerThumbSize ?? 10;
			set
			{
				if (value == _scrollerThumbSize)
					return;
				_scrollerThumbSize = value;
			}
		}

		private int? _gPURendering;
		public int GPURendering
		{
			get => _gPURendering ?? 1;
			set
			{
				if (value == _gPURendering)
					return;
				_gPURendering = value;
			}
		}

		private string? _selectedStrokeColor;
		public string SelectedStrokeColor
		{
			get => _selectedStrokeColor ?? "#50CB93";
			set
			{
				if (value == _selectedStrokeColor)
					return;
				_selectedStrokeColor = value;
			}
		}

		private string _selectedFillColor;
		public string SelectedFillColor
		{
			get => _selectedFillColor ?? "#71EFA3";
			set
			{
				if (value == _selectedFillColor)
					return;
				_selectedFillColor = value;
			}
		}

		private string _applicationLogPath;
		public string AppLicationLogPath
		{
			get => _applicationLogPath ?? Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LapsRemoteV2");
			set
			{
				if (value == _applicationLogPath)
					return;
				_applicationLogPath = value;
			}
		}
	}
}
