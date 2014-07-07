using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Blitzschutz_Rechner
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BlitzschutzRechner());
        }
    }
}
