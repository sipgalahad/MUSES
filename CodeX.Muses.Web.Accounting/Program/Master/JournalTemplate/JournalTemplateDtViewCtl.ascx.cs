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

namespace Codex.Muses.Web.Accounting.Program
{
    public partial class JournalTemplateDtViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnTemplateID.Value = param;
            JournalTemplateHd entity = BusinessLayer.GetJournalTemplateHd(Convert.ToInt32(hdnTemplateID.Value));
            txtTemplateCode.Text = entity.TemplateCode;
            txtTemplateName.Text = entity.TemplateName;
            
            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("TemplateID = {0} AND IsDeleted = 0 ORDER BY DisplayOrder", hdnTemplateID.Value);
            List<vJournalTemplateDt> lstEntity = BusinessLayer.GetvJournalTemplateDtList(filterExpression);
            grdViewD.DataSource = lstEntity.Where(p => p.Position == "D").ToList();
            grdViewD.DataBind();

            grdViewK.DataSource = lstEntity.Where(p => p.Position == "K").ToList();
            grdViewK.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }
            
        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            BindGridView();
        }
    }
}