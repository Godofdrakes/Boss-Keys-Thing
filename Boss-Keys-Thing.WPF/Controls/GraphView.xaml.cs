using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace BossKeysThing.WPF.Controls;

public partial class GraphView
{
	public GraphView()
	{
		InitializeComponent();

		this.WhenActivated(onDispose =>
		{
			this.OneWayBind(ViewModel, vm => vm.Children, v => v.TreeView.ItemsSource)
				.DisposeWith(onDispose);

			if (this.TreeView.ItemContainerGenerator.ContainerFromIndex(0) is TreeViewItem item)
			{
				item.ExpandSubtree();
			}
		});
	}
}