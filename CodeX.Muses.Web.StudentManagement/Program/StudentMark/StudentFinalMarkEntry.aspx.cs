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
using System.IO;
using System.Text.RegularExpressions;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class StudentFinalMarkEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.STUDENT_FINAL_MARK;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
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

            if (cboSchoolPeriod.Value != "")
            {
                List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("SchoolPeriodID = {0} AND '{1}' BETWEEN StartDate AND EndDate", cboSchoolPeriod.Value, DateTime.Now.ToString("yyyyMMdd")));
                if (lstPeriodSection.Count > 0)
                {
                    PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                    tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                    tacPeriodSection.Text = periodSection.PeriodSectionName;
                }
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

        List<ClassStudentMark> lstStudentMark = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (tacSchoolClass.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value));
            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassStudent entity = (vClassStudent)e.Row.DataItem;
                ClassStudentMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    HtmlGenericControl lblFinalMark = (HtmlGenericControl)e.Row.FindControl("lblFinalMark");
                    lblFinalMark.InnerHtml = studentMark.FinalMark.ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "upload")
            {
                string imageData = hdnUploadedFile1.Value;
                if (imageData != "")
                {
                    string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                    imageData = String.Join(",", parts);
                }

                byte[] data = Convert.FromBase64String(imageData);
                StreamReader stream = new StreamReader(new MemoryStream(data));

                if (OnUploadFile(stream, ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        public bool OnUploadFile(StreamReader stream, ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentMarkDao classStudentMarkDao = new ClassStudentMarkDao(ctx);
            ClassStudentSubjectMarkDao classStudentSubjectMarkDao = new ClassStudentSubjectMarkDao(ctx);
            ClassStudentAttendanceDao classStudentAttendanceDao = new ClassStudentAttendanceDao(ctx);
            try
            {
                int ctr = 0;
                List<Student> lstStudent = BusinessLayer.GetStudentList(string.Format("SchoolClassID = {0}", tacSchoolClass.Value), ctx);
                List<ClassStudentMark> lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value), ctx);
                List<vClassSubject> lstClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectGCClassStudyType = '{1}'", tacSchoolClass.Value, Constant.ClassStudyType.PERSONALITY), ctx);
                List<ClassStudentAttendance> lstClassStudentAttendance = BusinessLayer.GetClassStudentAttendanceList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", tacSchoolClass.Value, tacPeriodSection.Value), ctx);
                List<ClassStudentSubjectMark> lstClassStudentSubjectMark = null;
                if (lstClassSubject.Count > 0)
                {
                    string lstClassSubjectID = string.Join(",", lstClassSubject.Select(p => p.ClassSubjectID).ToList());
                    lstClassStudentSubjectMark = BusinessLayer.GetClassStudentSubjectMarkList(string.Format("ClassSubjectID IN ({0})", lstClassSubjectID), ctx);
                }
                else
                    lstClassStudentSubjectMark = new List<ClassStudentSubjectMark>();
                while (true)
                {
                    string line = stream.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (ctr > 3)
                    {
                        var regex = new Regex("\"([^\"]*)\"");
                        foreach (Match match in regex.Matches(line))
                        {
                            string temp = match.ToString();
                            string temp2 = temp.Replace(",", "").Replace("\"", "");
                            line = line.Replace(temp, temp2);
                        }

                        CStudentFinalMarkUpload entity = new CStudentFinalMarkUpload();
                        String[] lstTemp = line.Split(',');
                        entity.StudentCode = lstTemp[2].Split('/')[0].Trim();
                        entity.Notes = lstTemp[92];
                        Student student = lstStudent.FirstOrDefault(p => p.StudentCode == entity.StudentCode);
                        if (student != null)
                        {
                            for (int i = 0; i < 3; ++i)
                            {
                                string GCAttendanceStatus = "";
                                switch (i)
                                {
                                    case 0: GCAttendanceStatus = Constant.AttendanceStatus.SAKIT; break;
                                    case 1: GCAttendanceStatus = Constant.AttendanceStatus.IZIN; break;
                                    case 2: GCAttendanceStatus = Constant.AttendanceStatus.ALPA; break;
                                }
                                ClassStudentAttendance classStudentAttendance = lstClassStudentAttendance.FirstOrDefault(p => p.StudentID == student.StudentID && p.GCAttendanceStatus == GCAttendanceStatus);
                                if (classStudentAttendance == null)
                                {

                                    classStudentAttendance = new ClassStudentAttendance();
                                    classStudentAttendance.SchoolClassID = Convert.ToInt32(tacSchoolClass.Value);
                                    classStudentAttendance.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
                                    classStudentAttendance.StudentID = student.StudentID;
                                    classStudentAttendance.GCAttendanceStatus = GCAttendanceStatus;
                                    classStudentAttendance.TotalAttendanceStatus = 0;
                                    if (lstTemp[89 + i] != "" && lstTemp[89 + i] != "-")
                                        classStudentAttendance.TotalAttendanceStatus = Convert.ToInt16(lstTemp[89 + i]);
                                    classStudentAttendanceDao.Insert(classStudentAttendance);
                                }
                                else
                                {
                                    if (lstTemp[89 + i] != "" && lstTemp[89 + i] != "-")
                                        classStudentAttendance.TotalAttendanceStatus = Convert.ToInt16(lstTemp[89 + i]);
                                    classStudentAttendanceDao.Update(classStudentAttendance);
                                }
                            }

                            for (int i = 0; i < 10; ++i)
                            {
                                vClassSubject classSubject = lstClassSubject[i];
                                ClassStudentSubjectMark classStudentSubjectMark = lstClassStudentSubjectMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectID == classSubject.ClassSubjectID);
                                if (classStudentSubjectMark != null)
                                {
                                    classStudentSubjectMark.DescriptionMark = lstTemp[79 + i];
                                    classStudentSubjectMarkDao.Update(classStudentSubjectMark);
                                }
                                else
                                {
                                    classStudentSubjectMark = new ClassStudentSubjectMark();
                                    classStudentSubjectMark.ClassSubjectID = classSubject.ClassSubjectID;
                                    classStudentSubjectMark.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
                                    classStudentSubjectMark.StudentID = student.StudentID;
                                    classStudentSubjectMark.DescriptionMark = lstTemp[79 + i];
                                    classStudentSubjectMark.CurriculumMarkTypeID = 3;
                                    classStudentSubjectMarkDao.Insert(classStudentSubjectMark);
                                }
                            }
                            ClassStudentMark entityMark = lstStudentMark.FirstOrDefault(p => p.StudentID == student.StudentID);
                            if (entityMark != null)
                            {
                                entityMark.Remarks = entity.Notes;
                                classStudentMarkDao.Update(entityMark);
                            }
                            else
                            {
                                entityMark = new ClassStudentMark();
                                entityMark.SchoolClassID = Convert.ToInt32(tacSchoolClass.Value);
                                entityMark.PeriodSectionID = Convert.ToInt32(tacPeriodSection.Value);
                                entityMark.StudentID = student.StudentID;
                                entityMark.Remarks = entity.Notes;
                                classStudentMarkDao.Insert(entityMark);
                            }
                        }
                    }
                    ctr++;
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }

        class CStudentFinalMarkUpload
        {
            public String StudentCode { get; set; } 
            public List<String> LstMarkAffective { get; set; }
            public String Notes { get; set; }
            public CStudentFinalMarkUpload()
            {
                LstMarkAffective = new List<string>();
            }
        }
    }
}