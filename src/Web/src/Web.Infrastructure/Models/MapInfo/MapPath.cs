using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot(ElementName = "path", IsNullable = false)]
    public class MapPath
    {
        [XmlElement("value", IsNullable = false)]
        public string Value { get; set; }

        [XmlElement("style", IsNullable = true)]
        public string? Style { get; set; }
    }
}
