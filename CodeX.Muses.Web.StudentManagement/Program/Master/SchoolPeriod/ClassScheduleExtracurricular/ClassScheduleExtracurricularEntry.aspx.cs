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
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassScheduleExtracurricularEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_CLASS_SCHEDULE_EXTRACURRICULAR;
        }
        protected override void InitializeDataControl()
        {
            BindGridView();
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        List<Variable> lstDay = null;
        List<Room> lstRoom = null;
        List<ClassSchedule> lstClassSchedule = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.EXTRACURRICULAR);
            lstDay = new List<Variable>();
            lstDay.Add(new Variable { Code = "1", Value = GetLabel("Senin") });
            lstDay.Add(new Variable { Code = "2", Value = GetLabel("Selasa") });
            lstDay.Add(new Variable { Code = "3", Value = GetLabel("Rabu") });
            lstDay.Add(new Variable { Code = "4", Value = GetLabel("Kamis") });
            lstDay.Add(new Variable { Code = "5", Value = GetLabel("Jumat") });
            lstDay.Add(new Variable { Code = "6", Value = GetLabel("Sabtu") });
            lstDay.Add(new Variable { Code = "7", Value = GetLabel("Minggu") });

            lstRoom = BusinessLayer.GetRoomList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            lstRoom.Insert(0, new Room { RoomID = 0, RoomName = "" });

            List<vClassSubject> lstEntity = BusinessLayer.GetvClassSubjectList(filterExpression);

            if (lstEntity.Count > 0)
            {
                string lstClassSubjectID = string.Join(",", lstEntity.Select(p => p.ClassSubjectID).ToList());
                lstClassSchedule = BusinessLayer.GetClassScheduleList(string.Format("ClassSubjectID IN ({0}) AND IsDeleted = 0", lstClassSubjectID));
            }
            else
                lstClassSchedule = new List<ClassSchedule>();
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubject entity = (vClassSubject)e.Row.DataItem;

                ClassSchedule classSchedule = lstClassSchedule.FirstOrDefault(p => p.ClassSubjectID == entity.ClassSubjectID);

                ASPxComboBox cboDayNumber = e.Row.FindControl("cboDayNumber") as ASPxComboBox;
                cboDayNumber.ClientInstanceName = string.Format("cboDayNumber{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField<Variable>(cboDayNumber, lstDay, "Value", "Code");
                cboDayNumber.SelectedIndex = 0;

                ASPxComboBox cboRoom = e.Row.FindControl("cboRoom") as ASPxComboBox;
                cboRoom.ClientInstanceName = string.Format("cboRoom{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField<Room>(cboRoom, lstRoom, "RoomName", "RoomCode");
                cboRoom.SelectedIndex = 0;

                if (classSchedule != null)
                {
                    TextBox txtStartTime = e.Row.FindControl("txtStartTime") as TextBox;
                    TextBox txtEndTime = e.Row.FindControl("txtEndTime") as TextBox;
                    cboDayNumber.Value = classSchedule.DayNumber.ToString();
                    cboRoom.Value = classSchedule.RoomID.ToString();
                    txtStartTime.Text = classSchedule.StartTime;
                    txtEndTime.Text = classSchedule.EndTime;
                }
                
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassScheduleDao entityDtDao = new ClassScheduleDao(ctx);
            try
            {
                string[] temp = hdnSaveValue.Value.Split('|');
                ClassSchedule entityDt = new ClassSchedule();
                entityDt.ClassSubjectID = Convert.ToInt16(temp[0]);
                entityDt.SchoolClassID = Convert.ToInt32(temp[1]);
                entityDt.HoursIndex = 0;
                entityDt.DayNumber = Convert.ToInt16(temp[2]);
                entityDt.RoomID = Convert.ToInt32(temp[3]);
                if (entityDt.RoomID == 0)
                    entityDt.RoomID = null;
                entityDt.StartTime = temp[4];
                entityDt.EndTime = temp[5];
                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDtDao.Insert(entityDt);
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