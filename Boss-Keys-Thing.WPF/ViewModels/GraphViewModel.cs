using System.Collections.ObjectModel;
using System.Windows.Media;
using DynamicData;
using DynamicData.Alias;
using ReactiveUI;

namespace BossKeysThing.WPF.ViewModels;

public class GraphViewModel : ReactiveObject
{
	public ReadOnlyObservableCollection<NodeViewModel> Children => _children;

	public ReadOnlyObservableCollection<NodeTypeViewModel> Types => _types;

	private readonly ReadOnlyObservableCollection<NodeViewModel> _children;

	private readonly ReadOnlyObservableCollection<NodeTypeViewModel> _types;

	private readonly SourceCache<NodeViewModel, string> _nodeSource = new(node => node.Id);

	private readonly SourceCache<NodeTypeViewModel, string> _typeSource = new(type => type.Id);

	public GraphViewModel()
	{
		_nodeSource.Connect()
			.Where(node => string.IsNullOrEmpty(node.Parent))
			.Bind(out _children)
			.Subscribe();

		_typeSource.Connect()
			.Bind(out _types)
			.Subscribe();
	}

	public GraphViewModel LoadModel(Graph graphModel)
	{
		ArgumentNullException.ThrowIfNull(graphModel);

		using (_nodeSource.SuspendNotifications())
		{
			foreach (var nodeModel in graphModel.Nodes)
			{
				var node = CreateNode(nodeModel.Id, nodeModel.Type);

				node.Label = nodeModel.Label;
				node.Parent = nodeModel.Parent;
			}
		}

		return this;
	}

	public Graph SaveModel()
	{
		var graphModel = new Graph();

		foreach (var node in _nodeSource.Items)
		{
			graphModel.Nodes.Add(new Node()
			{
				Id = node.Id,
				Type = node.Type,
				Parent = node.Parent,
				Label = node.Label,
			});
		}

		return graphModel;
	}

	public NodeViewModel CreateNode(string id, string type)
	{
		var node = new NodeViewModel(this, id, type);

		_nodeSource.AddOrUpdate(node);

		return node;
	}

	public NodeViewModel? FindNode(string id)
	{
		return _nodeSource.KeyValues.GetValueOrDefault(id);
	}

	public NodeTypeViewModel CreateType(string id, ImageSource imageSource)
	{
		var type = new NodeTypeViewModel(this, id, imageSource);

		_typeSource.AddOrUpdate(type);

		return type;
	}

	public IObservable<IChangeSet<NodeViewModel, string>> FindChildren(string id)
	{
		ArgumentException.ThrowIfNullOrEmpty(id);

		return _nodeSource.Connect().Where(node => string.Equals(node.Parent, id));
	}
}