using System.Xml.Serialization;

namespace BossKeysThing;

public class Node
{
	[XmlAttribute] public string Id { get; set; } = string.Empty;

	[XmlAttribute] public string Type { get; set; } = string.Empty;

	[XmlElement] public string? Parent { get; set; }

	[XmlElement] public string? Label { get; set; }
}