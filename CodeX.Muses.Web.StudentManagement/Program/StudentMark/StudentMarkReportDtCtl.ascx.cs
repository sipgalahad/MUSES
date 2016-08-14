using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using CodeX.Web.CustomControl;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentMarkReportDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                string moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                List<vCurriculumReport> lstReport = BusinessLayer.GetvCurriculumReportList(string.Format("CurriculumID = {0} AND GCReportType = '{1}' ORDER BY DisplayOrder", param, Constant.CurriculumReportType.RAPOR));
                rptPrint.DataSource = lstReport;
                rptPrint.DataBind();
            }
        }
    }
}