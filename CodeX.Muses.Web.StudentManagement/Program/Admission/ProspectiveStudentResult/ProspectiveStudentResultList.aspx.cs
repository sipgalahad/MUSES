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
    public partial class ProspectiveStudentResultList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_PROSPECTIVE_STUDENT_RESULT;
        }
        List<AdmissionSelection> lstAdmissionSelection = null;
        protected override void InitializeDataControl()
        {
            lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
            rptHeader.DataSource = lstAdmissionSelection;
            rptHeader.DataBind();

            lstStudentMark = BusinessLayer.GetProspectiveStudentMarkList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID));

            List<vProspectiveStudent> lstStudent = BusinessLayer.GetvProspectiveStudentList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<ProspectiveStudentMark> lstStudentMark = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstAdmissionSelection;
                rptStudentMark.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                AdmissionSelection admissionSelection = (AdmissionSelection)e.Item.DataItem;
                vProspectiveStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vProspectiveStudent;

                ProspectiveStudentMark entity = lstStudentMark.FirstOrDefault(p => p.AdmissionSelectionID == admissionSelection.AdmissionSelectionID && p.ProspectiveStudentID == student.ProspectiveStudentID);
                if (entity != null)
                {
                    HtmlGenericControl divStudentMark = (HtmlGenericControl)e.Item.FindControl("divStudentMark");
                    divStudentMark.InnerHtml = entity.Mark.ToString();
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }
    }
}