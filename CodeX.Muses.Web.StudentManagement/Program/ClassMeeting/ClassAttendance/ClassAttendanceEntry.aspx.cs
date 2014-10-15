using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassAttendanceEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.WS_CLASS_ATTENDANCE;
        }

        List<StandardCode> lstAttendanceStatus = null;
        protected override void InitializeDataControl()
        {
            lstAttendanceStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.STUDENT_ATTENDANCE));
            rptHeader.DataSource = lstAttendanceStatus;
            rptHeader.DataBind();

            lstClassMeetingAttendance = BusinessLayer.GetClassMeetingAttendanceList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID));

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", classSubject.SchoolClassID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<ClassMeetingAttendance> lstClassMeetingAttendance = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                HtmlInputHidden hdnAttendance = (HtmlInputHidden)e.Item.FindControl("hdnAttendance");

                ClassMeetingAttendance attendance = lstClassMeetingAttendance.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (attendance != null)
                    hdnAttendance.Value = attendance.GCAttendanceStatus;

                Repeater rptStudentAttendance = (Repeater)e.Item.FindControl("rptStudentAttendance");
                rptStudentAttendance.DataSource = lstAttendanceStatus;
                rptStudentAttendance.DataBind();
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassMeetingAttendanceDao entityDtDao = new ClassMeetingAttendanceDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');

                List<ClassMeetingAttendance> lstClassMeetingAttendance = BusinessLayer.GetClassMeetingAttendanceList(string.Format("ClassMeetingID = {0}", AppSession.ClassSubject.ClassMeetingID), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    string GCAttendanceStatus = temp[1];
                    if (GCAttendanceStatus != "")
                    {
                        ClassMeetingAttendance entityDt = lstClassMeetingAttendance.FirstOrDefault(p => p.StudentID == studentID);
                        if (entityDt == null)
                        {
                            entityDt = new ClassMeetingAttendance();
                            entityDt.ClassMeetingID = AppSession.ClassSubject.ClassMeetingID;
                            entityDt.StudentID = studentID;
                            entityDt.GCAttendanceStatus = GCAttendanceStatus;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            entityDt.GCAttendanceStatus = GCAttendanceStatus;
                            entityDtDao.Update(entityDt);
                        }
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
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