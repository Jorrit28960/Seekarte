using System.Windows;

namespace SeekarteXAML
{
    /// <summary>
    /// Interaction logic for Choose.xaml
    /// </summary>
    public partial class Choose : Window
    {
        public Choose()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenMain("Risiko");
        }

        private void OpenMain(string game)
        {
            var mainWindow = new MainWindow(game);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenMain("GameOfThrones");
        }
    }
}
