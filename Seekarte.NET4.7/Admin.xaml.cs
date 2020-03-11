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
        public Admin(List<Country> countries)
        {
            InitializeComponent();

            this.countries = countries;

            ShowCountries();
            this.LabelRoundAdmin.Content = MainWindow.Round;

            ShowShipsInit();
        }

        private void ShowShipsInit()
        {
            //this.BtnGrid.Children.Add(tmpBtn);

            var scrollViewer = this.scrollViewer;

            scrollViewer.Children.Clear();

            ShowShipsInitFirstLine();


            //Grid.SetColumn(scrollViewer, 0);
            //Grid.SetRow(scrollViewer, 0);
            //Grid.SetColumnSpan(scrollViewer, 1);
            //Grid.SetRowSpan(scrollViewer, 2);



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

                    this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                }
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

            Label geschwader = new Label();
            geschwader.Content = "Geschwader";
            Grid.SetColumn(geschwader, 5);
            Grid.SetRow(geschwader, 0);
            this.scrollViewer.Children.Add(geschwader);

            this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });

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

                if (addShip.ShowDialog() == true)
                {
                    //SelectedCountry.Ships.Add((Linienschiff_1)addShip.selectedShip);
                }
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
            Choose.mainWindow.txtRoundCounter.Text = "Runde " + MainWindow.Round;
        }

        private void RoundDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Round--;
            this.LabelRoundAdmin.Content = MainWindow.Round;
            Choose.mainWindow.txtRoundCounter.Text = "Runde " + MainWindow.Round;
        }
    }
}
