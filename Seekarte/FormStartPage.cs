using System;
using System.Windows.Forms;

namespace Seekarte
{
    public partial class FormStartPage : Form
    {
        public FormStartPage()
        {

            InitializeComponent();
        }

        private void ButtonAdmin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!", "test merge", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonPolen_Click(object sender, EventArgs e)
        {
            FunctionLayouts.ActivateCountry(sender as Button, e);
        }

        private void FormStartPage_Load(object sender, EventArgs e)
        {
            //shows map on application start
            //resize is not working properly in this case
            FunctionLayouts.ResizeImage();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            FunctionLayouts.ResizeImage();
        }
    }
}
