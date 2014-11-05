using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CodeX.DesktopTools.Properties;
using System.Diagnostics;
using System.Threading;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;

namespace CodeX.DesktopTools
{
    public class SyncNotifyIcon
    {
        NotifyIcon ni = null;
        String siteID = "";
        public SyncNotifyIcon(NotifyIcon ni1)
        {
            siteID = ConfigurationManager.AppSettings["SiteID"];
            ni = ni1;

            string ipAddress = GetIPAddress();
            string syncIPAddress = BusinessLayer.GetSiteParameter(siteID, Constant.SiteParameter.IP_ADDRESS_SYNC).ParameterValue;

            if (syncIPAddress == "" || ipAddress == syncIPAddress)
            {
                client = new SyncService.SyncServiceSoapClient();
                client.Endpoint.Address = new System.ServiceModel.EndpointAddress(ConfigurationManager.AppSettings["SyncServiceAddress"]);
            }
        }
        public void Display()
        {
            if (client != null)
            {
                client.Join(siteID);
                Thread thread1 = new Thread(new ThreadStart(SOAPClient));
                thread1.IsBackground = true;
                thread1.Start();
            }
        }

        SyncService.SyncServiceSoapClient client = null;
        private void SOAPClient()
        {
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
            string result = "";
            try
            {
                result = client.WaitMessage(siteID);
                if (result != "")
                {
                    if (result == "openTeamViewer")
                    {
                        string fileName = @"C:\Program Files (x86)\TeamViewer\Version9\TeamViewer.exe";
                        string saveLocation = @"D:\Personal\Dropbox\Dropbox\Personal\id_teamviewer" + siteID +".jpg";

                        ProcessStartInfo processInfo = new ProcessStartInfo();
                        processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        processInfo.FileName = "cmd.exe";
                        processInfo.Arguments = string.Format("/c START \"\" \"{0}\"", fileName);
                        Process.Start(processInfo);

                        Thread.Sleep(2000);

                        Rectangle bounds = Screen.GetBounds(Point.Empty);
                        using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                        {
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                            }
                            bitmap.Save(saveLocation, ImageFormat.Jpeg);
                        }
                    }
                    else
                    {
                        ni.ShowBalloonTip(300, "New Notification", "Start Sync", ToolTipIcon.Info);
                        SyncProcess.Sync(client, siteID, result);
                        ni.ShowBalloonTip(300, "New Notification", "Sync Berhasil", ToolTipIcon.Info);
                    }
                }
            }
            catch
            {
                result = "error";
            }
            SOAPClient();
        }

        public void Dispose()
        {
            if (client != null)
                client.Fork(siteID);
        }

        private string GetIPAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
