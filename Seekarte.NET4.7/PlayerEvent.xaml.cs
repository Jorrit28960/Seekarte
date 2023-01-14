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
using System.Windows.Shapes;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for PlayerEvent.xaml
    /// </summary>
    public partial class PlayerEventOverview : Window
    {
        public int PlayerEventReturn { get; set; }
        public List<Country> affectedCountries;
        
        public PlayerEventOverview()
        {
            InitializeComponent();
        }

        private void EnemyFleet_Click(object sender, RoutedEventArgs e)
        {
            PlayerEventReturn = 2;

            var affectedCountriesWindow = new AffectedCountries()
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (affectedCountriesWindow.ShowDialog() == true)
            {
                affectedCountries = affectedCountriesWindow.affectedCountries;
                this.DialogResult = true;
            }

            this.Close();
        }
    }
}
