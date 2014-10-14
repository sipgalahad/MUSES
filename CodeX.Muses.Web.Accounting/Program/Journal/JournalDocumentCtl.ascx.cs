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
using System.Data;

namespace CodeX.Web.Accounting.Program
{
    public partial class JournalDocumentCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            List<String> lstParam = param.Split('|').ToList();
            hdnGLAccount.Value = lstParam[0];
            hdnSubLedger.Value = lstParam[1];
            hdnReferenceNo.Value = lstParam[2];

            string filterExpression = String.Format("GLAccount = {0}", hdnGLAccount.Value);
            if (lstParam[1] != "" && lstParam[1] != "0")
                filterExpression += String.Format(" AND SubLedger = {0}", hdnSubLedger.Value);
            else
                filterExpression += String.Format(" AND SubLedger IS NULL");
            filterExpression += String.Format(" AND ReferenceNo = '{0}'", hdnReferenceNo.Value);
            hdnFilterExpression.Value = filterExpression;
            List<vGLBalanceDtDocument> lstEntity = BusinessLayer.GetvGLBalanceDtDocumentList(filterExpression);

            if (lstEntity.Count > 0) 
            {
                txtReferenceNo.Text = hdnReferenceNo.Value;
                txtGLAccountName.Text = lstEntity[0].GLAccountName;
                txtSubLedgerName.Text = lstEntity[0].SubLedgerName;

                txtTotalDebet.Text = lstEntity[0].BalanceDEBIT.ToString("N");
                txtTotalKredit.Text = lstEntity[0].BalanceCREDIT.ToString("N");
                txtTotalSelisih.Text = (lstEntity[0].BalanceDEBIT - lstEntity[0].BalanceCREDIT).ToString("N");
                BindGridView();
                divCreatedBy.InnerHtml = lstEntity[0].CreatedByName;
                divCreatedDate.InnerHtml = lstEntity[0].CreatedDate.ToString(Constant.FormatString.DATE_FORMAT);
                divLastUpdatedBy.InnerHtml = lstEntity[0].LastUpdatedByName;
                divLastUpdatedDate.InnerHtml = lstEntity[0].LastUpdatedDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("{0} AND GCTransactionStatus = '{1}'", hdnFilterExpression.Value, Constant.TransactionStatus.APPROVED);
            List<vGLTransactionDtCustom> lstEntity = BusinessLayer.GetvGLTransactionDtCustomList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}