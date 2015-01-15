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
    public partial class ProspectiveStudentPastStudyDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_TYPE));
                Methods.SetComboBoxField<StandardCode>(cboSchoolType, lstSc, "StandardCodeName", "StandardCodeID");
                cboSchoolType.SelectedIndex = 0;

                String ID = Request.QueryString["id"];
                Registration registration = BusinessLayer.GetRegistration(Convert.ToInt32(ID));
                hdnID.Value = registration.ProspectiveStudentID.ToString();
                BindGridView();

                Helper.SetControlEntrySetting(cboSchoolType, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtSchoolName, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtStartYear, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtEndYear, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("ProspectiveStudentID = {0} AND IsDeleted = 0", hdnID.Value);
            List<vProspectiveStudentPastStudy> lstEntity = BusinessLayer.GetvProspectiveStudentPastStudyList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(ProspectiveStudentPastStudy entity)
        {
            entity.GCSchoolType = cboSchoolType.Value.ToString();
            entity.SchoolName = txtSchoolName.Text;
            entity.StartYear = Convert.ToInt32(txtStartYear.Text);
            entity.EndYear = Convert.ToInt32(txtEndYear.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentPastStudy entity = new ProspectiveStudentPastStudy();
                ControlToEntity(entity);
                entity.ProspectiveStudentID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertProspectiveStudentPastStudy(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentPastStudy entity = BusinessLayer.GetProspectiveStudentPastStudy(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProspectiveStudentPastStudy(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ProspectiveStudentPastStudy entity = BusinessLayer.GetProspectiveStudentPastStudy(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateProspectiveStudentPastStudy(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}