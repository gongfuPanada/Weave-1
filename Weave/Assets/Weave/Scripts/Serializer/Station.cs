using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Station
{
    [XmlArray("Artefacts")]
    [XmlArrayItem("Artefact")]
    public List<Artefact> Artefacts = new List<Artefact>();

    [XmlAttribute("name")]
    public string Name;
}
