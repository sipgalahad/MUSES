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
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class LocationPermissionDtEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnRestrictionID.Value = param;

            RestrictionHd entityHd = BusinessLayer.GetRestrictionHd(Convert.ToInt32(hdnRestrictionID.Value));
            txtRestrictionName.Text = entityHd.RestrictionName;

            if (param != "")
            {
                List<vRestrictionDt> lstSelected = BusinessLayer.GetvRestrictionDtList(string.Format("RestrictionID = {0}", hdnRestrictionID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.TransactionCode).ToList());
            }

            BindGridView(1, true, ref PageCount);
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("TransactionCode LIKE '%{0}%' AND TransactionName LIKE '%{1}%' AND IsInventoryTransaction = 1", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetTransactionTypeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<TransactionType> lstEntity = BusinessLayer.GetTransactionTypeList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransactionType entity = e.Row.DataItem as TransactionType;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.TransactionCode.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RestrictionDtDao entityDtDao = new RestrictionDtDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int RestrictionID = Convert.ToInt32(hdnRestrictionID.Value);

                List<RestrictionDt> lstRestrictionDt = BusinessLayer.GetRestrictionDtList(string.Format("RestrictionID = {0}", RestrictionID, hdnSelectedMember.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    string TransactionCode = lstSelectedMember[ct];
                    RestrictionDt entityDt = lstRestrictionDt.FirstOrDefault(p => p.TransactionCode == TransactionCode);
                    if (entityDt == null)
                    {
                        entityDt = new RestrictionDt();
                        entityDt.RestrictionID = RestrictionID;
                        entityDt.TransactionCode = TransactionCode;
                        entityDtDao.Insert(entityDt);
                    }
                    ct++;
                }
                foreach (RestrictionDt entity in lstRestrictionDt)
                {
                    if (!lstSelectedMember.Contains(entity.TransactionCode.ToString()))
                        entityDtDao.Delete(RestrictionID, entity.TransactionCode);
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
    }
}