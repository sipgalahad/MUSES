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
    public partial class EmployeeRenumerationInformation : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.EMPLOYEE_RENUMERATION_INFORMATION;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            //txtDateFrom.Text = DateTime.Now.AddDays(-7).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            //txtDateTo.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);

            List<OrganizationDepartment> lstOD = BusinessLayer.GetOrganizationDepartmentList(string.Format("IsDeleted = 0"));
            lstOD.Insert(0, new OrganizationDepartment {OrganizationDepartmentID = 0, OrganizationDepartmentName = "" });
            Methods.SetComboBoxField<OrganizationDepartment>(cboOrganizationDepartment, lstOD, "OrganizationDepartmentName", "OrganizationDepartmentID");

            

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);


            Helper.SetControlEntrySetting(tacOrganizationPositionID, new ControlEntrySetting(true, true, true),"");
            //SetControlProperties(tacOrganizationPositionID, new ControlEntrySetting(true, true, false));
            //SetControlEntrySetting(tacOrganizationPositionID, new ControlEntrySetting(true, true, false));
        }

        
        protected string OnGetLocationFilterExpression()
        {
            return string.Format("{0};{1};;", AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID);
        }

        //public string OnGetFilterExpression()
        //{
        //    return Request.Form[hdnFilterExpression.UniqueID];
        //}

        public string OnGetFilterEmployeeExpression() 
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') ", AppSession.UserLogin.SiteID); 
        }
    
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            
            //if (hdnLocationID.Value != "")
            //{
            //    hdnFilterExpression.Value = string.Format("LocationID = {0} AND MovementDate BETWEEN '{1}' AND '{2}'", hdnLocationID.Value, Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd"));
            //    if (isCountPageCount)
            //    {
            //        string filterExpression = string.Format("LocationID = {0} AND ItemName1 LIKE '%{1}%' AND IsDeleted = 0", hdnLocationID.Value, txtItemName.Text);
            //        rowCount = BusinessLayer.GetvItemBalanceRowCount(filterExpression);
            //        pageCount = Helper.GetPageCount(rowCount, 10);
            //    }

            //    List<GetItemMovementPerPeriodeDetail> lstEntity = BusinessLayer.GetItemMovementPerPeriodeDetail(string.Format("{0}|{1}", Helper.GetDatePickerValue(txtDateFrom.Text).ToString("yyyyMMdd"), Helper.GetDatePickerValue(txtDateTo.Text).ToString("yyyyMMdd")), Convert.ToInt32(hdnLocationID.Value), txtItemName.Text, pageIndex, 10);
            //    lvwView.DataSource = lstEntity;
            //    lvwView.DataBind();
            //}

            string filterExpression = OnGetFilterEmployeeExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvEmployeeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            } 

            lstRenumComp = BusinessLayer.GetvRenumerationCompList("IsDeleted = 0 ");
            if (txtNIK.Text != "" && txtNIK.Text != null)
            {
                filterExpression += String.Format(" AND EmployeeCode LIKE '%{0}%' ", txtNIK.Text);
            }
            if(txtEmployeeName.Text != "" && txtEmployeeName.Text != null)
            {
                filterExpression += String.Format(" AND EmployeeName LIKE '%{0}%' ", txtEmployeeName.Text);
            }
            if(cboOrganizationDepartment.Value != null && cboOrganizationDepartment.Value.ToString() != "0")
            {
                filterExpression += String.Format(" AND OrganizationDepartmentID = {0} ", cboOrganizationDepartment.Value);
            }
            if (tacOrganizationPositionID.Value != null && tacOrganizationPositionID.Value != "")
            {
                filterExpression += String.Format(" AND OrganizationPositionID = {0} ", tacOrganizationPositionID.Value);
            }

            filterExpression += String.Format(" AND IsDeleted = 0 ");

            List<vEmployee> lstEm = BusinessLayer.GetvEmployeeList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "EmployeeName ASC");

            string lstEmpID = string.Join(",", lstEm.Select(p => p.EmployeeID).ToList());
            if (lstEmpID != "")
                lstEmpRenumeration = BusinessLayer.GetvEmployeeRenumerationList(string.Format("EmployeeID IN ({0})", lstEmpID));
            else
                lstEmpRenumeration = new List<vEmployeeRenumeration>();

            rptView.DataSource = lstEm;
            rptView.DataBind();
                        
            rptCompHd.DataSource = lstRenumComp;
            rptCompHd.DataBind();
        }

        List<vRenumerationComp> lstRenumComp = null;
        List<vEmployeeRenumeration> lstEmpRenumeration = null;
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
                vEmployee employee = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vEmployee;
                vRenumerationComp renumComp = (vRenumerationComp)e.Item.DataItem;

                vEmployeeRenumeration renum = lstEmpRenumeration.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID && p.RenumerationCompID == renumComp.RenumerationCompID);

                HtmlGenericControl divAmount = (HtmlGenericControl)e.Item.FindControl("divAmount");
                HtmlGenericControl lblFormula = (HtmlGenericControl)e.Item.FindControl("lblFormula");
                HtmlInputHidden hdnEmployeePositionTransID = (HtmlInputHidden)e.Item.FindControl("hdnEmployeePositionTransID");
                HtmlInputHidden hdnRenumerationTransID = (HtmlInputHidden)e.Item.FindControl("hdnRenumerationTransID");
                HtmlInputHidden hdnEmployeeID = (HtmlInputHidden)e.Item.FindControl("hdnEmployeeID");
                HtmlInputHidden hdnRenumerationCompID = (HtmlInputHidden)e.Item.FindControl("hdnRenumerationCompID");
                
                if (renum != null)
                {
                    if (renum.IsUseFormula)
                    {
                        hdnEmployeePositionTransID.Value = renum.EmployeePositionTransactionDtID.ToString();
                        hdnRenumerationTransID.Value = renum.RenumerationPositionTransactionDtID.ToString();
                        hdnEmployeeID.Value = renum.EmployeeID.ToString();
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