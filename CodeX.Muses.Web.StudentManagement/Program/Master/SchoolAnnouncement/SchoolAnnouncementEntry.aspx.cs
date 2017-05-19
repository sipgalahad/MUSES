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

namespace CodeX.Ottimo.Web.ControlPanel.Program
{
    public partial class SchoolAnnouncementEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SCHOOL_ANNOUNCEMENT;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                SchoolAnnouncement entity = BusinessLayer.GetSchoolAnnouncement(Convert.ToInt32(ID));
                hdnSiteID.Value = entity.SiteID;
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                hdnSiteID.Value = param[1];
                SetControlProperties();
                IsAdd = true;
            }
            txtTitle.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.ANNOUCEMENT_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboAnnouncementType, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTitle, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboAnnouncementType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true, Constant.DefaultValueEntry.TIME_NOW));
        }

        private void EntityToControl(SchoolAnnouncement entity)
        {
            txtTitle.Text = entity.Title;
            txtTemplateContent.Text = entity.Remarks;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = entity.StartTime;
            cboAnnouncementType.Value = entity.GCAnnouncementType;
        }

        private void ControlToEntity(SchoolAnnouncement entity)
        {
            entity.Title = txtTitle.Text;
            entity.Remarks = Helper.GetHTMLEditorText(txtTemplateContent);
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate);
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate);
            entity.StartTime = txtStartTime.Text;
            entity.GCAnnouncementType = cboAnnouncementType.Value.ToString();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolAnnouncementDao entityDao = new SchoolAnnouncementDao(ctx);
            bool result = true;
            try
            {
                SchoolAnnouncement entity = new SchoolAnnouncement();
                ControlToEntity(entity);
                entity.SiteID = hdnSiteID.Value;
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
            try
            {
                SchoolAnnouncement entity = BusinessLayer.GetSchoolAnnouncement(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSchoolAnnouncement(entity);
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