using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fietsclient
{
    static class Program
    {
        /// <summary>
        /// 'Hier begint de applicatie met draaien. hij maakt een gui aan en in het gui object geeft hij een instantie mee van AppGlobal.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(AppGlobal.Instance));

            //tijdelijke code om te testen
            Connect connect = new Connect();
            connect.LoginServer("tommy", "jemoeder");
        }
    }
}
