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
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ClassTypeManagement.Program
{
    public partial class SubjectClassTypeQuickPicksEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;

        public override void InitializeDataControl(string param)
        {
            hdnGCClassStudyType.Value = param;
            BindGridView(1, true, ref PageCount);
        }

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("SiteID = '{0}' AND ClassTypeName LIKE '%{1}%' AND IsDeleted = 0 AND ClassTypeID NOT IN (SELECT ClassTypeID FROM SubjectClassType WHERE SubjectID = {2}) AND GCClassStudyType = '{3}'", AppSession.UserLogin.SiteID, hdnFilterItem.Value, AppSession.SubjectID, hdnGCClassStudyType.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetClassTypeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<ClassType> lstEntity = BusinessLayer.GetClassTypeList(filterExpression, 10, pageIndex, "ClassTypeName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClassType entity = e.Row.DataItem as ClassType;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ClassTypeID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SubjectClassTypeDao entityDao = new SubjectClassTypeDao(ctx);
            bool result = false;
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            try
            {
                foreach (String studentID in lstSelectedMember)
                {
                    SubjectClassType entity = new SubjectClassType();
                    entity.SubjectID = AppSession.SubjectID;
                    entity.ClassTypeID = Convert.ToInt32(studentID);
                    entityDao.Insert(entity);
                }
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
    }
}