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
    public partial class StudentFinalMarkFormulaDtEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            StudentFinalMarkFormulaHd entity = BusinessLayer.GetStudentFinalMarkFormulaHd(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.StudentFinalMarkFormulaCode, entity.StudentFinalMarkFormulaName);

            Repeater rptTaskType = (Repeater)ddeTaskType.FindControl("rptTaskType");
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.TASK_TYPE));
            rptTaskType.DataSource = lstSc;
            rptTaskType.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtStudentFinalMarkFormulaDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptTaskType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkTaskType = (CheckBox)e.Item.FindControl("chkTaskType");
                chkTaskType.Attributes.Add("tasktypename", obj.StandardCodeName);
                chkTaskType.Attributes.Add("tasktypeid", obj.StandardCodeID);
            }
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvStudentFinalMarkFormulaDtList(string.Format("StudentFinalMarkFormulaID = {0} ORDER BY DisplayOrder ASC", hdnID.Value));
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

        private void ControlToEntity(StudentFinalMarkFormulaDt entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.FinalMarkPercentage = Convert.ToDecimal(txtFinalMarkPercentage.Text);
            entity.StudentFinalMarkFormulaDtName = txtStudentFinalMarkFormulaDtName.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentFinalMarkFormulaDtDao entityDao = new StudentFinalMarkFormulaDtDao(ctx);
            StudentFinalMarkFormulaDtTaskTypeDao entityDtDao = new StudentFinalMarkFormulaDtTaskTypeDao(ctx);
            try
            {
                StudentFinalMarkFormulaDt entity = new StudentFinalMarkFormulaDt();
                ControlToEntity(entity);
                entity.StudentFinalMarkFormulaID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.StudentFinalMarkFormulaDtID = BusinessLayer.GetStudentFinalMarkFormulaDtMaxID(ctx);

                string[] lstTaskTypeID = hdnLstTaskTypeID.Value.Split(',');
                foreach (string taskTypeID in lstTaskTypeID)
                {
                    StudentFinalMarkFormulaDtTaskType entityDt = new StudentFinalMarkFormulaDtTaskType();
                    entityDt.StudentFinalMarkFormulaDtID = entity.StudentFinalMarkFormulaDtID;
                    entityDt.GCTaskType = taskTypeID;
                    entityDtDao.Insert(entityDt);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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
            StudentFinalMarkFormulaDtDao entityDao = new StudentFinalMarkFormulaDtDao(ctx);
            StudentFinalMarkFormulaDtTaskTypeDao entityDtDao = new StudentFinalMarkFormulaDtTaskTypeDao(ctx);
            try
            {
                StudentFinalMarkFormulaDt entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<StudentFinalMarkFormulaDtTaskType> lstEntityDt = BusinessLayer.GetStudentFinalMarkFormulaDtTaskTypeList(string.Format("StudentFinalMarkFormulaDtID = {0}", entity.StudentFinalMarkFormulaDtID), ctx);
                string[] lstTaskTypeID = hdnLstTaskTypeID.Value.Split(',');
                foreach (string taskTypeID in lstTaskTypeID)
                {
                    StudentFinalMarkFormulaDtTaskType entityDt = lstEntityDt.FirstOrDefault(p => p.GCTaskType == taskTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new StudentFinalMarkFormulaDtTaskType();
                        entityDt.StudentFinalMarkFormulaDtID = entity.StudentFinalMarkFormulaDtID;
                        entityDt.GCTaskType = taskTypeID;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }

                foreach (StudentFinalMarkFormulaDtTaskType entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.StudentFinalMarkFormulaDtID, entityDt.GCTaskType);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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
                StudentFinalMarkFormulaDt entity = BusinessLayer.GetStudentFinalMarkFormulaDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentFinalMarkFormulaDt(entity);
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