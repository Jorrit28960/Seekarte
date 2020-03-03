using System.Collections.Generic;

namespace Seekarte.NET4._7
{
    public class Country
    {

        public Dictionary<int, List<ZoomBorder>> Route = new Dictionary<int, List<ZoomBorder>>();

        public List<ZoomBorder> zoomBorders = new List<ZoomBorder>();
        public Fleet Fleet { get; set; } = new Fleet();
        public string countryName { get; set; }
        public string password { get; set; }
        public int geschwader { get; set; }

        public Country(string contryName)
        {
            this.countryName = contryName;
        }
    }
}
