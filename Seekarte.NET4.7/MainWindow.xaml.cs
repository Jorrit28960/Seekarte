using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Seekarte.NET4._7
{


    //const string abmelden = Properties.Resources.Abmelden;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Color> risikoDic = new Dictionary<string, Color>
        {
            {"Admin", Colors.Transparent },
            {"Preussen", Colors.Red },
            {"Tartarenreich", Colors.Blue },
            {"Spanien", Colors.Blue },
            {"Polen", Colors.Brown }
        };

        private readonly Dictionary<string, Color> GOTDic = new Dictionary<string, Color>
        {
            {"Admin", Colors.Transparent },
            {"Stark", Colors.Red },
            {"Lennister", Colors.Blue },
            {"Tyrell", Colors.Brown },
            {"Targaryen", Colors.Violet },
            {"Nachtkönig", Colors.Yellow}
        };

        private ResourceDictionary gameDict;
        private readonly List<Country> countries;
        private readonly List<Button> buttonCountries = new List<Button>();
        private bool normalModus = true;
        private readonly string game;
        public static int Round { get; set; } = 1;
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


        private void SetBtn(List<Country> countries)
        {
            for (int i = 0; i < countries.Count; i++)
            {
                var tmpBtn = new Button();
                buttonCountries.Add(tmpBtn);
                tmpBtn.Content = countries[i].countryName;

                tmpBtn.Click += CountryButtonClick;

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
                if (tmpButton.Content.ToString() == Properties.Resources.Abmelden)
                {
                    Abmelden();
                    normalModus = true;
                    IsCountrySelected = false;
                }

                if (tmpButton.Content.ToString() == Properties.Resources.Flotte)
                {
                    Flotte();
                }

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
            var adminWindow = new Admin(countries)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
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

            foreach (var item in SelctedCountry.Route)
            {
                foreach (var item2 in SelctedCountry.Route[item.Key])
                {
                    //item2.solidColorBrush.Opacity = 1 / (double) (2 * (Round - item.Key));
                    //item2.Line.Stroke.Opacity = 1 / (double)(2 * (Round - item.Key));

                    item2.Opacity = 1 / (double)(4 * (Round - item.Key));
                    item2.Visibility = Visibility.Visible;
                }
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

            foreach (var country in countries)
            {
                foreach (var route in country.Route)
                {
                    foreach (var item in country.Route[route.Key])
                    {
                        item.Opacity = 1 / (double)(4 * (Round - route.Key));
                        item.Visibility = Visibility.Visible;

                    }
                }
            }

            normalModus = false;
        }

        private void Geschwader()
        {
            throw new NotImplementedException();
        }

        private bool RequestPassword(Country tmpCountry)
        {
            var dialoguePassword = new Dialogue(Properties.Resources.PasswordRequest)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
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
            var dialogueNewPassword = new Dialogue(Properties.Resources.PasswordSet)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
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

            foreach (var country in countries)
            {
                foreach (var route in country.Route)
                {
                    foreach (var item in country.Route[route.Key])
                    {
                        item.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void Flotte()
        {
            var test = new Dialogue("Hi")
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            test.ShowDialog();

            //throw new NotImplementedException();
        }

        private List<Country> InitCountries()
        {
            List<Country> countries = new List<Country>();

            if (game.Equals("Risiko"))
                foreach (var item in risikoDic)
                    countries.Add(new Country(item.Key, item.Value));

            if (game.Equals("GameOfThrones"))
                foreach (var item in GOTDic)
                    countries.Add(new Country(item.Key, item.Value));

            return countries;
        }
    }
}
