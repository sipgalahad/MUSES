using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ProspectiveStudentFormSettingCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnSchoolClassID.Value = temp[0];
            hdnSubjectID.Value = temp[1];
            hdnPeriodClassTypeSubjectID.Value = temp[2];

            vPeriodClassTypeSubject entityHd = BusinessLayer.GetvPeriodClassTypeSubjectList(string.Format("PeriodClassTypeSubjectID = {0}", hdnPeriodClassTypeSubjectID.Value)).FirstOrDefault();
            txtSubjectName.Text = entityHd.SubjectName;
            txtNumberMeetingInHours.Text = entityHd.NoMeetingHoursInWeek.ToString();

            if (param != "")
            {
                List<vClassSubject> lstSelected = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectID = {1} AND IsDeleted = 0", hdnSchoolClassID.Value, hdnSubjectID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.TeacherID).ToList());
            }

            BindGridView(1, true, ref PageCount);
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("SubjectID = {0} AND TeacherCode LIKE '%{1}%' AND TeacherName LIKE '%{2}%'", hdnSubjectID.Value, hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvTeacherSubjectRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 14);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vTeacherSubject> lstEntity = BusinessLayer.GetvTeacherSubjectList(filterExpression, 14, pageIndex, "");
            if (lstEntity.Count > 0)
            {
                string lstTeacherID = string.Join(",", lstEntity.Select(p => p.TeacherID).ToList());
                lstTeacherClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("TeacherID IN ({0}) AND IsDeleted = 0", lstTeacherID));
            }
            else
                lstTeacherClassSubject = new List<vClassSubject>();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<vClassSubject> lstTeacherClassSubject = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vTeacherSubject entity = e.Row.DataItem as vTeacherSubject;

                int slotNum = lstTeacherClassSubject.Where(p => p.TeacherID == entity.TeacherID).Sum(p => p.NoMeetingHoursInWeek);
                
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                HtmlGenericControl divSlotNum = e.Row.FindControl("divSlotNum") as HtmlGenericControl;
                divSlotNum.InnerHtml = slotNum.ToString();
                if (lstSelectedMember.Contains(entity.TeacherID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassSubjectDao entityDtDao = new ClassSubjectDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                string[] lstSelectedMemberQty = hdnSelectedMemberQty.Value.Split(',');
                int SchoolClassID = Convert.ToInt32(hdnSchoolClassID.Value);
                int PeriodClassTypeSubjectID = Convert.ToInt32(hdnPeriodClassTypeSubjectID.Value);

                List<ClassSubject> lstClassSubject = BusinessLayer.GetClassSubjectList(string.Format("SchoolClassID = {0} AND PeriodClassTypeSubjectID = {1} AND IsDeleted = 0", SchoolClassID, PeriodClassTypeSubjectID), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    int TeacherID = Convert.ToInt32(lstSelectedMember[ct]);
                    ClassSubject entityDt = lstClassSubject.FirstOrDefault(p => p.TeacherID == TeacherID);
                    if (entityDt == null)
                    {
                        entityDt = new ClassSubject();
                        entityDt.SchoolClassID = SchoolClassID;
                        entityDt.PeriodClassTypeSubjectID = PeriodClassTypeSubjectID;
                        entityDt.TeacherID = TeacherID;
                        entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                        entityDt.IsCreatedBySystem = false;
                        entityDt.CreatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                        entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDtDao.Update(entityDt);

                        lstClassSubject.Remove(entityDt);
                    }
                    ct++;
                }
                foreach (ClassSubject entityDt in lstClassSubject)
                {
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
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