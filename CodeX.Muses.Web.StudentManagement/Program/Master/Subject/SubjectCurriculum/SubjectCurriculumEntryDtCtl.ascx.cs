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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCurriculumEntryDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');

            hdnSubjectCurriculumID.Value = temp[1];
            hdnCurriculumDtID.Value = temp[2];
            if (temp[0] == "edit")
            {
                hdnSubjectCurriculumDtID.Value = temp[3];
                hdnIsAdd.Value = "0";

                SubjectCurriculumDt entity = BusinessLayer.GetSubjectCurriculumDt(Convert.ToInt32(hdnSubjectCurriculumDtID.Value));
                txtSubjectCurriculumDtName.Text = entity.SubjectCurriculumDtName;
                txtRemarks.Text = entity.Remarks;
            }
            else
            {
                hdnSubjectCurriculumDtID.Value = "0";
            }

            CurriculumDt entityDt = BusinessLayer.GetCurriculumDt(Convert.ToInt32(hdnCurriculumDtID.Value));
            txtType.Text = entityDt.CurriculumDtName;

            Helper.SetControlEntrySetting(txtSubjectCurriculumDtName, new ControlEntrySetting(true, true, true), "mpEntryPopup");
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
        private void ControlToEntity(SubjectCurriculumDt entity)
        {
            entity.SubjectCurriculumDtName = txtSubjectCurriculumDtName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumDtDao entityDao = new SubjectCurriculumDtDao(ctx);
            try
            {
                SubjectCurriculumDt entity = new SubjectCurriculumDt();
                ControlToEntity(entity);
                entity.SubjectCurriculumID = Convert.ToInt32(hdnSubjectCurriculumID.Value);
                entity.CurriculumDtID = Convert.ToInt32(hdnCurriculumDtID.Value);
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
                SubjectCurriculumDt entity = BusinessLayer.GetSubjectCurriculumDt(Convert.ToInt32(hdnSubjectCurriculumDtID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCurriculumDt(entity);
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