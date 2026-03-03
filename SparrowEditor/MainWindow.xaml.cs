using Microsoft.Win32;
using SparrowEditor.Sparrow;
using System.Diagnostics;
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
using System.Xml;
using System.Xml.Linq;

namespace SparrowEditor {
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
				Filter = $"{SparrowEditor.Language.GetString("filedialog.SparrowFilter")} (*.xml)|*.xml",
				Title = $"{SparrowEditor.Language.GetString("filedialog.open.SparrowTitle")}"
			};

			if (dialog.ShowDialog() == true)
			{
				string path = dialog.FileName;

				XDocument doc;
				try
				{
					doc = XDocument.Load(path);
				}
				catch (XmlException ex)
				{
					MessageBox.Show(ex.Message,
						SparrowEditor.Language.GetString("messagebox.title.Error"),
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					return;
				}

				if (doc.Root == null)
				{
					MessageBox.Show("The choosen XML data contains invalid root data.",
						SparrowEditor.Language.GetString("messagebox.title.Error"),
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					return;
				}

				XNamespace ns = doc.Root.GetDefaultNamespace();
				XElement? atlas = (ns != null) ? doc.Root.Element(ns + "TextureAtlas") : doc.Element("TextureAtlas");

				if (atlas != null)
				{
					foreach (var sub in atlas.Elements("SubTexture"))
					{
						SparrowFormat.x = (double)sub.Attribute("x");
						SparrowFormat.y = (double)sub.Attribute("y");
						SparrowFormat.width = (double)sub.Attribute("width");
						SparrowFormat.height = (double)sub.Attribute("height");

						if (sub.Attribute("rotate") != null)
						{
							SparrowFormat.rotated = (bool)sub!.Attribute("rotated");
						}

						if (sub.Attribute("flipX") != null)
						{
							SparrowFormat.flipX = (bool)sub!.Attribute("flipX");
						}

						if (sub.Attribute("flipY") != null)
						{
							SparrowFormat.flipY = (bool)sub!.Attribute("flipY");
						}

#if DEBUG
						Debug.WriteLine($"X:       {SparrowFormat.x}");
						Debug.WriteLine($"Y:       {SparrowFormat.y}");
						Debug.WriteLine($"Width:   {SparrowFormat.width}");
						Debug.WriteLine($"Height:  {SparrowFormat.height}");
						Debug.WriteLine($"Rotated: {SparrowFormat.rotated}");
						Debug.WriteLine($"FlipX:   {SparrowFormat.flipX}");
						Debug.WriteLine($"FlipY:   {SparrowFormat.flipY}");
#endif
					}
				}
				else
				{
					MessageBox.Show("Parsed data was null.",
						SparrowEditor.Language.GetString("messagebox.title.Error"),
						MessageBoxButton.OK,
						MessageBoxImage.Error);
				}
			}
		}

		private void mniSaveAs_Click(object sender, RoutedEventArgs e)
		{
			var save = new SaveFileDialog
			{
				Title = $"{SparrowEditor.Language.GetString("filedialog.save_as.SparrowTitle")}",
				Filter = $"{SparrowEditor.Language.GetString("filedialog.SparrowFilter")} (*.xml)|*.xml",
			};

			if (save.ShowDialog() == true)
			{

				try
				{
					System.IO.File.WriteAllText(save.FileName, "fileContent");
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
					// Handle error
				}
			}
		}
	}
}