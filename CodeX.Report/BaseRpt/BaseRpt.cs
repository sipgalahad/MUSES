using System;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CodeX.Common;
using CodeX.Common;

namespace CodeX.Report
{
    public partial class BaseRpt : DevExpress.XtraReports.UI.XtraReport
    {
        protected ReportMaster reportMaster = null;
        protected List<Words> words = null;
        protected AppSessionReport appSession = null;
        public BaseRpt()
        {
            InitializeComponent();
        }

        public void Init(AppSessionReport session, int reportID, string reportCode, string[] param)
        {
            appSession = session;
            reportMaster = BusinessLayer.GetReportMaster(reportID);
            InitializeReport(param);
        }

        public string GetLabel(string code)
        {
            return "";
        }

        public virtual void InitializeReport(string[] param)
        {
        }

        protected string ResolveUrl(string url)
        {
            return url.Replace("~", AppConfigManager.CDXAppVirtualDirectory);
        }
    }
}
