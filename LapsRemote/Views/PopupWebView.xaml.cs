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
using MahApps.Metro.Controls;

namespace LapsRemote.Views
{
	/// <summary>
	/// Interaction logic for PopupWebView.xaml
	/// </summary>
	public partial class PopupWebView : MetroWindow
	{
		PopupWebviewViewModel _viewModel;
		public PopupWebView(string URI)
		{
			InitializeComponent();
			_viewModel = new PopupWebviewViewModel(URI);
			DataContext = _viewModel;
		}
	}
}
