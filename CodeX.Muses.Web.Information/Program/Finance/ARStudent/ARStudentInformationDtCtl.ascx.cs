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

namespace CodeX.Muses.Web.Information.Program
{
    public partial class ARStudentInformationDtCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;

        private ARStudentInformation DetailPage
        {
            get { return (ARStudentInformation)Page; }
        }

        public override void InitializeDataControl(string param)
        {
            String[] lstParam = param.Split('|');
            hdnStudentID.Value = lstParam[0];

            Student im = BusinessLayer.GetStudent(Convert.ToInt32(hdnStudentID.Value));
            txtItemName.Text = string.Format("{0} - {1}", im.VirtualAccountNo, im.StudentName);

            hdnDateFrom.Value = lstParam[1];
            hdnDateTo.Value = lstParam[2];

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            //List<string> lst = DetailPage.GetMovementDate().Split('|').ToList();
            //string filterExpression = String.Format("MovementDate BETWEEN '{0}' AND '{1}' AND BusinessPartnerID = {2}",lst[0],lst[1],hdnStudentID.Value);
            //if (isCountPageCount)
            //{
            //    int rowCount = BusinessLayer.GetvAPMovementRowCount(filterExpression);
            //    pageCount = Helper.GetPageCount(rowCount, 10);
            //}

            List<GetARStudentInformationDt> lstEntity = BusinessLayer.GetARStudentInformationDtList(DetailPage.GetMovementDate(), Convert.ToInt32(hdnStudentID.Value), Convert.ToInt32(hdnDateFrom.Value), Convert.ToInt32(hdnDateTo.Value));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
        }

        protected void cbpPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        public override void SetToolbarVisibility(ref bool IsAllowExport)
        {
            IsAllowExport = true;
        }        

        public override Control OnGetExportControl()
        {
            List<GetARStudentInformationDt> lstEntity = BusinessLayer.GetARStudentInformationDtList(DetailPage.GetMovementDate(), Convert.ToInt32(hdnStudentID.Value), Convert.ToInt32(hdnDateFrom.Value), Convert.ToInt32(hdnDateTo.Value));
            grdPopupView.DataSource = lstEntity;
            grdPopupView.DataBind();
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Siswa : {0}", Request.Form[txtItemName.UniqueID]);
            div.Controls.Add(h4);
            div.Controls.Add(grdPopupView);
            return div;
        }
    }
}