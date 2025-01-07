using System.Windows;
using BossKeysThing.WPF.Services;

namespace BossKeysThing.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
	private GraphNodeProvider GraphNodeProvider { get; }

	public App()
	{
		GraphNodeProvider = new GraphNodeProvider();

		InitializeComponent();
	}

	private void App_OnStartup(object sender, StartupEventArgs e)
	{
		this.MainWindow = new MainWindow();
		this.MainWindow.Show();
	}
}