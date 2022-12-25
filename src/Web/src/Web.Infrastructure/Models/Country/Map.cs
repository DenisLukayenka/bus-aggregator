using System.Collections.Generic;

namespace Web.Infrastructure.Models.Country
{
    public class Map
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string? Description { get; set; }

        public virtual IList<MapPath> Paths { get; set; }
        public virtual IList<City> Cities { get; set; }
    }
}
