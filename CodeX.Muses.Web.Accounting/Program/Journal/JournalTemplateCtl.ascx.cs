using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Data.Model;
using Codex.Muses.Web.Accounting.Program;

namespace CodeX.Web.Accounting.Program
{
    public partial class JournalTemplateCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            hdnGLTransactionID.Value = param;
        }

        private JournalEntry DetailPage
        {
            get { return (JournalEntry)Page; }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTemplateCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAmount, new ControlEntrySetting(true, true, true, "0"));
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            retval = string.Format("{0}|{1}", hdnTemplateID.Value, Convert.ToDecimal(txtAmount.Text));
            return true;
        }
    }
}