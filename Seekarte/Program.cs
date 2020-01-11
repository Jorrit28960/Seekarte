using System;
using System.Windows.Forms;

namespace Seekarte
{
    static class Program
    {
        public static FormStartPage formStartPage;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Jorrit's Comment
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formStartPage = new FormStartPage();
            Application.Run(formStartPage);
        }
    }
}
