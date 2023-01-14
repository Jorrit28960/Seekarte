using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Seekarte.NET4._7
{
    //Class that implements the image zoom and pan
    public class ZoomBorder : Border
    {
        private static readonly List<UIElement> listZoomBorders = new List<UIElement>();
        private static readonly List<Point> pointsZoomBorders = new List<Point>();
        private static readonly List<ZoomBorder> zoomBorders = new List<ZoomBorder>();
        private UIElement child = null;
        private Point origin;
        private Point startLeftBtn;
        public Point startRightBtn;
        public Point endRightBtn;
        private static ScaleTransform latestScale = new ScaleTransform(1, 1);
        private static TranslateTransform latestTransform = new TranslateTransform(0, 0);
        public string playerEvent { get; set; } = null;

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

        public void ResetZoom()
        {
            foreach (var child in listZoomBorders)
            {
                if (child != null)
                {
                    var st = GetScaleTransform(child);
                    var tt = GetTranslateTransform(child);

                    st.ScaleX = 1;
                    st.ScaleY =  1;

                    tt.X =  0;
                    tt.Y =  0;

                    SaveLatest(st, tt);
                }
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

                    SaveLatest(st, tt);
                    //center image if maximum size
                    tt.X = (st.ScaleX + zoom >= 1) ? absoluteX - relative.X * st.ScaleX : 0;
                    tt.Y = (st.ScaleY + zoom >= 1) ? absoluteY - relative.Y * st.ScaleY : 0;
                }
            }
        }

        private void SaveLatest(ScaleTransform st, TranslateTransform tt)
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

                        SaveLatest(st, tt);
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

            if (MainWindow.IsCountrySelected && MainWindow.SelctedCountry.countryName != "Admin")
            {
                bool isListInDic = MainWindow.SelctedCountry.Route.TryGetValue(MainWindow.Round, out List<ZoomBorder> borders);

                if (!isListInDic)
                {
                    MainWindow.SelctedCountry.Route.Add(MainWindow.Round, borders = new List<ZoomBorder>());
                }

                foreach (var item in CreateALine(MainWindow.SelctedCountry))
                {
                    borders.Add(item);
                }
            }

            if (MainWindow.IsCountrySelected && MainWindow.SelctedCountry.countryName == "Admin")
            {
                switch (MainWindow.PlayerEventNumber)
                {
                    case 2:
                        foreach (var country in MainWindow.PlayerEventCountries)
                        {
                            EnemyFleet(country);
                        }
                        break;
                    default:
                        MessageBox.Show("Reset");
                        break;
                }

                MainWindow.PlayerEventCountries.Clear();
                MainWindow.PlayerEventNumber = -1;
            }
            //need because MouseRightDown is not reacting always
            startRightBtn = e.GetPosition(this);
        }

        public void EnemyFleet(Country country)
        {
            EnemyFleet(country, 0, 0, 0, 0, false);
        }

        public void EnemyFleet(Country country, double scaleX, double scaleY, double transformX, double transformY, bool load)
        {
            Point end1 = new Point();
            Point end2 = new Point();
            Point start1 = new Point();
            Point start2 = new Point();

            double versatz = 3 * latestScale.ScaleX;

            if(load)
                versatz = 3 * scaleX;

            start1.X = startRightBtn.X - versatz;
            start1.Y = startRightBtn.Y - versatz;

            end1.X = startRightBtn.X + versatz;
            end1.Y = startRightBtn.Y + versatz;

            start2.X = startRightBtn.X - versatz;
            start2.Y = startRightBtn.Y + versatz;

            end2.X = startRightBtn.X + versatz;
            end2.Y = startRightBtn.Y - versatz;

            //text to show
            //== null when it was saved it is not equal null
            string txt;
            if (playerEvent == null)
            {

                txt = "Die folgende Flotten begegenen sich:\n";

                foreach (var item in MainWindow.PlayerEventCountries)
                {
                    txt += item.countryName + ":" + "\n";
                    txt += "\n";
                }
            }
            else
            {
                txt = playerEvent;
            }

            if (!load)
            {
                SaveData(country, new TwoPoints(startRightBtn, startRightBtn, country.ToSave(), latestScale.ScaleX, latestScale.ScaleY, latestTransform.X, latestTransform.Y, "EnemyFleet", txt));
            }



            //actual code
            List<ZoomBorder> list = new List<ZoomBorder>
            {
                EnemyFleet(country, start1, end1, txt, scaleX, scaleY, transformX, transformY, load),
                EnemyFleet(country, start2, end2, txt, scaleX, scaleY, transformX, transformY, load)
            };

            // add data to List
            bool isListInDic = country.Route.TryGetValue(MainWindow.Round, out List<ZoomBorder> enemyfleet);

            if (!isListInDic)
            {
                country.Route.Add(MainWindow.Round, enemyfleet = new List<ZoomBorder>());
            }

            foreach (var item in list)
            {
                enemyfleet.Add(item);
            }
        }

        private void SaveData(Country country, TwoPoints twoPoints)
        {
            //to save data
            bool isListInDic = country.RoutePoints.TryGetValue(MainWindow.Round, out List<TwoPoints> routePoints);

            if (!isListInDic)
            {
                country.RoutePoints.Add(MainWindow.Round, routePoints = new List<TwoPoints>());
            }

            routePoints.Add(twoPoints);
        }

        public ZoomBorder EnemyFleet(Country country, Point start, Point end, string txt, double scaleX, double scaleY, double transformX, double transformY, bool load)
        {
            ZoomBorder zoomBorder = new ZoomBorder();

            //mouse over
            zoomBorder.MouseDown += ZoomBorder_MouseDown;
            zoomBorder.playerEvent = txt;


            // Create a Line  
            Line line = new Line();
            line.MouseEnter += Line_MouseEnter;

            zoomBorder.Child = line;
            ChooseGameMode.mainWindow.Map.Children.Add(zoomBorder);

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

            if (load)
            {
                st.ScaleX = scaleX;
                st.ScaleY = scaleY;
                tt.X = transformX;
                tt.Y = transformY;

                SaveLatest(st, tt);
            }

            line.X1 = ((start.X - latestTransform.X) / latestScale.ScaleX);
            line.Y1 = ((start.Y - latestTransform.Y) / latestScale.ScaleY);

            line.X2 = ((end.X - latestTransform.X) / latestScale.ScaleX);
            line.Y2 = ((end.Y - latestTransform.Y) / latestScale.ScaleY);

            // Create a red Brush  
            SolidColorBrush solidColorBrush = new SolidColorBrush
            {
                Color = Colors.Black
            };

            // Set Line's width and color  
            line.StrokeThickness = 2;
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

        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            Line line = (Line)sender;

            if (MainWindow.IsCountrySelected)
                if (MainWindow.SelctedCountry.countryName.Equals("Admin"))
                    foreach (var item in MainWindow.countries)
                        if (line.Stroke.ToString().Equals(item.color.ToString()))
                            ChooseGameMode.mainWindow.txtCountryName.Text = item.countryName;
        }

        private void ZoomBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ZoomBorder tmp = (ZoomBorder)sender;
            MessageBox.Show(tmp.playerEvent);
        }

        public List<ZoomBorder> CreateALine(Country country)
        {
            return CreateALine(country, 0, 0, 0, 0, false);
        }

        public List<ZoomBorder> CreateALine(Country country, double scaleX, double scaleY, double transformX, double transformY, bool load)
        {
            //to save data
            if (MainWindow.IsCountrySelected && !load )
            {
                SaveData(MainWindow.SelctedCountry, new TwoPoints(startRightBtn, endRightBtn, country.ToSave(), latestScale.ScaleX, latestScale.ScaleY, latestTransform.X, latestTransform.Y, "Line", ""));
            }

            //actual code
            List<ZoomBorder> list = new List<ZoomBorder>
            {
                CreateALine(country, startRightBtn, endRightBtn, scaleX, scaleY, transformX, transformY, load)
            };

            int lineLength = 10;

            Point left = Pfeil(lineLength, 170, startRightBtn, endRightBtn);
            Point right = Pfeil(lineLength, 190, startRightBtn, endRightBtn);

            list.Add(CreateALine(country, endRightBtn, left, scaleX, scaleY, transformX, transformY, load));
            list.Add(CreateALine(country, endRightBtn, right, scaleX, scaleY, transformX, transformY, load));

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

        public ZoomBorder CreateALine(Country country, Point start, Point end, double scaleX, double scaleY, double transformX, double transformY, bool load)
        {
            //check for correct input (if NaN program crashes)
            if (Double.IsNaN(end.X))
                return new ZoomBorder();

            //not working correct when saving executed
            //check for correct input (if mouse right boutton up is executed over line the end Point is false defined)
            //if (!ChooseGameMode.mainWindow.Map.Children[2].IsMouseOver)
            //    return new ZoomBorder();



            ZoomBorder zoomBorder = new ZoomBorder();

            // Create a Line  
            Line line = new Line();
            line.MouseEnter += Line_MouseEnter;

            zoomBorder.Child = line;
            ChooseGameMode.mainWindow.Map.Children.Add(zoomBorder);

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

            if (load)
            {
                st.ScaleX = scaleX;
                st.ScaleY = scaleY;
                tt.X = transformX;
                tt.Y = transformY;

                SaveLatest(st, tt);
            }

            line.X1 = ((start.X - latestTransform.X) / latestScale.ScaleX);
            line.Y1 = ((start.Y - latestTransform.Y) / latestScale.ScaleY);

            line.X2 = ((end.X - latestTransform.X) / latestScale.ScaleX);
            line.Y2 = ((end.Y - latestTransform.Y) / latestScale.ScaleY);

            // Create a red Brush  
            SolidColorBrush solidColorBrush = new SolidColorBrush
            {
                Color = country.color
            };

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
