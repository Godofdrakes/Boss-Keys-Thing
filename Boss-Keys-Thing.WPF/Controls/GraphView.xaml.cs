using System.Reactive.Disposables;
using ReactiveUI;

namespace BossKeysThing.WPF.Controls;

public partial class GraphView
{
	public GraphView()
	{
		InitializeComponent();

		this.WhenActivated(onDispose =>
		{
			this.OneWayBind(ViewModel, vm => vm.Children, v => v.ListView.ItemsSource)
				.DisposeWith(onDispose);
		});
	}
}