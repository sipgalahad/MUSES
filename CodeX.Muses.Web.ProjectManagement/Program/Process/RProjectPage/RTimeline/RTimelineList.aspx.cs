using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Data.Model;
using CodeX.Web.Common;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using System.Net;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RTimelineList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RTIMELINE;
        }

        //protected string OnGetEmployeeFilterExpression()
        //{
        //    return string.Format("SiteID = '{0}' AND GCEmployeeStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeStatus.FULL_TIME_EMPLOYED);
        //}

        protected override void InitializeDataControl()
        {
            //List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID = '{0}'", Constant.StandardCode.PROJECT_TASK_STATUS));
            //lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "All" });
            //Methods.SetComboBoxField(cboStatus, lstStandardCode.Where(x => x.StandardCodeID != Constant.ProjectTaskStatus.VOID).ToList(), "StandardCodeName", "StandardCodeID");
            //cboStatus.SelectedIndex = 0;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MATRIX;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        #region Bind Grid View
        private String OnGetFilterExpression() 
        {
            String filterExpression = "IsDeleted = 0";
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            String filterExpression = OnGetFilterExpression();
            
            //if (isCountPageCount)
            //{
            //    rowCount = BusinessLayer.GetvProjectTaskCustomRowCount(filterExpression);
            //    pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);
            //}

            //List<ActivityHistory> lstEntity = BusinessLayer.GetActivityHistoryList(filterExpression);
            
            //grdView.DataSource = lstEntity;
            //grdView.DataBind();
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
        #endregion

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}