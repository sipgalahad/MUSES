using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using System.Data;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class BudgetEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.BUDGET;
        }

        #region Html Getter
        protected string OnGetEmployeeFilterExpression() 
        {
            return "IsDeleted = 0";
        }
        protected string OnGetBudgetFilterExpression() 
        {
            return String.Format("IsHeader = 1 AND GCTransactionStatus != '{0}'", Constant.TransactionStatus.VOID);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("ProjectID = {0}", Convert.ToInt32(ID));
                vProjectBudgetHd entity = BusinessLayer.GetvProjectBudgetHdList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            txtBudgetCode.Focus();
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtBudgetCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtBudgetName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vProjectBudgetHd entity)
        {
            txtBudgetCode.Text = entity.BudgetCode;
            txtBudgetName.Text = entity.BudgetName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            chkIsHeader.Checked = entity.IsHeader;
            tacParent.Value = entity.ParentID.ToString();
            hdnParentID.Value = entity.ParentID.ToString();
            hdnBudgetLevel.Value = (entity.BudgetLevel - 1).ToString();
            tacParent.Text = entity.ParentBudgetName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(ProjectBudgetHd entity)
        {
            entity.BudgetCode = txtBudgetCode.Text;
            entity.BudgetName = txtBudgetName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.IsHeader = chkIsHeader.Checked;
            if (hdnParentID.Value != "" && hdnParentID.Value != "0")
            {
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
                entity.BudgetLevel = Convert.ToInt16(Convert.ToInt32(hdnBudgetLevel.Value) + 1);
            }
            else 
            {
                entity.ParentID = null;
                entity.BudgetLevel = 0;
            } 
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProjectBudgetHdDao entityDao = new ProjectBudgetHdDao(ctx);
            bool result = false;
            try
            {
                ProjectBudgetHd entity = new ProjectBudgetHd();
                ControlToEntity(entity);
                entity.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
                entityDao.Insert(entity);
                entity.BudgetID = BusinessLayer.GetProjectMaxID(ctx);
                retval = entity.BudgetID.ToString();

                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectBudgetHdDao entityDao = new ProjectBudgetHdDao(ctx);
            
            try
            {
                ProjectBudgetHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}