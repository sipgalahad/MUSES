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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectCurriculumSyllabusEntryDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            hdnSubjectCurriculumID.Value = temp[1];
            hdnCurriculumSyllabusID.Value = temp[2];
            hdnParentID.Value = temp[3];
            hdnIsPerSchoolTimeUnit.Value = temp[4];
            hdnCurriculumSchoolTimeUnitID.Value = temp[5];

            if (hdnIsPerSchoolTimeUnit.Value == "1")
                txtSchoolTimeUnitName.Text = BusinessLayer.GetCurriculumSchoolTimeUnit(Convert.ToInt32(hdnCurriculumSchoolTimeUnitID.Value)).CurriculumSchoolTimeUnitName;
            else
                trSchoolTimeUnit.Style.Add("display", "none");

            vCurriculumSyllabus entityDt = BusinessLayer.GetvCurriculumSyllabusList(String.Format("CurriculumSyllabusID = {0}", hdnCurriculumSyllabusID.Value)).FirstOrDefault();
            txtType.Text = entityDt.CurriculumSyllabusName;
            if (!entityDt.IsUsingCode)
                trCode.Style.Add("display", "none");
            if (entityDt.ReferenceID == 0)
                trReferenceID.Style.Add("display", "none");
            else
            {
                lblReference.InnerHtml = entityDt.ReferenceName;
                List<vSubjectCurriculumSyllabus> lstReference = BusinessLayer.GetvSubjectCurriculumSyllabusList(string.Format("SubjectID = {0} AND CurriculumSyllabusID = {1} AND IsDeleted = 0", AppSession.SubjectID, entityDt.ReferenceID));
                Methods.SetComboBoxField<vSubjectCurriculumSyllabus>(cboReferenceID, lstReference, "SubjectCurriculumSyllabusCode", "SubjectCurriculumSyllabusID");
            }
            hdnIsUsingCode.Value = entityDt.IsUsingCode ? "1" : "0";

            if (temp[0] == "edit")
            {
                hdnSubjectCurriculumSyllabusID.Value = temp[6];
                hdnIsAdd.Value = "0";

                SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnSubjectCurriculumSyllabusID.Value));
                txtSubjectCurriculumSyllabusCode.Text = entity.SubjectCurriculumSyllabusCode;
                txtSubjectCurriculumSyllabusName.Text = entity.SubjectCurriculumSyllabusName;
                txtRemarks.Text = entity.Remarks;
                cboReferenceID.Value = entity.ReferenceID.ToString();
            }
            else
            {
                hdnSubjectCurriculumSyllabusID.Value = "0";
            }

            Helper.SetControlEntrySetting(txtSubjectCurriculumSyllabusCode, new ControlEntrySetting(true, true, true), "mpEntryPopup");
            Helper.SetControlEntrySetting(txtSubjectCurriculumSyllabusName, new ControlEntrySetting(true, true, true), "mpEntryPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpEntryPopup");
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            string param = e.Parameter;

            string result = param + "|";
            string errMessage = "";

            if (hdnIsAdd.Value.ToString() == "0")
            {
                if (OnSaveEditRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (OnSaveAddRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;

        }

        #region CRUD Process Method
        private void ControlToEntity(SubjectCurriculumSyllabus entity)
        {
            entity.SubjectCurriculumSyllabusCode = txtSubjectCurriculumSyllabusCode.Text;
            entity.SubjectCurriculumSyllabusName = txtSubjectCurriculumSyllabusName.Text;
            if (cboReferenceID.Value != null && cboReferenceID.Value.ToString() != "")
                entity.ReferenceID = Convert.ToInt32(cboReferenceID.Value);
            else
                entity.ReferenceID = null;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumSyllabusDao entityDao = new SubjectCurriculumSyllabusDao(ctx);
            try
            {
                SubjectCurriculumSyllabus entity = new SubjectCurriculumSyllabus();
                ControlToEntity(entity);
                if (hdnParentID.Value != "")
                    entity.ParentID = Convert.ToInt32(hdnParentID.Value);
                else
                    entity.ParentID = null;
                if (hdnIsPerSchoolTimeUnit.Value == "1")
                    entity.CurriculumSchoolTimeUnitID = Convert.ToInt32(hdnCurriculumSchoolTimeUnitID.Value);
                else
                    entity.CurriculumSchoolTimeUnitID = null;
                entity.SubjectCurriculumID = Convert.ToInt32(hdnSubjectCurriculumID.Value);
                entity.CurriculumSyllabusID = Convert.ToInt32(hdnCurriculumSyllabusID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnSubjectCurriculumSyllabusID.Value));
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
        #endregion
    }
}