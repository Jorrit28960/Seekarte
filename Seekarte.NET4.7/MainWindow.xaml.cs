using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;


namespace Seekarte.NET4._7
{


    //const string abmelden = Properties.Resources.Abmelden;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] risikoStrings = new string[] { "Admin", "Preussen", "Tartarenreich", "Spanien", "Polen" };
        string[] gameOfThronesStrings = new string[] { "Admin", "Stark", "Lennister", "Tyrell", "Targaryen", "Nachtkönig" };

        private ResourceDictionary gameDict;
        private List<Country> countries;
        private List<Button> buttonCountries = new List<Button>();
        private bool normalModus = true;
        private string game;
        public static int round { get; set; } = 1;

        public static bool IsCountrySelected { get; set; }
        public static Country SelctedCountry { get; set; }


        public MainWindow(string game)
        {
            this.game = game;
            SetGameDictionary();
            InitializeComponent();
            countries = InitCountries();
            SetBtn(countries);
        }

        private void SetGameDictionary()
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

        private void BtnPreussen_Click(object sender, RoutedEventArgs e)
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

        private void BtnCountry4_Click(object sender, RoutedEventArgs e)
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
            //MessageBox.Show("Admin");
        }

        private void SetBtn(List<Country> countries)
        {
            //int start = 1;
            //Array[] array = new Array[gameDict.Values.Count];
            //gameDict.Values.CopyTo(array, 0);

            for (int i = 0; i < countries.Count; i++)
            {
                var tmpBtn = new Button();
                buttonCountries.Add(tmpBtn);
                tmpBtn.Content = countries[i].countryName;

                tmpBtn.Click += CountryButtonClick;


                //tmpBtn.Content = Resources.;
                Grid.SetRow(tmpBtn, i);
                this.BtnGrid.Children.Add(tmpBtn);
                this.BtnGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            }
        }

        private void CountryButtonClick(object sender, RoutedEventArgs e)
        {
            Button tmpButton = (Button)sender;
            if (normalModus)
            {
                Country tmpCountry = countries.Find(x => x.countryName == tmpButton.Content.ToString());
                SelctedCountry = tmpCountry;

                foreach (var item in buttonCountries)
                {
                    item.IsEnabled = false;
                }

                if (tmpCountry.password == null)
                    SetPassword(tmpCountry);

                if (RequestPassword(tmpCountry))
                {
                    if (SelctedCountry.countryName == Properties.Resources.Admin)
                    {
                        Admin();
                    }
                    else
                    {
                        Country();

                    }
                }
                foreach (var item in buttonCountries)
                {
                    item.IsEnabled = true;
                }



            }
            else
            {
                //switch (tmpString.Content.ToString())
                //{
                //    case Properties.Resources.Abmelden:
                //        Abmelden();
                //        break;
                //    default:
                //        break;
                //}



                if (tmpButton.Content.ToString() == Properties.Resources.Abmelden)
                {
                    Abmelden();
                    normalModus = true;
                    IsCountrySelected = false;
                }

                if (tmpButton.Content.ToString() == Properties.Resources.Flotte)
                    Flotte();

                if (tmpButton.Content.ToString().Contains(Properties.Resources.Geschwader))
                    Geschwader();

                if (tmpButton.Content.ToString().Contains(Properties.Resources.PasswortÄndern))
                {
                    foreach (var item in buttonCountries)
                    {
                        item.IsEnabled = false;
                    }

                    SetPassword(SelctedCountry);

                    foreach (var item in buttonCountries)
                    {
                        item.IsEnabled = true;
                    }
                }

                if (tmpButton.Content.ToString().Contains(Properties.Resources.Country))
                {
                    AdminWindow();
                }






            }


        }

        private void AdminWindow()
        {
            var adminWindow = new Admin(countries);
            adminWindow.Owner = this;
            adminWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            adminWindow.ShowDialog();
        }

        private void Country()
        {
            foreach (var item in buttonCountries)
            {
                item.Visibility = Visibility.Hidden;
            }

            buttonCountries[0].Visibility = Visibility.Visible;
            buttonCountries[0].Content = Properties.Resources.Abmelden;
            buttonCountries[1].Visibility = Visibility.Visible;
            buttonCountries[1].Content = Properties.Resources.Flotte;
            buttonCountries[2].Visibility = Visibility.Visible;
            buttonCountries[2].Content = Properties.Resources.PasswortÄndern;


            foreach (var item in SelctedCountry.zoomBorders)
            {
                item.Visibility = Visibility.Visible;
            }

            normalModus = false;
        }

        private void Admin()
        {
            foreach (var item in buttonCountries)
            {
                item.Visibility = Visibility.Hidden;
            }

            buttonCountries[0].Visibility = Visibility.Visible;
            buttonCountries[0].Content = Properties.Resources.Abmelden;
            buttonCountries[1].Visibility = Visibility.Visible;
            buttonCountries[1].Content = Properties.Resources.Country;
            buttonCountries[2].Visibility = Visibility.Visible;
            buttonCountries[2].Content = Properties.Resources.PasswortÄndern;


            foreach (var item in SelctedCountry.zoomBorders)
            {
                item.Visibility = Visibility.Visible;
            }

            normalModus = false;

        }

        private void Geschwader()
        {
            throw new NotImplementedException();
        }

        private bool RequestPassword(Country tmpCountry)
        {
            var dialoguePassword = new Dialogue(Properties.Resources.PasswordRequest);
            dialoguePassword.Owner = this;
            dialoguePassword.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (dialoguePassword.ShowDialog() == true && dialoguePassword.ResponseText.Equals(tmpCountry.password))
            {
                IsCountrySelected = true;
                return true;
            }
            else
            {
                MessageBox.Show(Properties.Resources.PasswordWrong);
                return false;
            }
        }
        private void SetPassword(Country tmpCountry)
        {
            var dialogueNewPassword = new Dialogue(Properties.Resources.PasswordSet);
            dialogueNewPassword.Owner = this;
            dialogueNewPassword.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialogueNewPassword.ShowDialog();
            tmpCountry.password = dialogueNewPassword.ResponseText;
        }

        private void Abmelden()
        {
            for (int i = 0; i < buttonCountries.Count; i++)
            {
                buttonCountries[i].Content = countries[i].countryName;
                buttonCountries[i].Visibility = Visibility.Visible;
            }

            foreach (var item in SelctedCountry.zoomBorders)
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        private void Flotte()
        {
            throw new NotImplementedException();
        }

        private List<Country> InitCountries()
        {
            List<Country> countries = new List<Country>();

            if (game.Equals("Risiko"))
                foreach (var item in risikoStrings)
                    countries.Add(new Country(item));

            if (game.Equals("GameOfThrones"))
                foreach (var item in gameOfThronesStrings)
                    countries.Add(new Country(item));

            return countries;
        }
    }

    public class Fleet
    {
        public List<Linienschiff_1> linienschiff_1 { get; set; } = new List<Linienschiff_1>();
    }

    public abstract class Ship
    {
        public int HitPoints { get; set; }
        public string Name { get; set; }
        public int Troops { get; set; }
        public abstract int MaxHitPoints { get; }
        public abstract string Typ { get; }
    }

    public class Linienschiff_1 : Ship
    {
        public override int MaxHitPoints { get { return 40; } }
        public override string Typ { get { return "Linienschiff 1. Klasse"; } }

        public Linienschiff_1()
        {
            HitPoints = MaxHitPoints;
            Troops = 0;
            Name = "Linienschiff 1. Klasse";
        }

    }

    public class Linienschiff_2 : Ship
    {
        public override int MaxHitPoints { get { return 30; } }
        public override string Typ { get { return "Linienschiff 2. Klasse"; } }

        public Linienschiff_2()
        {
            HitPoints = MaxHitPoints;
            Troops = 0;
            Name = "Linienschiff 2. Klasse";
        }

    }
}
