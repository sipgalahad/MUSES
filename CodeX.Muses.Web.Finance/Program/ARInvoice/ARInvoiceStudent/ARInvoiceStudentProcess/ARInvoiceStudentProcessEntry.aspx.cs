using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;
using System.Data;
using CodeX.Common;
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class ARInvoiceStudentProcessEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.AR_INVOICE_STUDENT_PROCESS;
        }

        protected string OnGetCustomerFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

        protected override void InitializeDataControl()
        {
            List<Bank> lstBank = BusinessLayer.GetBankList("IsDeleted = 0");
            Methods.SetComboBoxField<Bank>(cboBank, lstBank, "BankName", "BankID");
            cboBank.SelectedIndex = 0;
            txtInvoiceDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDueDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            List<StudentFeeCompType> lstStudentFeeCompType = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<StudentFeeCompType>(cboStudentFeeCompType, lstStudentFeeCompType, "StudentFeeCompTypeName", "StudentFeeCompTypeID");

            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            IsLoadFirstRecord = (OnGetRowCount() > 0);

            Helper.SetControlEntrySetting(cboStudentFeeCompType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboYear, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboMonth, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtTransactionAmount, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnARInvoiceID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtARInvoiceNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtInvoiceDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtDueDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboBank, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        protected string onGetARInvoiceFilterExpression()
        {
            return string.Format("StudentID = {0} AND GCTransactionStatus != '{1}'", AppSession.StudentID, Constant.TransactionStatus.VOID);
        }

        protected string GetFilterExpression()
        {
            return onGetARInvoiceFilterExpression();
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvARInvoiceHdRowCount(filterExpression);
        }

        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
            hdnARInvoiceID.Value = "0";
            BindGridView(1, false, ref PageCount, ref RowCount);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vARInvoiceHd entity = BusinessLayer.GetvARInvoiceHdList(filterExpression, PageIndex, " ARInvoiceID DESC")[0];
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvARInvoiceHdRowIndex(filterExpression, keyValue, "ARInvoiceID DESC");
            vARInvoiceHd entity = BusinessLayer.GetvARInvoiceHdList(filterExpression, PageIndex, "ARInvoiceID DESC")[0];
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vARInvoiceHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            hdnARInvoiceID.Value = entity.ARInvoiceID.ToString();
            txtARInvoiceNo.Text = entity.ARInvoiceNo;
            txtInvoiceDate.Text = entity.ARInvoiceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboBank.Value = entity.BankID.ToString();
            txtDueDate.Text = entity.DueDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnARInvoiceID.Value != "")
                filterExpression = string.Format("ARInvoiceID = {0} AND IsDeleted = 0", hdnARInvoiceID.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvARInvoiceDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vARInvoiceDt> lstInvoiceDt = BusinessLayer.GetvARInvoiceDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ARInvoiceID ASC");
            grdView.DataSource = lstInvoiceDt;
            grdView.DataBind();
        }

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

        #region Save Edit Header
        private void ControlToEntity(ARInvoiceHd entityHd)
        {
            entityHd.ARInvoiceDate = Helper.GetDatePickerValue(txtInvoiceDate);
            entityHd.BankID = Convert.ToInt32(cboBank.Value);
            entityHd.DueDate = Helper.GetDatePickerValue(txtDueDate);            
            entityHd.Remarks = txtRemarks.Text;
            entityHd.TermID = null;
        }

        public void SaveARInvoiceHd(IDbContext ctx, ref int ARInvoiceID)
        {
            ARInvoiceHdDao entityHdDao = new ARInvoiceHdDao(ctx);
            if (hdnARInvoiceID.Value == "0")
            {
                ARInvoiceHd entityHd = new ARInvoiceHd();
                ControlToEntity(entityHd);
                entityHd.StudentID = AppSession.StudentID;
                entityHd.BusinessPartnerID = null;
                entityHd.ProspectiveStudentID = null;
                entityHd.TransactionCode = Constant.TransactionCode.AR_INVOICE_STUDENT;
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entityHd.ARInvoiceNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.ARInvoiceDate, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                ARInvoiceID = BusinessLayer.GetARInvoiceHdMaxID(ctx);
            }
            else
            {
                ARInvoiceID = Convert.ToInt32(hdnARInvoiceID.Value);
            }
        }


        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int ARInvoiceID = 0;
                SaveARInvoiceHd(ctx, ref ARInvoiceID);
                retval = ARInvoiceID.ToString();
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
                ARInvoiceHd entity = BusinessLayer.GetARInvoiceHd(Convert.ToInt32(hdnARInvoiceID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateARInvoiceHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int ARInvoiceID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    ARInvoiceID = Convert.ToInt32(hdnARInvoiceID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref ARInvoiceID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                ARInvoiceID = Convert.ToInt32(hdnARInvoiceID.Value);
                if (OnDeleteEntityDt(ref errMessage, ARInvoiceID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpARInvoiceID"] = ARInvoiceID.ToString();
        }

        private void ControlToEntity(ARInvoiceDt entityDt)
        {
            entityDt.StudentFeeCompTypeID = Convert.ToInt32(cboStudentFeeCompType.Value);
            entityDt.TransactionYear = Convert.ToInt32(cboYear.Value);
            entityDt.TransactionMonth = Convert.ToInt32(cboMonth.Value);
            entityDt.ClaimedAmount = entityDt.TransactionAmount = Convert.ToDecimal(Request.Form[txtTransactionAmount.UniqueID]);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int ARInvoiceID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceDtDao entityDtDao = new ARInvoiceDtDao(ctx);
            try
            {
                SaveARInvoiceHd(ctx, ref ARInvoiceID);
                ARInvoiceDt entityDt = new ARInvoiceDt();
                ControlToEntity(entityDt);
                entityDt.StudentFeeDtID = null;
                entityDt.ARInvoiceID = ARInvoiceID;
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            ARInvoiceDtDao entityDtDao = new ARInvoiceDtDao(ctx);
            try
            {
                ARInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceDtDao entityDtDao = new ARInvoiceDtDao(ctx);
            try
            {
                ARInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.IsDeleted = true;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Update(entityDt);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}