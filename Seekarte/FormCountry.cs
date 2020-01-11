using System;
using System.Windows.Forms;

namespace Seekarte
{
    public partial class FormCountry : Form
    {
        public FormCountry()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.formStartPage.Show();
        }
    }
}
