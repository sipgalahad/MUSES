using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CodeX.DesktopTools;

namespace CodeX.DesktopTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static NotifyIcon ni;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ni = new NotifyIcon();
            // Show the system tray icon.					
            using (ProcessIcon pi = new ProcessIcon(ni))
            {
                pi.Display();

                // Make sure the application runs!
                StartHttpCommandDispatcher();
                Application.Run();
            }
            //Application.Run(new MainForm());
        }

        private static void StartHttpCommandDispatcher()
        {
            HttpCommandDispatcher mCmdDispatcher = new HttpCommandDispatcher(ni, Properties.Resources.dummy);
            mCmdDispatcher.AddResourceLocator(new ImageLocator(Properties.Resources.ResourceManager));
            mCmdDispatcher.Start("http://localhost:60025/");
        }

    }
}
