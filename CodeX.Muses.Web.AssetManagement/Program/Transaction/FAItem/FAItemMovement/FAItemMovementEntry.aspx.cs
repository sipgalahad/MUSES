using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;

namespace Codex.Muses.Web.AssetManagement.Program
{
    public partial class FAItemMovementEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.AssetManagement.FA_ITEM_MOVEMENT;
        }

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            txtMovementDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            BindGridView(1, true, ref PageCount, ref RowCount);
            Helper.SetControlEntrySetting(txtMovementDate, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtToLocationCode, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = String.Format("FixedAssetDtID = {0} AND GCTransactionStatus NOT IN ('{1}','{2}')", AppSession.FixedAssetDtID, Constant.TransactionStatus.VOID, Constant.TransactionStatus.CLOSED);

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvFAItemMovementRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vFAItemMovement> lstEntity = BusinessLayer.GetvFAItemMovementList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "MovementNo DESC");
            if (lstEntity.Count > 0 && pageIndex == 1) lstEntity[0].IsAllowEditItem = true;
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

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
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

        private void ControlToEntity(FAItemMovement entity)
        {
            entity.ToFALocationID = Convert.ToInt32(hdnToLocationID.Value);
            entity.MovementDate = Helper.GetDatePickerValue(txtMovementDate.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemMovementDao entityDao = new FAItemMovementDao(ctx);
            FAItemDtDao entityFAItemDao = new FAItemDtDao(ctx);
            try
            {
                FAItemDt entityFAItem = entityFAItemDao.Get(AppSession.FixedAssetDtID);
                FAItemMovement entity = new FAItemMovement();

                entity.FixedAssetDtID = AppSession.FixedAssetDtID;
                entity.FromFALocationID = entityFAItem.FALocationID;
                entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                ControlToEntity(entity);

                entity.MovementNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.FIXED_ASSET_ITEM_MOVEMENT, entity.MovementDate, ctx);
                ctx.CommandType = System.Data.CommandType.Text;
                ctx.Command.Parameters.Clear();
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                entityFAItem.FALocationID = entity.ToFALocationID;
                entityFAItem.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityFAItemDao.Update(entityFAItem);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
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
            FAItemMovementDao entityDao = new FAItemMovementDao(ctx);
            FAItemDtDao entityFAItemDao = new FAItemDtDao(ctx);
            try
            {
                FAItemMovement entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                FAItemDt entityFAItem = entityFAItemDao.Get(entity.FixedAssetDtID);

                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityFAItem.FALocationID = entity.ToFALocationID;
                entityFAItem.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityFAItemDao.Update(entityFAItem);
                entityDao.Update(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemMovementDao entityDao = new FAItemMovementDao(ctx);
            FAItemDtDao entityFAItemDao = new FAItemDtDao(ctx);
            try
            {
                FAItemMovement entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entity.GCTransactionStatus = Constant.TransactionStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                FAItemDt entityFAItem = entityFAItemDao.Get(AppSession.FixedAssetDtID);
                entityFAItem.FALocationID = entity.FromFALocationID;
                entityFAItem.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityFAItemDao.Update(entityFAItem);
                entityDao.Update(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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
        #endregion
    }
}