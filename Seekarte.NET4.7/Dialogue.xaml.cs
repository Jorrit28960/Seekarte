using System.Windows;

namespace Seekarte.NET4._7
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Dialogue : Window
    {
        public Dialogue()
        {
            InitializeComponent();
            //TexResponseTextBoxt.Focus();
        }

        public Dialogue(string displayText)
        {
            InitializeComponent();
            this.Text.Text = displayText;

            ResponseTextBox.Focus();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ResponseTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                DialogResult = true;
                //this.Close();
            }
        }
    }
}