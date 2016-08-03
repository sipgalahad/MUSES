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
    public partial class ClassSubjectDtEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnSchoolClassID.Value = temp[0];
            hdnSubjectID.Value = temp[1];
            hdnCurriculumSubjectGroupID.Value = temp[2];
            hdnPeriodClassTypeSubjectID.Value = temp[3];

            vPeriodClassTypeSubject entityHd = BusinessLayer.GetvPeriodClassTypeSubjectList(string.Format("PeriodClassTypeSubjectID = {0}", hdnPeriodClassTypeSubjectID.Value)).FirstOrDefault();
            txtSubjectName.Text = entityHd.SubjectName;
            txtNumberMeetingInHours.Text = entityHd.NoMeetingHoursInWeek.ToString();

            if (param != "")
            {
                List<vClassSubject> lstSelected = BusinessLayer.GetvClassSubjectList(string.Format("SchoolClassID = {0} AND SubjectID = {1} AND CurriculumSubjectGroupID = {2} AND IsDeleted = 0", hdnSchoolClassID.Value, hdnSubjectID.Value, hdnCurriculumSubjectGroupID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.TeacherID).ToList());
            }

            BindGridView(1, true, ref PageCount);
        }

        protected void rptSelected_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassSubject entity = (vClassSubject)e.Item.DataItem;
                DropDownList ddlAssistantTeacher = e.Item.FindControl("ddlAssistantTeacher") as DropDownList;
                Methods.SetComboBoxField<vTeacherSubject>(ddlAssistantTeacher, lstEntity, "TeacherName", "TeacherID");
                ddlAssistantTeacher.SelectedValue = entity.AssistantTeacherID.ToString();
            }
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
            string filterExpression = string.Format("SiteID = '{0}' AND SubjectID = {1} AND TeacherCode LIKE '%{2}%' AND TeacherName LIKE '%{3}%'", AppSession.UserLogin.SiteID, hdnSubjectID.Value, hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        List<vTeacherSubject> lstEntity = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvTeacherSubjectRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 14);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            lstEntity = BusinessLayer.GetvTeacherSubjectList(filterExpression, 14, pageIndex, "");
            if (lstEntity.Count > 0)
            {
                string lstTeacherID = string.Join(",", lstEntity.Select(p => p.TeacherID).ToList());
                lstTeacherClassSubject = BusinessLayer.GetvClassSubjectList(string.Format("SchoolPeriodID = {0} AND TeacherID IN ({1}) AND GCClassStudyType = '{2}' AND IsDeleted = 0", AppSession.SchoolPeriodID, lstTeacherID, Constant.ClassStudyType.REGULAR));
            }
            else
                lstTeacherClassSubject = new List<vClassSubject>();

            grdView.DataSource = lstEntity;
            grdView.DataBind();

            lstEntity.Insert(0, new vTeacherSubject { TeacherID = 0, TeacherName = "" });
            Methods.SetComboBoxField<vTeacherSubject>(ddlAssistantTeacher, lstEntity, "TeacherName", "TeacherID");
        }

        List<vClassSubject> lstTeacherClassSubject = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vTeacherSubject entity = e.Row.DataItem as vTeacherSubject;

                int slotNum = lstTeacherClassSubject.Where(p => p.TeacherID == entity.TeacherID || p.AssistantTeacherID == entity.TeacherID).Sum(p => p.NoMeetingHoursInWeek);

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
                string[] lstSelectedIsMainTeacher = hdnSelectedIsMainTeacher.Value.Split(',');
                string[] lstAssistantTeacher = hdnSelectedAssistantTeacher.Value.Split(',');
                int SchoolClassID = Convert.ToInt32(hdnSchoolClassID.Value);
                int PeriodClassTypeSubjectID = Convert.ToInt32(hdnPeriodClassTypeSubjectID.Value);

                List<ClassSubject> lstClassSubject = BusinessLayer.GetClassSubjectList(string.Format("SchoolClassID = {0} AND PeriodClassTypeSubjectID = {1} AND IsDeleted = 0", SchoolClassID, PeriodClassTypeSubjectID), ctx);
                int ct = 0;
                int parentID = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    if (lstSelectedIsMainTeacher[ct] == "1")
                    {
                        int TeacherID = Convert.ToInt32(lstSelectedMember[ct]);
                        ClassSubject entityDt = lstClassSubject.FirstOrDefault(p => p.TeacherID == TeacherID);
                        if (entityDt == null)
                        {
                            entityDt = new ClassSubject();
                            entityDt.SchoolClassID = SchoolClassID;
                            entityDt.PeriodClassTypeSubjectID = PeriodClassTypeSubjectID;
                            entityDt.TeacherID = TeacherID;
                            if (lstAssistantTeacher[ct] != "" && lstAssistantTeacher[ct] != "0")
                                entityDt.AssistantTeacherID = Convert.ToInt32(lstAssistantTeacher[ct]);
                            else
                                entityDt.AssistantTeacherID = null;
                            entityDt.ParentID = null;
                            entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                            entityDt.IsCreatedBySystem = false;
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Insert(entityDt);

                            parentID = BusinessLayer.GetClassSubjectMaxID(ctx);
                        }
                        else
                        {
                            if (lstAssistantTeacher[ct] != "" && lstAssistantTeacher[ct] != "0")
                                entityDt.AssistantTeacherID = Convert.ToInt32(lstAssistantTeacher[ct]);
                            else
                                entityDt.AssistantTeacherID = null;
                            entityDt.ParentID = null;
                            entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Update(entityDt);

                            parentID = entityDt.ClassSubjectID;

                            lstClassSubject.Remove(entityDt);
                        }
                    }
                    ct++;
                }

                ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    if (lstSelectedIsMainTeacher[ct] == "0")
                    {
                        int TeacherID = Convert.ToInt32(lstSelectedMember[ct]);
                        ClassSubject entityDt = lstClassSubject.FirstOrDefault(p => p.TeacherID == TeacherID);
                        if (entityDt == null)
                        {
                            entityDt = new ClassSubject();
                            entityDt.SchoolClassID = SchoolClassID;
                            entityDt.PeriodClassTypeSubjectID = PeriodClassTypeSubjectID;
                            entityDt.TeacherID = TeacherID;
                            if (lstAssistantTeacher[ct] != "" && lstAssistantTeacher[ct] != "0")
                                entityDt.AssistantTeacherID = Convert.ToInt32(lstAssistantTeacher[ct]);
                            else
                                entityDt.AssistantTeacherID = null;
                            entityDt.ParentID = parentID;
                            entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                            entityDt.IsCreatedBySystem = false;
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            if (lstAssistantTeacher[ct] != "" && lstAssistantTeacher[ct] != "0")
                                entityDt.AssistantTeacherID = Convert.ToInt32(lstAssistantTeacher[ct]);
                            else
                                entityDt.AssistantTeacherID = null;
                            entityDt.ParentID = parentID;
                            entityDt.NoMeetingHoursInWeek = Convert.ToInt16(lstSelectedMemberQty[ct]);
                            entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Update(entityDt);

                            lstClassSubject.Remove(entityDt);
                        }
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