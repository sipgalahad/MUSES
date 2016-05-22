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
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class StudentReRegistrationEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Finance.STUDENT_REREGISTRATION;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetSchoolPeriodNextFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.AddYears(1).ToString("yyyyMMdd"));
        }

        List<StudentFeeCompType> lstComp = null;
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("GCAdmissionPaymentPeriod != '{0}' AND IsDeleted = 0", Constant.AdmissionPaymentPeriod.SEKALI_BAYAR));
            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            List<StandardCode> lstStudentType = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_TYPE));
            lstStudentType.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "Semua" });
            Methods.SetComboBoxField<StandardCode>(cboStudentType, lstStudentType, "StandardCodeName", "StandardCodeID");
            cboStudentType.SelectedIndex = 0;

            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (tacSchoolPeriod.Value == "" || tacPeriodClassType.Value == "")
                return "1 = 0";

            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("PeriodClassTypeID = {0}", tacPeriodClassType.Value);
            if (cboStudentType.Value != null && cboStudentType.Value.ToString() != "")
                filterExpression += string.Format(" AND GCStudentType = '{0}'", cboStudentType.Value);
            return filterExpression;
        }

        List<StudentFeeComp> lstStudentFeeComp = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            List<vStudentCustom> lstEntity = BusinessLayer.GetvStudentCustomList(String.Format("{0} AND IsGenerateStudentFeeNextPeriod = 0", filterExpression));
            
            if (lstEntity.Count > 0)
            {
                string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
                lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("StudentID IN ({0}) AND SchoolPeriodID IN ({1},{2})", lstStudentID, tacSchoolPeriod.Value, tacNextSchoolPeriod.Value));
            }
            else
                lstStudentFeeComp = new List<StudentFeeComp>();

            if (lstComp == null) 
                lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("GCAdmissionPaymentPeriod != '{0}' AND IsDeleted = 0", Constant.AdmissionPaymentPeriod.SEKALI_BAYAR));
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentCustom entity = (vStudentCustom)e.Item.DataItem;

                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                rptViewDt.DataSource = lstComp;
                rptViewDt.DataBind();
            }
        }
        protected void rptViewDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentCustom student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vStudentCustom;
                StudentFeeCompType studentFeeCompType = (StudentFeeCompType)e.Item.DataItem;
                StudentFeeComp entity = lstStudentFeeComp.FirstOrDefault(p => p.StudentID == student.StudentID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID && p.SchoolPeriodID == Convert.ToInt32(tacSchoolPeriod.Value));
                StudentFeeComp nextEntity = lstStudentFeeComp.FirstOrDefault(p => p.StudentID == student.StudentID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID && p.SchoolPeriodID == Convert.ToInt32(tacNextSchoolPeriod.Value));
                TextBox txtOldCompValue = (TextBox)e.Item.FindControl("txtOldCompValue");
                TextBox txtNewCompValue = (TextBox)e.Item.FindControl("txtNewCompValue");
                if (entity != null)
                    txtNewCompValue.Text = txtOldCompValue.Text = entity.TotalAmount.ToString();
                if (nextEntity != null)
                    txtNewCompValue.Text = entity.TotalAmount.ToString();
                txtNewCompValue.Attributes.Add("studentfeecomptypeid", studentFeeCompType.StudentFeeCompTypeID.ToString());
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentFeeCompDao entityStudentFeeCompDao = new StudentFeeCompDao(ctx);
            try
            {
                int SchoolPeriodID = Convert.ToInt32(tacNextSchoolPeriod.Value);
                List<StudentFeeComp> lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(string.Format("StudentID IN ({0}) AND SchoolPeriodID = {1}", hdnLstStudentID.Value, SchoolPeriodID), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    string[] lstSaveValue1 = temp[1].Split(';');
                    foreach (string saveValue1 in lstSaveValue1)
                    {
                        string[] temp1 = saveValue1.Split('^');
                        int studentFeeCompTypeID = Convert.ToInt32(temp1[0]);
                        decimal totalAmount = Convert.ToDecimal(temp1[1]);
                        StudentFeeComp entity = lstStudentFeeComp.FirstOrDefault(p => p.StudentID == studentID && p.StudentFeeCompTypeID == studentFeeCompTypeID);
                        if (entity != null)
                        {
                            entity.TotalAmount = totalAmount;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeCompDao.Update(entity);
                        }
                        else
                        {
                            entity = new StudentFeeComp();
                            entity.SchoolPeriodID = SchoolPeriodID;
                            entity.StudentID = studentID;
                            entity.StudentFeeCompTypeID = studentFeeCompTypeID;
                            entity.TotalAmount = totalAmount;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            entityStudentFeeCompDao.Insert(entity);
                        }
                    }
                }
                BusinessLayer.ProcessReRegistrationStudent(hdnSelectedValue.Value, SchoolPeriodID, AppSession.UserLogin.UserID, ctx);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}