using System;
using System.Windows;
using System.Windows.Shapes;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDictionary gameDict;

        public MainWindow(string game)
        {
            SetGameDictionary(game);
            InitializeComponent();
            SetBtn(5);
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

            gameDict = dict;
        }

        private void btnPreussen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogue();
            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show("You said: " + dialog.ResponseText);
            }

            var dialog2 = new Dialogue("Hallo, geben SIe bitte Ihr Passwort ein");
            if (dialog2.ShowDialog() == true)
            {
                MessageBox.Show("You said: " + dialog2.ResponseText);
            }
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

        private void AdminBtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Admin");
        }

        private void SetBtn(int number)
        {
            //int start = 1;
            //Array[] array = new Array[gameDict.Values.Count];
            //gameDict.Values.CopyTo(array, 0);

            //for (int i = start; i < number + start; i++)
            //{
            //    var tmpBtn = new Button();
            //    tmpBtn.Content = "testdgfhgjhkjlkö";


            //    //tmpBtn.Content = Resources.;
            //    Grid.SetRow(tmpBtn, 7 + i);
            //    this.BtnGrid.Children.Add(tmpBtn);
            //    this.BtnGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            //}

            //foreach (var item in array)
            //{
            //    MessageBox.Show(item.ToString());
            //}
        }
    }
}
