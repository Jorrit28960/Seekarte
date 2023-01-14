﻿using System.Collections.Generic;
using System.Windows.Media;

namespace Seekarte.NET4._7
{
    public class Country
    {
        public Dictionary<int, List<ZoomBorder>> Route = new Dictionary<int, List<ZoomBorder>>();
        public Dictionary<int, List<TwoPoints>> RoutePoints = new Dictionary<int, List<TwoPoints>>();
        public Color color { get; set; } = Colors.Red;
        public List<Ship> Ships { get; } = new List<Ship>();
        public string countryName { get; set; }
        public string password { get; set; }
        public int geschwader { get; set; }

        public Country(string contryName, Color color)
        {
            this.countryName = contryName;
            this.color = color;
        }

        public Country ToSave()
        {
            Country country = new Country(countryName, color);

            return country;
        }
    }
}
