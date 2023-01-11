using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class ShipEdit : Window
    {
        public static Country SelectedCountry { get; set; }
        private static Ship Ship { get; set; }
        private static int AdminControl { get; set; }
        private readonly List<TextBox> textboxes = new List<TextBox>();

        public ShipEdit(Country _SelectedCountry, Ship _Ship, int _AdminControl)
        {
            InitializeComponent();

            var scrollViewer = this.scrollViewer;

            scrollViewer.Children.Clear();
            ShowShipsInitFirstLine();

            SelectedCountry = _SelectedCountry;
            Ship = _Ship;
            AdminControl = _AdminControl;

            ShowShip(Ship);
            EnterChange(Ship);

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                Ship newShip = new Ship
                {
                    Name = textboxes[0].Text,
                    ShipType = textboxes[1].Text,
                    HitPoints = Int32.Parse(textboxes[2].Text),
                    MaxHitPoints = Int32.Parse(textboxes[3].Text),
                    Troops = Int32.Parse(textboxes[4].Text),
                    MaxTroops = Int32.Parse(textboxes[5].Text),
                    Geschwader = Int32.Parse(textboxes[6].Text)
                };

                SelectedCountry.Ships.Remove(Ship);
                SelectedCountry.Ships.Add(newShip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falsche Eingabe: " + ex.ToString());
            }



            MessageBox.Show("save");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cancel");
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

        private void ShowShip(Ship Ship)
        {
            Label labelName = new Label
            {
                Content = Ship.Name
            };
            Grid.SetColumn(labelName, 0);
            Grid.SetRow(labelName, 1);
            this.scrollViewer.Children.Add(labelName);

            Label labelType = new Label
            {
                Content = Ship.ShipType
            };
            Grid.SetColumn(labelType, 1);
            Grid.SetRow(labelType, 1);
            this.scrollViewer.Children.Add(labelType);

            Label labelHitPoints = new Label
            {
                Content = Ship.HitPoints
            };
            Grid.SetColumn(labelHitPoints, 2);
            Grid.SetRow(labelHitPoints, 1);
            this.scrollViewer.Children.Add(labelHitPoints);

            Label labelMaxHitPoints = new Label
            {
                Content = Ship.MaxHitPoints
            };
            Grid.SetColumn(labelMaxHitPoints, 3);
            Grid.SetRow(labelMaxHitPoints, 1);
            this.scrollViewer.Children.Add(labelMaxHitPoints);

            Label labelTroops = new Label
            {
                Content = Ship.Troops
            };
            Grid.SetColumn(labelTroops, 4);
            Grid.SetRow(labelTroops, 1);
            this.scrollViewer.Children.Add(labelTroops);

            Label labelMaxTroops = new Label
            {
                Content = Ship.MaxTroops
            };
            Grid.SetColumn(labelMaxTroops, 5);
            Grid.SetRow(labelMaxTroops, 1);
            this.scrollViewer.Children.Add(labelMaxTroops);

            Label labelGeschwader = new Label
            {
                Content = Ship.Geschwader
            };
            Grid.SetColumn(labelGeschwader, 6);
            Grid.SetRow(labelGeschwader, 1);
            this.scrollViewer.Children.Add(labelGeschwader);
        }

        private void EnterChange(Ship Ship)
        {
            TextBox Name = new TextBox
            {
                Text = Ship.Name
            };
            Grid.SetColumn(Name, 0);
            Grid.SetRow(Name, 2);
            this.scrollViewer.Children.Add(Name);
            textboxes.Add(Name);

            TextBox Type = new TextBox
            {
                Text = Ship.ShipType
            };
            Grid.SetColumn(Type, 1);
            Grid.SetRow(Type, 2);
            this.scrollViewer.Children.Add(Type);
            textboxes.Add(Type);

            TextBox HitPoints = new TextBox
            {
                Text = Ship.HitPoints.ToString(),
            };
            Grid.SetColumn(HitPoints, 2);
            Grid.SetRow(HitPoints, 2);
            this.scrollViewer.Children.Add(HitPoints);
            textboxes.Add(HitPoints);

            TextBox MaxHitPoints = new TextBox
            {
                Text = Ship.MaxHitPoints.ToString(),
            };
            Grid.SetColumn(MaxHitPoints, 3);
            Grid.SetRow(MaxHitPoints, 2);
            this.scrollViewer.Children.Add(MaxHitPoints);
            textboxes.Add(MaxHitPoints);

            TextBox Troops = new TextBox
            {
                Text = Ship.Troops.ToString(),
            };
            Grid.SetColumn(Troops, 4);
            Grid.SetRow(Troops, 2);
            this.scrollViewer.Children.Add(Troops);
            textboxes.Add(Troops);

            TextBox MaxTroops = new TextBox
            {
                Text = Ship.MaxTroops.ToString(),
            };
            Grid.SetColumn(MaxTroops, 5);
            Grid.SetRow(MaxTroops, 2);
            this.scrollViewer.Children.Add(MaxTroops);
            textboxes.Add(MaxTroops);

            TextBox Geschwader = new TextBox
            {
                Text = Ship.Geschwader.ToString(),
            };
            Grid.SetColumn(Geschwader, 6);
            Grid.SetRow(Geschwader, 2);
            this.scrollViewer.Children.Add(Geschwader);
            textboxes.Add(Geschwader);
        }
    }
}
