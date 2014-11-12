using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public partial class ItemAlternateUnitEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnItemID.Value = param;

            vItemProduct item = BusinessLayer.GetvItemProductList(String.Format("ItemID = {0}", param)).FirstOrDefault();
            txtItemCode.Text = item.ItemCode;
            txtItemName.Text = item.ItemName1;
            txtItemUnit.Text = item.ItemUnit;

            BindGridView();
            InitializeComboBoxFields();
            cboGCAlternateUnit.Attributes.Add("validationgroup", "mpEntryPopup");
            txtConversionFactor.Attributes.Add("validationgroup", "mpEntryPopup");
        }

        private void InitializeComboBoxFields()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ITEM_UNIT));
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });

            Methods.SetComboBoxField<StandardCode>(cboGCAlternateUnit, lstStandardCode, "StandardCodeName", "StandardCodeID");

            cboGCAlternateUnit.SelectedIndex = 0;
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvItemAlternateUnitList(string.Format("ItemID = {0} AND IsDeleted = 0", hdnItemID.Value));
            grdView.DataBind();
        }

        private void ControlToEntity(ItemAlternateUnit entity)
        {
            entity.ItemID = Convert.ToInt32(hdnItemID.Value);            
            entity.GCAlternateUnit= cboGCAlternateUnit.Value.ToString();
            entity.ConversionFactor = Convert.ToDecimal(txtConversionFactor.Text);
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();
            //BindGridView();

            string param = e.Parameter;

            string result = param + "|";
            string errMessage = "";

            if (param == "save")
            {
                if (hdnID.Value.ToString() != "")
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
            else if (param == "delete")
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            BindGridView();

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;

        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                ItemAlternateUnit entity = new ItemAlternateUnit();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
                BusinessLayer.InsertItemAlternateUnit(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                ItemAlternateUnit entity = BusinessLayer.GetItemAlternateUnit(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateItemAlternateUnit(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                ItemAlternateUnit entity = BusinessLayer.GetItemAlternateUnit(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateItemAlternateUnit(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}