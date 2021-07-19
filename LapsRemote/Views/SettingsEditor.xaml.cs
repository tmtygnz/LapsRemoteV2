using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LapsRemote.ViewsModel;

namespace LapsRemote.Views
{
	/// <summary>
	/// Interaction logic for SettingsEditor.xaml
	/// </summary>
	public partial class SettingsEditor : MetroWindow
	{
		SettingsViewModel _viewModel;
		public SettingsEditor()
		{
			InitializeComponent();
			_viewModel = new SettingsViewModel();
			DataContext = _viewModel;
		}
	}
}
