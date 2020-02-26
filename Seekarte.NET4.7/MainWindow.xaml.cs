using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;


namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceDictionary gameDict;
        private List<Country> countries;
        private List<Button> buttonCountries;


        public MainWindow(string game)
        {
            //Console.WriteLine(Resources.Test);

            SetGameDictionary(game);
            InitializeComponent();
            countries = InitCountries(game);
            SetBtn(countries);



            Console.WriteLine("Test");
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

        private void SetBtn(List<Country> countries)
        {
            //int start = 1;
            //Array[] array = new Array[gameDict.Values.Count];
            //gameDict.Values.CopyTo(array, 0);

            for (int i = 0; i < countries.Count; i++)
            {
                var tmpBtn = new Button();
                tmpBtn.Content = countries[i].contryName;

                tmpBtn.Click += countryButtonClick;


                //tmpBtn.Content = Resources.;
                Grid.SetRow(tmpBtn, 1 + i);
                this.BtnGrid.Children.Add(tmpBtn);
                this.BtnGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            }

            //foreach (var item in array)
            //{
            //    MessageBox.Show(item.ToString());
            //}


        }

        private void countryButtonClick(object sender, RoutedEventArgs e)
        {
            Button tmpString = (Button)sender;

            //MessageBox.Show(tmp.Content.ToString());

            Country tmpCountry = countries.Find(x => x.contryName == tmpString.Content.ToString());


            if (tmpCountry.password == null)
            {

                var dialogueNewPassword = new Dialogue("Bitte legen Sie ein Passwort fest");
                dialogueNewPassword.ShowDialog();
                tmpCountry.password = dialogueNewPassword.ResponseText;
                //if (dialogueNewPassword.ShowDialog() == true)
                //{
                //    MessageBox.Show("Your password is: " + dialogueNewPassword.ResponseText);
                //}
            }


            var dialoguePassword = new Dialogue("Geben Sie bitte Ihr Passwort ein");
            if (dialoguePassword.ShowDialog() == true && !dialoguePassword.ResponseText.Equals(tmpCountry.password))
            {
                MessageBox.Show("Das war das falsche Passwort!");
            }
            else
            {
                MessageBox.Show("looloolololo");
            }


        }

        private Predicate<T> Predicate<T>(T country)
        {
            throw new NotImplementedException();
        }

        private List<Country> InitCountries(string game)
        {
            List<Country> countries = new List<Country>();

            if (game.Equals("Risiko"))
            {
                string[] tmp = new string[] { "Admin", "Preussen", "Tartarenreich", "Spanien", "Polen" };

                foreach (var item in tmp)
                {
                    countries.Add(new Country(item));
                }
            }

            if (game.Equals("GameOfThrones"))
            {
                string[] tmp = new string[] { "Admin", "Stark", "Lennister", "Tyrell", "Targaryen", "Nachtkönig" };

                foreach (var item in tmp)
                {
                    countries.Add(new Country(item));
                }
            }

            return countries;
        }
    }
}
