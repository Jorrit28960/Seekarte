using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for ShipSelect.xaml
    /// </summary>
    public partial class ShipSelect : Window
    {
        public Ship selectedShip { get; set; }

        private List<Ship> list;
        private Dictionary<Button, string> DictButton = new Dictionary<Button, string>();
        public ShipSelect()
        {
            InitializeComponent();

            InitializeShipList();

            CreateButtons();
        }

        private void CreateButtons()
        {
            var subClassTypes = Assembly.GetAssembly(typeof(Ship)).GetTypes().Where(t => t.IsSubclassOf(typeof(Ship)));

            for (int i = 0; i < subClassTypes.Count(); i++)
            {
                var tmpLabel = new Label();
                tmpLabel.Content = subClassTypes.ElementAt(i).ToString();

                Grid.SetRow(tmpLabel, i + 1);
                Grid.SetColumn(tmpLabel, 0);
                this.scrollViewer.Children.Add(tmpLabel);
                this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });

                var tmpButton = new Button();
                tmpButton.Content = "hinzufügen";

                Grid.SetRow(tmpButton, i + 1);
                Grid.SetColumn(tmpButton, 1);
                this.scrollViewer.Children.Add(tmpButton);
                this.scrollViewer.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });


                tmpButton.Click += AddSHipButtonClick;

                DictButton.Add(tmpButton, (string)tmpLabel.Content);
            }
        }

        private void AddSHipButtonClick(object sender, RoutedEventArgs e)
        {
            Button tmpButton = (Button)sender;
            string typeString;
            if (DictButton.TryGetValue(tmpButton, out typeString))
            {

                foreach (var item in list)
                {
                    if (item.GetType().ToString().Equals(typeString))
                    {
                        AdminFleet.SelectedCountry.Ships.Add(item);

                        list.Clear();
                        InitializeShipList();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Something is wrong with the buttons :0");
            }
        }

        private void InitializeShipList()
        {
            list = new List<Ship>{
                { new Linienschiff_1()},
                { new Linienschiff_2()},
                { new Linienschiff_3()}
            };
        }

        private void AddShip_Click(object sender, RoutedEventArgs e)
        {
            selectedShip = new Linienschiff_1();
            DialogResult = true;
        }
    }
}
