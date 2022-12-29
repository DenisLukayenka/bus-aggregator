using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot(ElementName = "map", IsNullable = false)]
    public class Map
    {
        [XmlElement("id", IsNullable = false)]
        public int Id { get; set; }

        [XmlElement("caption", IsNullable = false)]
        public string Caption { get; set; }

        [XmlElement("description", IsNullable = true)]
        public string? Description { get; set; }

        [XmlArray("paths", IsNullable = false)]
        [XmlArrayItem("path")]
        public virtual MapPath[] Paths { get; set; }

        [XmlArray("cities", IsNullable = false)]
        [XmlArrayItem("city")]
        public virtual City[] Cities { get; set; }
    }
}
