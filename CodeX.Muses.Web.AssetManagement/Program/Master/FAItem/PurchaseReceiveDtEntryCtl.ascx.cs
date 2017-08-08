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
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace Codex.Ottimo.Web.AssetManagement.Program
{
    public partial class PurchaseReceiveDtEntryCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ASSET_ACCRUAL_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboAssetAccrualType, lstSc, "StandardCodeName", "StandardCodeID");

            IsAdd = false;
            hdnPurchaseReceiveDtID.Value = param;
            vPurchaseReceiveDt entity = BusinessLayer.GetvPurchaseReceiveDtList(string.Format("ID = {0}", hdnPurchaseReceiveDtID.Value)).FirstOrDefault();
            txtPurchaseReceiveNo.Text = entity.PurchaseReceiveNo;
            txtItemName1.Text = entity.ItemName1;
            chkIsProcessAssetClosed.Checked = entity.IsProcessAssetClosed;
            cboAssetAccrualType.Value = entity.GCAssetAccrualType;
        }

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PurchaseReceiveDtDao entityDao = new PurchaseReceiveDtDao(ctx);
            try
            {
                PurchaseReceiveDt entity = entityDao.Get(Convert.ToInt32(hdnPurchaseReceiveDtID.Value));
                entity.IsProcessAssetClosed = chkIsProcessAssetClosed.Checked;
                entity.GCAssetAccrualType = cboAssetAccrualType.Value.ToString();
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
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
    }
}