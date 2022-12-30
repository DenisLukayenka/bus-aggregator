using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot("indicator")]
    public class CityIndicator
    {
        [XmlElement("position", IsNullable = false)]
        public virtual Position Position { get; set; }

        [XmlElement("style", IsNullable = true)]
        public string? Style { get; set; }

        [XmlElement("size", IsNullable = true)]
        public int? Size { get; set; }
    }
}
