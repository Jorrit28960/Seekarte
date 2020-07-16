using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public static bool isCountrySelected { get; set; }
        public static Country SelectedCountry { get; set; }

        private List<Country> countries;
        private Dictionary<Button, Ship> listButoons = new Dictionary<Button, Ship>();
        public Admin(List<Country> countries)
        {
            InitializeComponent();

            this.countries = countries;

            ShowCountries();
            this.LabelRoundAdmin.Content = MainWindow.Round;
            isCountrySelected = false;

            ShowShipsInit();
        }

        private void ShowShipsInit()
        {
            var scrollViewer = this.scrollViewer;

            scrollViewer.Children.Clear();
            ShowShipsInitFirstLine();

            if (isCountrySelected)
            {
                for (int i = 0; i < SelectedCountry.Ships.Count; i++)
                {
                    Label labelName = new Label();
                    labelName.Content = SelectedCountry.Ships[i].Name;
                    Grid.SetColumn(labelName, 0);
                    Grid.SetRow(labelName, 1 + i);
                    this.scrollViewer.Children.Add(labelName);

                    Label labelType = new Label();
                    labelType.Content = SelectedCountry.Ships[i].ShipType;
                    Grid.SetColumn(labelType, 1);
                    Grid.SetRow(labelType, 1 + i);
                    this.scrollViewer.Children.Add(labelType);

                    Label labelHitPoints = new Label();
                    labelHitPoints.Content = SelectedCountry.Ships[i].HitPoints;
                    Grid.SetColumn(labelHitPoints, 2);
                    Grid.SetRow(labelHitPoints, 1 + i);
                    this.scrollViewer.Children.Add(labelHitPoints);

                    Label labelMaxHitPoints = new Label();
                    labelMaxHitPoints.Content = SelectedCountry.Ships[i].MaxHitPoints;
                    Grid.SetColumn(labelMaxHitPoints, 3);
                    Grid.SetRow(labelMaxHitPoints, 1 + i);
                    this.scrollViewer.Children.Add(labelMaxHitPoints);

                    Label labelTroops = new Label();
                    labelTroops.Content = SelectedCountry.Ships[i].Troops;
                    Grid.SetColumn(labelTroops, 4);
                    Grid.SetRow(labelTroops, 1 + i);
                    this.scrollViewer.Children.Add(labelTroops);

                    Label labelMaxTroops = new Label();
                    labelMaxTroops.Content = SelectedCountry.Ships[i].MaxTroops;
                    Grid.SetColumn(labelMaxTroops, 5);
                    Grid.SetRow(labelMaxTroops, 1 + i);
                    this.scrollViewer.Children.Add(labelMaxTroops);

                    Label labelGeschwader = new Label();
                    labelGeschwader.Content = SelectedCountry.Ships[i].Geschwader;
                    Grid.SetColumn(labelGeschwader, 6);
                    Grid.SetRow(labelGeschwader, 1 + i);
                    this.scrollViewer.Children.Add(labelGeschwader);

                    Button buttonConfig = new Button();
                    buttonConfig.Content = "bearbeiten";
                    Grid.SetColumn(buttonConfig, 6);
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
            Ship ship;
            if (listButoons.TryGetValue((Button)sender, out ship))
            {
                MessageBox.Show(ship.Name);
            }
        }

        private void ShowShipsInitFirstLine()
        {
            Label name = new Label();
            name.Content = "Schiffsname";
            Grid.SetColumn(name, 0);
            Grid.SetRow(name, 0);
            this.scrollViewer.Children.Add(name);

            Label type = new Label();
            type.Content = "Typ";
            Grid.SetColumn(type, 1);
            Grid.SetRow(type, 0);
            this.scrollViewer.Children.Add(type);

            Label hitpoints = new Label();
            hitpoints.Content = "Trefferpunkte";
            Grid.SetColumn(hitpoints, 2);
            Grid.SetRow(hitpoints, 0);
            this.scrollViewer.Children.Add(hitpoints);

            Label maxHitpoints = new Label();
            maxHitpoints.Content = "Maximale Trefferpunkte";
            Grid.SetColumn(maxHitpoints, 3);
            Grid.SetRow(maxHitpoints, 0);
            this.scrollViewer.Children.Add(maxHitpoints);

            Label troops = new Label();
            troops.Content = "Truppen";
            Grid.SetColumn(troops, 4);
            Grid.SetRow(troops, 0);
            this.scrollViewer.Children.Add(troops);

            Label maxTroops = new Label();
            maxTroops.Content = "Maximale Truppen";
            Grid.SetColumn(maxTroops, 5);
            Grid.SetRow(maxTroops, 0);
            this.scrollViewer.Children.Add(maxTroops);

            Label geschwader = new Label();
            geschwader.Content = "Geschwader";
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

            var listbox = new ListBox();
            listbox.ItemsSource = countriesStrings;

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

            isCountrySelected = true;
            SelectedCountry = countries.Find(x => x.countryName == list.SelectedItem.ToString());

            ShowShipsInit();

            isCountrySelected.GetType();
        }

        private void AddShip_Click(object sender, RoutedEventArgs e)
        {
            if (isCountrySelected)
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
