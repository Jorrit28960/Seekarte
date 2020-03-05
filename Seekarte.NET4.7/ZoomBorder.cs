using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Seekarte.NET4._7
{
    //Class that implements the image zoom and pan
    public class ZoomBorder : Border
    {
        private static List<UIElement> listZoomBorders = new List<UIElement>();
        private static List<Point> pointsZoomBorders = new List<Point>();
        private static List<ZoomBorder> zoomBorders = new List<ZoomBorder>();
        private UIElement child = null;
        private Point origin;
        private Point startLeftBtn;
        private Point startRightBtn;
        private Point endRightBtn;
        private static ScaleTransform latestScale = new ScaleTransform(1, 1);
        private static TranslateTransform latestTransform = new TranslateTransform(0, 0);

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                {
                    this.Initialize(value);
                    listZoomBorders.Add(value);
                    zoomBorders.Add(this);
                }
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                this.MouseWheel += Child_MouseWheel;
                this.MouseLeftButtonDown += Child_MouseLeftButtonDown;
                this.MouseLeftButtonUp += Child_MouseLeftButtonUp;
                this.MouseMove += Child_MouseMove;
                this.MouseRightButtonUp += Child_MouseRightButtonUp;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  Child_PreviewMouseRightButtonDown);
            }
        }

        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        #region Child Events

        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (var child in listZoomBorders)
            {
                if (child != null)
                {
                    var st = GetScaleTransform(child);
                    var tt = GetTranslateTransform(child);

                    double zoom = e.Delta > 0 ? .2 : -.2;
                    if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                        return;

                    Point relative = e.GetPosition(child);
                    double absoluteX = relative.X * st.ScaleX + tt.X;
                    double absoluteY = relative.Y * st.ScaleY + tt.Y;

                    //don't zoom greater than window
                    st.ScaleX = (st.ScaleX + zoom >= 1) ? st.ScaleX + zoom : 1;
                    st.ScaleY = (st.ScaleY + zoom >= 1) ? st.ScaleY + zoom : 1;

                    saveLatest(st, tt);
                    //center image if maximum size
                    tt.X = (st.ScaleX + zoom >= 1) ? absoluteX - relative.X * st.ScaleX : 0;
                    tt.Y = (st.ScaleY + zoom >= 1) ? absoluteY - relative.Y * st.ScaleY : 0;
                }
            }
        }

        private void saveLatest(ScaleTransform st, TranslateTransform tt)
        {
            if (st != null)
                latestScale = st;

            if (tt != null)
                latestTransform = tt;
        }

        private void Child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                pointsZoomBorders.Clear();
                foreach (var child in listZoomBorders)
                {
                    var tmp = GetTranslateTransform(child);
                    origin = new Point(tmp.X, tmp.Y);
                    pointsZoomBorders.Add(origin);
                }
                var tt = GetTranslateTransform(child);
                startLeftBtn = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void Child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;

                foreach (var item in listZoomBorders)
                {
                    if (item != null)
                    {
                        var st = GetScaleTransform(item);
                        var tt = GetTranslateTransform(item);

                        saveLatest(st, tt);
                    }
                }
            }
        }

        void Child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            startRightBtn = e.GetPosition(this);
        }
        private void Child_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            endRightBtn = e.GetPosition(this);

            if (MainWindow.IsCountrySelected)
            {
                List<ZoomBorder> borders;
                bool isListInDic = MainWindow.SelctedCountry.Route.TryGetValue(MainWindow.Round, out borders);

                if (!isListInDic)
                {
                    MainWindow.SelctedCountry.Route.Add(MainWindow.Round, borders = new List<ZoomBorder>());
                }

                foreach (var item in CreateALine(MainWindow.SelctedCountry.color))
                {
                    borders.Add(item);
                }
            }
            //need because MouseRightDown is not reacting always
            startRightBtn = e.GetPosition(this);
        }
        public List<ZoomBorder> CreateALine(Color color)
        {
            List<ZoomBorder> list = new List<ZoomBorder>();

            list.Add(CreateALine(color, startRightBtn, endRightBtn));

            int lineLength = 10;

            Point left = Pfeil(lineLength, 170, startRightBtn, endRightBtn);
            Point right = Pfeil(lineLength, 190, startRightBtn, endRightBtn);

            list.Add(CreateALine(color, endRightBtn, left));
            list.Add(CreateALine(color, endRightBtn, right));

            return list;
        }

        private Point Pfeil(int lineLength, int degree, Point point1, Point point2)
        {
            double X1 = (point1.X);
            double Y1 = (point1.Y);
            double X2 = (point2.X);
            double Y2 = (point2.Y);

            double r = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2)) / latestScale.ScaleX;
            double newY2 = Y2 + lineLength * (((Y2 - Y1) / r) * Math.Cos((degree / (double)360) * 2 * Math.PI) + ((X2 - X1) / r) * Math.Sin((degree / (double)360) * 2 * Math.PI));
            double newX2 = X2 + lineLength * (((X2 - X1) / r) * Math.Cos((degree / (double)360) * 2 * Math.PI) - ((Y2 - Y1) / r) * Math.Sin((degree / (double)360) * 2 * Math.PI));

            return new Point(newX2, newY2);

        }

        public ZoomBorder CreateALine(Color color, Point start, Point end)
        {
            ZoomBorder zoomBorder = new ZoomBorder();

            // Create a Line  
            Line line = new Line();

            zoomBorder.Child = line;
            Choose.mainWindow.Map.Children.Add(zoomBorder);

            var st = GetScaleTransform(zoomBorder.child);
            var tt = GetTranslateTransform(zoomBorder.child);

            if (latestScale != null)
            {
                st.ScaleX = latestScale.ScaleX;
                st.ScaleY = latestScale.ScaleY;
            }

            if (latestTransform != null)
            {
                tt.X = latestTransform.X;
                tt.Y = latestTransform.Y;
            }

            line.X1 = ((start.X - latestTransform.X) / latestScale.ScaleX);
            line.Y1 = ((start.Y - latestTransform.Y) / latestScale.ScaleY);

            line.X2 = ((end.X - latestTransform.X) / latestScale.ScaleX);
            line.Y2 = ((end.Y - latestTransform.Y) / latestScale.ScaleY);

            // Create a red Brush  
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = color;

            // Set Line's width and color  
            line.StrokeThickness = 1;
            line.Stroke = solidColorBrush;

            // Add line to the Grid. 
            Grid.SetColumn(zoomBorder, 0);
            Grid.SetRow(zoomBorder, 1);
            Grid.SetColumnSpan(zoomBorder, 4);
            Grid.SetRowSpan(zoomBorder, 2);
            zoomBorder.ClipToBounds = true;

            line.MouseRightButtonDown += Child_PreviewMouseRightButtonDown;

            return zoomBorder;
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            //if (child != null)
            //{
            //    if (child.IsMouseCaptured)
            //    {
            //        var tt = GetTranslateTransform(child);
            //        Vector v = start - e.GetPosition(this);
            //        tt.X = origin.X - v.X;
            //        tt.Y = origin.Y - v.Y;
            //    }
            //}

            if (listZoomBorders.Count > 0 && pointsZoomBorders.Count > 0)
            {
                for (int i = 0; i < listZoomBorders.Count; i++)
                {
                    var achild = listZoomBorders[i];

                    if (achild != null)
                    {
                        if (child.IsMouseCaptured)
                        {
                            var aorigin = pointsZoomBorders[i];
                            var tt = GetTranslateTransform(achild);
                            Vector v = startLeftBtn - e.GetPosition(zoomBorders[i]);
                            tt.X = aorigin.X - v.X;
                            tt.Y = aorigin.Y - v.Y;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
