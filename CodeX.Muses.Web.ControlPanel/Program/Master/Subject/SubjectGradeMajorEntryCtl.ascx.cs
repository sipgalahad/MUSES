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
    public partial class SubjectGradeMajorEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_GRADE, Constant.StandardCode.SCHOOL_MAJOR));
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboGrade, lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_GRADE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboMajor, lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_MAJOR || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");

            hdnID.Value = param;
            Subject entity = BusinessLayer.GetSubject(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.SubjectCode, entity.SubjectName);

            BindGridView();

            Helper.SetControlEntrySetting(cboMajor, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvSubjectGradeMajorList(string.Format("SubjectID = {0} ORDER BY GCGrade ASC", hdnID.Value));
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
                entity.SubjectID = Convert.ToInt32(hdnID.Value);
                BusinessLayer.InsertSubjectGradeMajor(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                SubjectGradeMajor entity = BusinessLayer.GetSubjectGradeMajor(Convert.ToInt32(hdnID.Value), cboGrade.Value.ToString());
                ControlToEntity(entity);
                BusinessLayer.UpdateSubjectGradeMajor(entity);
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
                BusinessLayer.DeleteSubjectGradeMajor(Convert.ToInt32(hdnID.Value), cboGrade.Value.ToString());
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