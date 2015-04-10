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
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class CreateTariffEntry : BasePageTrx
    {
        protected int PageCount = 1;
        private const int GridViewPageSize = 10;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.CREATE_TARIFF;
        }

        protected override void InitializeDataControl()
        {
        }

        protected override void SetControlProperties()
        {
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<Site> lstSite = BusinessLayer.GetSiteList("");
            Methods.SetComboBoxField<Site>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.Value = AppSession.UserLogin.SiteID;

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}', '{1}') AND IsDeleted = 0", Constant.StandardCode.TARIFF_SCHEME, Constant.StandardCode.ITEM_TYPE));

            Methods.SetComboBoxField<StandardCode>(cboTariffScheme, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.TARIFF_SCHEME).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboItemType, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ITEM_TYPE).ToList(), "StandardCodeName", "StandardCodeID");

            cboSite.SelectedIndex = 0;
            cboTariffScheme.SelectedIndex = 0;
            cboItemType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboSite, new ControlEntrySetting(true, false, true, AppSession.UserLogin.SiteID));
            SetControlEntrySetting(cboTariffScheme, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboItemType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtEffectiveDate, new ControlEntrySetting(true, true, true, DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtDocumentNo, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDocumentDate, new ControlEntrySetting(true, true, true, DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtRevisionNo, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(chkPPN, new ControlEntrySetting(true, true, false));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnPageCount.Value = "0";
        }

        protected string GetFilterExpression()
        {
            return "";
        }

        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvTariffBookHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTariffBookHd entity = BusinessLayer.GetvTariffBookHd(filterExpression, PageIndex, "BookID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTariffBookHdRowIndex(filterExpression, keyValue, "BookID DESC");
            vTariffBookHd entity = BusinessLayer.GetvTariffBookHd(filterExpression, PageIndex, "BookID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private bool IsEditable = false;
        private void EntityToControl(vTariffBookHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatus;
                IsEditable = false;
            }
            else
                IsEditable = true;

            hdnBookID.Value = entity.BookID.ToString();
            cboSite.Value = entity.SiteID;
            cboTariffScheme.Value = entity.GCTariffScheme;
            cboItemType.Value = entity.GCItemType;
            txtEffectiveDate.Text = entity.StartingDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtDocumentNo.Text = entity.DocumentNo;
            txtDocumentDate.Text = entity.DocumentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtRevisionNo.Text = entity.RevisionNo.ToString();
            chkPPN.Checked = entity.IsIncludeVAT;

            BindGridView(1, true, ref PageCount);
            hdnPageCount.Value = PageCount.ToString();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = "IsDeleted = 0";
            if (hdnItemGroupID.Value != "0" && hdnItemGroupID.Value != "")
                filterExpression += string.Format(" AND ItemGroupID IN (SELECT ItemGroupID FROM vItemGroup WHERE DisplayPath like '%/{0}/%')", hdnItemGroupID.Value);
            if (hdnFilterExpressionQuickSearch.Value == "Search")
                hdnFilterExpressionQuickSearch.Value = " ";
            if (hdnFilterExpressionQuickSearch.Value != "")
                filterExpression += string.Format(" AND {0}", hdnFilterExpressionQuickSearch.Value);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetItemMasterRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, GridViewPageSize);
            }

            List<ItemMaster> lstEntity = BusinessLayer.GetItemMasterList(filterExpression, GridViewPageSize, pageIndex, "ItemName1 ASC");
            string lstItemID = String.Join(",", lstEntity.Select(p => p.ItemID).ToList());
            if (lstItemID != "")
            {
                lstItemTariff = BusinessLayer.GetvItemTariffCustomList(string.Format("ItemID IN ({0})", lstItemID));
                lstItemTariffBookDt = BusinessLayer.GetTariffBookDtList(string.Format("BookID = {0} AND ItemID IN ({1})", hdnBookID.Value, lstItemID));
            }
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<vItemTariffCustom> lstItemTariff = null;
        List<TariffBookDt> lstItemTariffBookDt = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lstItemTariff != null)
                {
                    ItemMaster entity = (ItemMaster)e.Row.DataItem;
                    vItemTariffCustom itemTariff = lstItemTariff.FirstOrDefault(p => p.ItemID == entity.ItemID);
                    HtmlGenericControl divCurrentTariff = (HtmlGenericControl)e.Row.FindControl("divCurrentTariff");
                    HtmlGenericControl divCurrentTariffAfterVAT = (HtmlGenericControl)e.Row.FindControl("divCurrentTariffAfterVAT");
                    decimal tariff = 0;
                    if (itemTariff != null)
                        tariff = Convert.ToDecimal(itemTariff.Tariff);
                    divCurrentTariff.InnerHtml = tariff.ToString("N");
                    divCurrentTariffAfterVAT.InnerHtml = Math.Round(tariff * (100 + Convert.ToDecimal(hdnVATPercentage.Value)) / 100).ToString("N");

                    HtmlInputText txtNewTariff = (HtmlInputText)e.Row.FindControl("txtNewTariff");
                    TariffBookDt tariffBookDt = lstItemTariffBookDt.FirstOrDefault(p => p.ItemID == entity.ItemID);
                    decimal newTariff = 0;
                    if (tariffBookDt != null)
                        newTariff = tariffBookDt.BaseTariff;
                    txtNewTariff.Value = newTariff.ToString();

                }
            }
        }
        #endregion

        #region Save
        private void ControlToEntity(TariffBookHd entity)
        {
            entity.SiteID = cboSite.Value.ToString();
            entity.GCTariffScheme = cboTariffScheme.Value.ToString();
            entity.GCItemType = cboItemType.Value.ToString();
            entity.StartingDate = Helper.GetDatePickerValue(txtEffectiveDate);
            entity.DocumentNo = txtDocumentNo.Text;
            entity.DocumentDate = Helper.GetDatePickerValue(txtDocumentDate);
            entity.RevisionNo = Byte.Parse(txtRevisionNo.Text);
            entity.IsIncludeVAT = chkPPN.Checked;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TariffBookHdDao entityDao = new TariffBookHdDao(ctx);
            bool result = false;
            try
            {
                TariffBookHd entity = new TariffBookHd();
                ControlToEntity(entity);
                entity.PreparedBy = AppSession.UserLogin.UserID;
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetTariffBookHdMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
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
                TariffBookHd entity = BusinessLayer.GetTariffBookHd(Convert.ToInt32(hdnBookID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTariffBookHd(entity);
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
            string filterExpression = String.Format("BookID = {0}", hdnBookID.Value);

            List<vTariffBookDt> lstTariffBookDt = BusinessLayer.GetvTariffBookDtList(filterExpression);
            TariffBookHd tariffBookHd = BusinessLayer.GetTariffBookHd(Convert.ToInt32(hdnBookID.Value));

            IDbContext ctx = DbFactory.Configure(true);
            ItemTariffDao itemTariffDao = new ItemTariffDao(ctx);
            TariffBookHdDao tariffBookHdDao = new TariffBookHdDao(ctx);
            try
            {
                decimal vatPercetage = Convert.ToDecimal(hdnVATPercentage.Value);
                foreach (vTariffBookDt tariffBookDt in lstTariffBookDt)
                {
                    ItemTariff itemTariff = new ItemTariff();
                    itemTariff.SiteID = tariffBookHd.SiteID;
                    itemTariff.BookID = tariffBookDt.BookID;
                    itemTariff.ItemID = tariffBookDt.ItemID;
                    itemTariff.GCItemType = tariffBookDt.GCItemType;
                    itemTariff.GCTariffScheme = tariffBookDt.GCTariffScheme;
                    if (tariffBookHd.IsIncludeVAT)
                        itemTariff.Tariff = tariffBookDt.ApprovedTariff * 100 / (100 + vatPercetage);
                    else
                        itemTariff.Tariff = tariffBookDt.ApprovedTariff;
                    itemTariff.StartingDate = tariffBookHd.StartingDate;
                    itemTariff.CreatedBy = AppSession.UserLogin.UserID;

                    itemTariffDao.Insert(itemTariff);
                } 
                
                tariffBookHd.GCTransactionStatus = Constant.TransactionStatus.CLOSED;
                tariffBookHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                tariffBookHdDao.Update(tariffBookHd);

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

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (UpdateTariffBookDt(param, ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool UpdateTariffBookDt(string[] param, ref string errMessage)
        {
            try
            {
                Int32 bookID = Convert.ToInt32(hdnBookID.Value);
                Int32 itemID = Convert.ToInt32(param[1]);
                Decimal tariff = Convert.ToDecimal(param[2]);

                TariffBookDt entity = BusinessLayer.GetTariffBookDt(bookID, itemID, 1);
                if (entity != null)
                {
                    entity.BaseTariff = entity.ApprovedBaseTariff = entity.ApprovedTariff = entity.ProposedTariff = entity.SuggestedTariff = tariff;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateTariffBookDt(entity);
                }
                else
                {
                    entity = new TariffBookDt();
                    entity.ItemID = itemID;
                    entity.BookID = bookID;
                    entity.BaseTariff = entity.ApprovedBaseTariff = entity.ApprovedTariff = entity.ProposedTariff = entity.SuggestedTariff = tariff;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.InsertTariffBookDt(entity);
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

        protected override bool OnVoidRecord(ref string errMessage)
        {
            try
            {
                TariffBookHd entity = BusinessLayer.GetTariffBookHd(Convert.ToInt32(hdnBookID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTariffBookHd(entity);
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

        #region Callback
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}