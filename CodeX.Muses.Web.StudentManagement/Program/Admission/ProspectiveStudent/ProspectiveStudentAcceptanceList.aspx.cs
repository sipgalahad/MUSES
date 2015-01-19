using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ProspectiveStudentAcceptanceList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PROSPECTIVE_STUDENT_ACCEPTANCE;
        }
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus NOT IN ('{1}','{2}','{3}')", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID, Constant.RegistrationStatus.CLOSED, Constant.RegistrationStatus.OPEN);
            List<vRegistration> lstEntity = BusinessLayer.GetvRegistrationList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRegistration entity = e.Row.DataItem as vRegistration;
                CheckBox chkIsAccepted = e.Row.FindControl("chkIsAccepted") as CheckBox;
                if (entity.GCRegistrationStatus == Constant.RegistrationStatus.SETTLED) chkIsAccepted.Enabled = true;
                else chkIsAccepted.Enabled = false;
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnProcessEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnProcessEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = true;
            try
            {
                BusinessLayer.ProcessProspectiveStudentAcceptance(hdnSelectedValue.Value,AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }

            return result;
        }
        #endregion
    }
}