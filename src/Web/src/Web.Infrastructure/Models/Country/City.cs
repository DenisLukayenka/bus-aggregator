namespace Web.Infrastructure.Models.Country
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }

        public CityIndicator Indicator { get; set; }
        public CityText Text { get; set; }
    }
}
