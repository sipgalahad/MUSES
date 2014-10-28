using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CodeX.DesktopTools.Properties;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

namespace CodeX.DesktopTools
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        public ProcessIcon()
        {
            ni = new NotifyIcon();
        }

        public void Display()
        {
            client.Join("001.01.01");		
            ni.Icon = Resources.icon;
            ni.Text = "CODEX Desktop Tools";
            ni.Visible = true;
            ni.DoubleClick += new EventHandler(ni_DoubleClick);
            ni.BalloonTipClicked += new EventHandler(ni_BalloonTipClicked);

            Thread thread1 = new Thread(new ThreadStart(SOAPClient));
            thread1.IsBackground = true;
            thread1.Start();
            ni.ContextMenuStrip = new ContextMenus().Create();
        }

        private void ni_BalloonTipClicked(object sender, EventArgs e)
        {
            //string url = "http://localhost/medinfrasv2/dev/Inventory/Program/WareHouse/ItemOrder/ItemRequestApprovalList.aspx";
            //System.Diagnostics.Process.Start(url);
            //Form1 frm = new Form1();
            //frm.Show();
        }

        private void ni_DoubleClick(object sender, EventArgs e)
        {
            //Form1 frm = new Form1();
            //frm.Show();
        }

        SyncService.SyncServiceSoapClient client = new SyncService.SyncServiceSoapClient();
        private void SOAPClient()
        {
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(ConfigurationManager.AppSettings["ReportViewerApp"]);
            string result = "";
            try
            {
                result = client.WaitMessage("001.01.01");
                if (result != "")
                {
                    ni.ShowBalloonTip(300, "New Notification", "Start Sync", ToolTipIcon.Info);
                    SyncProcess.SyncItem(client);
                    ni.ShowBalloonTip(300, "New Notification", "Sync Berhasil", ToolTipIcon.Info);
                }
            }
            catch
            {
                result = "error";
            }
            //MessageBox.Show(result);
            SOAPClient();
            //textBox1.Text = result;
        }

        public void Dispose()
        {
            client.Fork("001.01.01");
            // When the application closes, this will remove the icon from the system tray immediately.
            ni.Dispose();
        }
    }
}
