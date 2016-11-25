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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectIndicatorEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if (id == "tcs")
                return Constant.MenuCode.StudentManagement.TCS_SUBJECT_INDICATOR;
            return Constant.MenuCode.StudentManagement.WS_SUBJECT_INDICATOR;
        }

        protected override void InitializeDataControl()
        {
            CurriculumSyllabus entityIndicator = BusinessLayer.GetCurriculumSyllabusList(string.Format("CurriculumID = {0} AND GCCurriculumSyllabusType = '{1}' AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, Constant.CurriculumSyllabusType.INDICATOR)).FirstOrDefault();
            if (entityIndicator != null)
                hdnCurriculumSyllabusIndicatorID.Value = entityIndicator.CurriculumSyllabusID.ToString();
            else
                hdnCurriculumSyllabusIndicatorID.Value = "0";

            vClassSubject classSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            hdnSubjectCurriculumID.Value = classSubject.SubjectCurriculumID.ToString();

            IsLoadFirstRecord = true;

            BindGridView();

            Helper.SetControlEntrySetting(txtSubjectCurriculumSyllabusName, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectCurriculumID = {0} AND GCCurriculumSyllabusType = '{1}' AND IsAllowTask = 1 AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumSyllabusType.INDICATOR);
            List<vSubjectCurriculumSyllabus> lstEntity = BusinessLayer.GetvSubjectCurriculumSyllabusList(filterExpression);
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

        private void ControlToEntity(SubjectCurriculumSyllabus entity)
        {
            entity.SubjectCurriculumSyllabusName = txtSubjectCurriculumSyllabusName.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectCurriculumSyllabus entity = new SubjectCurriculumSyllabus();
                ControlToEntity(entity);
                entity.SubjectCurriculumID = Convert.ToInt32(hdnSubjectCurriculumID.Value);
                entity.CurriculumSyllabusID = Convert.ToInt32(hdnCurriculumSyllabusIndicatorID.Value);
                entity.IsAllowTask = true;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertSubjectCurriculumSyllabus(entity);
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
                SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCurriculumSyllabus(entity);
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
                SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateSubjectCurriculumSyllabus(entity);
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