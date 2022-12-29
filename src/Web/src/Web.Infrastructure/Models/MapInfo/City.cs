using System.Xml.Serialization;

namespace Web.Infrastructure.Models.MapInfo
{
    [XmlRoot("city")]
    public class City
    {
        [XmlElement("id", IsNullable = false)]
        public int Id { get; set; }

        [XmlElement("indicator", IsNullable = false)]
        public CityIndicator Indicator { get; set; }

        [XmlElement("text", IsNullable = false)]
        public CityText Text { get; set; }
    }
}
