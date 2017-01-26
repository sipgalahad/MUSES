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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ItemDistributionEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
                return Constant.MenuCode.Inventory.ITEM_DISTRIBUTION_CROSS_SITE;
            return Constant.MenuCode.Inventory.ITEM_DISTRIBUTION;
        }

        #region Html Getter
        protected string OnGetItemQtyOnOrderFilterExpression()
        {
            return string.Format("ToSiteServiceUnitID = [SiteServiceUnitID] AND ItemID = [ItemID] AND GCDistributionStatus NOT IN ('{0}','{1}') AND IsDeleted = 0", Constant.DistributionStatus.RECEIVED, Constant.DistributionStatus.VOID);
        }
        protected string OnGetFilterExpressionFromServiceUnit()
        {
            if (hdnListFromSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListFromSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionToServiceUnit()
        {
            if (hdnListToSiteServiceUnitID.Value != "")
                return string.Format("SiteServiceUnitID IN ({0}) AND IsDeleted = 0", hdnListToSiteServiceUnitID.Value);
            return "1 = 0";
        }
        protected string OnGetFilterExpressionItemPlanning()
        {
            return string.Format("SiteID = '{0}' AND ItemID = [ItemID] AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        protected string OnGetFilterExpressionFromLocation()
        {
            return string.Format("{0};{1};{2};", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, hdnTransactionCode.Value);
        }
        protected string OnGetFilterExpressionToLocation()
        {
            return string.Format("{0};0;{1};", AppSession.UserLogin.SiteID, hdnTransactionCodeItemRequest.Value);
        }
        protected string OnGetFilterExpressionItemGroup()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        protected string OnGetFilterExpressionItemProduct()
        {
            return string.Format("GCItemType = '{0}' AND IsDeleted = 0", Constant.ItemType.PRODUCT);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString["type"] == "cs")
            {
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_DISTRIBUTION_CROSS_SITE;
                hdnTransactionCodeItemRequest.Value = Constant.TransactionCode.ITEM_REQUEST_CROSS_SITE;
            }
            else
            {
                hdnTransactionCode.Value = Constant.TransactionCode.ITEM_DISTRIBUTION;
                hdnTransactionCodeItemRequest.Value = Constant.TransactionCode.ITEM_REQUEST;
            }
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            List<GetLocationUserList> lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, hdnTransactionCode.Value, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListFromSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());

                List<vSiteServiceUnit> lstSiteServiceUnit = BusinessLayer.GetvSiteServiceUnitList(OnGetFilterExpressionFromServiceUnit());
                if (lstSiteServiceUnit.Count == 1)
                {
                    vSiteServiceUnit serviceUnit = lstSiteServiceUnit.FirstOrDefault();
                    hdnFromSiteServiceUnitID.Value = hdnDefaultSiteServiceUnitID.Value = serviceUnit.SiteServiceUnitID.ToString();
                    hdnDefaultServiceUnitCode.Value = serviceUnit.ServiceUnitCode;
                    hdnDefaultServiceUnitName.Value = serviceUnit.ServiceUnitName;

                    if (lstUserLocation.Count == 1)
                    {
                        GetLocationUserList location = lstUserLocation.FirstOrDefault();
                        hdnDefaultLocationID.Value = location.LocationID.ToString();
                        hdnDefaultLocationCode.Value = location.LocationCode;
                        hdnDefaultLocationName.Value = location.LocationName;
                    }
                }
            }

            lstUserLocation = BusinessLayer.GetLocationUserList(AppSession.UserLogin.SiteID, 0, hdnTransactionCodeItemRequest.Value, "");
            if (lstUserLocation.Count > 0)
            {
                List<ServiceUnitLocation> lstServiceUnitLocation = BusinessLayer.GetServiceUnitLocationList(string.Format("LocationID IN ({0})", string.Join(",", lstUserLocation.Select(p => p.LocationID).ToList())));
                hdnListToSiteServiceUnitID.Value = string.Join(",", lstServiceUnitLocation.Select(p => p.SiteServiceUnitID).ToList());
            }

            hdnRecordFilterExpression.Value = string.Format("FromSiteServiceUnitID IN ({0})", hdnListFromSiteServiceUnitID.Value);
            hdnIsAutoReceived.Value = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.IS_ITEM_DISTRIBUTION_AUTO_RECEIVED).ParameterValue;

            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(txtQuantity, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtItemCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboItemUnit, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnDistributionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtDistributionNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(lblFromSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnFromSiteServiceUnitID, new ControlEntrySetting(true, false, true, hdnDefaultSiteServiceUnitID.Value));
            SetControlEntrySetting(txtFromServiceUnitCode, new ControlEntrySetting(true, false, true, hdnDefaultServiceUnitCode.Value));
            SetControlEntrySetting(txtFromServiceUnitName, new ControlEntrySetting(false, false, true, hdnDefaultServiceUnitName.Value));
            SetControlEntrySetting(lblFromLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnFromLocationID, new ControlEntrySetting(true, false, true, hdnDefaultLocationID.Value));
            SetControlEntrySetting(txtFromLocationCode, new ControlEntrySetting(true, false, true, hdnDefaultLocationCode.Value));
            SetControlEntrySetting(txtFromLocationName, new ControlEntrySetting(false, false, true, hdnDefaultLocationName.Value));
            SetControlEntrySetting(hdnLstFilterFromLocationItemGroup, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnLstFilterToLocationItemGroup, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtItemDistributionDate, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtItemDistributionTime, new ControlEntrySetting(true, false, true, DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT)));

            SetControlEntrySetting(lblToSiteServiceUnit, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnToSiteServiceUnitID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToServiceUnitCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToServiceUnitName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(lblToLocation, new ControlEntrySetting(true, false));
            SetControlEntrySetting(hdnToLocationID, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToLocationCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtToLocationName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
        }

        protected string GetFilterExpression()
        {
            string filterExpression = String.Format("TransactionCode = '{0}'", hdnTransactionCode.Value);
            if (hdnRecordFilterExpression.Value != "")
                filterExpression += string.Format(" AND {0}", hdnRecordFilterExpression.Value);
            return filterExpression;
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvItemDistributionHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vItemDistributionHd entity = BusinessLayer.GetvItemDistributionHd(filterExpression, PageIndex, "DistributionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvItemDistributionHdRowIndex(filterExpression, keyValue, "DistributionID DESC");
            vItemDistributionHd entity = BusinessLayer.GetvItemDistributionHd(filterExpression, PageIndex, "DistributionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vItemDistributionHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCDistributionStatus != Constant.DistributionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.DistributionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";

            if (entity.GCDistributionStatus != Constant.DistributionStatus.OPEN && entity.GCDistributionStatus != Constant.DistributionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";
            
            hdnDistributionID.Value = entity.DistributionID.ToString();
            txtDistributionNo.Text = entity.DistributionNo;
            txtItemDistributionDate.Text = entity.DeliveryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtItemDistributionTime.Text = entity.DeliveryTime;

            hdnFromSiteServiceUnitID.Value = entity.FromSiteServiceUnitID.ToString();
            txtFromServiceUnitCode.Text = entity.FromServiceUnitCode;
            txtFromServiceUnitName.Text = entity.FromServiceUnitName;
            hdnFromLocationID.Value = entity.FromLocationID.ToString();
            txtFromLocationCode.Text = entity.FromLocationCode;
            txtFromLocationName.Text = entity.FromLocationName;

            hdnToSiteServiceUnitID.Value = entity.ToSiteServiceUnitID.ToString();
            txtToServiceUnitCode.Text = entity.ToServiceUnitCode;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            hdnToLocationID.Value = entity.ToLocationID.ToString();
            txtToLocationCode.Text = entity.ToLocationCode;
            txtToLocationName.Text = entity.ToLocationName;

            txtNotes.Text = entity.DeliveryRemarks;

            BindGridView(1, true, ref PageCount, ref RowCount);
            hdnPageCount.Value = PageCount.ToString();
            hdnRowCount.Value = RowCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = "1 = 0";
            if (hdnDistributionID.Value != "")
                filterExpression = string.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvItemDistributionDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vItemDistributionDt> lstEntity = BusinessLayer.GetvItemDistributionDtList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "ItemName1 ASC");
            hdnPageCount.Value = pageCount.ToString();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
        #endregion

        #region Save & Edit Header

        private void ControlToEntityHd(ItemDistributionHd entityHd)
        {
            entityHd.FromSiteServiceUnitID = Convert.ToInt32(hdnFromSiteServiceUnitID.Value);
            entityHd.FromLocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entityHd.ToSiteServiceUnitID = Convert.ToInt32(hdnToSiteServiceUnitID.Value);
            entityHd.ToLocationID = Convert.ToInt32(hdnToLocationID.Value);
            entityHd.DeliveryDate = Helper.GetDatePickerValue(txtItemDistributionDate.Text);
            entityHd.DeliveryTime = txtItemDistributionTime.Text;
            entityHd.DeliveryRemarks = txtNotes.Text;
        }

        public void SaveItemDistributionHd(IDbContext ctx, ref int distributionID)
        {
            ItemDistributionHdDao entityHdDao = new ItemDistributionHdDao(ctx);
            if (hdnDistributionID.Value == "0")
            {
                ItemDistributionHd entityHd = new ItemDistributionHd();
                ControlToEntityHd(entityHd);
                entityHd.TransactionCode = hdnTransactionCode.Value;
                entityHd.DistributionNo = BusinessLayer.GenerateTransactionNo(entityHd.TransactionCode, entityHd.DeliveryDate, ctx);
                entityHd.GCDistributionStatus = Constant.DistributionStatus.OPEN;
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);
                distributionID = BusinessLayer.GetItemDistributionHdMaxID(ctx);
            }
            else
            {
                distributionID = Convert.ToInt32(hdnDistributionID.Value);
            }
        }
        
        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                int distributionID = 0;
                SaveItemDistributionHd(ctx, ref distributionID);
                retval = distributionID.ToString();
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
                ItemDistributionHd entity = BusinessLayer.GetItemDistributionHd(Convert.ToInt32(hdnDistributionID.Value));
                if (entity.GCDistributionStatus == Constant.DistributionStatus.OPEN)
                {
                    entity.DeliveryRemarks = txtNotes.Text;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateItemDistributionHd(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        #region Approve Proposed Void Entity
        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemDistributionHdDao itemHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionDtDao itemDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                string GCDistributionStatus = Constant.DistributionStatus.ON_DELIVERY;
                if (hdnIsAutoReceived.Value == "1")
                    GCDistributionStatus = Constant.DistributionStatus.RECEIVED;
                ItemDistributionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnDistributionID.Value));
                if (itemHd.GCDistributionStatus == Constant.DistributionStatus.OPEN || itemHd.GCDistributionStatus == Constant.DistributionStatus.WAIT_FOR_APPROVAL)
                {
                    ControlToEntityHd(itemHd);
                    itemHd.GCDistributionStatus = GCDistributionStatus;
                    itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemHdDao.Update(itemHd);

                    string filterExpressionItemDistributionHd = String.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);
                    List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionItemDistributionHd, ctx);
                    foreach (ItemDistributionDt itemDt in lstItemDistributionDt)
                    {
                        itemDt.GCItemDetailStatus = GCDistributionStatus;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
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
            ItemDistributionHdDao itemHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionDtDao itemDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                ItemDistributionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnDistributionID.Value));
                if (itemHd.GCDistributionStatus == Constant.DistributionStatus.OPEN)
                {
                    ControlToEntityHd(itemHd);
                    itemHd.GCDistributionStatus = Constant.DistributionStatus.WAIT_FOR_APPROVAL;
                    itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemHdDao.Update(itemHd);

                    string filterExpressionItemDistributionHd = String.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);
                    List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionItemDistributionHd, ctx);
                    foreach (ItemDistributionDt itemDt in lstItemDistributionDt)
                    {
                        itemDt.GCItemDetailStatus = Constant.DistributionStatus.WAIT_FOR_APPROVAL;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemDistributionHdDao itemHdDao = new ItemDistributionHdDao(ctx);
            ItemDistributionDtDao itemDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                ItemDistributionHd itemHd = itemHdDao.Get(Convert.ToInt32(hdnDistributionID.Value));
                if (itemHd.GCDistributionStatus == Constant.DistributionStatus.OPEN)
                {
                    ControlToEntityHd(itemHd);
                    itemHd.GCDistributionStatus = Constant.DistributionStatus.VOID;
                    itemHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    itemHdDao.Update(itemHd);

                    string filterExpressionItemDistributionHd = String.Format("DistributionID = {0} AND IsDeleted = 0", hdnDistributionID.Value);
                    List<ItemDistributionDt> lstItemDistributionDt = BusinessLayer.GetItemDistributionDtList(filterExpressionItemDistributionHd, ctx);
                    foreach (ItemDistributionDt itemDt in lstItemDistributionDt)
                    {
                        itemDt.GCItemDetailStatus = Constant.DistributionStatus.VOID;
                        itemDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        itemDtDao.Update(itemDt);
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
        #endregion

        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            int distributionID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    distributionID = Convert.ToInt32(hdnDistributionID.Value);
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage, ref distributionID))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                distributionID = Convert.ToInt32(hdnDistributionID.Value);
                if (OnDeleteEntityDt(ref errMessage, distributionID))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpOrderID"] = distributionID.ToString();
        }

        private void ControlToEntity(ItemDistributionDt entityDt)
        {
            entityDt.ItemID = Convert.ToInt32(hdnItemID.Value);
            entityDt.Quantity = Convert.ToDecimal(txtQuantity.Text);
            entityDt.GCItemUnit = cboItemUnit.Value.ToString().Split('|')[0];
            entityDt.GCBaseUnit = hdnGCBaseUnit.Value;
            entityDt.ConversionFactor = Convert.ToDecimal(hdnConversionFactor.Value);
            entityDt.GCItemDetailStatus = Constant.DistributionStatus.OPEN;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage, ref int distributionID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemDistributionDtDao entityDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                SaveItemDistributionHd(ctx, ref distributionID);
                ItemDistributionDt entityDt = new ItemDistributionDt();
                ControlToEntity(entityDt);
                entityDt.DistributionID = distributionID;
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
            ItemDistributionDtDao entityDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                ItemDistributionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (!entityDt.IsDeleted)
                {
                    ControlToEntity(entityDt);
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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

        private bool OnDeleteEntityDt(ref string errMessage, int ID)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemDistributionDtDao entityDtDao = new ItemDistributionDtDao(ctx);
            try
            {
                ItemDistributionDt entityDt = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                if (!entityDt.IsDeleted)
                {
                    entityDt.IsDeleted = true;
                    entityDt.GCItemDetailStatus = Constant.TransactionStatus.VOID;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }
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

        #region callBack Trigger
        protected void cboItemUnit_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<vItemAlternateUnitCustom> lst = BusinessLayer.GetvItemAlternateUnitCustomList(string.Format("ItemID = {0}", hdnItemID.Value));
            Methods.SetComboBoxField<vItemAlternateUnitCustom>(cboItemUnit, lst, "cfAlternateUnit", "cfID");
            cboItemUnit.SelectedIndex = -1;
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
        #endregion
    }
}