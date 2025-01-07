using System.Xml.Serialization;

namespace BossKeysThing;

[XmlRoot]
public class Graph
{
	[XmlElement] public List<Node> Nodes { get; } = [];
}