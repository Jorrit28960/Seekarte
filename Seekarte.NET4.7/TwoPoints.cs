using System.Windows;
using System.Windows.Media;

namespace Seekarte.NET4._7
{
    public class TwoPoints
    {
        public Point startPoint;
        public Point endPoint;
        public Country country;
        public double scaleX;
        public double scaleY;
        public double transformX;
        public double transformY;
        public string type;
        public string eventText;

        //public TwoPoints(Point startPoint, Point endPoint, string type, string eventText)
        //{
        //    this.startPoint = startPoint;
        //    this.endPoint = endPoint;
        //    this.type = type;
        //    this.eventText = eventText;
        //}

        //public TwoPoints(Point startPoint, Point endPoint, string type)
        //{
        //    this.startPoint = startPoint;
        //    this.endPoint = endPoint;
        //    this.type = type;
        //}

        //public TwoPoints(Point startPoint, Point endPoint, Color color, string type)
        //{
        //    this.startPoint = startPoint;
        //    this.endPoint = endPoint;
        //    this.color = color;
        //    this.type = type;
        //}

        public TwoPoints(Point startPoint, Point endPoint, Country country, double scaleX, double scaleY, double transformX, double transformY, string type, string eventText)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.country = country;
            this.type = type;
            this.eventText = eventText;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.transformX = transformX;
            this.transformY = transformY;
        }
    }
}