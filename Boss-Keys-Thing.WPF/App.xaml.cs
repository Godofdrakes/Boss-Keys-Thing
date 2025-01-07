using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using BossKeysThing.WPF.ViewModels;
using BossKeysThing.WPF.Windows;

namespace BossKeysThing.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
	private const string ExampleProject = @"Resources\Graph\Example.zip";

	public App()
	{
		InitializeComponent();
	}

	private void App_OnStartup(object sender, StartupEventArgs e)
	{
		var projectPath = Path.Combine(Environment.CurrentDirectory, ExampleProject);

		var graph = LoadProject(projectPath);

		this.MainWindow = new MainWindow()
		{
			ViewModel = new MainWindowViewModel()
			{
				Graph = graph,
			}
		};
		this.MainWindow.Show();
	}

	private static GraphViewModel LoadProject(string path)
	{
		var graph = new GraphViewModel();

		using (var project = ZipFile.OpenRead(path))
		{
			foreach (var entry in project.Entries)
			{
				if (entry.Name.EndsWith("Graph.xml"))
				{
					var graphModel = LoadModelFromArchive(entry);

					graph.LoadModel(graphModel);
				}

				if (entry.Name.EndsWith(".png"))
				{
					var typeName = Path.GetFileNameWithoutExtension(entry.Name);
					var bitmap = LoadImageFromArchive(entry);

					graph.CreateType(typeName, bitmap);
				}
			}
		}

		return graph;
	}

	private static BitmapImage LoadImageFromArchive(ZipArchiveEntry zipArchiveEntry)
	{
		var bitmap = new BitmapImage();

		using (var memoryStream = new MemoryStream())
		{
			using (var zipStream = zipArchiveEntry.Open())
			{
				// Gotta do this for some reason
				zipStream.CopyTo(memoryStream);
			}

			memoryStream.Position = 0;

			bitmap.BeginInit();
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.StreamSource = memoryStream;
			bitmap.EndInit();
			bitmap.Freeze();
		}

		return bitmap;
	}

	private static Graph LoadModelFromArchive(ZipArchiveEntry zipArchiveEntry)
	{
		using var stream = zipArchiveEntry.Open();

		var serializer = new XmlSerializer(typeof(Graph));

		var result = serializer.Deserialize(stream);

		if (result is not Graph graphModel)
		{
			throw new InvalidOperationException("Failed to deserialize graph model");
		}

		return graphModel;
	}
}