﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class UserFleet : Window
    {
        public static Country SelectedCountry { get; set; }

        private readonly Dictionary<Button, Ship> listButoons = new Dictionary<Button, Ship>();
        public UserFleet(Country _SelectedCountry)
        {
            InitializeComponent();

            SelectedCountry = _SelectedCountry;

            //ShowCountries();
            this.LabelRoundAdmin.Content = MainWindow.Round;

            ShowShipsInit();
        }

        public void ShowShipsInit()
        {
            var scrollViewer = this.scrollViewer;

            scrollViewer.Children.Clear();
            ShowShipsInitFirstLine();


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

        private void ConfigShip(object sender, RoutedEventArgs e)
        {


            if (listButoons.TryGetValue((Button)sender, out Ship ship))
            {
                UserShipEdit _shipEdit = new UserShipEdit(SelectedCountry, ship, false);
                _shipEdit.Show();
            }
            else
            {
                MessageBox.Show("Something went wrong :(");
            }
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