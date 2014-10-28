using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CodeX.DesktopTools.Properties;
using System.Diagnostics;

namespace CodeX.DesktopTools
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        SyncNotifyIcon syncNotifyIcon;
        public ProcessIcon(NotifyIcon ni1)
        {
            ni = ni1;
            syncNotifyIcon = new SyncNotifyIcon(ni);
        }

        public void Display()
        {
            syncNotifyIcon.Display();

            ni.Icon = Resources.icon;
            ni.Text = "CODEX Desktop Tools";
            ni.Visible = true;
            ni.DoubleClick += new EventHandler(ni_DoubleClick);
            ni.BalloonTipClicked += new EventHandler(ni_BalloonTipClicked);

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

        public void Dispose()
        {
            syncNotifyIcon.Dispose();
            ni.Dispose();
        }
    }
}
