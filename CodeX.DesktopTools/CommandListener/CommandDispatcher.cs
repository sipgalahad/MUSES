// MedinfrasDesktopTools
//
// by Mark Gladding
// Copyright 2009 Tumbywood Software
// http://www.text2go.com
//
// You are free to reuse this code in any commercial or non-commercial work.
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace CodeX.DesktopTools
{
    public class CommandDispatcher
    {
        public static void ProcessCommand(NotifyIcon ni, NameValueCollection queryCollection)
        {
            string type = queryCollection["type"];
            Process process = new Process();
            if (type == "report")
            {
                string siteID = queryCollection["siteid"];
                string userID = queryCollection["userid"];
                string userName = queryCollection["username"];
                string userFullName = queryCollection["userfullname"];

                string reportID = queryCollection["reportid"];
                string param = queryCollection["param"];
                string fileName = ConfigurationManager.AppSettings["ReportViewerApp"];

                string[] args = { siteID, userID, userName, userFullName, reportID, param };

                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.FileName = "cmd.exe";
                processInfo.Arguments = string.Format("/c START \"\" \"{0}\" \"{1}\"", fileName, String.Join("\" \"", args));
                Process.Start(processInfo);
            }
            else if (type == "sync")
            {
                string syncType = queryCollection["synctype"];

                string siteID = ConfigurationManager.AppSettings["SiteID"];
                SyncService.SyncServiceSoapClient client = new SyncService.SyncServiceSoapClient();
                client.Endpoint.Address = new System.ServiceModel.EndpointAddress(ConfigurationManager.AppSettings["SyncServiceAddress"]);
                
                ni.ShowBalloonTip(300, "New Notification", "Start Sync", ToolTipIcon.Info);
                SyncProcess.Sync(client, siteID, syncType);
                ni.ShowBalloonTip(300, "New Notification", "Sync Berhasil", ToolTipIcon.Info);
            }
        }
    }
}
