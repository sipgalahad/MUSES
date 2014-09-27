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
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show the system tray icon.					
            using (ProcessIcon pi = new ProcessIcon())
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
            mCmdDispatcher.AddResourceLocator(new ImageLocator(Properties.Resources.ResourceManager));
            mCmdDispatcher.Start("http://localhost:60025/");
        }

        private static HttpCommandDispatcher mCmdDispatcher = new HttpCommandDispatcher(Properties.Resources.dummy);
    }
}
