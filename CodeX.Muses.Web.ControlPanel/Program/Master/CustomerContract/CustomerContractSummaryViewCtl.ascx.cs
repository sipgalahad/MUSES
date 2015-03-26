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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CustomerContractSummaryViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            CustomerContract entity = BusinessLayer.GetCustomerContract(Convert.ToInt32(param));
            divContractSummary.InnerHtml = entity.ContractSummary;            
        }
    }
}