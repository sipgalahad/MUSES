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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentSchoolFeeEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_SCHOOL_FEE;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        List<StudentFeeCompType> lstComp = null;
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("GCAdmissionPaymentPeriod != '{0}' AND IsDeleted = 0", Constant.AdmissionPaymentPeriod.SEKALI_BAYAR));
            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            Methods.SetComboBoxField<SchoolPeriod>(cboNextSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();
            cboNextSchoolPeriod.SelectedIndex = 0;

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (tacSchoolClass.Value == "")
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolClassID = {0}", tacSchoolClass.Value);
            return filterExpression;
        }

        List<StudentFeeComp> lstStudentFeeComp = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            List<vStudentCustom> lstEntity = BusinessLayer.GetvStudentCustomList(String.Format("{0} AND GCClassStudentStatus != '{1}' AND SchoolClassID IS NOT NULL", filterExpression, Constant.ClassStudentStatus.OPEN));
            
            if (lstEntity.Count > 0)
            {
                string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
                lstStudentFeeComp = BusinessLayer.GetStudentFeeCompList(String.Format("StudentID IN ({0}) AND SchoolPeriodID IN ({1},{2})", lstStudentID, cboSchoolPeriod.Value, cboNextSchoolPeriod.Value));
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
                StudentFeeComp entity = lstStudentFeeComp.FirstOrDefault(p => p.StudentID == student.StudentID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID && p.SchoolPeriodID == Convert.ToInt32(cboSchoolPeriod.Value));
                StudentFeeComp nextEntity = lstStudentFeeComp.FirstOrDefault(p => p.StudentID == student.StudentID && p.StudentFeeCompTypeID == studentFeeCompType.StudentFeeCompTypeID && p.SchoolPeriodID == Convert.ToInt32(cboNextSchoolPeriod.Value));
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
                int SchoolPeriodID = Convert.ToInt32(cboNextSchoolPeriod.Value);
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