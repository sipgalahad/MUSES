using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxCallbackPanel;
using System.Data;
using CodeX.Web.Common.UI;
using CodeX.Data.Core.Dal;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Common;

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class JournalListDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnGLTransactionID.Value = param;
            vGLTransactionHd entity = BusinessLayer.GetvGLTransactionHdList(string.Format("GLTransactionID = {0}", hdnGLTransactionID.Value))[0];
            txtJournalGroup.Text = entity.JournalGroup;
            txtJournalNo.Text = entity.JournalNo;

            txtTotalDebet.Text = entity.DebitAmount.ToString("N");
            txtTotalKredit.Text = entity.CreditAmount.ToString("N");
            txtTotalSelisih.Text = (entity.DebitAmount - entity.CreditAmount).ToString("N");
            BindGridView();
            divCreatedBy.InnerHtml = string.Format(@"{0} / {1}", entity.CreatedByName, entity.CreatedDate.ToString(Constant.FormatString.DATE_FORMAT));
            string lastUpdatedDate = string.Empty;
            if (entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) == Constant.ConstantDate.DEFAULT_NULL)
                lastUpdatedDate = "";
            else
                lastUpdatedDate = " / " + entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT);
            divLastUpdatedBy.InnerHtml = string.Format(@"{0} {1}", entity.LastUpdatedByName, lastUpdatedDate);
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("GLTransactionID = {0} AND GCItemDetailStatus != '{1}' ORDER BY DisplayOrder", hdnGLTransactionID.Value, Constant.TransactionStatus.VOID);
            List<vGLTransactionDt> lstEntity = BusinessLayer.GetvGLTransactionDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}