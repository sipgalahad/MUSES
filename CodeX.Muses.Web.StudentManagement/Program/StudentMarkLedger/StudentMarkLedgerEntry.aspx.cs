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
    public partial class StudentMarkLedgerEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_MARK_LEDGER;
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

        List<vPeriodClassType> lstPeriodClassType = null;
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

            rptStudent.DataSource = lstEntity;
            rptStudent.DataBind();

            TableWidth = (((60 * 3 * lstPeriodSection.Count) + 45) * lstSubject.Count) + 650;
        }

        protected void rptColHeaderLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptColHeaderLevel2Dt = (Repeater)e.Item.FindControl("rptColHeaderLevel2Dt");
                rptColHeaderLevel2Dt.DataSource = lstPeriodSection;
                rptColHeaderLevel2Dt.DataBind();
            }
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentSubject = (Repeater)e.Item.FindControl("rptStudentSubject");
                rptStudentSubject.DataSource = lstSubject;
                rptStudentSubject.DataBind();
            }
        }

        protected void rptStudentSubject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentSubjectPeriodSection = (Repeater)e.Item.FindControl("rptStudentSubjectPeriodSection");
                rptStudentSubjectPeriodSection.DataSource = lstPeriodSection;
                rptStudentSubjectPeriodSection.DataBind();
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
                    //tdTheoryMark.InnerHtml = mark.TheoryMark.ToString();
                    //tdPracticeMark.InnerHtml = mark.PracticeMark.ToString();
                    //tdAffectiveMark.InnerHtml = mark.AffectiveMark.ToString();

                    //if (mark.TheoryMark < entitySubject.PassingGrade)
                    //    tdTheoryMark.Attributes.Add("class", "belowpassinggrade");
                }
            }
        }

        protected void cbpSubject_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Int32 SchoolClassID = 0;
            if(tacSchoolClass.Value != "")
                SchoolClassID = Convert.ToInt32(tacSchoolClass.Value);
            lstSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}' AND IsDeleted = 0", SchoolClassID, Constant.ClassStudyType.REGULAR));
            
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

        public override Control OnGetExportControl()
        {
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = String.Format("Tahun Ajaran : {0}<br/>Kelas : {1}", cboSchoolPeriod.Text, hdnClassName.Value);
            div.InnerHtml = hdnExportData.Value;
            div.Controls.AddAt(0, h4);
            return div;
        }
    }
}