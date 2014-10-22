using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Web.Common;
using QIS.Medinfras.Data.Service;
using QIS.Data.Core.Dal;

namespace QIS.Medinfras.Web.Accounting.Program
{
    public partial class FAWriteOffEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.FA_WRITE_OFF;
        }

        public String GetFAWriteOffDateFilterExpression() 
        {
            DateTime date = Helper.GetDatePickerValue(txtFAWriteOffDate.Text);
            string filterExpression = String.Format("YEAR(DepreciationDate) = {0} AND MONTH(DepreciationDate) = {1}",date.Year,date.Month);
            return filterExpression;
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowAdd = false;
        }

        protected override void InitializeDataControl()
        {
            hdnFixedAssetID.Value = AppSession.FixedAssetID.ToString();
            string filterExpression = String.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SUPPLIER_PAYMENT_METHOD, Constant.StandardCode.TIPE_PEMUSNAHAN);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField(cboAssetWriteOffType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TIPE_PEMUSNAHAN).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboAssetSalesType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUPPLIER_PAYMENT_METHOD).ToList(), "StandardCodeName", "StandardCodeID");

            List<FAWriteOff> lstEntity = BusinessLayer.GetFAWriteOffList(String.Format("FixedAssetID = {0} AND GCTransactionStatus = '{1}'", hdnFixedAssetID.Value, Constant.TransactionStatus.APPROVED));

            hdnFAWriteOffID.Value = "";
            if (lstEntity.Count > 0)
            {
                EntityToControl(lstEntity[0]);
                IsLoadFirstRecord = true;
            }
            else
                IsLoadFirstRecord = false;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtFAWriteOffNo, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtFAWriteOffDate, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboAssetWriteOffType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboAssetSalesType, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtAssetValue, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtWriteOffAmount, new ControlEntrySetting(false, false, true));
        }

        private void EntityToControl(FAWriteOff entity) 
        {
            hdnFAWriteOffID.Value = entity.FAWriteOffID.ToString();
            txtFAWriteOffDate.Text = entity.FAWriteOffDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboAssetWriteOffType.Value = entity.GCAssetWriteOffType;
            cboAssetSalesType.Value = entity.GCAssetSalesType;
            txtAssetValue.Text = entity.AssetValue.ToString();
            txtWriteOffAmount.Text = entity.WriteOffAmount.ToString();
            txtSelisih.Text = (entity.AssetValue - entity.WriteOffAmount).ToString("N");
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(FAWriteOff entity) 
        {
            entity.FixedAssetID = Convert.ToInt32(hdnFixedAssetID.Value);
            entity.FAWriteOffDate = Helper.GetDatePickerValue(txtFAWriteOffDate.Text);
            entity.GCAssetWriteOffType = cboAssetWriteOffType.Value.ToString();
            entity.GCAssetSalesType = cboAssetSalesType.Value.ToString();
            entity.AssetValue = Convert.ToDecimal(Request.Form[txtAssetValue.UniqueID]);
            entity.WriteOffAmount = Convert.ToDecimal(Request.Form[txtWriteOffAmount.UniqueID]);
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            FAWriteOffDao faWriteOffDao = new FAWriteOffDao(ctx);
            FAItemDao faItemDao = new FAItemDao(ctx);

            bool result = true;
            try
            {
                FAWriteOff faWriteOff = new FAWriteOff();

                ControlToEntity(faWriteOff);
                faWriteOff.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                faWriteOff.LastUpdatedBy = faWriteOff.CreatedBy = AppSession.UserLogin.UserID;
                faWriteOff.FAWriteOffNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.FIXED_ASSET_WRITE_OFF, faWriteOff.FAWriteOffDate, ctx);
                ctx.CommandType = System.Data.CommandType.Text;
                ctx.Command.Parameters.Clear();
                faWriteOffDao.Insert(faWriteOff);

                FAItem faItem = faItemDao.Get(faWriteOff.FixedAssetID);
                faItem.GCItemStatus = Constant.ItemStatus.IN_ACTIVE;
                faItem.LastUpdatedBy = AppSession.UserLogin.UserID;
                faItemDao.Update(faItem);

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
            bool result = true;
            
            try
            {
                FAWriteOff faWriteOff = BusinessLayer.GetFAWriteOff(Convert.ToInt32(hdnFAWriteOffID.Value));
                faWriteOff.Remarks = txtRemarks.Text;
                BusinessLayer.UpdateFAWriteOff(faWriteOff);
            }
            catch(Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }

            return result;
        }
    }
}