using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using System.Data;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class DirectPurchaseConfirmationEditDtCtl : BaseEntryPopupCtl
    {
        protected string GetVATPercentageLabel()
        {
            return hdnVATPercentage.Value;
        }

        public override void InitializeDataControl(string param)
        {
            IsAdd = false;
            hdnVATPercentage.Value = BusinessLayer.GetSettingParameter(Constant.SettingParameter.VAT_PERCENTAGE).ParameterValue;

            List<StandardCode> listStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.DIRECT_PURCHASE_TYPE, Constant.StandardCode.ITEM_UNIT));
            Methods.SetComboBoxField<StandardCode>(cboDirectPurchaseType, listStandardCode.Where(p => p.ParentID == Constant.StandardCode.DIRECT_PURCHASE_TYPE).ToList<StandardCode>(), "StandardCodeName", "StandardCodeID");
            cboDirectPurchaseType.SelectedIndex = 0;

            hdnDirectPurchaseID.Value = param;

            vDirectPurchaseHd entity = BusinessLayer.GetvDirectPurchaseHdList(string.Format("DirectPurchaseID = {0}", hdnDirectPurchaseID.Value)).FirstOrDefault();
            txtDirectPurchaseNo.Text = entity.DirectPurchaseNo;
            txtDirectPurchaseDate.Text = entity.PurchaseDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtReferenceNo.Text = entity.ReferenceNo;
            if (entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtReferenceDate.Text = entity.ReferenceDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            else
                txtReferenceDate.Text = "";            
            txtRemarks.Text = entity.Remarks;
            chkPPN.Checked = entity.IsIncludeVAT;
            txtPPN.Text = entity.VATAmount.ToString();
            txtTransactionAmount.Text = entity.TransactionAmount.ToString();
            txtTotalNetTransactionAmount.Text = entity.TotalNetTransactionAmount.ToString();
            txtFinalDiscountPercentage.Text = entity.FinalDiscountPercentage.ToString();
            txtFinalDiscountAmount.Text = entity.FinalDiscountAmount.ToString();
            txtLocationName.Text = entity.LocationName;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            txtToServiceUnitName.Text = entity.ToServiceUnitName;
            cboDirectPurchaseType.Value = entity.GCDirectPurchaseType;

            vSupplier sup = BusinessLayer.GetvSupplierList(string.Format("BusinessPartnerID = {0}", entity.BusinessPartnerID)).FirstOrDefault();
            hdnIsLineAmountRounded.Value = sup.IsLineAmountRounded ? "1" : "0";
            hdnLineAmountRoundedFormat.Value = sup.LineAmountRoundedFormat.ToString();
            hdnIsTotalAmountRounded.Value = sup.IsTotalAmountRounded ? "1" : "0";
            hdnTotalAmountRoundedFormat.Value = sup.TotalAmountRoundedFormat.ToString();
            txtSupplier.Text = sup.BusinessPartnerName;

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("DirectPurchaseID = {0} AND GCItemDetailStatus != '{1}'", hdnDirectPurchaseID.Value, Constant.TransactionStatus.VOID);
            List<vDirectPurchaseDt> lstEntity = BusinessLayer.GetvDirectPurchaseDtList(filterExpression);
            lvwView.DataSource = lstEntity;
            lvwView.DataBind();
        }

        protected void lvwView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                vDirectPurchaseDt entity = (vDirectPurchaseDt)e.Item.DataItem;
                TextBox txtUnitPrice = (TextBox)e.Item.FindControl("txtUnitPrice");
                TextBox txtDiscountPercentage = (TextBox)e.Item.FindControl("txtDiscountPercentage");
                TextBox txtDiscountAmount = (TextBox)e.Item.FindControl("txtDiscountAmount");
                TextBox txtLineAmount = (TextBox)e.Item.FindControl("txtLineAmount");
                txtUnitPrice.Text = entity.UnitPrice.ToString();
                txtDiscountPercentage.Text = entity.DiscountPercentage.ToString();
                txtDiscountAmount.Text = entity.DiscountAmount.ToString();
                txtLineAmount.Text = entity.LineAmount.ToString();
            }
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            DirectPurchaseHdDao directPurchaseHdDao = new DirectPurchaseHdDao(ctx);
            DirectPurchaseDtDao directPurchaseDtDao = new DirectPurchaseDtDao(ctx);

            string[] lstSaveValue = hdnSaveValue.Value.Split('|');
            try
            {
                string tempGCTransactionStatus = "";
                DirectPurchaseHd directPurchaseHd = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                tempGCTransactionStatus = directPurchaseHd.GCTransactionStatus;
                directPurchaseHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                directPurchaseHdDao.Update(directPurchaseHd);

                directPurchaseHd = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                directPurchaseHd.ReferenceNo = Request.Form[txtReferenceNo.UniqueID];
                directPurchaseHd.ReferenceDate = Helper.GetDatePickerValue(Request.Form[txtReferenceDate.UniqueID]);
                directPurchaseHd.GCDirectPurchaseType = cboDirectPurchaseType.Value.ToString();
                directPurchaseHd.IsIncludeVAT = chkPPN.Checked;
                if (directPurchaseHd.IsIncludeVAT)
                    directPurchaseHd.VATPercentage = Convert.ToInt32(hdnVATPercentage.Value);
                else
                    directPurchaseHd.VATPercentage = 0;
                directPurchaseHd.VATAmount = Convert.ToDecimal(Request.Form[txtPPN.UniqueID]);
                directPurchaseHd.Remarks = Request.Form[txtRemarks.UniqueID];
                directPurchaseHd.FinalDiscountPercentage = Convert.ToDecimal(Request.Form[txtFinalDiscountPercentage.UniqueID]);
                directPurchaseHd.FinalDiscountAmount = Convert.ToDecimal(Request.Form[txtFinalDiscountAmount.UniqueID]);
                directPurchaseHd.TransactionAmountBeforeRounded = directPurchaseHd.TransactionAmount + directPurchaseHd.VATAmount - directPurchaseHd.FinalDiscountAmount;
                directPurchaseHd.TotalNetTransactionAmount = Convert.ToDecimal(Request.Form[txtTotalNetTransactionAmount.UniqueID]);
                directPurchaseHd.RoundedAmount = directPurchaseHd.TotalNetTransactionAmount - directPurchaseHd.TransactionAmountBeforeRounded;

                List<DirectPurchaseDt> lstDirectPurchaseDt = BusinessLayer.GetDirectPurchaseDtList(string.Format("ID IN ({0})", hdnLstID.Value), ctx);
                
                ctx.CommandText = "ALTER TABLE DirectPurchaseDt DISABLE TRIGGER onDirectPurchaseDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    DirectPurchaseDt directPurchaseDt = lstDirectPurchaseDt.FirstOrDefault(p => p.ID == Convert.ToInt32(temp[0]));
                    directPurchaseDt.UnitPrice = Convert.ToDecimal(temp[1]);
                    directPurchaseDt.DiscountPercentage = Convert.ToDecimal(temp[2]);
                    directPurchaseDt.DiscountAmount = Convert.ToDecimal(temp[3]);
                    directPurchaseDt.LineAmountBeforeRounded = (directPurchaseDt.Quantity * directPurchaseDt.UnitPrice) - directPurchaseDt.DiscountAmount;
                    directPurchaseDt.LineAmount = Convert.ToDecimal(temp[4]);
                    directPurchaseDt.RoundedAmount = directPurchaseDt.LineAmount - directPurchaseDt.LineAmountBeforeRounded;
                    directPurchaseDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    directPurchaseDtDao.Update(directPurchaseDt);
                }
                ctx.CommandText = "ALTER TABLE DirectPurchaseDt ENABLE TRIGGER onDirectPurchaseDtChanged";
                DaoBase.ExecuteNonQuery(ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                directPurchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                directPurchaseHdDao.Update(directPurchaseHd);

                directPurchaseHd = directPurchaseHdDao.Get(Convert.ToInt32(hdnDirectPurchaseID.Value));
                directPurchaseHd.TotalNetTransactionAmount = directPurchaseHd.TotalNetTransactionAmount;
                directPurchaseHd.GCTransactionStatus = tempGCTransactionStatus;
                directPurchaseHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                directPurchaseHdDao.Update(directPurchaseHd);

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
    }
}