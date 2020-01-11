using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace Seekarte
{
    public partial class Form1 : Form
    {
        Country polen = new Country("polen");

        public Form1()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!", "test merge", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string pwdInput;
            if (!polen.passwordSet)
            {
                var pwdOutput = Interaction.InputBox("Bitte legen Sie ihr Passwort fest", "Passwort", "");
                polen.SetPassowrd(pwdOutput);
                //MessageBox.Show("Bitte legen Sien ein Passwort fest", "Enter Password", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }
            //MessageBox.Show(test, "Enter Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            do
            {
                pwdInput = Interaction.InputBox("Bitte geben Sie ihr Passwort ein", "Passwort", "");
            } while (pwdInput != polen.GetPassword());


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double width = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Width;
            double height = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Height;
            double ratio = height / width;

            double ratioWidth = pictureBox1.Width / width;
            double ratioHeight = pictureBox1.Height / height;

            if (ratioWidth > ratioHeight)
            {
                pictureBox1.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, (int)Math.Round(pictureBox1.Height / ratio), pictureBox1.Height);
            }
            else
            {
                pictureBox1.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, pictureBox1.Width, (int)Math.Round(pictureBox1.Width * ratio));
            }

            //pictureBox1.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, pictureBox1.Width, (int)Math.Round(ratio * pictureBox1.Height));
            //this.pictureBox1.Image = global::Seekarte.Properties.Resources.amslernetz;
            //this.pictureBox1.Image = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44;




        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            double width = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Width;
            double height = global::Seekarte.Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44.Height;
            double ratio = height / width;

            double ratioWidth = pictureBox1.Width / width;
            double ratioHeight = pictureBox1.Height / height;

            if (ratioWidth > ratioHeight)
            {
                pictureBox1.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, (int)Math.Round(pictureBox1.Height / ratio), pictureBox1.Height);
            }
            else
            {
                pictureBox1.Image = Functions.ResizeImage(Properties.Resources.WhatsApp_Image_2020_01_08_at_22_27_44, pictureBox1.Width, (int)Math.Round(pictureBox1.Width * ratio));
            }
        }
    }
}
