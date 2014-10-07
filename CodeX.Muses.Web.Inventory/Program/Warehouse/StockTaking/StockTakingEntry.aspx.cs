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
using DevExpress.Web.ASPxCallbackPanel;
using System.Data;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class StockTakingEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Inventory.STOCK_TAKING;
        }

        #region Html Getter
        protected string GetLocationFilterExpression()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, Constant.TransactionCode.STOCK_TAKING);
        }
        #endregion

        public string GetTransactionApprove() 
        {
            return Constant.TransactionStatus.APPROVED;
        }

        protected string DateTimeNowDatePicker()
        {
            return DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            hdnDefaultCycleCountType.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.DEFAULT_CYCLE_COUNT_TYPE).ParameterValue;
            btnStartCalculate.Attributes.Add("enabled", "false");

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
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnStockTakingID, new ControlEntrySetting(true, true, false, "0"));
            SetControlEntrySetting(txtStockTakingNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFormDate, new ControlEntrySetting(true, false, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(hdnLocationID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLocationName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(lblLocation, new ControlEntrySetting(true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            btnStartCalculate.Attributes.Add("enabled", "false");
        }

        protected string GetFilterExpression()
        {
            return hdnRecordFilterExpression.Value;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvStockTakingHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vStockTakingHd entity = BusinessLayer.GetvStockTakingHd(filterExpression, PageIndex, "StockTakingID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvStockTakingHdRowIndex(filterExpression, keyValue, "StockTakingID DESC");
            vStockTakingHd entity = BusinessLayer.GetvStockTakingHd(filterExpression, PageIndex, "StockTakingID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private bool IsEditable = true;
        private void EntityToControl(vStockTakingHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            hdnFilterExpression.Value = "";
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                IsEditable = false;
            }
            else
                IsEditable = true;
            hdnStockTakingID.Value = entity.StockTakingID.ToString();
            txtStockTakingNo.Text = entity.StockTakingNo;
            txtFormDate.Text = entity.FormDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnLocationID.Value = entity.LocationID.ToString();
            txtLocationCode.Text = entity.LocationCode;
            txtLocationName.Text = entity.LocationName;
            txtRemarks.Text = entity.Remarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnStockTakingID.Value != "")
                filterExpression = string.Format("StockTakingID = {0} AND GCItemDetailStatus != '{1}'", hdnStockTakingID.Value, Constant.TransactionStatus.VOID);
            if (hdnFilterExpression.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpression.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStockTakingDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstCheckCountType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.CHECK_COUNT_TYPE));
            List<vStockTakingDt> lstEntity = BusinessLayer.GetvStockTakingDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();

            if (lstEntity.Count > 0)
                btnStartCalculate.Attributes.Add("enabled", "false");
            else
                btnStartCalculate.Attributes.Remove("enabled");
        }

        List<StandardCode> lstCheckCountType = null;
        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vStockTakingDt entity = e.Item.DataItem as vStockTakingDt;
                ASPxComboBox cboCheckCountType = e.Item.FindControl("cboCheckCountType") as ASPxComboBox;
                cboCheckCountType.ClientInstanceName = string.Format("cboCheckCountType{0}", e.Item.DataItemIndex);
                Methods.SetComboBoxField<StandardCode>(cboCheckCountType, lstCheckCountType, "StandardCodeName", "StandardCodeID");
                cboCheckCountType.Value = entity.GCCheckCountType;

                HtmlGenericControl lblExpiredDate = e.Item.FindControl("lblExpiredDate") as HtmlGenericControl;
                HtmlInputText txtAdjustment = e.Item.FindControl("txtAdjustment") as HtmlInputText;
                HtmlInputText txtQuantityEND = e.Item.FindControl("txtQuantityEND") as HtmlInputText;

                txtQuantityEND.Value = entity.QuantityEND.ToString();

                if (!entity.IsControlExpired || entity.QuantityEND > 0) lblExpiredDate.Attributes.Add("class", "lblDisabled");
                if (entity.PurchaseUnit == "")
                    entity.PurchaseUnit = entity.ItemUnit;
                if (entity.PurchaseUnit != entity.ItemUnit)
                {
                    HtmlGenericControl divPurchaseUnit = e.Item.FindControl("divPurchaseUnit") as HtmlGenericControl;
                    HtmlGenericControl divConversionFactor = e.Item.FindControl("divConversionFactor") as HtmlGenericControl;

                    divPurchaseUnit.InnerHtml = entity.PurchaseUnit;
                    divConversionFactor.InnerHtml = string.Format("1 {0} = {1} {2}", entity.PurchaseUnit, entity.ConversionFactor, entity.ItemUnit);
                }

                if (!IsEditable)
                {
                    txtQuantityEND.Attributes.Add("readonly", "readonly");
                    txtAdjustment.Attributes.Add("readonly", "readonly");
                }
                else
                {
                    txtQuantityEND.Attributes.Remove("readonly");
                    txtAdjustment.Attributes.Remove("readonly");
                }
            }
        }

        #endregion

        #region Save
        private void ControlToEntity(StockTakingHd entity)
        {
            entity.FormDate = Helper.GetDatePickerValue(txtFormDate);
            entity.LocationID = Convert.ToInt32(hdnLocationID.Value);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            try
            {
                StockTakingHd entity = new StockTakingHd();
                ControlToEntity(entity);
                entity.StockTakingNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.STOCK_TAKING, entity.FormDate);
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertStockTakingHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            try
            {
                StockTakingHd entity = BusinessLayer.GetStockTakingHd(Convert.ToInt32(hdnStockTakingID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStockTakingHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "calculate")
            {
                if (FillStockTakingDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (UpdateStockTakingDt(param, ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool FillStockTakingDt(ref string errMessage)
        {
            try
            {
                int stockTakingID = Convert.ToInt32(hdnStockTakingID.Value);
                int locationID = Convert.ToInt32(hdnLocationID.Value);
                BusinessLayer.FillStockTakingDt(stockTakingID, locationID, DateTime.Now, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT), AppSession.UserLogin.UserID);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool UpdateStockTakingDt(string[] param, ref string errMessage)
        {
            //IDbContext ctx = DbFactory.Configure(true);
            //StockTakingDtDao stockTakingDtDao = new StockTakingDtDao(ctx);
            //StockTakingDtExpiredDao stockTakingDtExpiredDao = new StockTakingDtExpiredDao(ctx);

            try
            {
                Int32 itemID = Convert.ToInt32(param[1]);
                Decimal adjustment = Convert.ToDecimal(param[2]);
                Decimal quantityEND = Convert.ToDecimal(param[3]);
                String GCCheckCountType = param[4];

                StockTakingDt entity = BusinessLayer.GetStockTakingDt(Convert.ToInt32(hdnStockTakingID.Value), itemID);
                entity.QuantityAdjustment = adjustment;
                entity.QuantityEND = quantityEND;
                entity.GCCheckCountType = GCCheckCountType;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStockTakingDt(entity);
                
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StockTakingHdDao stockTakingHdDao = new StockTakingHdDao(ctx);
            StockTakingDtDao stockTakingDtDao = new StockTakingDtDao(ctx);
            try
            {
                StockTakingHd stockTakingHd = stockTakingHdDao.Get(Convert.ToInt32(hdnStockTakingID.Value));
                stockTakingHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                stockTakingHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                stockTakingHdDao.Update(stockTakingHd);

                string filterExpression = String.Format("StockTakingID = {0} AND GCItemDetailStatus != '{1}' AND QuantityAdjustment != 0", hdnStockTakingID.Value, Constant.TransactionStatus.VOID);
                List<StockTakingDt> lstStockTakingDt = BusinessLayer.GetStockTakingDtList(filterExpression, ctx);
                
                foreach (StockTakingDt stockTakingDt in lstStockTakingDt)
                {
                    stockTakingDt.GCItemDetailStatus = Constant.TransactionStatus.APPROVED;
                    stockTakingDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    stockTakingDtDao.Update(stockTakingDt);
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
            try
            {
                StockTakingHd entity = BusinessLayer.GetStockTakingHd(Convert.ToInt32(hdnStockTakingID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStockTakingHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
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
        #endregion
    }
}
