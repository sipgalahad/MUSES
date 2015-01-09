using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ProspectiveStudentFormEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.PROSPECTIVE_STUDENT_FORM;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("FormID = {0}", Convert.ToInt32(ID));
                ProspectiveStudentForm entity = BusinessLayer.GetProspectiveStudentFormList(filterExpression)[0];
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            
            txtFormCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtFormCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFormName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ProspectiveStudentForm entity)
        {
            txtFormCode.Text = entity.FormCode;
            txtFormName.Text = entity.FormName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(ProspectiveStudentForm entity)
        {
            entity.FormCode = txtFormCode.Text;
            entity.FormName = txtFormName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("FormCode = '{0}'", txtFormCode.Text);
            List<ProspectiveStudentForm> lst = BusinessLayer.GetProspectiveStudentFormList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Form with Code " + txtFormCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 ID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("FormCode = '{0}' AND FormID != {1}", txtFormCode.Text, ID);
            List<ProspectiveStudentForm> lst = BusinessLayer.GetProspectiveStudentFormList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Form with Code " + txtFormCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentFormDao entityDao = new ProspectiveStudentFormDao(ctx);
            bool result = false;
            try
            {
                ProspectiveStudentForm entity = new ProspectiveStudentForm();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetProspectiveStudentFormMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            try
            {
                ProspectiveStudentForm entity = BusinessLayer.GetProspectiveStudentForm(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProspectiveStudentForm(entity);
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
            }

            return result;
        }
    }
}