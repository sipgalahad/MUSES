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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class PeriodScheduleEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_SCHEDULE;
        }

        protected string GetInternalUjianValue()
        {
            return Constant.PeriodScheduleType.INTERNAL_EXAM;
        }
        protected override void InitializeDataControl()
        {
            Repeater rptClassType = (Repeater)ddeClassType.FindControl("rptClassType");
            List<vPeriodClassType> lstClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.REGULAR));
            rptClassType.DataSource = lstClassType;
            rptClassType.DataBind();

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_PERIOD_SCHEDULE_TYPE));
            List<StandardCode> lstScheduleType = lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_PERIOD_SCHEDULE_TYPE).ToList();
            rptRemarks.DataSource = lstScheduleType;
            rptRemarks.DataBind();

            rptDateStyle.DataSource = lstScheduleType;
            rptDateStyle.DataBind();

            SchoolPeriod schoolPeriod = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID);
            hdnMaxDate.Value = schoolPeriod.EndDate.ToString("yyyy-MM-dd");
            hdnMinDate.Value = schoolPeriod.StartDate.ToString("yyyy-MM-dd");
            hdnYear.Value = DateTime.Now.Year.ToString();
            hdnMonth.Value = DateTime.Now.Month.ToString();


            Methods.SetComboBoxField<StandardCode>(cboScheduleType, lstScheduleType.Where(p => p.StandardCodeID != Constant.PeriodScheduleType.KBM).ToList(), "StandardCodeName", "StandardCodeID");

            List<vCurriculumMarkTypeDt> lstCurriculumMarkTypeDt = BusinessLayer.GetvCurriculumMarkTypeDtList(string.Format("CurriculumID = {0} AND IsExam = 1 AND IsDeleted = 0", schoolPeriod.CurriculumID));
            Methods.SetComboBoxField<vCurriculumMarkTypeDt>(cboCurriculumMarkTypeDt, lstCurriculumMarkTypeDt, "cfCurriculumMarkTypeDtName", "CurriculumMarkTypeDtID");

            IsAutoReInitControl = false;
            //chkShowAll.Checked = false;

            BindGridView();

            Helper.SetControlEntrySetting(txtPeriodScheduleCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPeriodScheduleName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(cboScheduleType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCurriculumMarkTypeDt, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptClassType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vPeriodClassType obj = (vPeriodClassType)e.Item.DataItem;
                CheckBox chkClassType = (CheckBox)e.Item.FindControl("chkClassType");
                chkClassType.Attributes.Add("classtypename", obj.CurriculumClassTypeName);
                chkClassType.Attributes.Add("classtypeid", obj.PeriodClassTypeID.ToString());
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID);
            if(!chkShowAll.Checked)
            {
                Int32 Month = Convert.ToInt32(hdnMonth.Value);
                filterExpression += String.Format(" AND (StartDate LIKE '{0}-{1}%' OR EndDate LIKE '{0}-{1}%')", hdnYear.Value, Month.ToString("00"));
            }
            List<vPeriodSchedule> lstEntity = BusinessLayer.GetvPeriodScheduleList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(PeriodSchedule entity)
        {
            entity.PeriodScheduleCode = txtPeriodScheduleCode.Text;
            entity.PeriodScheduleName = txtPeriodScheduleName.Text;
            entity.GCPeriodScheduleType = cboScheduleType.Value.ToString();
            if (entity.GCPeriodScheduleType == Constant.PeriodScheduleType.INTERNAL_EXAM)
                entity.CurriculumMarkTypeDtID = Convert.ToInt32(cboCurriculumMarkTypeDt.Value);
            else
                entity.CurriculumMarkTypeDtID = null;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate.Text);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate.Text);
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PeriodScheduleDao entityDao = new PeriodScheduleDao(ctx);
            PeriodScheduleClassTypeDao entityClassTypeDao = new PeriodScheduleClassTypeDao(ctx);
            try
            {
                PeriodSchedule entity = new PeriodSchedule();
                ControlToEntity(entity);
                entity.SchoolPeriodID = AppSession.SchoolPeriodID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.PeriodScheduleID = BusinessLayer.GetPeriodScheduleMaxID(ctx);

                if (hdnLstClassTypeID.Value != "")
                {
                    string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                    foreach (string classTypeID in lstClassTypeID)
                    {
                        PeriodScheduleClassType entityClassType = new PeriodScheduleClassType();
                        entityClassType.PeriodScheduleID = entity.PeriodScheduleID;
                        entityClassType.PeriodClassTypeID = Convert.ToInt32(classTypeID);
                        entityClassTypeDao.Insert(entityClassType);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PeriodScheduleDao entityDao = new PeriodScheduleDao(ctx);
            PeriodScheduleClassTypeDao entityClassTypeDao = new PeriodScheduleClassTypeDao(ctx);
            try
            {
                PeriodSchedule entity = BusinessLayer.GetPeriodSchedule(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PeriodScheduleClassType> lstEntityClassType = BusinessLayer.GetPeriodScheduleClassTypeList(string.Format("PeriodScheduleID = {0}", entity.PeriodScheduleID), ctx);
                if (hdnLstClassTypeID.Value != "")
                {
                    string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                    foreach (string classTypeID in lstClassTypeID)
                    {
                        PeriodScheduleClassType entityClassType = lstEntityClassType.FirstOrDefault(p => p.PeriodClassTypeID.ToString() == classTypeID);
                        if (entityClassType == null)
                        {
                            entityClassType = new PeriodScheduleClassType();
                            entityClassType.PeriodScheduleID = entity.PeriodScheduleID;
                            entityClassType.PeriodClassTypeID = Convert.ToInt32(classTypeID);
                            entityClassTypeDao.Insert(entityClassType);
                        }
                        else
                            lstEntityClassType.Remove(entityClassType);
                    }
                }
                foreach (PeriodScheduleClassType entityClassType in lstEntityClassType)
                {
                    entityClassTypeDao.Delete(entityClassType.PeriodScheduleID, entityClassType.PeriodClassTypeID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                PeriodSchedule entity = BusinessLayer.GetPeriodSchedule(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePeriodSchedule(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}