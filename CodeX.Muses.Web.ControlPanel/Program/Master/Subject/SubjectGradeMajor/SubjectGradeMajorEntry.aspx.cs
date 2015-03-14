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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectGradeMajorEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SB_SUBJECT_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("SiteID = '{0}' ORDER BY DisplayOrder", AppSession.UserLogin.SiteID));
            List<vSchoolMajor> lstMajor = BusinessLayer.GetvSchoolMajorList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID));
            lstMajor.Insert(0, new vSchoolMajor { GCMajor = "", Major = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField<vSchoolMajor>(cboMajor, lstMajor, "Major", "GCMajor");

            BindGridView();

            Helper.SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvSubjectGradeMajorList(string.Format("SubjectID = {0} ORDER BY GCGrade ASC", AppSession.SubjectID));
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
                if (hdnIsAdd.Value.ToString() != "1")
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

        private void ControlToEntity(SubjectGradeMajor entity)
        {
            if (cboMajor.Value != null && cboMajor.Value.ToString() != "")
                entity.GCMajor = cboMajor.Value.ToString();
            else
                entity.GCMajor = null;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectGradeMajor entity = new SubjectGradeMajor();
                ControlToEntity(entity);
                entity.GCGrade = cboGrade.Value.ToString();
                entity.SubjectID = AppSession.SubjectID;
                BusinessLayer.InsertSubjectGradeMajor(entity);
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
                SubjectGradeMajor entity = BusinessLayer.GetSubjectGradeMajor(AppSession.SubjectID, cboGrade.Value.ToString());
                ControlToEntity(entity);
                BusinessLayer.UpdateSubjectGradeMajor(entity);
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
                BusinessLayer.DeleteSubjectGradeMajor(AppSession.SubjectID, cboGrade.Value.ToString());
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}