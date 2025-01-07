using System.Xml.Serialization;

namespace BossKeysThing;

[XmlRoot("Graph")]
public class Graph
{
	[XmlElement("Node")] public List<Node> Nodes { get; } = [];
}