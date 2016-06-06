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
    public partial class CurriculumSyllabusEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_SYLLABUS;
        }

        protected string OnGetCurriculumSyllabusTypeStandardCode()
        {
            return Constant.CurriculumSyllabusType.STANDARD_CODE;
        }

        protected string OnGetParentFilterExpression()
        {
            return string.Format("CurriculumID = {0} AND IsHeader = 1 AND IsDeleted = 0", AppSession.CurriculumID);
        }

        protected string OnGetReferenceFilterExpression()
        {
            return string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID);
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CURRICULUM_SYLLABUS_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboCurriculumSyllabusType, lstSc, "StandardCodeName", "StandardCodeID");

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumSyllabusName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCurriculumSyllabusType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacStandardCode, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID);
            grdView.DataSource = BusinessLayer.GetvCurriculumSyllabusList(filterExpression);
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

        private void ControlToEntity(CurriculumSyllabus entity)
        {
            entity.CurriculumSyllabusName = txtCurriculumSyllabusName.Text;
            entity.GCCurriculumSyllabusType = cboCurriculumSyllabusType.Value.ToString();
            entity.StandardCodeID = tacStandardCode.Value;
            if (tacParent.Value != "0" && tacParent.Value != "")
                entity.ParentID = Convert.ToInt32(tacParent.Value);
            else
                entity.ParentID = null;
            if (tacReference.Value != "0" && tacReference.Value != "")
                entity.ReferenceID = Convert.ToInt32(tacReference.Value);
            else
                entity.ReferenceID = null;
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.IsHeader = chkIsHeader.Checked;
            entity.IsUsingCode = chkIsUsingCode.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumSyllabusDao entityDao = new CurriculumSyllabusDao(ctx);
            try
            {
                CurriculumSyllabus entity = new CurriculumSyllabus();
                ControlToEntity(entity);
                entity.CurriculumID = AppSession.CurriculumID;
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumSyllabusDao entityDao = new CurriculumSyllabusDao(ctx);
            try
            {
                CurriculumSyllabus entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                CurriculumSyllabus entity = BusinessLayer.GetCurriculumSyllabus(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumSyllabus(entity);
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