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
        public bool isCountrySelected { get; set; }
        public Country SelectedCountry { get; set; }

        private List<Country> countries;
        public Admin(List<Country> countries)
        {
            InitializeComponent();

            this.countries = countries;

            ShowCountries();
            this.LabelRoundAdmin.Content = MainWindow.Round;
        }

        private void ShowShipsInit()
        {
            var scrollViewer = new ScrollViewer();


            if (isCountrySelected)
            {
                foreach (var item in SelectedCountry.Ships)
                {
                    Label labelName = new Label();
                    labelName.Content = item.Name;
                    Grid.SetColumn(labelName, 0);
                    Grid.SetRow(labelName, 1);
                    this.scrollViewer.Children.Add(labelName);

                    Label labelType = new Label();
                    labelType.Content = item.Typ;
                    Grid.SetColumn(labelType, 1);
                    Grid.SetRow(labelType, 1);
                    this.scrollViewer.Children.Add(labelType);

                    Label labelHitPoints = new Label();
                    labelHitPoints.Content = item.HitPoints;
                    Grid.SetColumn(labelHitPoints, 2);
                    Grid.SetRow(labelHitPoints, 1);
                    this.scrollViewer.Children.Add(labelHitPoints);
                }
            }
            Grid.SetColumn(scrollViewer, 0);
            Grid.SetRow(scrollViewer, 0);
            Grid.SetColumnSpan(scrollViewer, 1);
            Grid.SetRowSpan(scrollViewer, 2);
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
                    //MessageBox.Show(addShip.selectedShip.GetType().ToString());

                    SelectedCountry.Ships.Add((Linienschiff_1)addShip.selectedShip);
                }
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
