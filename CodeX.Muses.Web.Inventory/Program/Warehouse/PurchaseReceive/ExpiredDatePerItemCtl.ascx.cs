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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class ExpiredDatePerItemCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            BindGridView();
        }

        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            //if (grdView.Rows.Count < 1)
            //    BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = string.Format("ID = {0}", hdnID.Value);
            
            List<PurchaseReceiveDtExpired> lstEntity = BusinessLayer.GetPurchaseReceiveDtExpiredList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        

        protected void cbpPopupProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "save")
            {
                if (hdnID.Value.ToString() != "" && hdnBatchNumber.Value.ToString() != "")
                {
                    if (OnSaveEditRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnSaveDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    vPurchaseOrderDt entity = e.Row.DataItem as vPurchaseOrderDt;
            //    TextBox txtReceivedItem = e.Row.FindControl("txtReceivedItem") as TextBox;
            //    TextBox txtUnitPrice = e.Row.FindControl("txtUnitPrice") as TextBox;
            //    txtReceivedItem.Text = (entity.Quantity - entity.ReceivedQuantity).ToString();
            //    txtUnitPrice.Text = entity.UnitPrice.ToString();
            //}
        }

        private void ControlToEntity(PurchaseReceiveDtExpired entity)
        {
            entity.BatchNumber = txtBatchNumber.Text;
            entity.ExpiredDate = Helper.GetDatePickerValue(txtExpiredDate.Text);
            entity.Quantity = Convert.ToInt32(txtQuantity.Text);
        }

        protected bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            try
            {
                PurchaseReceiveDtExpired entity = new PurchaseReceiveDtExpired();
                entity.ID = Convert.ToInt32(hdnID.Value);
                ControlToEntity(entity);
                BusinessLayer.InsertPurchaseReceiveDtExpired(entity);
            }
            catch (Exception ex) 
            {
                result = false;
                errMessage = ex.Message;
            }
            return result;
        }

        protected bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            try
            {
                PurchaseReceiveDtExpired entity = BusinessLayer.GetPurchaseReceiveDtExpired(Convert.ToInt32(hdnID.Value), Request.Form[txtBatchNumber.UniqueID]);
                entity.Quantity = Convert.ToInt32(txtQuantity.Text);
                entity.ExpiredDate = Helper.GetDatePickerValue(txtExpiredDate.Text);
                BusinessLayer.UpdatePurchaseReceiveDtExpired(entity);
            }
            catch (Exception ex) 
            {
                result = false;
                errMessage = ex.Message;
            }
            return result;
        }

        protected bool OnSaveDeleteRecord(ref string errMessage) 
        {
            bool result = true;
            try
            {
                BusinessLayer.DeletePurchaseReceiveDtExpired(Convert.ToInt32(hdnID.Value), Request.Form[txtBatchNumber.UniqueID]);
            }
            catch (Exception ex)
            {
                result = false;
                errMessage = ex.Message;
            }
            return result;
        }
    }
}