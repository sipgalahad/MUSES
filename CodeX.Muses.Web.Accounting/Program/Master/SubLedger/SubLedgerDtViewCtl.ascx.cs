using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class SubLedgerDtViewCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        private void CreateGridColumn()
        {
            grdView.Columns.Add(CreateColumn("ID", hdnIDFieldName.Value, "keyField"));
            grdView.Columns.Add(CreateColumn("Code", hdnCodeFieldName.Value, "", 150));
            grdView.Columns.Add(CreateColumn("Name", hdnDisplayFieldName.Value, ""));

        }

        private BoundField CreateColumn(string title, string dataField, string cssClass, int width = 0)
        {
            BoundField field = new BoundField();
            field.HeaderText = title;
            field.DataField = dataField;
            if (cssClass != "")
                field.HeaderStyle.CssClass = field.ItemStyle.CssClass = cssClass;
            if (width > 0)
                field.HeaderStyle.Width = new Unit(width);
            return field;
        }

        public override void InitializeDataControl(string param)
        {
            hdnSubLedgerID.Value = param;

            vSubLedgerHd entity = BusinessLayer.GetvSubLedgerHdList(string.Format("SubLedgerID = {0}", param))[0];
            txtSubLedgerName.Text = string.Format("{0} - {1}", entity.SubLedgerCode, entity.SubLedgerName);
            hdnSubLedgerTypeID.Value = entity.SubLedgerTypeID.ToString();
            hdnTableName.Value = entity.TableName;
            hdnFilterExpression.Value = entity.FilterExpression.Replace("@SubLedgerID", param);
            hdnIDFieldName.Value = entity.IDFieldName;
            hdnCodeFieldName.Value = entity.CodeFieldName;
            hdnDisplayFieldName.Value = entity.DisplayFieldName;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                CreateGridColumn();
                string filterExpression = hdnFilterExpression.Value;
                if (isCountPageCount)
                {
                    string result = string.Format("SELECT COUNT(*) FROM {0} ", hdnTableName.Value);
                    if (filterExpression != null && filterExpression.Trim().Length > 0)
                        result += string.Format("WHERE {0}", string.Format(filterExpression));

                    ctx.CommandText = result;
                    DataRow row = DaoBase.GetDataRow(ctx);
                    rowCount = Convert.ToInt32(row.ItemArray.GetValue(0));
                    pageCount = Helper.GetPageCount(rowCount, 8);
                }
                ctx.CommandText = Select(hdnTableName.Value, filterExpression, 8, pageIndex, "");
                DataTable dataTable = DaoBase.GetDataTable(ctx);
                grdView.DataSource = dataTable;
                grdView.DataBind();
            }
            finally
            {
                ctx.Close();
            }
        }

        public string Select(string tableName, string filterExpression, int numRows, int pageIndex, string orderByExpression)
        {
            if (filterExpression != "")
                filterExpression = " WHERE " + filterExpression;
            int startIndex = (pageIndex - 1) * numRows;
            int endIndex = pageIndex * numRows;
            if (orderByExpression == null || orderByExpression == "")
                orderByExpression = "(SELECT 0)";
            return string.Format("SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY {0}) - 1 as row FROM {1}{4}) a WHERE a.row >= {2} and a.row < {3}", orderByExpression, tableName, startIndex, endIndex, filterExpression);
            //return string.Format("SELECT * FROM {0} ", _tableName);
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
    }
}