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
    public partial class SubjectClassTypeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SB_SUBJECT_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            hdnGCClassStudyType.Value = BusinessLayer.GetSubject(AppSession.SubjectID).GCClassStudyType;

            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvSubjectClassTypeList(string.Format("SubjectID = {0} ORDER BY GCGrade ASC", AppSession.SubjectID));
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
            if (OnDeleteEntityDt(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteSubjectClassType(AppSession.SubjectID, Convert.ToInt32(hdnEntryID.Value));
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