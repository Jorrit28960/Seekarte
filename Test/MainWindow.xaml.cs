using System.Windows;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var a = Resources.Keys.Count;
            MessageBox.Show(a.ToString());
            //var a = Resources.test;
        }
    }
}
