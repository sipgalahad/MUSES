using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using System.Reflection;
using CodeX.Report;
using CodeX.Web.Common.UI;
using CodeX.Common;
using CodeX.Web.Common;

namespace CodeX.Web.CommonLibs.Program
{
    public partial class ReportViewer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["param"] != null)
                hdnParam.Value = Request.Form["param"].ToString();
            string[] param = hdnParam.Value.Split('|');

            string reportCode = Page.Request.QueryString["id"];
            List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
            if (lstReportMaster.Count < 1)
                throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
            ReportMaster reportMaster = lstReportMaster[0];
            string reportClassName = reportMaster.ClassName;
            BaseRpt report = GetReport(reportClassName);
            AppSessionReport appSession = new AppSessionReport();
            appSession.SiteID = AppSession.UserLogin.SiteID;
            appSession.UserID = AppSession.UserLogin.UserID;
            appSession.UserName = AppSession.UserLogin.UserName;
            appSession.UserFullName = AppSession.UserLogin.UserFullName;
            report.Init(appSession, reportMaster.ReportID, reportCode, param);
            this.ReportViewer1.Report = report;
            //if (Page.IsCallback)
            //{
            //    string[] param = hdnParam.Value.Split('|');

            //    string reportCode = Page.Request.QueryString["id"];
            //    List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
            //    if (lstReportMaster.Count < 1)
            //        throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
            //    ReportMaster reportMaster = lstReportMaster[0];
            //    BaseRpt report = GetReport(reportMaster.ClassName);
            //    report.Init(reportMaster.ReportID, reportCode, param, this);
            //    this.ReportViewer1.Report = report;                
            //}
            //else
            //    if (Request.Form["param"] != null)
            //        hdnParam.Value = Request.Form["param"].ToString();
        }

        public BaseRpt GetReport(string className)
        {
            Assembly assembly = Assembly.Load("CodeX.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            Object o = assembly.CreateInstance("CodeX.Report." + className);
            return (BaseRpt)o;
        }
    }
}