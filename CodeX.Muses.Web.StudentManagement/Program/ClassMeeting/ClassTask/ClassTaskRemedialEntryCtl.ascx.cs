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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassTaskRemedialEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetDateNow()
        {
            return DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }
        protected string OnGetTimeNow()
        {
            return DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT);
        }

        List<ClassSubjectTaskRemedial> lstRemedial = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnID.Value = temp[0];
            hdnPassingGrade.Value = txtPassingGrade.Text = temp[1];
            ClassSubjectTask entity = BusinessLayer.GetClassSubjectTask(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ClassTaskCode, entity.Topic);

            ClassSubject classSubject = BusinessLayer.GetClassSubject(AppSession.ClassSubject.ClassSubjectID);
            hdnSchoolClassID.Value = classSubject.SchoolClassID.ToString();

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtTaskDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.TIME_NOW), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, false), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrxPopup");     
        }

        List<ClassStudentSubjectTaskMark> lstStudentMark = null;
        List<ClassStudentSubjectTaskRemedialMark> lstStudentRemedialMark = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("ClassSubjectTaskID = {0}", hdnID.Value);
            lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(filterExpression);
            lstRemedial = BusinessLayer.GetClassSubjectTaskRemedialList(string.Format("ClassSubjectTaskID = {0} AND IsDeleted = 0 ORDER BY DisplayOrder ASC", hdnID.Value));

            string lstRemedialID = string.Join(",", lstRemedial.Select(p => p.ClassSubjectTaskRemedialID).ToList());
            if (lstRemedialID != "")
                lstStudentRemedialMark = BusinessLayer.GetClassStudentSubjectTaskRemedialMarkList(string.Format("ClassSubjectTaskRemedialID IN ({0})", lstRemedialID));
            else
                lstStudentRemedialMark = new List<ClassStudentSubjectTaskRemedialMark>();

            rptHeader.DataSource = lstRemedial;
            rptHeader.DataBind();

            filterExpression = string.Format("SchoolClassID = {0}", hdnSchoolClassID.Value);
            if (chkFilterUnderPassingGrade.Checked)
                filterExpression += string.Format(" AND StudentID IN (SELECT StudentID FROM ClassStudentSubjectTaskMark WHERE ClassSubjectTaskID = {0} AND OriginalMark < {1})", hdnID.Value, hdnPassingGrade.Value);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(filterExpression);
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent student = (vClassStudent)e.Item.DataItem;
                Repeater rptClassSubjectRemedial = (Repeater)e.Item.FindControl("rptClassSubjectRemedial");
                rptClassSubjectRemedial.DataSource = lstRemedial;
                rptClassSubjectRemedial.DataBind();

                ClassStudentSubjectTaskMark entity = lstStudentMark.FirstOrDefault(p => p.StudentID == student.StudentID);
                if (entity != null)
                {
                    TextBox txtOriginalMark = (TextBox)e.Item.FindControl("txtOriginalMark");
                    txtOriginalMark.Text = entity.OriginalMark.ToString();
                    TextBox txtFinalMark = (TextBox)e.Item.FindControl("txtFinalMark");
                    txtFinalMark.Text = entity.Mark.ToString();
                }
            }
        }

        protected void rptClassSubjectRemedial_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ClassSubjectTaskRemedial entityRemedial = (ClassSubjectTaskRemedial)e.Item.DataItem;
                vClassStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vClassStudent;

                TextBox txtMark = (TextBox)e.Item.FindControl("txtMark");
                txtMark.Attributes.Add("ClassSubjectTaskRemedialID", entityRemedial.ClassSubjectTaskRemedialID.ToString());

                ClassStudentSubjectTaskRemedialMark entity = lstStudentRemedialMark.FirstOrDefault(p => p.StudentID == student.StudentID && p.ClassSubjectTaskRemedialID == entityRemedial.ClassSubjectTaskRemedialID);
                if (entity != null)
                    txtMark.Text = entity.Mark.ToString();
            }
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "savemark")
            {
                if (OnSaveRemedialMark(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(ClassSubjectTaskRemedial entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.TaskDate = Helper.GetDatePickerValue(txtTaskDate.Text);
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.StartTime = txtStartTime.Text;
            entity.EndTime = txtEndTime.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            try
            {
                ClassSubjectTaskRemedial entity = new ClassSubjectTaskRemedial();
                ControlToEntity(entity);
                entity.ClassSubjectTaskID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertClassSubjectTaskRemedial(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            try
            {
                ClassSubjectTaskRemedial entity = BusinessLayer.GetClassSubjectTaskRemedial(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateClassSubjectTaskRemedial(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ClassSubjectTaskRemedial entity = BusinessLayer.GetClassSubjectTaskRemedial(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateClassSubjectTaskRemedial(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveRemedialMark(ref string errMessage)
        {

            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentSubjectTaskMarkDao entityMarkDao = new ClassStudentSubjectTaskMarkDao(ctx);
            ClassStudentSubjectTaskRemedialMarkDao entityRemedialMarkDao = new ClassStudentSubjectTaskRemedialMarkDao(ctx);
            try
            {
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                List<ClassStudentSubjectTaskMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID = {0} AND StudentID IN ({1})", hdnID.Value, hdnListStudentID.Value), ctx);
                List<ClassStudentSubjectTaskRemedialMark> lstStudentRemedialMark = BusinessLayer.GetClassStudentSubjectTaskRemedialMarkList(string.Format("ClassSubjectTaskRemedialID IN ({0}) AND StudentID IN ({1})", hdnListRemedialID.Value, hdnListStudentID.Value), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int studentID = Convert.ToInt32(temp[0]);
                    ClassStudentSubjectTaskMark entityDt = lstStudentMark.FirstOrDefault(p => p.StudentID == studentID);
                    if (temp[1] != "" || temp[2] != "")
                    {
                        entityDt.IsRemedial = true;
                        if (temp[1] != "")
                            entityDt.OriginalMark = Convert.ToDecimal(temp[1]);
                        if (temp[2] != "")
                            entityDt.Mark = Convert.ToDecimal(temp[2]);
                        entityMarkDao.Update(entityDt);
                    }
                    string[] lstSaveValue1 = temp[3].Split('^');
                    foreach (String saveValue1 in lstSaveValue1)
                    {
                        string[] temp1 = saveValue1.Split(',');
                        int ClassSubjectTaskRemedialID = Convert.ToInt32(temp1[0]);
                        decimal mark = Convert.ToDecimal(temp1[1]);
                        ClassStudentSubjectTaskRemedialMark entityRemedialMark = lstStudentRemedialMark.FirstOrDefault(p => p.StudentID == studentID && p.ClassSubjectTaskRemedialID == ClassSubjectTaskRemedialID);
                        if (entityRemedialMark != null)
                        {
                            entityRemedialMark.Mark = mark;
                            entityRemedialMarkDao.Update(entityRemedialMark);
                            lstStudentRemedialMark.Remove(entityRemedialMark);
                        }
                        else
                        {
                            entityRemedialMark = new ClassStudentSubjectTaskRemedialMark();
                            entityRemedialMark.StudentID = studentID;
                            entityRemedialMark.ClassSubjectTaskRemedialID = ClassSubjectTaskRemedialID;
                            entityRemedialMark.Mark = mark;
                            entityRemedialMarkDao.Insert(entityRemedialMark);
                        }
                    }
                }
                foreach (ClassStudentSubjectTaskRemedialMark entityRemedialMark in lstStudentRemedialMark)
                {
                    entityRemedialMarkDao.Delete(entityRemedialMark.ClassSubjectTaskRemedialID, entityRemedialMark.StudentID);
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
        #endregion
    }
}