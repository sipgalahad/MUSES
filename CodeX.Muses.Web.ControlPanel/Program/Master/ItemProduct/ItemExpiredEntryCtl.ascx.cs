using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Web.UI.HtmlControls;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ItemExpiredEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            ItemMaster entity = BusinessLayer.GetItemMaster(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = entity.ItemName1;

            List<Variable> lstViewType = new List<Variable>();
            lstViewType.Add(new Variable { Code = "0", Value = GetLabel("-- Semua -- ") });
            lstViewType.Add(new Variable { Code = "1", Value = GetLabel("Masih Ada Stok") });
            lstViewType.Add(new Variable { Code = "2", Value = GetLabel("Kosong") });
            Methods.SetComboBoxField<Variable>(cboViewType, lstViewType, "Value", "Code");
            cboViewType.Value = "1";

            RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);

            Helper.SetControlEntrySetting(txtBatchNumber, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtExpiredDate, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("ItemID = {0} AND IsDeleted = 0", hdnID.Value);
            string viewType = cboViewType.Value.ToString();
            if (viewType == "1")
                filterExpression += " AND IsEmpty = 0";
            else if (viewType == "2")
                filterExpression += " AND IsEmpty = 1";
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetItemExpiredRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_POPUP);
            }

            List<ItemExpired> lstEntity = BusinessLayer.GetItemExpiredList(filterExpression, Constant.GridViewPageSize.GRID_POPUP, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnIsAdd.Value == "0")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(ItemExpired entity)
        {
            entity.ExpiredDate = Helper.GetDatePickerValue(txtExpiredDate.Text);
            entity.IsEmpty = chkIsEmpty.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemExpiredDao entityDao = new ItemExpiredDao(ctx);
            try
            {
                ItemExpired entity = entityDao.Get(Convert.ToInt32(hdnID.Value), Request.Form[txtBatchNumber.UniqueID]);
                if (entity == null)
                {
                    entity = new ItemExpired();
                    ControlToEntity(entity);
                    entity.ItemID = Convert.ToInt32(hdnID.Value);
                    entity.BatchNumber = txtBatchNumber.Text;
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    entityDao.Insert(entity);
                }
                else
                {
                    ControlToEntity(entity);
                    entity.IsDeleted = false;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDao.Update(entity);
                }
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ItemExpiredDao entityDao = new ItemExpiredDao(ctx);
            try
            {
                ItemExpired entity = entityDao.Get(Convert.ToInt32(hdnID.Value), Request.Form[txtBatchNumber.UniqueID]);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ItemExpired entity = BusinessLayer.GetItemExpired(Convert.ToInt32(hdnID.Value), Request.Form[txtBatchNumber.UniqueID]);
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateItemExpired(entity);
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
    }
}