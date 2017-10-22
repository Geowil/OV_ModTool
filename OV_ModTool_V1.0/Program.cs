using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OV_ModTool_V1._0.Properties;
namespace OV_ModTool_V1._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!(bool)Settings.Default["bALDisabled"])
            {
                Application.Run(new autoload());
            }
            else
            {
                Application.Run(new MainWin());
            }
        }
    }
}
