using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            {"Polen1", Colors.Brown },
            {"Polen2", Colors.Brown },
            {"Polen3", Colors.Brown },
            {"Polen4", Colors.Brown },
            {"Polen5", Colors.Brown }
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
        public List<Country> countries;
        private readonly List<Button> buttonCountries = new List<Button>();
        private bool normalModus = true;
        private readonly string game;
        public static int Round { get; set; } = 1;
        public static bool IsCountrySelected { get; set; }
        public static Country SelctedCountry { get; set; }
        public static int PlayerEventNumber { get; set; }
        public static List<Country> PlayerEventCountries { get; set; } = new List<Country>();

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
                    AdminWindow();

                if (tmpButton.Content.ToString().Contains(Properties.Resources.PasswordResetBtn))
                    PasswordResetWindwow();

                if (tmpButton.Content.ToString().Contains(Properties.Resources.PasswordSetBtn))
                    PasswordSetWindwow();

                if (tmpButton.Content.ToString().Contains(Properties.Resources.SaveData))
                {
                    //DeleteZoomBorders();
                    DeleteLines(countries);

                    var dateString = DateTime.Now.ToString().Replace(".", "_").Replace(":", "_").Replace(" ", "_");

                    SaveData<List<Country>>("test" + dateString + ".json", countries);
                    SaveLines(countries);
                }

                if (tmpButton.Content.ToString().Contains(Properties.Resources.ReadData))
                {
                    DeleteZoomBorders();

                    countries = null;

                    countries = ReadData<List<Country>>();
                    if (countries != null)
                    {
                        SaveLines(countries);
                    }
                }

                if (tmpButton.Content.ToString().Contains(Properties.Resources.Event))
                    CreatePlayerEvent();
            }


        }

        //Admin Buttons

        private void AdminWindow()
        {
            var adminWindow = new AdminFleet(countries)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            adminWindow.ShowDialog();
        }

        private void PasswordResetWindwow()
        {
            var window = new PasswordReset(countries)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }
        private void PasswordSetWindwow()
        {
            var window = new PasswordSet(countries, this)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        private void CreatePlayerEvent()
        {
            PlayerEventNumber = -1;
            var window = new PlayerEventOverview()
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (window.ShowDialog() == true)
            {
                PlayerEventNumber = window.PlayerEventReturn;
            }

            List<string> players = new List<string>();
            players.Add("Preussen");
            players.Add("Spanien");

            foreach (var country in players)
            {
                Country tmpCountry = countries.Find(x => x.countryName == country);
                PlayerEventCountries.Add(tmpCountry);
            }
        }

        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void SaveData<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                MessageBox.Show("Done");
            }
        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadData<T>() where T : new()
        {
            TextReader reader = null;
            string filePath = ChooseFile();

            if (filePath == null)
            {
                return default(T);
            }



            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                MessageBox.Show("Done");
            }
        }

        private static string ChooseFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "Database files (*.json)|*.json";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return null;
            }
        }

        private void DeleteZoomBorders()
        {
            var children = ChooseGameMode.mainWindow.Map.Children;

            children.RemoveRange(3, children.Count - 3);
        }

        private void AddZoomBorders()
        {

            foreach (var country in countries)
            {
                if (country.Route == null)
                    break;

                foreach (var dicZoomBorder in country.Route)
                {
                    foreach (var zoomBorder in dicZoomBorder.Value)
                    {
                        ChooseGameMode.mainWindow.Map.Children.Add(zoomBorder);
                    }
                }

            }
        }

        private void DeleteLines(List<Country> list)
        {
            try
            {
                foreach (var country in list)
                    country.Route = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SaveLines(List<Country> list)
        {
            List<ZoomBorder> zoomBorders;
            foreach (var country in list)
            {
                country.Route = new Dictionary<int, List<ZoomBorder>>();

                foreach (var dicRoutePoints in country.RoutePoints)
                {
                    country.Route.Add(dicRoutePoints.Key, zoomBorders = new List<ZoomBorder>());

                    //used ToList() because it seemed having sometimes issues while execute the loop because the value changed during execution
                    foreach (var routePoints in dicRoutePoints.Value.ToList())
                    {
                        if (routePoints.type.Equals("Line"))
                        {
                            MessageBox.Show("Line");

                            ZoomBorder tmp = new ZoomBorder();
                            tmp.startRightBtn = routePoints.startPoint;
                            tmp.endRightBtn = routePoints.endPoint;

                            //foreach (var item in tmp.CreateALine(country.color))
                            foreach (var item in tmp.CreateALine(country.color, routePoints.scaleX, routePoints.scaleY, routePoints.transformX, routePoints.transformY, true))
                            {
                                zoomBorders.Add(item);
                            }
                        }

                        if (routePoints.type.Equals("EnemyFleet"))
                        {
                            MessageBox.Show("EnemyFleet");

                            ZoomBorder tmp = new ZoomBorder();
                            tmp.startRightBtn = routePoints.startPoint;
                            tmp.playerEvent = routePoints.eventText;

                            tmp.EnemyFleet(country, routePoints.scaleX, routePoints.scaleY, routePoints.transformX, routePoints.transformY, true);
                        }

                    }
                }
            }
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
            buttonCountries[3].Visibility = Visibility.Visible;
            buttonCountries[3].Content = Properties.Resources.PasswordResetBtn;
            buttonCountries[4].Visibility = Visibility.Visible;
            buttonCountries[4].Content = Properties.Resources.PasswordSetBtn;
            buttonCountries[5].Visibility = Visibility.Visible;
            buttonCountries[5].Content = Properties.Resources.SaveData;
            buttonCountries[6].Visibility = Visibility.Visible;
            buttonCountries[6].Content = Properties.Resources.ReadData;
            buttonCountries[7].Visibility = Visibility.Visible;
            buttonCountries[7].Content = Properties.Resources.Event;


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
        public void SetPassword(Country tmpCountry)
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
            var userWindow = new UserFleet(SelctedCountry)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            userWindow.ShowDialog();
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
