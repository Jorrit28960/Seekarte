using System.Windows;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaction logic for Choose.xaml
    /// </summary>
    public partial class Choose : Window
    {
        public static MainWindow mainWindow;
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
            mainWindow = new MainWindow(game);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenMain("GameOfThrones");
        }
    }
}
