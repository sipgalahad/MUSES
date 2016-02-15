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
using CodeX.Common;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentList : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;
            if (Request.Form["siteID"] != null)
                cboSite.Value = Request.Form["siteID"].ToString();

            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetvStudentRowIndex(filterExpression, keyValue) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
                CurrPage = 1;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Student Code", "Student Name" };
            fieldListValue = new string[] { "StudentCode", "StudentName" };
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SiteID = '{0}' AND IsDeleted = 0", cboSite.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvStudentRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vStudent> lstEntity = BusinessLayer.GetvStudentList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            url = ResolveUrl(string.Format("~/Program/Master/Student/StudentEntry.aspx?id=add|{0}", cboSite.Value));
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/Master/Student/StudentEntry.aspx?id=edit|{0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                Student entity = BusinessLayer.GetStudent(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudent(entity);
                return true;
            }
            return false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                IDbContext ctx = DbFactory.Configure(true);
                StudentDao entityDao = new StudentDao(ctx);
                StudentFeeDao entityStudentFeeDao = new StudentFeeDao(ctx);
                StudentFeeDtDao entityStudentFeeDtDao = new StudentFeeDtDao(ctx);
                bool result = true;
                try
                {
                    Student entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                    entity.DropOutDate = Helper.GetDatePickerValue(hdnDropOutDate.Value);
                    entity.GCStudentStatus = Constant.StudentStatus.DROP_OUT;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDao.Update(entity);

                    List<vStudentFee> lstvStudentFee = BusinessLayer.GetvStudentFeeList(String.Format("StudentID = {0} AND TransactionMonth IS NOT NULL AND TransactionYear IS NOT NULL AND ({1} < TransactionYear OR ({1} = TransactionYear AND {2} < TransactionMonth)) AND IsPaid = 0", hdnID.Value, entity.DropOutDate.Year, entity.DropOutDate.Month), ctx);
                    if (lstvStudentFee.Count > 0)
                    {
                        string lstStudentFeeID = string.Join(",", lstvStudentFee.Select(p => p.StudentFeeID).ToList());
                        List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(string.Format("StudentFeeID IN ({0})", lstStudentFeeID), ctx);
                        foreach (StudentFee studentFee in lstStudentFee)
                        {
                            studentFee.TransactionAmount = studentFee.StudentAmount = studentFee.StudentPenaltyAmount = studentFee.TotalDiscountAmount = studentFee.DiscountAmount = studentFee.TotalStudentPenaltyAmount = studentFee.PayerAmount = studentFee.LineAmount = 0;
                            studentFee.IsDeleted = true;
                            studentFee.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeDao.Update(studentFee);
                        }
                        List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND IsDeleted = 0 AND IsPaid = 0", lstStudentFeeID), ctx);
                        foreach (StudentFeeDt studentFeeDt in lstStudentFeeDt)
                        {
                            studentFeeDt.TransactionAmount = studentFeeDt.StudentAmount = studentFeeDt.TotalStudentPenaltyAmount = studentFeeDt.TotalStudentAmount = studentFeeDt.PayerAmount = studentFeeDt.LineAmount = 0;
                            studentFeeDt.IsDeleted = true;
                            studentFeeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeDtDao.Update(studentFeeDt);
                        }
                    }

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
            return false;
        }
    }
}