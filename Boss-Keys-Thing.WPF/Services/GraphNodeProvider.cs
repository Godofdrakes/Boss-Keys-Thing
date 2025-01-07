namespace BossKeysThing.WPF.Services;

public class GraphNodeProvider : IGraphNodeProvider
{
	private readonly Dictionary<string, Uri> _sources = new();

	public Uri GetNode(string type)
	{
		if (_sources.TryGetValue(type, out var source))
		{
			return source;
		}

		return new Uri(string.Empty);
	}

	public void AddNode(string type, Uri source)
	{
		ArgumentNullException.ThrowIfNull(source);

		_sources.Add(type, source);
	}
}