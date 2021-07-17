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
using System.IO;
using LapsRemote.Models;
using Newtonsoft.Json;
using LapsRemote.ViewsModel;

namespace LapsRemote.Views
{
	/// <summary>
	/// Interaction logic for Reader.xaml
	/// </summary>
	public partial class Reader : MetroWindow
	{
		ReaderViewModel _viewModel;
		public Reader()
		{
			InitializeComponent();
			_viewModel = new ReaderViewModel();
			DataContext = _viewModel;
		}
	}
}
