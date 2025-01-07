using System.Windows.Media;
using ReactiveUI;

namespace BossKeysThing.WPF.ViewModels;

public class NodeTypeViewModel : ReactiveObject
{
	public string Id { get; }

	public ImageSource Image { get; }

	private readonly GraphViewModel _graph;

	public NodeTypeViewModel(GraphViewModel graph, string id, ImageSource image)
	{
		ArgumentNullException.ThrowIfNull(graph);
		ArgumentNullException.ThrowIfNullOrEmpty(id);
		ArgumentNullException.ThrowIfNull(image);

		Id = id;
		Image = image;
		_graph = graph;
	}
}