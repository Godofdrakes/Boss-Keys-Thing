using System.Reactive.Disposables;
using ReactiveUI;

namespace BossKeysThing.WPF.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
	public MainWindow()
	{
		InitializeComponent();

		this.WhenActivated(onDispose =>
		{
			this.OneWayBind(this, view => view.ViewModel, view => view.DataContext)
				.DisposeWith(onDispose);
			this.OneWayBind(ViewModel, vm => vm.Graph, v => v.GraphView.ViewModel)
				.DisposeWith(onDispose);
		});
	}
}