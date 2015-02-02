using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SchoolPeriodClosingEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SCHOOL_PERIOD_CLOSING;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            SchoolPeriod entity = BusinessLayer.GetSchoolPeriodList(String.Format("SiteID = '{0}' AND GCSchoolPeriodStatus = '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.START)).FirstOrDefault();
            if (entity != null) 
            {
                txtCurrSchoolPeriod.Text = entity.SchoolPeriodName;
                hdnID.Value = entity.SchoolPeriodID.ToString();
            }

            PeriodSection entitySemester = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = '{0}' AND GCPeriodSectionStatus = '{1}'", entity.SchoolPeriodID, Constant.SchoolPeriodStatus.START)).FirstOrDefault();
            if (entitySemester != null) 
            {
                txtCurrPeriodSection.Text = entitySemester.PeriodSectionName;
                hdnCurrPeriodSectionID.Value = entitySemester.PeriodSectionID.ToString();
            } 

            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(String.Format("SiteID = '{0}' AND GCSchoolPeriodStatus = '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.OPEN));
            lstSchoolPeriod.Insert(0, entity);
            Methods.SetComboBoxField(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            cboSchoolPeriod.SelectedIndex = 0;

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = '{0}' AND GCPeriodSectionStatus = '{1}'", entity.SchoolPeriodID, Constant.SchoolPeriodStatus.OPEN));
            Methods.SetComboBoxField(cboPeriodSection, lstPeriodSection, "PeriodSectionName", "PeriodSectionID");
            cboPeriodSection.SelectedIndex = 0;
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "process")
            {
                if (OnClosingSchoolPeriod(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else 
            {
                List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = '{0}' AND GCPeriodSectionStatus = '{1}'", cboSchoolPeriod.Value, Constant.SchoolPeriodStatus.OPEN));
                Methods.SetComboBoxField(cboPeriodSection, lstPeriodSection, "PeriodSectionName", "PeriodSectionID");
                cboPeriodSection.SelectedIndex = 0;
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnClosingSchoolPeriod(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SchoolPeriodDao schoolPeriodDao = new SchoolPeriodDao(ctx);
            PeriodSectionDao periodSectionDao = new PeriodSectionDao(ctx);
            try
            {
                if (hdnID.Value != cboSchoolPeriod.Value.ToString())
                {
                    SchoolPeriod entity = schoolPeriodDao.Get(Convert.ToInt32(hdnID.Value));
                    entity.GCSchoolPeriodStatus = Constant.SchoolPeriodStatus.END;
                    schoolPeriodDao.Update(entity);

                    SchoolPeriod sp2 = schoolPeriodDao.Get(Convert.ToInt32(cboSchoolPeriod.Value));
                    sp2.GCSchoolPeriodStatus = Constant.SchoolPeriodStatus.START;
                    schoolPeriodDao.Update(sp2);
                }
                if (hdnCurrPeriodSectionID.Value != cboPeriodSection.Value.ToString())
                {
                    PeriodSection ps1 = periodSectionDao.Get(Convert.ToInt32(hdnCurrPeriodSectionID.Value));
                    ps1.GCPeriodSectionStatus = Constant.SchoolPeriodStatus.END;
                    periodSectionDao.Update(ps1);

                    PeriodSection ps2 = periodSectionDao.Get(Convert.ToInt32(cboPeriodSection.Value));
                    ps2.GCPeriodSectionStatus = Constant.SchoolPeriodStatus.START;
                    periodSectionDao.Update(ps2);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }
    }
}