using System.Xml;
using System.Xml.Serialization;

public class Artefact
{
    [XmlAttribute("name")]
    public string Name;

    [XmlAttribute("id")]
    public System.Guid id;

    [XmlElement]
    public string Location;

}
