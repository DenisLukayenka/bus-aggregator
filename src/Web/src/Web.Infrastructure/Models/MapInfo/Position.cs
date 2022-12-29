using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot("position")]
    public class Position
    {
        [XmlElement("x", IsNullable = false)]
        public int X { get; set; }

        [XmlElement("y", IsNullable = false)]
        public int Y { get; set; }
    }
}
