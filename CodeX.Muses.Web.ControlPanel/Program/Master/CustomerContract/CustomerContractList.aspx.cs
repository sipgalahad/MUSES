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
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CustomerContractList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CUSTOMER_CONTRACT;
        }

        protected string OnGetCustomerFilterExpression()
        {
            return string.Format("GCBusinessPartnerType = '{0}' AND IsDeleted = 0", Constant.BusinessObjectType.CUSTOMER);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            if (Request.Form["customerID"] != null)
            {
                hdnCustomerID.Value = Request.Form["customerID"].ToString();
                BusinessPartners entity = BusinessLayer.GetBusinessPartners(Convert.ToInt32(hdnCustomerID.Value));
                txtCustomerCode.Text = entity.BusinessPartnerCode;
                txtCustomerName.Text = entity.BusinessPartnerName;
            }

            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetCustomerContractRowIndex(filterExpression, keyValue) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
                CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Contract No" };
            fieldListValue = new string[] { "ContractNo" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = "1 = 0";
            if (hdnCustomerID.Value != "")
            {
                filterExpression = hdnFilterExpression.Value;
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("BusinessPartnerID = {0} AND IsDeleted = 0", hdnCustomerID.Value);
            }
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetCustomerContractRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }   

            List<CustomerContract> lstEntity = BusinessLayer.GetCustomerContractList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
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

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            if (hdnCustomerID.Value != "")
            {
                url = ResolveUrl(string.Format("~/Program/Master/CustomerContract/CustomerContractEntry.aspx?id={0}", hdnCustomerID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/Master/CustomerContract/CustomerContractEntry.aspx?id={0}|{1}", hdnCustomerID.Value, hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                CustomerContract entity = BusinessLayer.GetCustomerContract(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCustomerContract(entity);
                return true;
            }
            return false;
        }
    }
}