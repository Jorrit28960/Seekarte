using System.Windows;
using System.Windows.Media;

namespace Seekarte.NET4._7
{
    public class TwoPoints
    {
        public Point startPoint;
        public Point endPoint;
        public Color color;
        public string type;

        public TwoPoints(Point startPoint, Point endPoint, string type)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.type = type;
        }

        public TwoPoints(Point startPoint, Point endPoint, Color color, string type)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.color = color;
            this.type = type;
        }
    }
}