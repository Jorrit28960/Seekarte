using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for PasswordReset.xaml
    /// </summary>
    public partial class PasswordReset : Window
    {
        private readonly List<Button> buttonCountries = new List<Button>();
        public static Country SelctedCountry { get; set; }
        private readonly List<Country> countries;



        public PasswordReset(List<Country> countries)
        {
            InitializeComponent();
            this.countries = countries;

            SetBtn(countries);

        }

        private void SetBtn(List<Country> countries)
        {
            for (int i = 0; i < countries.Count; i++)
            {
                var tmpBtn = new Button();
                buttonCountries.Add(tmpBtn);
                tmpBtn.Content = countries[i].countryName;

                tmpBtn.Click += CountryButtonClick;

                Grid.SetRow(tmpBtn, i);
                this.BtnGrid.Children.Add(tmpBtn);
                this.BtnGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            }
        }

        private void CountryButtonClick(object sender, RoutedEventArgs e)
        {
            Button tmpButton = (Button)sender;
            Country tmpCountry = countries.Find(x => x.countryName == tmpButton.Content.ToString());
            SelctedCountry = tmpCountry;

            SelctedCountry.password = null;



        }

    }
}
