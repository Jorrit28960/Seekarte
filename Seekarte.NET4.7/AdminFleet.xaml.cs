using Seekarte.NET4._7;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class AdminFleet : Window
    {
        public static bool IsCountrySelected { get; set; }
        public static Country SelectedCountry { get; set; }

        private readonly List<Country> countries;
        private readonly Dictionary<Button, Ship> listButoons = new Dictionary<Button, Ship>();
        public AdminFleet(List<Country> countries)
        {
            InitializeComponent();

            this.countries = countries;

            ShowCountries();
            this.LabelRoundAdmin.Content = MainWindow.Round;
            IsCountrySelected = false;

            ShowShipsInit();
        }

        public void ShowShipsInit()
        {
            var scrollViewer = this.scrollViewer;

            scrollViewer.Children.Clear();
            ShowShipsInitFirstLine();

            if (IsCountrySelected)
            {
                for (int i = 0; i < SelectedCountry.Ships.Count; i++)
                {
                    Label labelName = new Label
                    {
                        Content = SelectedCountry.Ships[i].Name
                    };
                    Grid.SetColumn(labelName, 0);
                    Grid.SetRow(labelName, 1 + i);
                    this.scrollViewer.Children.Add(labelName);

                    Label labelType = new Label
                    {
                        Content = SelectedCountry.Ships[i].ShipType
                    };
                    Grid.SetColumn(labelType, 1);
                    Grid.SetRow(labelType, 1 + i);
                    this.scrollViewer.Children.Add(labelType);

                    Label labelHitPoints = new Label
                    {
                        Content = SelectedCountry.Ships[i].HitPoints
                    };
                    Grid.SetColumn(labelHitPoints, 2);
                    Grid.SetRow(labelHitPoints, 1 + i);
                    this.scrollViewer.Children.Add(labelHitPoints);

                    Label labelMaxHitPoints = new Label
                    {
                        Content = SelectedCountry.Ships[i].MaxHitPoints
                    };
                    Grid.SetColumn(labelMaxHitPoints, 3);
                    Grid.SetRow(labelMaxHitPoints, 1 + i);
                    this.scrollViewer.Children.Add(labelMaxHitPoints);

                    Label labelTroops = new Label
                    {
                        Content = SelectedCountry.Ships[i].Troops
                    };
                    Grid.SetColumn(labelTroops, 4);
                    Grid.SetRow(labelTroops, 1 + i);
                    this.scrollViewer.Children.Add(labelTroops);

                    Label labelMaxTroops = new Label
                    {
                        Content = SelectedCountry.Ships[i].MaxTroops
                    };
                    Grid.SetColumn(labelMaxTroops, 5);
                    Grid.SetRow(labelMaxTroops, 1 + i);
                    this.scrollViewer.Children.Add(labelMaxTroops);

                    Label labelGeschwader = new Label
                    {
                        Content = SelectedCountry.Ships[i].Geschwader
                    };
                    Grid.SetColumn(labelGeschwader, 6);
                    Grid.SetRow(labelGeschwader, 1 + i);
                    this.scrollViewer.Children.Add(labelGeschwader);

                    Button buttonConfig = new Button
                    {
                        Content = "bearbeiten"
                    };
                    Grid.SetColumn(buttonConfig, 7);
                    Grid.SetRow(buttonConfig, 1 + i);
                    this.scrollViewer.Children.Add(buttonConfig);

                    listButoons.Add(buttonConfig, SelectedCountry.Ships[i]);

                    buttonConfig.Click += ConfigShip;

                    this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
                }
            }
        }

        private void ConfigShip(object sender, RoutedEventArgs e)
        {


            if (listButoons.TryGetValue((Button)sender, out Ship ship))
            {

                var userWindow = new AdminShipEdit(SelectedCountry, ship, true)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                userWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Something went wrong :(");
            }
            ShowShipsInit();
        }

        public void ShowShipsInitFirstLine()
        {
            Label name = new Label
            {
                Content = "Schiffsname"
            };
            Grid.SetColumn(name, 0);
            Grid.SetRow(name, 0);
            this.scrollViewer.Children.Add(name);

            Label type = new Label
            {
                Content = "Typ"
            };
            Grid.SetColumn(type, 1);
            Grid.SetRow(type, 0);
            this.scrollViewer.Children.Add(type);

            Label hitpoints = new Label
            {
                Content = "Trefferpunkte"
            };
            Grid.SetColumn(hitpoints, 2);
            Grid.SetRow(hitpoints, 0);
            this.scrollViewer.Children.Add(hitpoints);

            Label maxHitpoints = new Label
            {
                Content = "Maximale Trefferpunkte"
            };
            Grid.SetColumn(maxHitpoints, 3);
            Grid.SetRow(maxHitpoints, 0);
            this.scrollViewer.Children.Add(maxHitpoints);

            Label troops = new Label
            {
                Content = "Truppen"
            };
            Grid.SetColumn(troops, 4);
            Grid.SetRow(troops, 0);
            this.scrollViewer.Children.Add(troops);

            Label maxTroops = new Label
            {
                Content = "Maximale Truppen"
            };
            Grid.SetColumn(maxTroops, 5);
            Grid.SetRow(maxTroops, 0);
            this.scrollViewer.Children.Add(maxTroops);

            Label geschwader = new Label
            {
                Content = "Geschwader"
            };
            Grid.SetColumn(geschwader, 6);
            Grid.SetRow(geschwader, 0);
            this.scrollViewer.Children.Add(geschwader);

            this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
        }

        private void ShowCountries()
        {
            List<string> countriesStrings = new List<string>();
            foreach (var item in countries)
            {
                if (item.countryName != Properties.Resources.Admin)
                    countriesStrings.Add(item.countryName);
            }

            var listbox = new ListBox
            {
                ItemsSource = countriesStrings
            };

            this.Grid.Children.Add(listbox);

            Grid.SetColumn(listbox, 0);
            Grid.SetRow(listbox, 0);
            Grid.SetColumnSpan(listbox, 1);
            Grid.SetRowSpan(listbox, 2);

            listbox.SelectionChanged += Listbox_SelectionChanged;
        }

        private void Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListBox)sender;

            IsCountrySelected = true;
            SelectedCountry = countries.Find(x => x.countryName == list.SelectedItem.ToString());

            ShowShipsInit();

            IsCountrySelected.GetType();
        }

        private void AddShip_Click(object sender, RoutedEventArgs e)
        {
            if (IsCountrySelected)
            {
                //new Window with stuff for new ship
                //MessageBox.Show("new ship");
                var addShip = new ShipSelect();

                addShip.ShowDialog();
                ShowShipsInit();
            }
            else
            {
                MessageBox.Show("No Counry selected");
            }
        }

        private void DeleteShip_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Schiff soll geslöscht werden");
        }

        private void RoundAdd_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Round++;
            this.LabelRoundAdmin.Content = MainWindow.Round;
            ChooseGameMode.mainWindow.txtRoundCounter.Text = "Runde " + MainWindow.Round;
        }

        private void RoundDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Round--;
            this.LabelRoundAdmin.Content = MainWindow.Round;
            ChooseGameMode.mainWindow.txtRoundCounter.Text = "Runde " + MainWindow.Round;
        }
    }
}