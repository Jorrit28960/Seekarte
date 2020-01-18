using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SeekarteXAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string game)
        {
            SetGameDictionary(game);
            InitializeComponent();
        }

        private void SetGameDictionary(string game)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (game)
            {
                case "Risiko":
                    dict.Source = new Uri("..\\Resources\\Risiko.xaml", UriKind.Relative);
                    break;
                case "GameOfThrones":
                    dict.Source = new Uri("..\\Resources\\GameOfThrones.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\Risiko.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);
        }

        private void btnPreussen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCountry4_Click(object sender, RoutedEventArgs e)
        {
            Line line = new Line();
            Thickness thickness = new Thickness(101, -11, 362, 250);
            line.Margin = thickness;
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 4;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = 10;
            line.X2 = 40;
            line.Y1 = 70;
            line.Y2 = 70;

            Map.Children.Add(line);
            //Window win = Window.GetWindow(this);

            //win.
        }
    }

    //Class that implements the image zoom and pan
    public class ZoomBorder : Border
    {
        static List<UIElement> list = new List<UIElement>();
        static List<Point> points = new List<Point>();
        static List<ZoomBorder> zoomBorders = new List<ZoomBorder>();
        private UIElement child = null;
        private Point origin;
        private Point start;

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
                    list.Add(value);
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
            foreach (var child in list)
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

                    //center image if maximum size
                    tt.X = (st.ScaleX + zoom >= 1) ? absoluteX - relative.X * st.ScaleX : 0;
                    tt.Y = (st.ScaleY + zoom >= 1) ? absoluteY - relative.Y * st.ScaleY : 0;

                }
            }

        }

        private void Child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                points.Clear();
                foreach (var child in list)
                {
                    var tmp = GetTranslateTransform(child);
                    origin = new Point(tmp.X, tmp.Y);
                    points.Add(origin);
                }
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
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
            }
        }

        void Child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.Reset();

            var aBackground = this.Background;
            CreateALine();


        }

        public void CreateALine()
        {
            ZoomBorder zoomBorder = new ZoomBorder();
            //zoomBorder.BorderThickness = "0,1,1,0";

            //Grid.Row = "1"
            //Grid.Column = "0"
            //Grid.ColumnSpan = "4"
            //Grid.RowSpan = "2"

            // Create a Line  
            Line redLine = new Line();
            redLine.X1 = 50;
            redLine.Y1 = 50;
            redLine.X2 = 200;
            redLine.Y2 = 50;

            Line redLine2 = new Line();
            redLine2.X1 = 210;
            redLine2.Y1 = 50;
            redLine2.X2 = 400;
            redLine2.Y2 = 200;


            // Create a red Brush  
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color  
            redLine.StrokeThickness = 4;
            redLine.Stroke = redBrush;

            redLine2.StrokeThickness = 4;
            redLine2.Stroke = redBrush;

            // Add line to the Grid. 
            //zoomBorder.AddLogicalChild(redLine);
            //zoomBorder.AddVisualChild(redLine);
            zoomBorder.Child = redLine;
            //zoomBorder.child = redLine;
            //zoomBorder.Initialize(redLine);
            Choose.mainWindow.Map.Children.Add(zoomBorder);
            //Choose.mainWindow.Map.Children.Add(redLine2);
            var a = Choose.mainWindow.Map.Children.Count;
            //var ab = Choose.mainWindow.Map.Children[3].ToString();
            //var abc = Choose.mainWindow.Map.s;
            Grid.SetColumn(zoomBorder, 0);
            Grid.SetRow(zoomBorder, 1);
            Grid.SetColumnSpan(zoomBorder, 4);
            Grid.SetRowSpan(zoomBorder, 4);
            MessageBox.Show(a.ToString());
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

            if (list.Count > 0 && points.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var achild = list[i];
                    var aorigin = points[i];

                    if (achild != null)
                    {
                        if (child.IsMouseCaptured)
                        {
                            var tt = GetTranslateTransform(achild);
                            Vector v = start - e.GetPosition(zoomBorders[i]);
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
