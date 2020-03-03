using System.Windows;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for ShipSelect.xaml
    /// </summary>
    public partial class ShipSelect : Window
    {
        public Ship selectedShip { get; set; }
        public ShipSelect()
        {
            InitializeComponent();
        }

        private void AddShip_Click(object sender, RoutedEventArgs e)
        {
            selectedShip = new Linienschiff_1();
            DialogResult = true;
        }
    }
}
