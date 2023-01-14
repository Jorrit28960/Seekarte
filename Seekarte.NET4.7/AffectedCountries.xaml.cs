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
    /// Interaction logic for AffectedCountries.xaml
    /// </summary>
    public partial class AffectedCountries : Window
    {
        public List<Country> affectedCountries = new List<Country>();
        List<CheckBox> checkedCountries = new List<CheckBox>();

        public AffectedCountries()
        {
            InitializeComponent();
            //SetLabels(MainWindow.countries);
            SetCheckBox(MainWindow.countries);
        }

        private void SetCheckBox(List<Country> countries)
        {
            for (int i = 0; i < countries.Count; i++)
            {
                var tmpCheckBox = new CheckBox();
                checkedCountries.Add(tmpCheckBox);
                tmpCheckBox.Content = countries[i].countryName;

                //tmpBtn.Click += CountryButtonClick;

                Grid.SetRow(tmpCheckBox, i);
                Grid.SetColumn(tmpCheckBox, 0);
                this.CheckBoxGrid.Children.Add(tmpCheckBox);
                this.CheckBoxGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            }
        }
        private void SetLabels(List<Country> countries)
        {
            for (int i = 0; i < countries.Count; i++)
            {
                var tmpLabel = new Label();
                //buttonCountries.Add(tmpBtn);
                tmpLabel.Content = countries[i].countryName;

                //tmpBtn.Click += CountryButtonClick;

                Grid.SetRow(tmpLabel, i);
                Grid.SetColumn(tmpLabel, 1);
                this.BtnGrid.Children.Add(tmpLabel);
                this.BtnGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            affectedCountries = new List<Country>();
            foreach (var item in checkedCountries)
                foreach (var country in MainWindow.countries)
                    if (item.IsChecked == true && item.Content.Equals(country.countryName))
                        affectedCountries.Add(country);

            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
