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
    public partial class SchoolClassEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_CLASS;
        }
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", hdnSiteID.Value);
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", hdnSiteID.Value);
        }
        protected override void InitializeDataControl()
        {
            List<vPeriodClassType> lstClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vPeriodClassType>(cboClassType, lstClassType, "CurriculumClassTypeName", "PeriodClassTypeID");
            cboClassType.SelectedIndex = 0;

            hdnSiteID.Value = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID).SiteID;

            hdnMaxStudent.Value = BusinessLayer.GetSiteParameter(hdnSiteID.Value, Constant.SiteParameter.MAX_STUDENT).ParameterValue;

            BindGridView();

            Helper.SetControlEntrySetting(tacRoom, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacTeacher, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(tacAssistantTeacher, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtMaxStudent, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if(cboClassType.Value != null && cboClassType.Value.ToString() != "0")
                filterExpression = string.Format("SchoolPeriodID = {0} AND PeriodClassTypeID = {1} AND IsDeleted = 0", AppSession.SchoolPeriodID, cboClassType.Value);
            List<vSchoolClass> lstEntity = BusinessLayer.GetvSchoolClassList(filterExpression);
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

        private void ControlToEntity(SchoolClass entity)
        {
            entity.SchoolClassCode = txtSchoolClassCode.Text;
            entity.SchoolClassName = txtSchoolClassName.Text;
            entity.RoomID = Convert.ToInt32(tacRoom.Value);
            entity.TeacherID = Convert.ToInt32(tacTeacher.Value);
            if (tacAssistantTeacher.Value == "" || tacAssistantTeacher.Value == "0")
                entity.AssistantTeacherID = null;
            else
                entity.AssistantTeacherID = Convert.ToInt32(tacAssistantTeacher.Value);
            entity.MaxStudent = Convert.ToInt16(txtMaxStudent.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SchoolClass entity = new SchoolClass();
                ControlToEntity(entity);
                entity.PeriodClassTypeID = Convert.ToInt32(cboClassType.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertSchoolClass(entity);
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
                SchoolClass entity = BusinessLayer.GetSchoolClass(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSchoolClass(entity);
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
                SchoolClass entity = BusinessLayer.GetSchoolClass(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateSchoolClass(entity);
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