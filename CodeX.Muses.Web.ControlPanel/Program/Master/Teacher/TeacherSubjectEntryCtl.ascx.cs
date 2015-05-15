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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TeacherSubjectEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetDailyScheduleTypeKBM()
        {
            return Constant.SchoolDailyScheduleType.KBM;
        }
        protected string OnGetSubjectFilterExpression()
        {
            return string.Format("GCClassStudyType IN ('{0}','{1}') AND IsDeleted = 0", Constant.ClassStudyType.REGULAR, Constant.ClassStudyType.EXTRACURRICULAR);
        }

        public override void InitializeDataControl(string param)
        {
            List<Site> lstSite = BusinessLayer.GetSiteList("IsHeader = 0");
            Methods.SetComboBoxField<Site>(cboSite, lstSite, "SiteName", "SiteID");

            hdnID.Value = param;
            Employee entity = BusinessLayer.GetEmployee(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.FullName);
            hdnDefaultSite.Value = entity.SiteID;

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboSite, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvTeacherSubjectList(string.Format("TeacherID = {0}", hdnID.Value));
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
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

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                TeacherSubject entity = new TeacherSubject();
                entity.SubjectID = Convert.ToInt32(hdnSubjectID.Value);
                entity.SiteID = cboSite.Value.ToString();
                entity.TeacherID = Convert.ToInt32(hdnID.Value);
                BusinessLayer.InsertTeacherSubject(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteTeacherSubject(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnSubjectID.Value), cboSite.Value.ToString());
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