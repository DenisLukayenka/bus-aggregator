using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot("text")]
    public class CityText
    {
        [XmlElement("caption", IsNullable = false)]
        public string Caption { get; set; }

        [XmlElement("style", IsNullable = true)]
        public string? Style { get; set; }

        [XmlElement("position", IsNullable = true)]
        public virtual Position? Position { get; set; }

        [XmlElement("align", IsNullable = true)]
        public string? Align { get; set; }

        [XmlElement("size", IsNullable = true)]
        public int? Size { get; set; }
    }
}
