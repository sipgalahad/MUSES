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
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.EMPLOYEE_PERFORMANCE_INDICATOR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {            
            List<StandardCode> listSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.EMPLOYEE_TYPE));
            listSc.Insert(0, new StandardCode { StandardCodeID = "0", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboEmployeeType, listSc, "StandardCodeName", "StandardCodeID");

            
            BindGridView();


            Helper.SetControlEntrySetting(cboEmployeeType, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(tacPeriod, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetIndicatorMarkTypeCustom()
        {
            return Constant.IndicatorMarkType.CUSTOM;
        }

        private void BindGridView()
        {
            if (tacPeriod.Value != "")
            {
                string filterExpression = OnGetEmployeeFilterExpression();
                lstPerformanceIndicator = BusinessLayer.GetvPerformanceIndicatorHdList(string.Format("IsDeleted = 0"));
                lstPerformanceIndicatorDt = BusinessLayer.GetPerformanceIndicatorDtList(string.Format("IsDeleted = 0"));
                if (cboEmployeeType.Value != null && cboEmployeeType.Value.ToString() != "0")
                    filterExpression += String.Format(" AND GCEmployeeType = '{0}' ", cboEmployeeType.Value);
                if (txtNamaKaryawan.Text != "" && txtNamaKaryawan != null)
                    filterExpression += String.Format(" AND Name LIKE '%{0}%' ", txtNamaKaryawan.Text);
                filterExpression += " ORDER BY EmployeeName";
                lstEmployee = BusinessLayer.GetvEmployeeList(string.Format(filterExpression));

                string lstEmpID = string.Join(",", lstEmployee.Select(p => p.EmployeeID).ToList());
                if (lstEmpID != "")
                    lstEmpPerformance = BusinessLayer.GetEmployeePerformanceIndicatorList(string.Format("EmployeeID IN ({0}) AND RevenuePeriodID = {1} ", lstEmpID, tacPeriod.Value));
                else
                    lstEmpPerformance = new List<EmployeePerformanceIndicator>();

                rptView.DataSource = lstEmployee;
                rptView.DataBind();

                rptPerfomanceIndicatorHd.DataSource = lstPerformanceIndicator;
                rptPerfomanceIndicatorHd.DataBind();
            }
        }

        List<PerformanceIndicatorDt> lstPerformanceIndicatorDt = null;
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
                TextBox txtInput = (TextBox)e.Item.FindControl("txtInput");
                DropDownList ddlInput = (DropDownList)e.Item.FindControl("ddlInput");
                EmployeePerformanceIndicator empPerformance = lstEmpPerformance.FirstOrDefault(p => p.EmployeeID == employee.EmployeeID && p.PerformanceIndicatorID == performance.PerformanceIndicatorID);
                if (performance.GCIndicatorMarkType == Constant.IndicatorMarkType.CUSTOM)
                {
                    ddlInput.Visible = true;
                    txtInput.Visible = false;
                    Methods.SetComboBoxField<PerformanceIndicatorDt>(ddlInput, lstPerformanceIndicatorDt.Where(p => p.PerformanceIndicatorID == performance.PerformanceIndicatorID).ToList(), "PerformanceIndicatorDtName", "PerformanceIndicatorDtID");
                    
                    if (empPerformance != null)
                        ddlInput.SelectedValue = empPerformance.PerformanceIndicatorDtID.ToString();
                    else
                        ddlInput.SelectedValue = "";
                }
                else
                {
                    ddlInput.Visible = false;
                    txtInput.Visible = true;

                    if (empPerformance != null)
                        txtInput.Text = empPerformance.Value.ToString();
                    else
                        txtInput.Text = "0";
                }
            }
        }

        public bool OnSaveEmployeePerformanceIndicator(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            EmployeePerformanceIndicatorDao entityDao = new EmployeePerformanceIndicatorDao(ctx);
            try 
            {
                string[] lstSaveValue = hdnListPerformanceID.Value.Split('|');
                List<EmployeePerformanceIndicator> lstEntity = BusinessLayer.GetEmployeePerformanceIndicatorList(string.Format("EmployeeID = {0} AND RevenuePeriodID = {1}", hdnEmployeeID.Value, tacPeriod.Value), ctx);
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int performanceIndicatorID = Convert.ToInt32(temp[0]);
                    EmployeePerformanceIndicator entity = lstEntity.FirstOrDefault(p => p.PerformanceIndicatorID == performanceIndicatorID);
                    if (entity == null)
                    {
                        entity = new EmployeePerformanceIndicator();
                        entity.EmployeeID = Convert.ToInt32(hdnEmployeeID.Value);
                        entity.RevenuePeriodID = Convert.ToInt32(tacPeriod.Value);
                        entity.PerformanceIndicatorID = performanceIndicatorID;
                        if (temp[1] == Constant.IndicatorMarkType.CUSTOM)
                        {
                            entity.Value = null;
                            entity.PerformanceIndicatorDtID = Convert.ToInt32(temp[2]);
                        }
                        else
                        {
                            entity.PerformanceIndicatorDtID = null;
                            entity.Value = Convert.ToInt32(temp[2]);
                        }
                        entityDao.Insert(entity);
                    }
                    else
                    {
                        if (temp[1] == Constant.IndicatorMarkType.CUSTOM)
                        {
                            entity.Value = null;
                            entity.PerformanceIndicatorDtID = Convert.ToInt32(temp[2]);
                        }
                        else
                        {
                            entity.PerformanceIndicatorDtID = null;
                            entity.Value = Convert.ToInt32(temp[2]);
                        }
                        entityDao.Update(entity);
                        lstEntity.Remove(entity);
                    }
                }

                foreach (EmployeePerformanceIndicator entity in lstEntity)
                {
                    entityDao.Delete(entity.EmployeeID, entity.RevenuePeriodID, entity.PerformanceIndicatorID);
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();

            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveEmployeePerformanceIndicator(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpErrorMessage"] = errMessage;
        }
    }
}