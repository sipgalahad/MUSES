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
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using CodeX.Muses.Web.Information.Program;
using CodeX.Common;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class RenumerationCompFormulaDtCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            //param = 1|01-01-2017


            RenumerationCompFormulaHd entityHd = BusinessLayer.GetRenumerationCompFormulaHd(Convert.ToInt32(param));
            txtHeader.Text = String.Format("{0} - {1}", entityHd.FormulaName, entityHd.FormulaCode);
            if (entityHd.CurrentTransactionID != null)
            {
                hdnID.Value = entityHd.CurrentTransactionID.ToString();
            }
            else
                hdnID.Value = "0";
            BindGridView();
        }

        private void BindGridView()
        {

            grdPopupView.DataSource = BusinessLayer.GetvTransRenumerationCompFormulaDtList(String.Format("TransactionID = {0} AND IsDeleted = 0", Convert.ToInt32(hdnID.Value)));
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}