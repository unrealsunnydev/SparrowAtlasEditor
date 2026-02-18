using Microsoft.Win32;
using SparrowEditor.Sparrow;
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
using System.Xml.Linq;

namespace SparrowEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void mniOpen_Click(object sender, RoutedEventArgs e)
		{

			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = $"{SparrowEditor.Language.GetString("open.file.SparrowFilter")} (*.xml)|*.xml",
				Title = $"{SparrowEditor.Language.GetString("open.file.SparrowTitle")}"
			};

			if (dialog.ShowDialog() == true)
			{
				string path = dialog.FileName;

				XDocument doc = XDocument.Load(path);

				if (doc.Root == null)
				{
					MessageBox.Show("ERROR: The choosen XML data contains invalid root data.");
					return;
				}

				SparrowFormat.x = Convert.ToDouble(doc.Root.Element("x")?.Value);
				SparrowFormat.y = Convert.ToDouble(doc.Root.Element("y")?.Value);
				SparrowFormat.width = Convert.ToDouble(doc.Root.Element("width")?.Value);
				SparrowFormat.height = Convert.ToDouble(doc.Root.Element("height")?.Value);
				SparrowFormat.flipX = Convert.ToBoolean(doc.Root.Element("flipX")?.Value);

				#if DEBUG
				System.Diagnostics.Debug.WriteLine($"X: {SparrowFormat.x}");
				System.Diagnostics.Debug.WriteLine($"Y: {SparrowFormat.y}");
				System.Diagnostics.Debug.WriteLine($"Width: {SparrowFormat.width}");
				System.Diagnostics.Debug.WriteLine($"Height: {SparrowFormat.height}");
				System.Diagnostics.Debug.WriteLine($"Rotated: {SparrowFormat.rotated}");
				System.Diagnostics.Debug.WriteLine($"FlipX: {SparrowFormat.flipX}");
				System.Diagnostics.Debug.WriteLine($"FlipY: {SparrowFormat.flipY}");
				#endif
			}
		}
	}
}