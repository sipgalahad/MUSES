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
    public partial class ProspectiveStudentList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_PROSPECTIVE_STUDENT;
        }
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus != '{1}'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID);
            List<vRegistration> lstEntity = BusinessLayer.GetvRegistrationList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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
            if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RegistrationDao entityRegistrationDao = new RegistrationDao(ctx);
            ProspectiveStudentDao entityDao = new ProspectiveStudentDao(ctx);
            bool result = true;
            try
            {
                Registration entityRegistration = entityRegistrationDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ProspectiveStudent entity = entityDao.Get(entityRegistration.ProspectiveStudentID);
                if (entity.PeriodAdmissionID == AppSession.PeriodAdmissionID)
                {
                    entity.GCProspectiveStudentStatus = Constant.ProspectiveStudentStatus.VOID;
                    entityDao.Update(entity);
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                }
                entityRegistration.GCRegistrationStatus = Constant.RegistrationStatus.VOID;
                entityRegistration.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityRegistrationDao.Update(entityRegistration);
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