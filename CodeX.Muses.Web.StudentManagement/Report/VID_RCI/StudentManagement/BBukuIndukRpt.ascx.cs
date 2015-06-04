using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Report
{
    public partial class BBukuIndukRpt : BaseCustomReportCtl
    {
        private Int32 PeriodSectionID = 0;
        private Int32 SchoolClassID = 0;
        private Int32 StudentID = 0;
        private Int32 SubjectID = 0;

        List<vClassSubject> lstClassSubject = null;
        List<SchoolPeriod> lstSchoolPeriod = null;
        List<PeriodSection> lstPeriodSection = null;
        
        List<vClassStudentSubjectMark> lstNilai = null;
        List<vClassStudent> lstClassStudent = new List<vClassStudent>();
        
        List<StandardCode> lstStandardCode = null;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            StudentID = Convert.ToInt32(param[0]);
            
            lstClassStudent = BusinessLayer.GetvClassStudentList(String.Format("StudentID = {0} OR (GCClassStudyType = '{1}' AND StudentID = {0})", param[0], Constant.ClassStudyType.EXTRACURRICULAR));

            String lstSchoolClassID = String.Join(",", lstClassStudent.GroupBy(s => s.SchoolClassID).Select(x => x.Key));
            lstClassSubject = BusinessLayer.GetvClassSubjectList(String.Format("SchoolClassID IN ({0}) AND IsDeleted = 0 AND ParentID IS NULL", lstSchoolClassID));
            String lstClassSubjectID = String.Join(",", lstClassSubject.Select(x => x.ClassSubjectID));

            lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(String.Format("SchoolPeriodID IN ({0})", String.Join(",", lstClassSubject.Select(x => x.SchoolPeriodID))));
            lstPeriodSection = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID IN ({0}) AND GCPeriodSectionStatus != '{0}'", String.Join(",",lstSchoolPeriod.Select(x => x.SchoolPeriodID)), Constant.SchoolPeriodStatus.VOID));
            lstNilai = BusinessLayer.GetvClassStudentSubjectMarkList(String.Format("StudentID = {0}", StudentID)).ToList();

            Int32 MaxSchoolPeriodID = lstClassSubject.Select(x => x.SchoolPeriodID).Max();
            Int32 MaxPeriodSectionID = lstClassSubject.Where(x => x.SchoolPeriodID == MaxSchoolPeriodID).Select(s => s.PeriodSectionID).Max();

            #region Personal
            vStudent st = BusinessLayer.GetvStudentList(String.Format("StudentID = {0}", StudentID))[0];
            lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("IsDeleted = 0 AND IsActive = 1 AND ParentID IN ('{0}','{1}','{2}','{3}','{4}')", Constant.StandardCode.GENDER, Constant.StandardCode.RELIGION,
                Constant.StandardCode.NATIONALITY, Constant.StandardCode.FAMILY_RELATION, Constant.StandardCode.EDUCATION));
            
            vProspectiveStudent ps = BusinessLayer.GetvProspectiveStudentList(String.Format("ProspectiveStudentID IN (SELECT ProspectiveStudentID FROM Registration WHERE RegistrationID = {0})", st.RegistrationID))[0];
            string text = divRBHeader.InnerHtml;
            text = text.Replace("{NISN}", String.Format("{0} / {1}", st.StudentCode, st.NationalStudentNo));
            divRBHeader.InnerHtml = text;

            text = divPersonal.InnerHtml;
            text = text.Replace("{Fullname}", st.StudentName);
            text = text.Replace("{PreferredName}", st.PreferredName);
            text = text.Replace("{Gender}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == st.GCGender).StandardCodeName);
            text = text.Replace("{CityOfBirth}", st.CityOfBirth);
            text = text.Replace("{DateOfBirth}", st.DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT));
            text = text.Replace("{Religion}", st.Religion);
            text = text.Replace("{Nationality}", st.Nationality);
            text = text.Replace("{Language}", ps.Language);
            
            List<StudentFamily> lstFamily = BusinessLayer.GetStudentFamilyList(String.Format("StudentID = {0}", StudentID));
            int sibling = lstFamily.Where(x => x.GCFamilyRelation == Constant.FamilyRelation.KAKAK || x.GCFamilyRelation == Constant.FamilyRelation.ADIK).Count();
            if (sibling != 0)
                text = text.Replace("{Sibling}", sibling.ToString());
            else
                text = text.Replace("{Sibling}", "-");
            divPersonal.InnerHtml = text;
            #endregion

            #region Address
            text = divAddress.InnerHtml;
            text = text.Replace("{StreetName}", st.StreetName);
            text = text.Replace("{District}", st.District);
            text = text.Replace("{HomePhone}", st.PhoneNo1);
            text = text.Replace("{HomeDistance}", ps.HomeDistance.ToString());
            divAddress.InnerHtml = text;
            #endregion

            #region KESEHATAN
            text = divMedical.InnerHtml;
            text = text.Replace("{BloodType}", ps.BloodType);
            divMedical.InnerHtml = text;
            #endregion

            #region KETERANGAN PENDIDIKAN SEBELUMNYA
            List<StudentPastStudy> lstSps = BusinessLayer.GetStudentPastStudyList(String.Format("StudentID = {0} AND GCSchoolType = '{1}'", StudentID, Constant.SchoolTypeName.SMP));
            text = divPastStudy.InnerHtml;
            if (lstSps.Count() > 0)
            {
                
                text = text.Replace("{PastSchoolName}", lstSps[0].SchoolName);
                text = text.Replace("{PastLongStudy}", (lstSps[0].EndYear - lstSps[0].StartYear).ToString());
                
            }
            else 
            {
                text = text.Replace("{PastSchoolName}", "-");
                text = text.Replace("{PastSTL}", "-");
                text = text.Replace("{PastIjazah}", "-");
                text = text.Replace("{PastLongStudy}", "-");
            }
            divPastStudy.InnerHtml = text;
            #endregion

            #region KETERANGAN ORANG TUA
            text = divParent.InnerHtml;
            StudentFamily father = lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.FATHER);
            StudentFamily mother = lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.MOTHER);

            if (father != null)
            {
                text = text.Replace("{FatherName}", father.FullName);
                text = text.Replace("{FatherDOB}", String.Format("{0}, {1}", father.CityOfBirth, father.DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT)));
                text = text.Replace("{FatherNationality}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == father.GCNationality).StandardCodeName);
                text = text.Replace("{FatherEducationLevel}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == father.GCEducationLevel).StandardCodeName);
                text = text.Replace("{FatherJob}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == father.GCJob).StandardCodeName);
                text = text.Replace("{FatherSalary}", father.Salary.ToString("N"));
                //text = text.Replace("{FatherAddress}", "-");
            }
            else
            {
                text = text.Replace("{FatherName}", "-");
                text = text.Replace("{FatherDOB}", "-");
                text = text.Replace("{FatherNationality}", "-");
                text = text.Replace("{FatherEducationLevel}", "-");
                text = text.Replace("{FatherJob}", "-");
                text = text.Replace("{FatherSalary}", "-");
                text = text.Replace("{FatherAddress}", "-");
            }

            if (mother != null)
            {
                text = text.Replace("{MotherName}", mother.FullName);
                text = text.Replace("{MotherDOB}", String.Format("{0}, {1}", mother.CityOfBirth, mother.DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT)));
                text = text.Replace("{MotherNationality}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == mother.GCNationality).StandardCodeName);
                text = text.Replace("{MotherEducationLevel}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == mother.GCEducationLevel).StandardCodeName);
                text = text.Replace("{MotherJob}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == mother.GCJob).StandardCodeName);
                text = text.Replace("{MotherSalary}", mother.Salary.ToString("N"));
                //text = text.Replace("{MotherAddress}", "-");
            }
            else
            {
                text = text.Replace("{MotherName}", "-");
                text = text.Replace("{MotherDOB}", "-");
                text = text.Replace("{MotherNationality}", "-");
                text = text.Replace("{MotherEducationLevel}", "-");
                text = text.Replace("{MotherJob}", "-");
                text = text.Replace("{MotherSalary}", "-");
                text = text.Replace("{MotherAddress}", "-");
            }
            divParent.InnerHtml = text;
            #endregion

            #region KETERANGAN WALI
            text = divWali.InnerHtml;
            StudentFamily WaliP = null;// lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.UNCLE);
            StudentFamily WaliW = null;// lstFamily.FirstOrDefault(x => x.GCFamilyRelation == Constant.FamilyRelation.AUNT);

            if (WaliP != null)
            {
                text = text.Replace("{WaliPName}", WaliP.FullName);
                text = text.Replace("{WaliPDOB}", String.Format("{0}, {1}", WaliP.CityOfBirth, WaliP.DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT)));
                text = text.Replace("{WaliPNationality}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliP.GCNationality).StandardCodeName);
                text = text.Replace("{WaliPEducationLevel}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliP.GCEducationLevel).StandardCodeName);
                text = text.Replace("{WaliPJob}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliP.GCJob).StandardCodeName);
                text = text.Replace("{WaliPSalary}", WaliP.Salary.ToString("N"));
                //text = text.Replace("{WaliPAddress}", "-");
                //text = text.Replace("{WaliPRelationship}", "-");
            }
            else
            {
                text = text.Replace("{WaliPName}", "-");
                text = text.Replace("{WaliPDOB}", "-");
                text = text.Replace("{WaliPNationality}", "-");
                text = text.Replace("{WaliPEducationLevel}", "-");
                text = text.Replace("{WaliPJob}", "-");
                text = text.Replace("{WaliPSalary}", "-");
                text = text.Replace("{WaliPAddress}", "-");
                text = text.Replace("{WaliPRelationship}", "-");
            }
            if (WaliW != null)
            {
                text = text.Replace("{WaliWName}", WaliW.FullName);
                text = text.Replace("{WaliWDOB}", String.Format("{0}, {1}", WaliW.CityOfBirth, WaliW.DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT)));
                text = text.Replace("{WaliWNationality}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliW.GCNationality).StandardCodeName);
                text = text.Replace("{WaliWEducationLevel}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliW.GCEducationLevel).StandardCodeName);
                text = text.Replace("{WaliWJob}", lstStandardCode.FirstOrDefault(x => x.StandardCodeID == WaliW.GCJob).StandardCodeName);
                text = text.Replace("{WaliWSalary}", WaliW.Salary.ToString("N"));
                //text = text.Replace("{WaliPAddress}", "-");
                //text = text.Replace("{WaliWRelationship}", WaliW.Salary.ToString("N"));
            }
            else
            {
                text = text.Replace("{WaliWName}", "-");
                text = text.Replace("{WaliWDOB}", "-");
                text = text.Replace("{WaliWNationality}", "-");
                text = text.Replace("{WaliWEducationLevel}", "-");
                text = text.Replace("{WaliWJob}", "-");
                text = text.Replace("{WaliWSalary}", "-");
                text = text.Replace("{WaliWAddress}", "-");
                text = text.Replace("{WaliWRelationship}", "-");
            }
            divWali.InnerHtml = text;
            #endregion

            #region INTELEGENSI DAN KEGEMARAN
            if (lstClassSubjectID != "")
            {
                rptPersonality.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.PERSONALITY && x.PeriodSectionID == MaxPeriodSectionID && x.PeriodSectionID == MaxPeriodSectionID);
                rptPersonality.DataBind();
            }
            List<StudentAchievement> lstAchievement = BusinessLayer.GetStudentAchievementList(String.Format("StudentID = {0} AND IsDeleted = 0", StudentID));
            string art = String.Join(",", lstAchievement.Where(x => x.GCAchievementType == Constant.AchievementType.KESENIAN).Select(s => s.AchievementName));
            string sport = String.Join(",", lstAchievement.Where(x => x.GCAchievementType == Constant.AchievementType.OLAHRAGA).Select(s => s.AchievementName));
            string academy = String.Join(",", lstAchievement.Where(x => x.GCAchievementType == Constant.AchievementType.AKADEMIS).Select(s => s.AchievementName));
            string organization = "-";
            tdAchievemntArt.InnerHtml = art == "" ? "-" : art;
            tdAchievemntSport.InnerHtml = sport == "" ? "-" : sport;
            tdAchievemntAcd.InnerHtml = academy == "" ? "-" : academy;
            tdAchievemntOrg.InnerHtml = organization == "" ? "-" : organization;
            #endregion

            #region KETERANGAN KEHADIRAN
            rptAttendace.DataSource = lstPeriodSection;
            rptAttendace.DataBind();
            #endregion

            #region KETERANGAN PERKEMBANGAN
            rptStudentProgress.DataSource = lstSchoolPeriod;
            rptStudentProgress.DataBind();
            #endregion

            #region BEASISWA
            List<vStudentScholarshipTransactionDt> lstScholarship = BusinessLayer.GetvStudentScholarshipTransactionDtList(String.Format("StudentID = {0}",StudentID));
            //if (lstScholarship.Count() == 0) 
            //{
            //    lstScholarship = new List<vStudentScholarshipTransactionDt>();
            //    lstScholarship.Add(new vStudentScholarshipTransactionDt { ScholarshipName = "" });
            //} 
            rptScholarship.DataSource = lstScholarship;
            rptScholarship.DataBind();
            #endregion

            #region PRESTASI BELAJAR
            if (lstClassSubjectID != "")
            {
                rptSchoolPeriod.DataSource = lstSchoolPeriod;
                rptSchoolPeriod.DataBind();
                
                rptSchoolPeriod1.DataSource = lstSchoolPeriod;
                rptSchoolPeriod1.DataBind();
                
                rptSubject.DataSource = lstClassSubject.Where(x => x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR);
                rptSubject.DataBind();
            }
            #endregion
        }

        protected void rptStudentProgress_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) 
            {
                SchoolPeriod sp = e.Item.DataItem as SchoolPeriod;
                HtmlTableCell tdSPGrade = e.Item.FindControl("tdSPGrade") as HtmlTableCell;
                HtmlTableCell tdSPPeriodName = e.Item.FindControl("tdSPPeriodName") as HtmlTableCell;

                Repeater rptSPPeriodSection = e.Item.FindControl("rptSPPeriodSection") as Repeater;

                List<SchoolClass> lstSc = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN (SELECT SchoolClassID FROM ClassStudent WHERE StudentID = {0}) AND " +
                             "PeriodClassTypeID IN (SELECT PeriodClassTypeID FROM PeriodClassType WHERE SchoolPeriodID = {1}) AND " +
                              "IsDeleted = 0", StudentID, sp.SchoolPeriodID));
                if (lstSc.Count() > 0)
                    tdSPGrade.InnerHtml = lstSc[0].SchoolClassName;
                else
                    tdSPGrade.InnerHtml = "-";
                
                tdSPPeriodName.RowSpan = tdSPGrade.RowSpan = lstPeriodSection.Where(x => x.SchoolPeriodID == sp.SchoolPeriodID).Count() + 1;
                rptSPPeriodSection.DataSource = lstPeriodSection.Where(x => x.SchoolPeriodID == sp.SchoolPeriodID).ToList();
                rptSPPeriodSection.DataBind();
            }
        }

        protected void rptSPPeriodSection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) 
            {
                PeriodSection ps = e.Item.DataItem as PeriodSection;
                List<SchoolClass> lstSc = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN (SELECT SchoolClassID FROM ClassStudent WHERE StudentID = {0}) AND " +
                             "PeriodClassTypeID IN (SELECT PeriodClassTypeID FROM PeriodClassType WHERE SchoolPeriodID = {1}) AND " +
                              "IsDeleted = 0",StudentID, ps.SchoolPeriodID));

                HtmlTableCell tdSPPSTotSubject = e.Item.FindControl("tdSPPSTotSubject") as HtmlTableCell;
                HtmlTableCell tdSPPSFinish = e.Item.FindControl("tdSPPSFinish") as HtmlTableCell;
                HtmlTableCell tdSPPSStudentStatus = e.Item.FindControl("tdSPPSStudentStatus") as HtmlTableCell;
                
                if (lstSc.Count() > 0) 
                {
                    List<vClassSubject> lstTempClassSubject = lstClassSubject.Where(x => x.SchoolClassID == lstSc[0].SchoolClassID && x.SubjectGCClassStudyType == Constant.ClassStudyType.REGULAR).ToList();
                    int countSPPSFinish = 0;

                    foreach (vClassSubject obj in lstTempClassSubject) 
                    {
                        List<vClassStudentSubjectMark> lstMark = lstNilai.Where(x => x.ClassSubjectID == obj.ClassSubjectID && x.StudentID == StudentID && x.PeriodSectionID == ps.PeriodSectionID).ToList();
                        if (lstMark.Count() > 0 && Convert.ToDecimal(lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY).MarkTypeDtName) > obj.PassingGrade) countSPPSFinish++;
                    }

                    vClassStudent cs = lstClassStudent.FirstOrDefault(x => x.SchoolClassID == lstSc[0].SchoolClassID && x.StudentID == StudentID);

                    tdSPPSFinish.InnerHtml = countSPPSFinish.ToString();
                    tdSPPSTotSubject.InnerHtml = lstTempClassSubject.Count().ToString();
                    tdSPPSStudentStatus.InnerHtml = cs.ClassStudentStatus;
                }
            }
        }

        protected void rptAttendace_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                PeriodSection ps = e.Item.DataItem as PeriodSection;
                List<ClassStudentDailyAttendance> csda = BusinessLayer.GetClassStudentDailyAttendanceList(String.Format("SchoolClassID = {0} AND PeriodSectionID = {1} AND StudentID = {2}", SchoolClassID, PeriodSectionID, StudentID));
                HtmlTableCell tdSakit = (HtmlTableCell)e.Item.FindControl("tdSakit");
                HtmlTableCell tdIzin = (HtmlTableCell)e.Item.FindControl("tdIzin");
                HtmlTableCell tdAlfa = (HtmlTableCell)e.Item.FindControl("tdAlfa");
                HtmlTableCell tdJmlIzin = (HtmlTableCell)e.Item.FindControl("tdJmlIzin");
                int sick = csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.SAKIT).Count();
                int permit = csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.IZIN).Count();
                int alfa = csda.Where(x => x.GCAttendanceStatus == Constant.AttendanceStatus.ALPA).Count();
                tdSakit.InnerHtml = String.Format("{0}", sick);
                tdIzin.InnerHtml = String.Format("{0}", permit);
                tdAlfa.InnerHtml = String.Format("{0}", alfa);
                tdJmlIzin.InnerHtml = String.Format("{0}", sick + permit + alfa);
            }
        }

        protected void rptSchoolPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                SchoolPeriod sp = e.Item.DataItem as SchoolPeriod;
                rptPeriodSection.DataSource = lstPeriodSection.Where(x => x.SchoolPeriodID == sp.SchoolPeriodID);
                rptPeriodSection.DataBind();

                rptPeriodSection1.DataSource = lstPeriodSection.Where(x => x.SchoolPeriodID == sp.SchoolPeriodID);
                rptPeriodSection1.DataBind();
            }
        }

        protected void rptSchoolPeriod1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) 
            {
                SchoolPeriod sp = e.Item.DataItem as SchoolPeriod;
                HtmlTableCell tdGrade = e.Item.FindControl("tdGrade") as HtmlTableCell;
                List<SchoolClass> lstSc = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN (SELECT SchoolClassID FROM ClassStudent WHERE StudentID = {0}) AND " +
                             "PeriodClassTypeID IN (SELECT PeriodClassTypeID FROM PeriodClassType WHERE SchoolPeriodID = {1}) AND " +
                              "IsDeleted = 0", StudentID, sp.SchoolPeriodID));
                if (lstSc.Count() > 0)
                    tdGrade.InnerHtml = lstSc[0].SchoolClassName;
                else
                    tdGrade.InnerHtml = "-";
            }
        }

        protected void rptSubject_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vClassSubject entity = e.Item.DataItem as vClassSubject;
                Repeater rptSbjPerPeriod = e.Item.FindControl("rptSbjPerPeriod") as Repeater;
                SubjectID = entity.SubjectID;
                rptSbjPerPeriod.DataSource = lstPeriodSection;
                rptSbjPerPeriod.DataBind();
            }
        }

        protected void rptSbjPerPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PeriodSection ps = e.Item.DataItem as PeriodSection;
            HtmlTableCell tdPassingGrade = e.Item.FindControl("tdPassingGrade") as HtmlTableCell;
            HtmlTableCell tdTheory = e.Item.FindControl("tdTheory") as HtmlTableCell;
            HtmlTableCell tdPractice = e.Item.FindControl("tdPractice") as HtmlTableCell;
            HtmlTableCell tdAffective = e.Item.FindControl("tdAffective") as HtmlTableCell;

            List<SchoolClass> lstSc = BusinessLayer.GetSchoolClassList(String.Format("SchoolClassID IN (SELECT SchoolClassID FROM ClassStudent WHERE StudentID = {0}) AND " +
                             "PeriodClassTypeID IN (SELECT PeriodClassTypeID FROM PeriodClassType WHERE SchoolPeriodID = {1}) AND " +
                              "IsDeleted = 0",StudentID, ps.SchoolPeriodID));

            vClassSubject cs = null;
            List<vClassStudentSubjectMark> lstMark = null;
            if (lstSc.Count() > 0) 
            {
                cs = lstClassSubject.FirstOrDefault(x => x.SubjectID == SubjectID && x.SchoolClassID == lstSc[0].SchoolClassID);
                lstMark = lstNilai.Where(x => x.ClassSubjectID == cs.ClassSubjectID && x.StudentID == StudentID && x.PeriodSectionID == ps.PeriodSectionID).ToList();
            }

            if (cs != null)
                tdPassingGrade.InnerHtml = cs.PassingGrade.ToString("N");
            else
                tdPassingGrade.InnerHtml = "-";

            vClassStudentSubjectMark theoryMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.THEORY);
            if (theoryMark != null)
            {
                tdTheory.InnerHtml = theoryMark.MarkTypeDtName;
            }
            else
            {
                tdTheory.InnerHtml = "-";
            }
            vClassStudentSubjectMark practiceMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.PRACTICE);
            if (practiceMark != null)
            {
                tdPractice.InnerHtml = practiceMark.MarkTypeDtName;
            }
            else
            {
                tdPractice.InnerHtml = "-";
            }

            vClassStudentSubjectMark affectiveMark = lstMark.FirstOrDefault(p => p.GCStudentMarkGroup == Constant.StudentMarkGroup.AFFECTIVE);
            if (affectiveMark != null)
                tdAffective.InnerHtml = affectiveMark.MarkTypeDtName;
            else
                tdAffective.InnerHtml = "-";
        }
    }
}