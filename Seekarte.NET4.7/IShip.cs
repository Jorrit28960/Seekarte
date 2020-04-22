using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    class IShip : Window
    {
        public Dictionary<Button, Ship> listButoons { get; } = new Dictionary<Button, Ship>();
        //private readonly Dictionary<Button, Ship> listButoons = new Dictionary<Button, Ship>();
        public void ShowShipsInit(Country country, bool isCountrySelected, Grid scrollViewer)
        {
            //var scrollViewer = scrollViewer;

            scrollViewer.Children.Clear();
            ShowShipsInitFirstLine(scrollViewer);

            if (isCountrySelected)
            {
                for (int i = 0; i < country.Ships.Count; i++)
                {
                    Label labelName = new Label();
                    labelName.Content = country.Ships[i].Name;
                    Grid.SetColumn(labelName, 0);
                    Grid.SetRow(labelName, 1 + i);
                    scrollViewer.Children.Add(labelName);

                    Label labelType = new Label();
                    labelType.Content = country.Ships[i].ShipType;
                    Grid.SetColumn(labelType, 1);
                    Grid.SetRow(labelType, 1 + i);
                    scrollViewer.Children.Add(labelType);

                    Label labelHitPoints = new Label();
                    labelHitPoints.Content = country.Ships[i].HitPoints;
                    Grid.SetColumn(labelHitPoints, 2);
                    Grid.SetRow(labelHitPoints, 1 + i);
                    scrollViewer.Children.Add(labelHitPoints);

                    Label labelMaxHitPoints = new Label();
                    labelMaxHitPoints.Content = country.Ships[i].MaxHitPoints;
                    Grid.SetColumn(labelMaxHitPoints, 3);
                    Grid.SetRow(labelMaxHitPoints, 1 + i);
                    scrollViewer.Children.Add(labelMaxHitPoints);

                    Label labelTroops = new Label();
                    labelTroops.Content = country.Ships[i].Troops;
                    Grid.SetColumn(labelTroops, 4);
                    Grid.SetRow(labelTroops, 1 + i);
                    scrollViewer.Children.Add(labelTroops);

                    Label labelMaxTroops = new Label();
                    labelMaxTroops.Content = country.Ships[i].MaxTroops;
                    Grid.SetColumn(labelMaxTroops, 5);
                    Grid.SetRow(labelMaxTroops, 1 + i);
                    scrollViewer.Children.Add(labelMaxTroops);

                    Label labelGeschwader = new Label();
                    labelGeschwader.Content = country.Ships[i].Geschwader;
                    Grid.SetColumn(labelGeschwader, 6);
                    Grid.SetRow(labelGeschwader, 1 + i);
                    scrollViewer.Children.Add(labelGeschwader);

                    Button buttonConfig = new Button();
                    buttonConfig.Content = "bearbeiten";
                    Grid.SetColumn(buttonConfig, 6);
                    Grid.SetRow(buttonConfig, 1 + i);
                    scrollViewer.Children.Add(buttonConfig);

                    listButoons.Add(buttonConfig, country.Ships[i]);

                    buttonConfig.Click += ConfigShip;


                    scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
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

        private void ShowShipsInitFirstLine(Grid scrollViewer)
        {
            Label name = new Label();
            name.Content = "Schiffsname";
            Grid.SetColumn(name, 0);
            Grid.SetRow(name, 0);
            scrollViewer.Children.Add(name);

            Label type = new Label();
            type.Content = "Typ";
            Grid.SetColumn(type, 1);
            Grid.SetRow(type, 0);
            scrollViewer.Children.Add(type);

            Label hitpoints = new Label();
            hitpoints.Content = "Trefferpunkte";
            Grid.SetColumn(hitpoints, 2);
            Grid.SetRow(hitpoints, 0);
            scrollViewer.Children.Add(hitpoints);

            Label maxHitpoints = new Label();
            maxHitpoints.Content = "Maximale Trefferpunkte";
            Grid.SetColumn(maxHitpoints, 3);
            Grid.SetRow(maxHitpoints, 0);
            scrollViewer.Children.Add(maxHitpoints);

            Label troops = new Label();
            troops.Content = "Truppen";
            Grid.SetColumn(troops, 4);
            Grid.SetRow(troops, 0);
            scrollViewer.Children.Add(troops);

            Label maxTroops = new Label();
            maxTroops.Content = "Maximale Truppen";
            Grid.SetColumn(maxTroops, 5);
            Grid.SetRow(maxTroops, 0);
            scrollViewer.Children.Add(maxTroops);

            Label geschwader = new Label();
            geschwader.Content = "Geschwader";
            Grid.SetColumn(geschwader, 6);
            Grid.SetRow(geschwader, 0);
            scrollViewer.Children.Add(geschwader);

            scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
        }

    }
}
