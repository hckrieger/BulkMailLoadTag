using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BulkMailLoadTagApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			dateTexBox.Text = DateTime.Now.ToString("M/d/yyyy");

			setTotalBox();
		}

		private void palletsTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			setTotalBox();
		}

		private void setTotalBox()
		{
			if (palletTotalTextBox != null && palletsTextBox != null)
			{
				int total = 0;
				if (Int32.TryParse(palletsTextBox.Text.Trim(), out int pallets))
				{
					if (pallets > 0)
					{
						total = pallets;
					}
					
				}
				palletTotalTextBox.Text = $"{total} skids";
				//palletTotalTextBox.Text = $"{palletsTextBox.Text.Trim()} pallets";
			}
				

		}
	}
}