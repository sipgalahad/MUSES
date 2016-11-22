using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using System.Data;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class UpdateEmployeeJobLevelEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int PageCount2 = 1;
        protected int RowCount2 = 1;

        protected string OnGetTransactionStatusApproved()
        {
            return Constant.TransactionStatus.APPROVED;
        }

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_EMPLOYEE_JOB_LEVEL;
        }


        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            hdnRowCountPerPage2.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";

            BindGridView(1, true, ref PageCount, ref RowCount);
            BindGridView2(1, true, ref PageCount2, ref RowCount2);

            Helper.SetControlEntrySetting(tacEmployeeID, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void SetControlProperties()
        {
            List<vJobLevel> listRenumerationHd = BusinessLayer.GetvJobLevelList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<vJobLevel>(cboJobLevelID, listRenumerationHd, "JobLevelName", "JobLevelID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartEffectiveDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(cboJobLevelID, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false, ""));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnPageCount2.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string GetFilterExpression()
        {
            string filterExpression = String.Format("");
            return filterExpression;
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvTransEmployeeJobLevelHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransEmployeeJobLevelHd entity = BusinessLayer.GetvTransEmployeeJobLevelHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransEmployeeJobLevelHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransEmployeeJobLevelHd entity = BusinessLayer.GetvTransEmployeeJobLevelHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransEmployeeJobLevelHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN || entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";

            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtStartEffectiveDate.Text = entity.StartEffectiveDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboJobLevelID.Value = entity.JobLevelID.ToString();
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            BindGridView2(1, true, ref PageCount2, ref RowCount2);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
            hdnPageCount2.Value = PageCount2.ToString();
            hdnRowCount2.Value = RowCount2.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0}", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvTransEmployeeJobLevelDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransEmployeeJobLevelDt> lstEntity = BusinessLayer.GetvTransEmployeeJobLevelDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "EmployeeName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private void BindGridView2(int pageIndex, bool isCountPageCount, ref int pageCount2, ref int rowCount2)
        {
            string filterExpression = "1 = 0";
            if (hdnTransactionID.Value != "")
                filterExpression = string.Format("TransactionID = {0} AND IsDeleted = 0", hdnTransactionID.Value);
            if (isCountPageCount)
            {
                rowCount2 = BusinessLayer.GetvTransEmployeeJobLevelRenumerationRowCount(filterExpression);
                pageCount2 = Helper.GetPageCount(rowCount2, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vTransEmployeeJobLevelRenumeration> lstEntity2 = BusinessLayer.GetvTransEmployeeJobLevelRenumerationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "RenumerationCompName ASC");
            grdView2.DataSource = lstEntity2;
            grdView2.DataBind();
        }
        #endregion

        #region Save Header
        public void SaveTransEmployeeJobLevelHd(IDbContext ctx, ref int TransactionID)
        {
            TransEmployeeJobLevelHdDao entityHdDao = new TransEmployeeJobLevelHdDao(ctx);
            if (hdnTransactionID.Value == "0")
            {
                TransEmployeeJobLevelHd entityHd = new TransEmployeeJobLevelHd();
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.JobLevelID = Convert.ToInt32(cboJobLevelID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.EMPLOYEE_JOB_LEVEL, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                TransactionID = BusinessLayer.GetTransEmployeeJobLevelHdMaxID(ctx);
            }
            else
            {
                TransactionID = Convert.ToInt32(hdnTransactionID.Value);
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int OrderID = 0;
                SaveTransEmployeeJobLevelHd(ctx, ref OrderID);
                retval = OrderID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                TransEmployeeJobLevelHd entityHd = BusinessLayer.GetTransEmployeeJobLevelHd(Convert.ToInt32(hdnTransactionID.Value));
                entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
                entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
                entityHd.JobLevelID = Convert.ToInt32(cboJobLevelID.Value);
                entityHd.Remarks = txtRemarks.Text;
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTransEmployeeJobLevelHd(entityHd);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }

        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelHdDao transEmployeeJobLevelHdDao = new TransEmployeeJobLevelHdDao(ctx);
            EmployeeDao employeeDao = new EmployeeDao(ctx);
            try
            {
                TransEmployeeJobLevelHd transEmployeeJobLevelHd = transEmployeeJobLevelHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transEmployeeJobLevelHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transEmployeeJobLevelHd.Remarks = txtRemarks.Text;
                transEmployeeJobLevelHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transEmployeeJobLevelHdDao.Update(transEmployeeJobLevelHd);

                if (String.Compare(transEmployeeJobLevelHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    List<Employee> lstEmpl = BusinessLayer.GetEmployeeList(String.Format("EmployeeID IN (SELECT EmployeeID FROM TransEmployeeJobLevelDt WHERE TransactionID = {0})", hdnTransactionID.Value), ctx);
                    foreach (Employee employee in lstEmpl)
                    {
                        employee.CurrentTransJobLevelID = Convert.ToInt32(hdnTransactionID.Value);
                        employee.LastProcessedJobLevelDate = DateTime.Now;
                        employeeDao.Update(employee);
                    }
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

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelHdDao transEmployeeJobLevelHdDao = new TransEmployeeJobLevelHdDao(ctx);
            
            try
            {
                TransEmployeeJobLevelHd transEmployeeJobLevelHd = transEmployeeJobLevelHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transEmployeeJobLevelHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transEmployeeJobLevelHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transEmployeeJobLevelHdDao.Update(transEmployeeJobLevelHd);

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

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelHdDao transEmployeeJobLevelHdDao = new TransEmployeeJobLevelHdDao(ctx);
            
            try
            {
                TransEmployeeJobLevelHd transEmployeeJobLevelHd = transEmployeeJobLevelHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));

                if (String.Compare(transEmployeeJobLevelHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else
                {
                    transEmployeeJobLevelHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transEmployeeJobLevelHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transEmployeeJobLevelHdDao.Update(transEmployeeJobLevelHd);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelHdDao transEmployeeJobLevelHdDao = new TransEmployeeJobLevelHdDao(ctx);
            
            try
            {
                TransEmployeeJobLevelHd transEmployeeJobLevelHd = transEmployeeJobLevelHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transEmployeeJobLevelHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transEmployeeJobLevelHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transEmployeeJobLevelHdDao.Update(transEmployeeJobLevelHd);

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

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt(ref errMessage, ref adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpTransactionID"] = adjustmentID.ToString();
        }

        private void ControlToEntity(TransEmployeeJobLevelDt entityDt)
        {
            entityDt.EmployeeID = Convert.ToInt32(tacEmployeeID.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelDtDao entityDtDao = new TransEmployeeJobLevelDtDao(ctx);
            try
            {
                SaveTransEmployeeJobLevelHd(ctx, ref TransactionID);
                TransEmployeeJobLevelDt entityDt = new TransEmployeeJobLevelDt();
                ControlToEntity(entityDt);
                entityDt.TransactionID = TransactionID;
                entityDtDao.Insert(entityDt);
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


        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            try
            {
                BusinessLayer.DeleteTransEmployeeJobLevelDt(Convert.ToInt32(hdnTransactionID.Value), Convert.ToInt32(tacEmployeeID.Value));
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

        #region Process Detail2
        protected void cbpProcess2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                    if (OnSaveEditRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt2(ref errMessage, ref adjustmentID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
                if (OnDeleteEntityDt2(ref errMessage, adjustmentID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult2"] = result;
            panel.JSProperties["cpTransactionID2"] = adjustmentID.ToString();
        }

        private void ControlToEntity2(TransEmployeeJobLevelRenumeration entityDt)
        {
            //entityDt.RenumerationCompID = Convert.ToInt32(cboRenumerationCompID.Value);
            entityDt.RenumerationCompID = Convert.ToInt32(tacRenumerationCompID.Value);
            entityDt.Amount = Convert.ToDecimal(Request.Form[txtAmount.UniqueID]);
            //entityDt.IsAllowChange = chkIsAllowChange.Checked;
            entityDt.IsUseFormula = chkIsUseFormula.Checked;
        }


        private bool OnSaveAddRecordEntityDt2(ref string errMessage, ref int TransactionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelRenumerationDao entityDtDao = new TransEmployeeJobLevelRenumerationDao(ctx);
            try
            {
                SaveTransEmployeeJobLevelHd(ctx, ref TransactionID);
                TransEmployeeJobLevelRenumeration entityDt = new TransEmployeeJobLevelRenumeration();
                ControlToEntity2(entityDt);
                entityDt.TransactionID = TransactionID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
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

        private bool OnSaveEditRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelRenumerationDao entityDtDao = new TransEmployeeJobLevelRenumerationDao(ctx);
            try
            {
                TransEmployeeJobLevelRenumeration entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity2(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
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

        private bool OnDeleteEntityDt2(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransEmployeeJobLevelRenumerationDao entityDtDao = new TransEmployeeJobLevelRenumerationDao(ctx);
            try
            {
                TransEmployeeJobLevelRenumeration entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDt.IsDeleted = true;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }
        #endregion

        #region Callback
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpView2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView2(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView2(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult2"] = result;
        }
        #endregion
    }
}