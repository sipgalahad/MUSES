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
    public partial class GradePromotionEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.GRADE_PROMOTION;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }

            SchoolPeriod nextSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now.AddYears(1) && p.EndDate >= DateTime.Now.AddYears(1));
            if (nextSchoolPeriod != null)
                hdnNextSchoolPeriod.Value = nextSchoolPeriod.SchoolPeriodID.ToString();
            else
                hdnNextSchoolPeriod.Value = cboSchoolPeriod.Value.ToString();

            //BindGridView();
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

        List<ClassStudentMark> lstStudentMark = null;
        List<vPeriodClassType> lstPeriodClassType = null;

        List<GradePromotionFormulaDt> lstGradePromotionFormula = null;
        List<ClassStudentSubjectMark> lstStudentSubjectMark = null;
        List<vPeriodSection> lstPeriodSection = null;
        List<vClassSubject> lstSubject = null;

        private void BindGridView(ref int TableWidth)
        {
            if (tacSchoolClass.Value != "")
            {
                if (hdnLstSubjectID.Value != "")
                    lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID IN ({0})", hdnLstSubjectID.Value));
                else
                    lstSubject = new List<vClassSubject>();
                lstPeriodSection = BusinessLayer.GetvPeriodSectionList(string.Format("SchoolPeriodID = {0} AND GCPeriodSectionStatus != '{1}'", cboSchoolPeriod.Value, Constant.SchoolPeriodStatus.VOID));
                rptColHeaderLevel1.DataSource = lstSubject;
                rptColHeaderLevel1.DataBind();
                rptColHeaderLevel2.DataSource = lstSubject;
                rptColHeaderLevel2.DataBind();

                List<Variable> lstVariable = new List<Variable>();
                foreach (vClassSubject subject in lstSubject)
                {
                    foreach (vPeriodSection periodSection in lstPeriodSection)
                    {
                        lstVariable.Add(new Variable());
                    }
                }
                rptColHeaderLevel3.DataSource = lstVariable;
                rptColHeaderLevel3.DataBind();
                divContainerTable.Style.Remove("display");
            }
            else
                divContainerTable.Style.Add("display", "none");
            

            string filterExpression = GetFilterExpression();
            if (tacSchoolClass.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value));

            lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCGrade = '{1}' AND IsDeleted = 0", hdnNextSchoolPeriod.Value, hdnNextGCGrade.Value));

            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(String.Format("{0} AND GCClassStudentStatus = '{1}'", filterExpression, Constant.ClassStudentStatus.OPEN));

            string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
            string lstClassSubjectID = "";
            if (lstSubject != null)
                lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            if (lstStudentID != "" && lstClassSubjectID != "")
                lstStudentSubjectMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("StudentID IN ({0}) AND ClassSubjectID IN ({1})", lstStudentID, lstClassSubjectID));
            else
                lstStudentSubjectMark = new List<ClassStudentSubjectMark>();

            lstGradePromotionFormula = BusinessLayer.GetGradePromotionFormulaDtList(string.Format("IsDeleted = 0"));
            rptStudent.DataSource = lstEntity;
            rptStudent.DataBind();

            if (chkIsOnlyFinalMark.Checked)
                TableWidth = (160 * lstSubject.Count) + 650;
            else
                TableWidth = (((60 * 3 * lstPeriodSection.Count) + 135) * lstSubject.Count) + 650;
        }

        protected void rptColHeaderLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell tdSubjectName = (HtmlTableCell)e.Item.FindControl("tdSubjectName");
                if (chkIsOnlyFinalMark.Checked)
                    tdSubjectName.ColSpan = 1;
                else
                    tdSubjectName.ColSpan = 7;
            }
        }

        protected void rptColHeaderLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptColHeaderLevel2Dt = (Repeater)e.Item.FindControl("rptColHeaderLevel2Dt");
                rptColHeaderLevel2Dt.DataSource = lstPeriodSection;
                rptColHeaderLevel2Dt.DataBind();

                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                if (chkIsOnlyFinalMark.Checked)
                    tdFinalMark.Style.Add("display", "none");
                else
                    tdFinalMark.Style.Remove("display");
            }
        }

        protected void rptColHeaderLevel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell tdTheory = (HtmlTableCell)e.Item.FindControl("tdTheory");
                HtmlTableCell tdPractice = (HtmlTableCell)e.Item.FindControl("tdPractice");
                HtmlTableCell tdAffective = (HtmlTableCell)e.Item.FindControl("tdAffective");
                if (chkIsOnlyFinalMark.Checked)
                {
                    tdTheory.Style.Add("display", "none");
                    tdPractice.Style.Add("display", "none");
                    tdAffective.Style.Add("display", "none");
                }
                else
                {
                    tdTheory.Style.Remove("display");
                    tdPractice.Style.Remove("display");
                    tdAffective.Style.Remove("display");
                }
            }
        }

        protected void rptColHeaderLevel2Dt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableCell tdPeriodSection = (HtmlTableCell)e.Item.FindControl("tdPeriodSection");
                if (chkIsOnlyFinalMark.Checked)
                    tdPeriodSection.Style.Add("display", "none");
                else
                    tdPeriodSection.Style.Remove("display");
            }
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentSubject = (Repeater)e.Item.FindControl("rptStudentSubject");
                rptStudentSubject.DataSource = lstSubject;
                rptStudentSubject.DataBind();

                HtmlGenericControl divNextGrade = (HtmlGenericControl)e.Item.FindControl("divNextGrade");
                divNextGrade.InnerHtml = hdnNextGrade.Value;

                ASPxComboBox cboGCMajor = (ASPxComboBox)e.Item.FindControl("cboGCMajor");
                cboGCMajor.ClientInstanceName = string.Format("cboGCMajor{0}", e.Item.ItemIndex);
                Methods.SetComboBoxField<vPeriodClassType>(cboGCMajor, lstPeriodClassType, "Major", "GCMajor");

                if (hdnGCMajor.Value != "")
                {
                    cboGCMajor.Value = hdnGCMajor.Value;
                    cboGCMajor.ClientEnabled = false;
                }
            }
        }

        protected void rptStudentSubject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entitySubject = (vClassSubject)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                Repeater rptStudentSubjectPeriodSection = (Repeater)e.Item.FindControl("rptStudentSubjectPeriodSection");
                rptStudentSubjectPeriodSection.DataSource = lstPeriodSection;
                rptStudentSubjectPeriodSection.DataBind();

                decimal finalMark = 0;
                foreach (vPeriodSection periodSection in lstPeriodSection)
                {
                    decimal mark = 0;
                    ClassStudentSubjectMark classStudentSubjectMark = lstStudentSubjectMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectID == entitySubject.ClassSubjectID && p.PeriodSectionID == periodSection.PeriodSectionID);
                    if (classStudentSubjectMark != null)
                        mark = classStudentSubjectMark.TheoryMark;

                    finalMark = mark * lstGradePromotionFormula.FirstOrDefault(p => p.GCPeriodSection == periodSection.GCPeriodSection).FinalMarkPercentage / 100;
                }
                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                if (finalMark < entitySubject.PassingGrade)
                    tdFinalMark.Attributes.Add("class", "belowpassinggrade");                
                tdFinalMark.InnerHtml = finalMark.ToString("N");
                if (chkIsOnlyFinalMark.Checked)
                    tdFinalMark.Style.Add("width", "150px");
                else
                    tdFinalMark.Style.Remove("width");
            }
        }

        protected void rptStudentSubjectPeriodSection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vPeriodSection entityPeriodSection = (vPeriodSection)e.Item.DataItem;
                vClassSubject entitySubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem as vClassStudent;
                ClassStudentSubjectMark mark = lstStudentSubjectMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectID == entitySubject.ClassSubjectID && p.PeriodSectionID == entityPeriodSection.PeriodSectionID);
                HtmlTableCell tdTheoryMark = (HtmlTableCell)e.Item.FindControl("tdTheoryMark");
                HtmlTableCell tdPracticeMark = (HtmlTableCell)e.Item.FindControl("tdPracticeMark");
                HtmlTableCell tdAffectiveMark = (HtmlTableCell)e.Item.FindControl("tdAffectiveMark");

                if (mark != null)
                {
                    tdTheoryMark.InnerHtml = mark.TheoryMark.ToString();
                    tdPracticeMark.InnerHtml = mark.PracticeMark.ToString();
                    tdAffectiveMark.InnerHtml = mark.AffectiveMark.ToString();
                }

                if (chkIsOnlyFinalMark.Checked)
                {
                    tdTheoryMark.Style.Add("display", "none");
                    tdPracticeMark.Style.Add("display", "none");
                    tdAffectiveMark.Style.Add("display", "none");
                }
                else
                {
                    tdTheoryMark.Style.Remove("display");
                    tdPracticeMark.Style.Remove("display");
                    tdAffectiveMark.Style.Remove("display");
                }
            }
        }

        protected void cbpSubject_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", tacSchoolClass.Value, Constant.ClassStudyType.REGULAR));
            
            ASPxCallbackPanel cbpSubject = (ASPxCallbackPanel)ddeSubject.FindControl("cbpSubject");
            GridView grdSubject = (GridView)cbpSubject.FindControl("grdSubject");
            grdSubject.DataSource = lstSubject;
            grdSubject.DataBind();
        }

        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;
                CheckBox chkSubject = (CheckBox)e.Row.FindControl("chkSubject");
                chkSubject.Attributes.Add("id", entity.ClassSubjectID.ToString());
                chkSubject.Attributes.Add("name", entity.SubjectName);
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int TableWidth = 0;
            BindGridView(ref TableWidth);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpTableWidth"] = TableWidth;
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "promote")
            {
                if (OnPromoteEntity(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else 
            {
                if (OnRejectEntity(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnPromoteEntity(ref string errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentDao studentDao = new StudentDao(ctx);
            ClassStudentDao classStudentDao = new ClassStudentDao(ctx);
            try
            {
                List<Student> lstStudent = BusinessLayer.GetStudentList(String.Format("StudentID IN ({0})", hdnLstStudentID.Value), ctx);
                List<ClassStudent> lstClassStudent = BusinessLayer.GetClassStudentList(String.Format("SchoolClassID = {0} AND StudentID IN ({1})", tacSchoolClass.Value, hdnLstStudentID.Value), ctx);
                
                string[] lstSaveValue = hdnSelectedValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue) 
                {
                    string[] temp = saveValue.Split(';');

                    Student entity = lstStudent.FirstOrDefault(p => p.StudentID == Convert.ToInt32(temp[0]));
                    entity.GCGrade = hdnNextGCGrade.Value;
                    entity.GCMajor = temp[1];

                    ClassStudent csEntity = lstClassStudent.FirstOrDefault(p => p.StudentID == entity.StudentID);
                    csEntity.GCClassStudentStatus = Constant.ClassStudentStatus.NAIK_KELAS;
                    classStudentDao.Update(csEntity);
                    studentDao.Update(entity);
                }
                ctx.CommitTransaction();
            }
            catch(Exception ex)
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

        private bool OnRejectEntity(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentDao classStudentDao = new ClassStudentDao(ctx);
            try
            {
                List<ClassStudent> lstClassStudent = BusinessLayer.GetClassStudentList(String.Format("SchoolClassID = {0} AND StudentID IN ({1})", tacSchoolClass.Value, hdnLstStudentID.Value), ctx);

                foreach (ClassStudent entity in lstClassStudent)
                {
                    entity.GCClassStudentStatus = Constant.ClassStudentStatus.TIDAK_NAIK_KELAS;
                    classStudentDao.Update(entity);
                }
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