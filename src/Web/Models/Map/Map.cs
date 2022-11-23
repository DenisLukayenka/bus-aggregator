using System.Collections.Generic;

namespace WebSPA.Models.Map
{
    public class Map
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public string? Description { get; set; }

        public virtual IList<MapPath> Paths { get; set; }
        public virtual IList<City> Cities { get; set; }
    }
}
