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
    public partial class ClassStudentNoteEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CS_CLASS_STUDENT_NOTE;
        }

        protected override void InitializeDataControl()
        {
            ClassStudentMark entityMark = BusinessLayer.GetClassStudentMark(AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.PeriodSectionID, AppSession.ClassStudent.StudentID);
            if (entityMark != null)
                txtRemarks.Text = entityMark.Remarks;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage, ref string retval)
        {
            try
            {
                ClassStudentMark entityMark = BusinessLayer.GetClassStudentMark(AppSession.ClassStudent.SchoolClassID, AppSession.ClassStudent.PeriodSectionID, AppSession.ClassStudent.StudentID);
                if (entityMark != null)
                {
                    entityMark.Remarks = txtRemarks.Text;
                    BusinessLayer.UpdateClassStudentMark(entityMark);
                }
                else
                {
                    entityMark = new ClassStudentMark();
                    entityMark.SchoolClassID = AppSession.ClassStudent.SchoolClassID;
                    entityMark.PeriodSectionID = AppSession.ClassStudent.PeriodSectionID;
                    entityMark.StudentID = AppSession.ClassStudent.StudentID;
                    entityMark.Remarks = txtRemarks.Text;
                    BusinessLayer.InsertClassStudentMark(entityMark);
                }
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
    }
}