using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("Narrative")]
public class Narrative
{
    [XmlArray("StationCollection")]
    [XmlArrayItem("Station")]
    public List<Station> StationCollection = new List<Station>();

    public void Save(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Narrative));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }

    }

    public static Narrative Load(string path)
    {
        Narrative narrative = new Narrative();
        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Narrative));

            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                narrative =  serializer.Deserialize(stream) as Narrative;
            }
        }
        return narrative;
        
    }
}

