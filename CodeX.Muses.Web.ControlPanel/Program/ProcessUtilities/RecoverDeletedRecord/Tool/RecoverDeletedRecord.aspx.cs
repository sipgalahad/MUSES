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
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class RecoverDeletedRecord : BasePage
    {
        RestoreDataConfiguration hd = null;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                hdnHeaderID.Value = Request.QueryString["id"];
            }

            hd = BusinessLayer.GetRestoreDataConfigurationList(string.Format("ID = {0}", Request.QueryString["id"]))[0];
            string[] gridColumns = hd.GridColumns.Split('|');
            for (int i = 0; i < gridColumns.Length; ++i)
            {
                BoundField field = new BoundField();
                field.DataField = field.HeaderText = gridColumns[i];
                if (i == 0)
                    field.HeaderStyle.CssClass = field.ItemStyle.CssClass = "keyField";
                else
                    txtSearchView.IntellisenseHints.Add(new CodeX.Web.CustomControl.QISIntellisenseHint { FieldName = gridColumns[i], Text = gridColumns[i] });
                grdView.Columns.Add(field);
            }
        }

        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
                BindGridView(1, true, ref PageCount, ref RowCount);
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            IDbContext ctx = DbFactory.Configure(true);
            try
            {
                string filterExpression = hdnFilterExpression.Value;
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += hd.FilterExpression;
                if (isCountPageCount)
                {
                    string result = string.Format("SELECT COUNT(*) FROM {0} ", hd.TableName);
                    if (filterExpression != null && filterExpression.Trim().Length > 0)
                        result += string.Format("WHERE {0}", string.Format(filterExpression));

                    ctx.CommandText = result;
                    DataRow row = DaoBase.GetDataRow(ctx);
                    rowCount = Convert.ToInt32(row.ItemArray.GetValue(0));
                    pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
                }
                ctx.CommandText = Select(hd.TableName, filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "");
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

        public string Select(string tableName, string filterExpression)
        {
            string result = string.Format("SELECT * FROM {0} ", tableName);
            if (filterExpression != null && filterExpression.Trim().Length > 0)
            {
                result += string.Format("WHERE {0}", filterExpression);
            }
            return result;
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

        protected void cbpRestore_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (hdnID.Value != "")
            {
                IDbContext ctx = DbFactory.Configure(true);
                try
                {
                    string idColumn = hd.GridColumns.Split('|')[0];
                    ctx.CommandText = string.Format("UPDATE {0} SET IsDeleted = 0 WHERE {1} = {2}", hd.TableName, idColumn, hdnID.Value);
                    DaoBase.ExecuteNonQuery(ctx);
                    ctx.CommitTransaction();
                    result = "success";
                }
                catch (Exception ex)
                {
                    result = "fail|" + ex.Message;
                    ctx.RollBackTransaction();
                }
                finally
                {
                    ctx.Close();
                }
            }
            else
            {
                result = "fail|Please Select Row First";
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}