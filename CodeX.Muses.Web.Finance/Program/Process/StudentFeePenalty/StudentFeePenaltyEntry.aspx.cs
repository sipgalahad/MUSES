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
using DevExpress.Web.ASPxEditors;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;
namespace CodeX.Muses.Web.Finance.Program
{
    public partial class StudentFeePenaltyEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.STUDENT_FEE_PENALTY;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }
        
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            //List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", cboSite.Value, Constant.SchoolPeriodStatus.VOID));
            //Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            //SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            //if (selectedSchoolPeriod == null)
            //    cboSchoolPeriod.SelectedIndex = 0;
            //else
            //    cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            cboYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboYear.EnableCallbackMode = false;
            cboYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboYear.DropDownStyle = DropDownStyle.DropDownList;
            cboYear.DataBind();
            cboYear.SelectedIndex = 0;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(1, true, ref PageCount);
        }

        #region Callback
        List<StudentFeeCompType> lstStudentFeeCompType = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = "1 = 0";
            if (tacSchoolClass.Value != "")
                filterExpression = string.Format("StudentID IN (SELECT StudentID FROM Student WHERE SchoolClassID = {0} AND GCStudentStatus = '{1}' AND IsDeleted = 0) AND TransactionMonth = {2} AND TransactionYear = {3} AND GCAdmissionPaymentPeriod = '{4}' AND IsPaid = 0 AND IsDeleted = 0", tacSchoolClass.Value, Constant.StudentStatus.ACTIVE, cboMonth.Value, cboYear.Value, Constant.AdmissionPaymentPeriod.BULANAN);
            List<vStudentFee> lstEntity = BusinessLayer.GetvStudentFeeList(filterExpression);
            lstStudentFeeCompType = BusinessLayer.GetStudentFeeCompTypeList(string.Format("GCAdmissionPaymentPeriod = '{0}' AND IsDeleted = 0", Constant.AdmissionPaymentPeriod.BULANAN));
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vStudentFee entity = (vStudentFee)e.Row.DataItem;
                HtmlInputHidden hdnPenaltyPercentage = (HtmlInputHidden)e.Row.FindControl("hdnPenaltyPercentage");
                StudentFeeCompType entityFeeCompType = lstStudentFeeCompType.FirstOrDefault(p => p.GCAdmissionPaymentPeriod == entity.GCAdmissionPaymentPeriod);
                hdnPenaltyPercentage.Value = entityFeeCompType.PenaltyPercentage.ToString();

                if (entity.StudentPenaltyAmount > 0)
                {
                    CheckBox chkIsSelected = (CheckBox)e.Row.FindControl("chkIsSelected");
                    chkIsSelected.Checked = true;
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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
        #endregion

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ARInvoiceHdDao arInvoiceHdDao = new ARInvoiceHdDao(ctx);
            StudentFeeDao entityDao = new StudentFeeDao(ctx);
            StudentFeeDtDao entityDtDao = new StudentFeeDtDao(ctx);
            try
            {
                List<ARInvoiceHd> lstARInvoiceHd = BusinessLayer.GetARInvoiceHdList(string.Format("StudentID IN ({0}) AND GCTransactionStatus != '{1}'", hdnListStudentID.Value, Constant.TransactionStatus.VOID), ctx);
                foreach (ARInvoiceHd arInvoiceHD in lstARInvoiceHd)
                {
                    arInvoiceHD.GCTransactionStatus = Constant.TransactionStatus.VOID;
                    arInvoiceHD.LastUpdatedBy = AppSession.UserLogin.UserID;
                    arInvoiceHdDao.Update(arInvoiceHD);
                }

                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                List<StudentFee> lstStudentFee = BusinessLayer.GetStudentFeeList(string.Format("StudentFeeID IN ({0})", hdnListStudentFeeID.Value), ctx);
                List<StudentFee> lstOldStudentFee = null;
                if(hdnOldListStudentFeeID.Value != "")
                    lstOldStudentFee = BusinessLayer.GetStudentFeeList(string.Format("StudentFeeID IN ({0})", hdnOldListStudentFeeID.Value), ctx);
                List<StudentFeeDt> lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND StudentAmount > 0", hdnListStudentFeeID.Value), ctx);
                for (int i = 0; i < lstSaveValue.Length; ++i)
                {
                    string[] temp = lstSaveValue[i].Split(';');
                    StudentFee entity = lstStudentFee.FirstOrDefault(p => p.StudentFeeID == Convert.ToInt32(temp[0]));
                    entity.IsStudentPenaltyAmountInPercentage = true;
                    entity.StudentPenaltyAmount = Convert.ToDecimal(temp[1]);
                    entity.TotalStudentPenaltyAmount = entity.StudentAmount * entity.StudentPenaltyAmount / 100;
                    entity.TotalStudentAmount = entity.StudentAmount + entity.TotalStudentPenaltyAmount;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDao.Update(entity);

                    StudentFee oldEntity = lstOldStudentFee.FirstOrDefault(p => p.StudentFeeID == entity.StudentFeeID);
                    if (oldEntity != null)
                        lstOldStudentFee.Remove(oldEntity);

                    StudentFeeDt entityDt = lstStudentFeeDt.FirstOrDefault(p => p.StudentFeeID == Convert.ToInt32(temp[0]));
                    entityDt.StudentAmount = entity.StudentAmount;
                    entityDt.TotalStudentPenaltyAmount = entity.TotalStudentPenaltyAmount;
                    entityDt.TotalStudentAmount = entity.TotalStudentAmount;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }

                string lstOldStudentFeeID = string.Join(",", lstOldStudentFee.Select(p => p.StudentFeeID).ToList());
                if (lstOldStudentFeeID != "")
                {
                    lstStudentFeeDt = BusinessLayer.GetStudentFeeDtList(string.Format("StudentFeeID IN ({0}) AND StudentAmount > 0", lstOldStudentFeeID), ctx);
                    foreach (StudentFee entity in lstOldStudentFee)
                    {
                        entity.IsStudentPenaltyAmountInPercentage = false;
                        entity.StudentPenaltyAmount = 0;
                        entity.TotalStudentPenaltyAmount = 0;
                        entity.TotalStudentAmount = entity.StudentAmount;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);

                        StudentFeeDt entityDt = lstStudentFeeDt.FirstOrDefault(p => p.StudentFeeID == entity.StudentFeeID);
                        entityDt.TotalStudentPenaltyAmount = 0;
                        entityDt.TotalStudentAmount = entityDt.StudentAmount;
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);
                    }
                }

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;            
        }
    }
}