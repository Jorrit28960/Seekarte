using System.Windows.Forms;

namespace Seekarte
{
    struct Ship
    {
        enum Schiff { Schnellsegler, Transportschiff };
        //string name;
        //Location location;
    }

    struct Location
    {
        //private int locationX;
        //private int locationY;
    }
    class Country
    {
        private string _name;
        private string _password = "123";
        public bool passwordSet = false;

        public Country(string name)
        {
            this._name = name;
        }

        public void SetPassowrd(string _password)
        {
            if (passwordSet)
            {
                MessageBox.Show("Passwort bereits vergeben", "Passwor vergeben", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                this._password = _password;
            }
        }

        public string GetPassword()
        {
            return _password;
        }

        public string GetName()
        {
            return _name;
        }


    }
}
