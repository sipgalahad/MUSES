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


namespace CodeX.Muses.Web.HumanResource.Program
{
    public partial class EmployeePerformanceIndicatorEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.EMPLOYEE_PERFORMANCE_INDICATOR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {            
            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;

            List<StandardCode> listSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.EMPLOYEE_TYPE));
            listSc.Insert(0, new StandardCode { StandardCodeID = "0", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboEmployeeType, listSc, "StandardCodeName", "StandardCodeID");

            
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);


            Helper.SetControlEntrySetting(cboEmployeeType, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(tacPeriod, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }
        

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = OnGetEmployeeFilterExpression();
            //if (txtJobLevel.Text != "" && txtJobLevel.Text != null)
            //    filterExpression += String.Format(" AND JobLevelName LIKE '%{0}%' ", txtJobLevel.Text);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvEmployeeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstPerformanceIndicator = BusinessLayer.GetvPerformanceIndicatorHdList(string.Format(" IsDeleted = 0 "));
            //List<vJobLevel> lstOp = BusinessLayer.GetvJobLevelList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "JobLevelName ASC");

            if (cboEmployeeType.Value != null && cboEmployeeType.Value.ToString() != "0")
                filterExpression += String.Format(" AND GCEmployeeType = '{0}' ", cboEmployeeType.Value);
            if (txtNamaKaryawan.Text != "" && txtNamaKaryawan != null)
                filterExpression += String.Format(" AND Name LIKE '%{0}%' ", txtNamaKaryawan.Text);

            lstEmployee = BusinessLayer.GetvEmployeeList(string.Format(filterExpression));

            string lstEmpID = string.Join(",", lstEmployee.Select(p => p.EmployeeID).ToList());
            if (lstEmpID != "")
                if(tacPeriod.Value != null && tacPeriod.Value != "")
                    lstEmpPerformance = BusinessLayer.GetEmployeePerformanceIndicatorList(string.Format("EmployeeID IN ({0}) AND RevenuePeriodID = {1} ", lstEmpID, tacPeriod.Value));
                else
                    lstEmpPerformance = BusinessLayer.GetEmployeePerformanceIndicatorList(string.Format("EmployeeID IN ({0}) ", lstEmpID));
            else
                lstEmpPerformance = new List<EmployeePerformanceIndicator>();

            rptView.DataSource = lstEmployee;
            rptView.DataBind();
                        
            rptPerfomanceIndicatorHd.DataSource = lstPerformanceIndicator;
            rptPerfomanceIndicatorHd.DataBind();
        }

        List<vPerformanceIndicatorHd> lstPerformanceIndicator = null;
        List<vEmployee> lstEmployee = null;
        List<EmployeePerformanceIndicator> lstEmpPerformance = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptCompDt = (Repeater)e.Item.FindControl("rptCompDt");
                rptCompDt.DataSource = lstPerformanceIndicator;
                rptCompDt.DataBind();
            }
        }

        protected void rptCompDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vEmployee employee = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vEmployee;
                vPerformanceIndicatorHd performance = (vPerformanceIndicatorHd)e.Item.DataItem;
                //vPerformanceIndicatorHd renum = lstPerformanceIndicator.FirstOrDefault(p => p.JobLevelID == position.JobLevelID && p.RenumerationCompID == renumComp.RenumerationCompID);

                EmployeePerformanceIndicator empPerformance = lstEmpPerformance.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID && p.PerformanceIndicatorID == performance.PerformanceIndicatorID);

                TextBox txtInput = (TextBox)e.Item.FindControl("txtInput");

                if (empPerformance != null)
                    txtInput.Text = empPerformance.Value.ToString();
                else
                    txtInput.Text = "0";
            }
        }

        public void onSaveEmployeePerformanceIndicator(ref string errMessage) 
        {
            IDbContext ctx = DbFactory.Configure(true);
            EmployeePerformanceIndicatorDao entityDao = new EmployeePerformanceIndicatorDao(ctx);
            try 
            {
                EmployeePerformanceIndicator entity = new EmployeePerformanceIndicator();
                entity.Value = Convert.ToInt32(hdnInput.Value);
                entity.PerformanceIndicatorID = Convert.ToInt32(hdnPerformanceID.Value);
                entity.EmployeeID = Convert.ToInt32(hdnID.Value);
                entity.RevenuePeriodID = 1;
                entityDao.Insert(entity);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                //result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();

            }
            finally
            {
                ctx.Close();
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

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            //String data = GetDataFromFile();

            //int adjustmentID = 0;
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            //if (param[0] == "save")
            //{
            //    //adjustmentID = Convert.ToInt32(hdnTransactionID.Value);
            //    if (OnSaveEditRecordEntityDt(ref errMessage))
            //        result += "success";
            //    else
            //        result += string.Format("fail|{0}", errMessage);
            //}

            if (param[0] == "save")
            {
                onSaveEmployeePerformanceIndicator( ref errMessage);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpErrorMessage"] = errMessage;
        }
    }
}