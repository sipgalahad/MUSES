using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class MenuReportEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnMenuID.Value = param;
            MenuMaster entity = BusinessLayer.GetMenuMaster(Convert.ToInt32(hdnMenuID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.MenuCode, entity.MenuCaption);

            BindGridView();

            txtReportCode.Attributes.Add("validationgroup", "mpEntryPopup");
            txtReportName.Attributes.Add("validationgroup", "mpEntryPopup");
            txtDisplayOrder.Attributes.Add("validationgroup", "mpEntryPopup");
        }

        protected string OnGetReportFilterExpression()
        {
            return string.Format("ReportID NOT IN (SELECT ReportID FROM MenuReport WHERE MenuID = '{0}') AND GCReportType = '{1}' AND IsDeleted = 0", hdnMenuID.Value, Constant.ReportType.FORM);
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvMenuReportList(string.Format("MenuID = {0} ORDER BY DisplayOrder ASC", hdnMenuID.Value));
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }

        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            string param = e.Parameter;

            string result = param + "|";
            string errMessage = "";

            if (param == "save")
            {
                if (hdnIsAdd.Value.ToString() != "1")
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

        private void ControlToEntity(MenuReport entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.IsSelected = chkIsSelected.Checked;
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                MenuReport entity = new MenuReport();
                ControlToEntity(entity);
                entity.ReportID = Convert.ToInt32(hdnReportID.Value);
                entity.MenuID = Convert.ToInt32(hdnMenuID.Value);
                BusinessLayer.InsertMenuReport(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                MenuReport entity = BusinessLayer.GetMenuReport(Convert.ToInt32(hdnMenuID.Value), Convert.ToInt32(hdnReportID.Value));
                ControlToEntity(entity);
                BusinessLayer.UpdateMenuReport(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteMenuReport(Convert.ToInt32(hdnMenuID.Value), Convert.ToInt32(hdnReportID.Value));
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}