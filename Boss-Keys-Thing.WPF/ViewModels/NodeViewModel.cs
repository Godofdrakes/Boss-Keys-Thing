using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BossKeysThing.WPF.ViewModels;

public class NodeViewModel : ReactiveObject
{
	public string Id { get; }

	public string Type { get; }

	[Reactive] public string? Label { get; set; }

	[Reactive] public string? Parent { get; set; }

	[ObservableAsProperty] public string DisplayName { get; } = string.Empty;

	public ReadOnlyObservableCollection<NodeViewModel> Children => _children;

	private readonly ReadOnlyObservableCollection<NodeViewModel> _children;

	private readonly GraphViewModel _graph;

	public NodeViewModel(GraphViewModel graph, string id, string type)
	{
		ArgumentNullException.ThrowIfNull(graph);
		ArgumentNullException.ThrowIfNullOrEmpty(id);
		ArgumentNullException.ThrowIfNullOrEmpty(type);

		Id = id;
		Type = type;
		_graph = graph;

		this.WhenAnyValue(x => x.Id, x => x.Label)
			.Select(GetDisplayName)
			.ToPropertyEx(this, x => x.DisplayName);

		graph.FindChildren(id)
			.Bind(out _children)
			.Subscribe();
	}

	private static string GetDisplayName((string id, string? label) tuple)
	{
		if (!string.IsNullOrEmpty(tuple.label))
		{
			return tuple.label;
		}

		return tuple.id;
	}
}