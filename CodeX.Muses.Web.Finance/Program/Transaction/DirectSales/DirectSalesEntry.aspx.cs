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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class DirectSalesEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected string SiteID = "0";
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.DIRECT_SALES;
        }

        protected override void InitializeDataControl()
        {
            SiteID = AppSession.UserLogin.SiteID;

            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode = '{0}'", Constant.SettingParameter.VAT_PERCENTAGE));
            hdnVATPercentage.Value = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<GetLocationUserList> lstLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.PURCHASE_ORDER, "");
            if (lstLocation.Count == 1)
            {
                hdnLocationID.Value = lstLocation[0].LocationID.ToString();
                txtLocationCode.Text = lstLocation[0].LocationCode;
                Location loc = BusinessLayer.GetLocation(lstLocation[0].LocationID);
            }

            int count = BusinessLayer.GetLocationUserRowCount(string.Format("UserID = {0} AND IsDeleted = 0", AppSession.UserLogin.UserID));
            if (count > 0)
                hdnRecordFilterExpression.Value = string.Format("LocationID IN (SELECT LocationID FROM LocationUser WHERE UserID = {0} AND IsDeleted = 0)", AppSession.UserLogin.UserID);
            else
            {
                count = BusinessLayer.GetLocationUserRoleRowCount(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID));
                if (count > 0)
                    hdnRecordFilterExpression.Value = string.Format("LocationID IN (SELECT LocationID FROM LocationUserRole WHERE RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0} AND SiteID = '{1}') AND IsDeleted = 0)", AppSession.UserLogin.UserID, AppSession.UserLogin.SiteID);
                else
                    hdnRecordFilterExpression.Value = "";
            }
            SetControlProperties();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);

            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(tacItem, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        #region Filter Expression Search Dialog
        protected string OnGetFilterExpressionStudent()
        {
            return string.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.StudentStatus.ACTIVE);
        }
        protected string OnGetFilterExpressionLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.DIRECT_SALES);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        #endregion

        protected override void SetControlProperties()
        {
            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.CURRENCY_CODE, Constant.StandardCode.FRANCO_REGION));
            List<Term> listTerm = BusinessLayer.GetTermList("IsDeleted = 0");
            Methods.SetComboBoxField<StandardCode>(cboFrancoRegion, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.FRANCO_REGION).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboCurrency, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.CURRENCY_CODE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<Term>(cboTerm, listTerm, "TermName", "TermID");
            cboFrancoRegion.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnPRID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtSalesInvoiceNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtSalesUnitDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtKurs, new ControlEntrySetting(true, true, true, "1.00"));
            SetControlEntrySetting(chkPPN, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFinalDiscount, new ControlEntrySetting(true, true, true, "0.00"));
            SetControlEntrySetting(txtPPN, new ControlEntrySetting(false, false, true, "0.00"));
            SetControlEntrySetting(txtPPNPercentage, new ControlEntrySetting(false, false, true, GetVATPercentageLabel()));
            SetControlEntrySetting(txtFinalDiscountInPercentage, new ControlEntrySetting(true, true, true, "0.00"));
            SetControlEntrySetting(txtTransactionAmountSaldo, new ControlEntrySetting(false, false, true, "0.00"));
            SetControlEntrySetting(cboTerm, new ControlEntrySetting(true, true, true, "1"));
            SetControlEntrySetting(cboCurrency, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboFrancoRegion, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnStudentID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtStudentCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtStudentName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, true, true, hdnDefaultLocationID.Value));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true, hdnDefaultLocationCode.Value));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, true, hdnDefaultLocationName.Value));

            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(lblStudent, new ControlEntrySetting(true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnRowCount.Value = "0";
            hdnIsEditable.Value = "1";
            hdnIsClosed.Value = "0";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string OnGetFilterExpression()
        {
            string filterExpression = hdnRecordFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("TransactionCode = '{0}'", Constant.TransactionCode.DIRECT_SALES);
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = OnGetFilterExpression();
            return BusinessLayer.GetvSalesInvoiceHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = OnGetFilterExpression();
            vSalesInvoiceHd entity = BusinessLayer.GetvSalesInvoiceHd(filterExpression, PageIndex, "SalesInvoiceID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = OnGetFilterExpression();
            PageIndex = BusinessLayer.GetvSalesInvoiceHdRowIndex(filterExpression, keyValue, "SalesInvoiceID DESC");
            vSalesInvoiceHd entity = BusinessLayer.GetvSalesInvoiceHd(filterExpression, PageIndex, "SalesInvoiceID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vSalesInvoiceHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                trBarcodeEntry.Style.Add("display", "none");
                hdnIsEditable.Value = "0";
            }
            else
            {
                hdnIsEditable.Value = "1";
                trBarcodeEntry.Style.Remove("display");
            }
            hdnIsClosed.Value = entity.GCTransactionStatus == Constant.TransactionStatus.CLOSED ? "1" : "0"; 
            hdnPRID.Value = entity.SalesInvoiceID.ToString();
            txtSalesInvoiceNo.Text = entity.SalesInvoiceNo;
            txtSalesUnitDate.Text = entity.SalesInvoiceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnStudentID.Value = entity.StudentID.ToString();
            txtStudentCode.Text = entity.StudentCode;
            txtStudentName.Text = entity.StudentName;
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            cboTerm.Value = entity.TermID.ToString();
            txtNotes.Text = entity.Remarks;
            cboCurrency.Value = entity.GCCurrencyCode.ToString();
            cboFrancoRegion.Value = entity.GCFrancoRegion.ToString();
            txtKurs.Text = entity.CurrencyRate.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;
            txtPPN.Text = entity.VATAmount.ToString();
            txtFinalDiscountInPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscount.Text = entity.FinalDiscountAmount.ToString();
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtTransactionAmountAfterVAT.Text = Math.Round(entity.TransactionAmount + entity.VATAmount).ToString();

            decimal tempTransactionAmount = -1;
            BindGridView(1, true, ref PageCount, ref RowCount, ref tempTransactionAmount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount, ref decimal transactionAmount)
        {
            string filterExpression = "1 = 0";
            if (hdnPRID.Value != "")
            {
                filterExpression = string.Format("SalesInvoiceID = {0} AND GCItemDetailStatus <> '{1}'", hdnPRID.Value, Constant.TransactionStatus.VOID);
            }
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvSalesInvoiceDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            if (transactionAmount > -1)
                transactionAmount = BusinessLayer.GetSalesInvoiceHd(Convert.ToInt32(hdnPRID.Value)).TransactionAmount;
            List<vSalesInvoiceDt> lstEntity = BusinessLayer.GetvSalesInvoiceDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save Edit Header
        private void ControlToEntity(IDbContext ctx, SalesInvoiceHd entityHd)
        {
            TermDao termDao = new TermDao(ctx);
            entityHd.SalesInvoiceDate = Helper.GetDatePickerValue(txtSalesUnitDate.Text);
            entityHd.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entityHd.StudentID = Convert.ToInt32(hdnStudentID.Value);

            entityHd.TermID = Convert.ToInt32(cboTerm.Value.ToString());
            int termDay = termDao.Get(entityHd.TermID).TermDay;
            entityHd.GCFrancoRegion = cboFrancoRegion.Value.ToString();
            entityHd.GCCurrencyCode = cboCurrency.Value.ToString();
            entityHd.CurrencyRate = Convert.ToDecimal(txtKurs.Text);
            entityHd.IsIncludeVAT = chkPPN.Checked;
            if (entityHd.IsIncludeVAT)
                entityHd.VATPercentage = Convert.ToDecimal(hdnVATPercentage.Value);
            else
                entityHd.VATPercentage = 0;
            entityHd.Remarks = txtNotes.Text;
            entityHd.FinalDiscountPercentage = Convert.ToDecimal(txtFinalDiscountInPercentage.Text);
            //entityHd.NetTransactionAmount = (entityHd.TransactionAmount * (100 - entityHd.FinalDiscountPercentage) / 100) * (100 + entityHd.VATPercentage) / 100;
            entityHd.NetTransactionAmount = (entityHd.TransactionAmount * (100 + entityHd.VATPercentage) / 100) * (100 - entityHd.FinalDiscountPercentage) / 100;
        }

        public void SaveSalesInvoiceHd(IDbContext ctx, ref int PRID, ref string PRNo)
        {
            SalesInvoiceHdDao entityHdDao = new SalesInvoiceHdDao(ctx);
            TermDao termDao = new TermDao(ctx);
            if (hdnPRID.Value == "0")
            {
                SalesInvoiceHd entityHd = new SalesInvoiceHd();
                ControlToEntity(ctx, entityHd);
                entityHd.TransactionCode = Constant.TransactionCode.DIRECT_SALES;
                entityHd.SalesInvoiceNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.SalesInvoiceDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                PRID = BusinessLayer.GetSalesInvoiceHdMaxID(ctx);
                PRNo = entityHd.SalesInvoiceNo;
            }
            else
            {
                PRID = Convert.ToInt32(hdnPRID.Value);
                PRNo = txtSalesInvoiceNo.Text;
                SalesInvoiceHd entityHd = entityHdDao.Get(PRID);
                ControlToEntity(ctx, entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);
            }
        }


        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int PRID = 0;
                string salesUnitNo = "";
                SaveSalesInvoiceHd(ctx, ref PRID, ref salesUnitNo);
                retval = PRID.ToString();
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                SalesInvoiceHd entity = BusinessLayer.GetSalesInvoiceHd(Convert.ToInt32(hdnPRID.Value));
                ControlToEntity(ctx, entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSalesInvoiceHd(entity);
                return true;
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SalesInvoiceHdDao salesHdDao = new SalesInvoiceHdDao(ctx);
            SalesInvoiceDtDao salesDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                SalesInvoiceHd entity = salesHdDao.Get(Convert.ToInt32(hdnPRID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                salesHdDao.Update(entity);

                string filterExpression = String.Format("SalesInvoiceID = {0} AND GCItemDetailStatus != '{1}'", entity.SalesInvoiceID, Constant.TransactionStatus.VOID);
                List<SalesInvoiceDt> lstEntityDt = BusinessLayer.GetSalesInvoiceDtList(filterExpression);
                foreach (SalesInvoiceDt entityDt in lstEntityDt)
                {
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    salesDtDao.Update(entityDt);
                }

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

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int PRID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    PRID = Convert.ToInt32(hdnPRID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref PRID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                PRID = Convert.ToInt32(hdnEntryID.Value);
                if (OnDeleteEntityDt(ref errMessage, PRID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "barcodeentry")
            {
                if (OnSaveRecordBarcodeEntityDt(ref errMessage, ref PRID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = PRID.ToString();
        }

        private void ControlToEntity(SalesInvoiceDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(tacItem.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString();
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = 1;
            entityDt.IsBonusItem = false;
            entityDt.DiscountPercentage1 = Convert.ToDecimal(txtDiscount1.Text);
            entityDt.DiscountPercentage2 = Convert.ToDecimal(txtDiscount2.Text);
            entityDt.UnitPrice = Convert.ToDecimal(txtPrice.Text);
            entityDt.LineAmount = Convert.ToDecimal(Request.Form[hdnLineAmount.UniqueID]);
        }

        private bool OnSaveRecordBarcodeEntityDt(ref string errMessage, ref int PRID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SalesInvoiceDtDao entityDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                ItemMaster itemMaster = BusinessLayer.GetItemMasterList(string.Format("ItemCode = '{0}' AND IsDeleted = 0", txtBarcodeEntryItem.Text), ctx).FirstOrDefault();
                if (itemMaster != null)
                {
                    string salesUnitNo = "";
                    SaveSalesInvoiceHd(ctx, ref PRID, ref salesUnitNo);
                    SalesInvoiceDt entityDt = new SalesInvoiceDt();

                    GetItemMasterSales itemMasterSales = BusinessLayer.GetItemMasterSalesList(AppSession.UserLogin.SiteID, itemMaster.ItemID, Convert.ToInt32(hdnStudentID.Value), Convert.ToInt32(hdnLocationID.Value), 1, Helper.GetDatePickerValue(txtSalesUnitDate.Text), ctx).FirstOrDefault();
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();

                    entityDt.ItemID = itemMaster.ItemID;
                    entityDt.Quantity = Convert.ToDecimal(txtBarcodeEntryQty.Text);
                    entityDt.GCItemUnit = itemMaster.GCItemUnit;
                    entityDt.GCBaseUnit = itemMaster.GCItemUnit;
                    entityDt.ConversionFactor = 1;
                    entityDt.IsBonusItem = false;
                    entityDt.DiscountPercentage1 = 0;
                    entityDt.DiscountPercentage2 = 0;
                    entityDt.UnitPrice = itemMasterSales.Price;
                    entityDt.LineAmount = entityDt.UnitPrice * entityDt.Quantity;
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                    entityDt.SalesInvoiceID = PRID;
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                    result = true;
                }
                else
                {
                    result = false;
                    errMessage = string.Format("Item Dengan Kode {0} Tidak Ditemukan", txtBarcodeEntryItem.Text);
                }
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

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int PRID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SalesInvoiceDtDao entityDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                string salesUnitNo = "";
                SaveSalesInvoiceHd(ctx, ref PRID, ref salesUnitNo);
                SalesInvoiceDt entityDt = new SalesInvoiceDt();
                ControlToEntity(entityDt);
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.OPEN;
                entityDt.SalesInvoiceID = PRID;
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SalesInvoiceDtDao entityDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                SalesInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityDt);
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

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SalesInvoiceDtDao entityDtDao = new SalesInvoiceDtDao(ctx);
            try
            {
                SalesInvoiceDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
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
        protected void cboItemUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND (StandardCodeID IN (SELECT GCAlternateUnit FROM ItemAlternateUnit WHERE ItemID = {1}) OR StandardCodeID = (SELECT GCItemUnit FROM ItemMaster WHERE ItemID = {1}))", Constant.StandardCode.ITEM_UNIT, tacItem.Value));
            Methods.SetComboBoxField<StandardCode>(cboItemUnit, lst, "StandardCodeName", "StandardCodeID");
            cboItemUnit.SelectedIndex = -1;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            decimal transactionAmount = 0;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    transactionAmount = -1;
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount, ref transactionAmount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount, ref transactionAmount);
                    result = string.Format("refresh|{0}|{1}|{2}", pageCount, rowCount, transactionAmount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}