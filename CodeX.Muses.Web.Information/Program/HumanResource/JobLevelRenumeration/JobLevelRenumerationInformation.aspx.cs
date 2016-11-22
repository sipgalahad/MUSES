using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using System.Web.UI.HtmlControls;


namespace CodeX.Muses.Web.Information.Program
{
    public partial class JobLevelRenumerationInformation : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.JOB_LEVEL_RENUMERATION_INFORMATION;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);

        }

        
        protected string OnGetLocationFilterExpression()
        {
            return string.Format("{0};{1};;", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
        }

        

        public string OnGetFilterEmployeeExpression() 
        {
            return String.Format("IsDeleted = 0 "); 
        }
    
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            
            

            string filterExpression = OnGetFilterEmployeeExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvJobLevelRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            } 

            lstRenumComp = BusinessLayer.GetvRenumerationCompList("IsDeleted = 0 ");

            if(txtJobLevel.Text != "" && txtJobLevel.Text != null)
            {
                filterExpression += String.Format(" AND JobLevelName LIKE '%{0}%' ", txtJobLevel.Text);
            }
            

            List<vJobLevel> lstOp = BusinessLayer.GetvJobLevelList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JobLevelName ASC");

            string lstOpID = string.Join(",", lstOp.Select(p => p.JobLevelID).ToList());
            if (lstOpID != "")
                lstOpRenumeration = BusinessLayer.GetvJobLevelRenumerationList(string.Format("JobLevelID IN ({0})", lstOpID));
            else
                lstOpRenumeration = new List<vJobLevelRenumeration>();

            rptView.DataSource = lstOp;
            rptView.DataBind();
                        
            rptCompHd.DataSource = lstRenumComp;
            rptCompHd.DataBind();
        }

        List<vRenumerationComp> lstRenumComp = null;
        List<vJobLevelRenumeration> lstOpRenumeration = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptCompDt = (Repeater)e.Item.FindControl("rptCompDt");
                rptCompDt.DataSource = lstRenumComp;
                rptCompDt.DataBind();
            }
        }

        protected void rptCompDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vJobLevel position = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vJobLevel;
                vRenumerationComp renumComp = (vRenumerationComp)e.Item.DataItem;

                vJobLevelRenumeration renum = lstOpRenumeration.FirstOrDefault(p => p.JobLevelID == position.JobLevelID && p.RenumerationCompID == renumComp.RenumerationCompID);

                HtmlGenericControl divAmount = (HtmlGenericControl)e.Item.FindControl("divAmount");
                HtmlGenericControl lblFormula = (HtmlGenericControl)e.Item.FindControl("lblFormula");
                HtmlInputHidden hdnRenumerationTransID = (HtmlInputHidden)e.Item.FindControl("hdnRenumerationTransID");
                HtmlInputHidden hdnOrganizationPositionID = (HtmlInputHidden)e.Item.FindControl("hdnOrganizationPositionID");
                HtmlInputHidden hdnRenumerationCompID = (HtmlInputHidden)e.Item.FindControl("hdnRenumerationCompID");
                
                if (renum != null)
                {
                    if (renum.IsUseFormula)
                    {
                        hdnRenumerationTransID.Value = renum.RenumerationTransactionDtID.ToString();
                        hdnOrganizationPositionID.Value = renum.JobLevelID.ToString();
                        hdnRenumerationCompID.Value = renum.RenumerationCompID.ToString();
                        lblFormula.Style.Remove("display");
                        divAmount.Style.Add("display", "none");
                    }
                    else
                        divAmount.InnerHtml = renum.Amount.ToString("N");
                }
                else
                    divAmount.InnerHtml = "-";
            }
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
    }
}