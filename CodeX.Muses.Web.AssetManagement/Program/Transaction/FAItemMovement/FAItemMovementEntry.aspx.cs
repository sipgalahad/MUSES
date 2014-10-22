using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Web.Common;
using QIS.Medinfras.Data.Service;
using QIS.Data.Core.Dal;
using DevExpress.Web.ASPxCallbackPanel;

namespace QIS.Medinfras.Web.Accounting.Program
{
    public partial class FAItemMovementEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.FA_ITEM_MOVEMENT;
        }

        protected override void InitializeDataControl()
        {
            hdnFixedAssetID.Value = AppSession.FixedAssetID.ToString();
            //FAItem entity = BusinessLayer.GetFAItem(Convert.ToInt32(hdnFixedAssetID.Value));
            //hdnFromLocationID.Value = entity.FALocationID.ToString();
            txtMovementDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            BindGridView(CurrPage, true, ref PageCount);
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount) 
        {
            string filterExpression = String.Format("GCTransactionStatus NOT IN ('{0}','{1}')", Constant.TransactionStatus.VOID, Constant.TransactionStatus.CLOSED);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvFAItemMovementRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vFAItemMovement> lstEntity = BusinessLayer.GetvFAItemMovementList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex,"MovementNo DESC");
            if (lstEntity.Count > 0 && pageIndex == 1) lstEntity[0].IsEditable = true;
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpViewProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                if (e.Parameter == "process")
                {
                    if(hdnIsAdd.Value == "1")
                        OnSaveAddRecord(ref result);
                    else
                        OnSaveEditRecord(ref result);
                }
                else // delete
                {
                    OnSaveDeleteRecord(ref result);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(FAItemMovement entity) 
        {
            entity.FixedAssetID = Convert.ToInt32(hdnFixedAssetID.Value);
            //entity.FromFALocationID = Convert.ToInt32(hdnFromLocationID.Value);
            entity.ToFALocationID = Convert.ToInt32(hdnToLocationID.Value);
            entity.MovementDate = Helper.GetDatePickerValue(txtMovementDate.Text);
            entity.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecord(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemMovementDao faItemMovementDao = new FAItemMovementDao(ctx);
            FAItemDao faItemDao = new FAItemDao(ctx);
            
            try
            {
                FAItem faItem = faItemDao.Get(Convert.ToInt32(hdnFixedAssetID.Value));
                FAItemMovement faItemMovement = new FAItemMovement();
                
                faItemMovement.FromFALocationID = faItem.FALocationID;
                ControlToEntity(faItemMovement);
                faItemMovement.MovementNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.FIXED_ASSET_ITEM_MOVEMENT, faItemMovement.MovementDate, ctx);
                ctx.CommandType = System.Data.CommandType.Text;
                ctx.Command.Parameters.Clear();
                faItemMovement.LastUpdatedBy = faItemMovement.CreatedBy = AppSession.UserLogin.UserID;
                faItemMovementDao.Insert(faItemMovement);

                faItem.FALocationID = faItemMovement.ToFALocationID;
                faItem.LastUpdatedBy = AppSession.UserLogin.UserID;
                faItemDao.Update(faItem);
                

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            return result;
        }

        private bool OnSaveEditRecord(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemMovementDao faItemMovementDao = new FAItemMovementDao(ctx);
            FAItemDao faItemDao = new FAItemDao(ctx);

            try
            {
                string filterExpression = String.Format("MovementID = {0}", hdnMovementID.Value);
                FAItemMovement faItemMovement = BusinessLayer.GetFAItemMovementList(filterExpression, ctx)[0];
                FAItem faItem = faItemDao.Get(faItemMovement.FixedAssetID);

                ControlToEntity(faItemMovement);
                faItemMovement.LastUpdatedBy = AppSession.UserLogin.UserID;

                faItem.FALocationID = faItemMovement.ToFALocationID;
                faItem.LastUpdatedBy = AppSession.UserLogin.UserID;

                faItemDao.Update(faItem);
                faItemMovementDao.Update(faItemMovement);
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        private bool OnSaveDeleteRecord(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            FAItemMovementDao faItemMovementDao = new FAItemMovementDao(ctx);
            FAItemDao faItemDao = new FAItemDao(ctx);
            try
            {
                string filterExpression = String.Format("MovementID = {0}", hdnMovementID.Value);
                FAItemMovement faItemMovement = BusinessLayer.GetFAItemMovementList(filterExpression, ctx)[0];
                faItemMovement.GCTransactionStatus = Constant.TransactionStatus.VOID;
                faItemMovement.LastUpdatedBy = AppSession.UserLogin.UserID;

                FAItem faItem = faItemDao.Get(AppSession.FixedAssetID);
                faItem.FALocationID = faItemMovement.FromFALocationID;
                faItem.LastUpdatedBy = AppSession.UserLogin.UserID;

                faItemDao.Update(faItem);
                faItemMovementDao.Update(faItemMovement);
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}