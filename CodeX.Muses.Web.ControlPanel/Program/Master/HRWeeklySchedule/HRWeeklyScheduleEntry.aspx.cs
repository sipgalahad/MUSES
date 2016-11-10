using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class HRWeeklyScheduleEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.HR_WEEKLY_SCHEDULE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                HRWeeklySchedule entity = BusinessLayer.GetHRWeeklySchedule(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.HR_WEEKLY_SCHEDULE);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<HRDailyScheduleHd> lstDs = BusinessLayer.GetHRDailyScheduleHdList(string.Format("IsDeleted = 0"));
            lstDs.Insert(0, new HRDailyScheduleHd { DailyScheduleID = 0, DailyScheduleName = "" });
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD1, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD2, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD3, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD4, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD5, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD6, lstDs, "DailyScheduleName", "DailyScheduleID");
            Methods.SetComboBoxField<HRDailyScheduleHd>(cboWeeklyScheduleD7, lstDs, "DailyScheduleName", "DailyScheduleID");
        }


        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtWeeklyScheduleName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboWeeklyScheduleD1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD3, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD4, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD5, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD6, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboWeeklyScheduleD7, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(HRWeeklySchedule entity)
        {
            ctlEntityCode.SetText(entity.WeeklyScheduleCode);
            txtWeeklyScheduleName.Text = entity.WeeklyScheduleName;
            cboWeeklyScheduleD1.Value = entity.DailyScheduleID1.ToString();
            cboWeeklyScheduleD2.Value = entity.DailyScheduleID2.ToString();
            cboWeeklyScheduleD3.Value = entity.DailyScheduleID3.ToString();
            cboWeeklyScheduleD4.Value = entity.DailyScheduleID4.ToString();
            cboWeeklyScheduleD5.Value = entity.DailyScheduleID5.ToString();
            cboWeeklyScheduleD6.Value = entity.DailyScheduleID6.ToString();
            cboWeeklyScheduleD7.Value = entity.DailyScheduleID7.ToString();
        }

        private void ControlToEntity(HRWeeklySchedule entity, IDbContext ctx)
        {
            entity.WeeklyScheduleName = txtWeeklyScheduleName.Text;
            if (cboWeeklyScheduleD1.Value != null && cboWeeklyScheduleD1.Value.ToString() != "0")
                entity.DailyScheduleID1 = Convert.ToInt32(cboWeeklyScheduleD1.Value);
            else
                entity.DailyScheduleID1 = null;

            if (cboWeeklyScheduleD2.Value != null && cboWeeklyScheduleD2.Value.ToString() != "0")
                entity.DailyScheduleID2 = Convert.ToInt32(cboWeeklyScheduleD2.Value);
            else
                entity.DailyScheduleID2 = null;

            if (cboWeeklyScheduleD3.Value != null && cboWeeklyScheduleD3.Value.ToString() != "0")
                entity.DailyScheduleID3 = Convert.ToInt32(cboWeeklyScheduleD3.Value);
            else
                entity.DailyScheduleID3 = null;

            if (cboWeeklyScheduleD4.Value != null && cboWeeklyScheduleD4.Value.ToString() != "0")
                entity.DailyScheduleID4 = Convert.ToInt32(cboWeeklyScheduleD4.Value);
            else
                entity.DailyScheduleID4 = null;

            if (cboWeeklyScheduleD5.Value != null && cboWeeklyScheduleD5.Value.ToString() != "0")
                entity.DailyScheduleID5 = Convert.ToInt32(cboWeeklyScheduleD5.Value);
            else
                entity.DailyScheduleID5 = null;

            if (cboWeeklyScheduleD6.Value != null && cboWeeklyScheduleD6.Value.ToString() != "0")
                entity.DailyScheduleID6 = Convert.ToInt32(cboWeeklyScheduleD6.Value);
            else
                entity.DailyScheduleID6 = null;

            if (cboWeeklyScheduleD7.Value != null && cboWeeklyScheduleD7.Value.ToString() != "0")
                entity.DailyScheduleID7 = Convert.ToInt32(cboWeeklyScheduleD7.Value);
            else
                entity.DailyScheduleID7 = null;
            entity.WeeklyScheduleCode = ctlEntityCode.GetCode(entity.WeeklyScheduleName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            HRWeeklyScheduleDao entityDao = new HRWeeklyScheduleDao(ctx);
            bool result = false;
            try
            {
                HRWeeklySchedule entity = new HRWeeklySchedule();
                ControlToEntity(entity, ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            HRWeeklyScheduleDao entityDao = new HRWeeklyScheduleDao(ctx);
            bool result = false;
            try
            {
                HRWeeklySchedule entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity, ctx);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}