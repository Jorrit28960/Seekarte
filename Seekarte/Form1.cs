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
    }
}
