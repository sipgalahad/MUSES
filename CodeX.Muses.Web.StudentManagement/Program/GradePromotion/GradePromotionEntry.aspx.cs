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

        protected string OnGetSchoolPeriodNowFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected string OnGetSchoolPeriodNextFilterExpression()
        {
            return string.Format("GCSchoolPeriodStatus != '{0}' AND StartDate <= '{1}' AND EndDate >= '{1}'", Constant.SchoolPeriodStatus.VOID, DateTime.Now.AddYears(1).ToString("yyyyMMdd"));
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
            List<vSite> lstSite = BusinessLayer.GetvSiteList(String.Format("SiteID IN (SELECT SiteID FROM vSite WHERE DisplayPath LIKE '%/{0}/%') AND IsHeader = 0", AppSession.UserLogin.SiteID));
            Methods.SetComboBoxField<vSite>(cboSite, lstSite, "SiteName", "SiteID");
            cboSite.SelectedIndex = 0;

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

        List<vPeriodClassType> lstPeriodClassType = null;
        List<GradePromotionFormulaDt> lstGradePromotionFormula = null;
        List<vClassStudentSubjectMark> lstStudentSubjectMark = null;
        List<vPeriodSection> lstPeriodSection = null;
        List<vClassSubject> lstSubject = null;
        List<vCurriculumSubjectMarkType> lstSubjectMarkType = null;

        int TableWidth = 0;
        private void BindGridView()
        {
            if (tacSchoolClass.Value != "")
            {
                TableWidth += 850;

                SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(tacSchoolPeriod.Value));

                if (hdnLstSubjectID.Value != "")
                {
                    lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID IN ({0})", hdnLstSubjectID.Value));
                    string lstSubjectID = string.Join(",", lstSubject.Select(p => p.SubjectID).ToList());
                    lstSubjectMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("SubjectID IN ({0}) AND CurriculumID = {1}", lstSubjectID, entitySchoolPeriod.CurriculumID));
                }
                else
                {
                    lstSubject = new List<vClassSubject>();
                    lstSubjectMarkType = new List<vCurriculumSubjectMarkType>();
                }
                lstPeriodSection = BusinessLayer.GetvPeriodSectionList(string.Format("SchoolPeriodID = {0} AND GCPeriodSectionStatus != '{1}'", tacSchoolPeriod.Value, Constant.SchoolPeriodStatus.VOID));
                rptColHeaderLevel1.DataSource = lstSubject;
                rptColHeaderLevel1.DataBind();
                rptColHeaderLevel2.DataSource = lstSubject;
                rptColHeaderLevel2.DataBind();
                rptColHeaderLevel3.DataSource = lstSubject;
                rptColHeaderLevel3.DataBind();
                divContainerTable.Style.Remove("display");
            }
            else
                divContainerTable.Style.Add("display", "none");
            

            string filterExpression = GetFilterExpression();
            if (tacNextSchoolPeriod.Value != "")
                lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCGrade = '{1}' AND IsDeleted = 0", tacNextSchoolPeriod.Value, hdnNextGCGrade.Value));
            else
                lstPeriodClassType = new List<vPeriodClassType>();

            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(String.Format("{0} AND GCClassStudentStatus = '{1}'", filterExpression, Constant.ClassStudentStatus.OPEN));

            string lstStudentID = string.Join(",", lstEntity.Select(p => p.StudentID).ToList());
            string lstClassSubjectID = "";
            if (lstSubject != null)
                lstClassSubjectID = string.Join(",", lstSubject.Select(p => p.ClassSubjectID).ToList());
            if (lstStudentID != "" && lstClassSubjectID != "")
                lstStudentSubjectMark = BusinessLayer.GetvClassStudentSubjectMarkList(string.Format("StudentID IN ({0}) AND ClassSubjectID IN ({1})", lstStudentID, lstClassSubjectID));
            else
                lstStudentSubjectMark = new List<vClassStudentSubjectMark>();

            if (hdnGradePromotionFormulaID.Value != "")
                lstGradePromotionFormula = BusinessLayer.GetGradePromotionFormulaDtList(string.Format("GradePromotionFormulaID = {0} AND IsDeleted = 0", hdnGradePromotionFormulaID.Value));
            else
                lstGradePromotionFormula = new List<GradePromotionFormulaDt>();
            rptStudent.DataSource = lstEntity;
            rptStudent.DataBind();
        }

        protected void rptColHeaderLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entitySubject = (vClassSubject)e.Item.DataItem;
                List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();
                HtmlTableCell tdSubjectName = (HtmlTableCell)e.Item.FindControl("tdSubjectName");

                if (lstMarkType.Count == 0)
                    tdSubjectName.Style.Add("display", "none");
                else
                {
                    if (chkIsOnlyFinalMark.Checked)
                    {
                        tdSubjectName.ColSpan = 1;
                        TableWidth += 60;
                    }
                    else
                    {
                        tdSubjectName.ColSpan = lstPeriodSection.Count * lstMarkType.Count + 1;
                        TableWidth += 60 + (90 * lstPeriodSection.Count * lstMarkType.Count);
                    }
                }
            }
        }

        protected void rptColHeaderLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entitySubject = (vClassSubject)e.Item.DataItem;
                Repeater rptColHeaderLevel2Dt = (Repeater)e.Item.FindControl("rptColHeaderLevel2Dt");
                rptColHeaderLevel2Dt.DataSource = lstPeriodSection;
                rptColHeaderLevel2Dt.DataBind();

                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                
                List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();

                if (chkIsOnlyFinalMark.Checked)
                    tdFinalMark.Style.Add("display", "none");
                else
                {
                    if (lstMarkType.Count == 0)
                        tdFinalMark.Style.Add("display", "none");
                    else
                        tdFinalMark.Style.Remove("display");
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
                {
                    vClassSubject entitySubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                    List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();
                    if (lstMarkType.Count > 0)
                    {
                        tdPeriodSection.Style.Remove("display");
                        tdPeriodSection.ColSpan = lstMarkType.Count;
                    }
                    else
                        tdPeriodSection.Style.Add("display", "none");
                }
            }
        }

        protected void rptColHeaderLevel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptColHeaderLevel3Dt = (Repeater)e.Item.FindControl("rptColHeaderLevel3Dt");
                rptColHeaderLevel3Dt.DataSource = lstPeriodSection;
                rptColHeaderLevel3Dt.DataBind();
            }
        }

        protected void rptColHeaderLevel3Dt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (!chkIsOnlyFinalMark.Checked)
                {
                    vClassSubject entitySubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                    Repeater rptColHeaderLevel3Dt2 = (Repeater)e.Item.FindControl("rptColHeaderLevel3Dt2");
                    List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();
                    rptColHeaderLevel3Dt2.DataSource = lstMarkType;
                    rptColHeaderLevel3Dt2.DataBind();
                }
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
                foreach (GradePromotionFormulaDt gradePromotionFormula in lstGradePromotionFormula)
                {
                    decimal mark = 0;
                    vClassStudentSubjectMark classStudentSubjectMark = lstStudentSubjectMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectID == entitySubject.ClassSubjectID && p.CurriculumSchoolPeriodSectionID == gradePromotionFormula.CurriculumSchoolPeriodSectionID && p.CurriculumMarkTypeID == gradePromotionFormula.CurriculumMarkTypeID);
                    if (classStudentSubjectMark != null)
                        mark = classStudentSubjectMark.Mark;
                    finalMark = mark * gradePromotionFormula.FinalMarkPercentage / 100;
                }
                List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();
                HtmlTableCell tdFinalMark = (HtmlTableCell)e.Item.FindControl("tdFinalMark");
                if (finalMark < entitySubject.PassingGrade)
                    tdFinalMark.Attributes.Add("class", "belowpassinggrade");                
                tdFinalMark.InnerHtml = finalMark.ToString("N");
                if (chkIsOnlyFinalMark.Checked)
                    tdFinalMark.Style.Add("width", "150px");
                else
                    tdFinalMark.Style.Remove("width");

                if (lstMarkType.Count == 0)
                    tdFinalMark.Style.Add("display", "none");
            }
        }

        protected void rptStudentSubjectPeriodSection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vPeriodSection entityPeriodSection = (vPeriodSection)e.Item.DataItem;
                vClassSubject entitySubject = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassSubject;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem as vClassStudent;

                List<vCurriculumSubjectMarkType> lstMarkType = lstSubjectMarkType.Where(p => p.SubjectID == entitySubject.SubjectID).ToList();
                if (!chkIsOnlyFinalMark.Checked)
                {
                    Repeater rptStudentSubjectMarkType = (Repeater)e.Item.FindControl("rptStudentSubjectMarkType");
                    rptStudentSubjectMarkType.DataSource = lstMarkType;
                    rptStudentSubjectMarkType.DataBind();
                }
            }
        }

        protected void rptStudentSubjectMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCurriculumSubjectMarkType entityMarkType = (vCurriculumSubjectMarkType)e.Item.DataItem;
                vPeriodSection entityPeriodSection = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vPeriodSection;
                vClassSubject entitySubject = ((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem as vClassSubject;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent.Parent.Parent.Parent.Parent).DataItem as vClassStudent;
                vClassStudentSubjectMark mark = lstStudentSubjectMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectID == entitySubject.ClassSubjectID && p.PeriodSectionID == entityPeriodSection.PeriodSectionID && p.CurriculumMarkTypeID == entityMarkType.CurriculumMarkTypeID);

                if (mark != null)
                {
                    HtmlTableCell tdStudentMark = (HtmlTableCell)e.Item.FindControl("tdStudentMark");
                    tdStudentMark.InnerHtml = mark.Mark.ToString();
                }
            }
        }

        protected void cbpSubject_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (tacSchoolClass.Value != "")
                lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", tacSchoolClass.Value, Constant.ClassStudyType.REGULAR));
            else
                lstSubject = new List<vClassSubject>();
            
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
            TableWidth = 0;
            BindGridView();

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