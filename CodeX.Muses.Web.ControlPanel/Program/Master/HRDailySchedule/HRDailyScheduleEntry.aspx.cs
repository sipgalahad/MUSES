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
    public partial class HRDailyScheduleEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.HR_DAILY_SCHEDULE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                HRDailyScheduleHd entity = BusinessLayer.GetHRDailyScheduleHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.HR_DAILY_SCHEDULE);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtDailyScheduleName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtNoOfWorkHours, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFromHour, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartGraceTimeArrive, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndGraceTimeArrive, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtToHour, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartGraceTimeDepart, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndGraceTimeDepart, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(HRDailyScheduleHd entity)
        {
            ctlEntityCode.SetText(entity.DailyScheduleCode);
            txtDailyScheduleName.Text = entity.DailyScheduleName;
            txtNoOfWorkHours.Text = entity.NoOfWorkHours.ToString();
            txtRemarks.Text = entity.Remarks;
            txtFromHour.Text = entity.FromHour;
            txtStartGraceTimeArrive.Text = entity.StartGraceTimeArrive;
            txtEndGraceTimeArrive.Text = entity.EndGraceTimeArrive;
            txtToHour.Text = entity.ToHour;
            txtStartGraceTimeDepart.Text = entity.StartGraceTimeDepart;
            txtEndGraceTimeDepart.Text = entity.ToGraceTimeDepart;

        }

        private void ControlToEntity(HRDailyScheduleHd entity, IDbContext ctx)
        {
            entity.DailyScheduleName = txtDailyScheduleName.Text;
            entity.NoOfWorkHours= Convert.ToInt16(txtNoOfWorkHours.Text);
            entity.Remarks = txtRemarks.Text;   
            entity.FromHour = txtFromHour.Text;
            entity.StartGraceTimeArrive = txtStartGraceTimeArrive.Text;
            entity.EndGraceTimeArrive = txtEndGraceTimeArrive.Text;
            entity.ToHour = txtToHour.Text;
            entity.StartGraceTimeDepart = txtStartGraceTimeDepart.Text;
            entity.ToGraceTimeDepart = txtStartGraceTimeDepart.Text;
            entity.DailyScheduleCode = ctlEntityCode.GetCode(entity.DailyScheduleName, ctx);            
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            HRDailyScheduleHdDao entityDao = new HRDailyScheduleHdDao(ctx);
            bool result = false;
            try
            {
                HRDailyScheduleHd entity = new HRDailyScheduleHd();
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
            HRDailyScheduleHdDao entityDao = new HRDailyScheduleHdDao(ctx);
            bool result = false;
            try
            {
                HRDailyScheduleHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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