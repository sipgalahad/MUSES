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
    public partial class DailySchedulePackageEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SCHOOL_DAILY_SCHEDULE_PACKAGE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            txtDailySchedulePackageCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE, Constant.StandardCode.SCHOOL_DAY));
            rptRemarks.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE).ToList();
            rptRemarks.DataBind();

            List<StandardCode> lstSchoolDay = lstSc.Where(p => p.ParentID == Constant.StandardCode.SCHOOL_DAY).ToList();
            decimal width = 100 / lstSchoolDay.Count;
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^001", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay1.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^002", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay2.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^003", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay3.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^004", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay4.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^005", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay5.Style.Add("display", "none");
            if (lstSchoolDay.Count(p => p.StandardCodeID == string.Format("{0}^006", Constant.StandardCode.SCHOOL_DAY)) < 1)
                tdSchoolDay6.Style.Add("display", "none");
            tdSchoolDay1.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay2.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay3.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay4.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay5.Style.Add("width", string.Format("{0}%", width));
            tdSchoolDay6.Style.Add("width", string.Format("{0}%", width));

            List<DailyScheduleTypeHd> lstEntityHd = BusinessLayer.GetDailyScheduleTypeHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            lstEntityHd.Insert(0, new DailyScheduleTypeHd { DailyScheduleTypeID = 0, DailyScheduleTypeName = "" });
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType1, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType2, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType3, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType4, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType5, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
            Methods.SetComboBoxField<DailyScheduleTypeHd>(cboScheduleType6, lstEntityHd, "DailyScheduleTypeName", "DailyScheduleTypeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtDailySchedulePackageCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDailySchedulePackageName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(DailySchedulePackage entity)
        {
            txtDailySchedulePackageCode.Text = entity.DailySchedulePackageCode;
            txtDailySchedulePackageName.Text = entity.DailySchedulePackageName;

            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5}) AND IsDeleted = 0",
                entity.DailyScheduleTypeID1 == null ? "0" : entity.DailyScheduleTypeID1.ToString(),
                entity.DailyScheduleTypeID2 == null ? "0" : entity.DailyScheduleTypeID2.ToString(),
                entity.DailyScheduleTypeID3 == null ? "0" : entity.DailyScheduleTypeID3.ToString(),
                entity.DailyScheduleTypeID4 == null ? "0" : entity.DailyScheduleTypeID4.ToString(),
                entity.DailyScheduleTypeID5 == null ? "0" : entity.DailyScheduleTypeID5.ToString(),
                entity.DailyScheduleTypeID6 == null ? "0" : entity.DailyScheduleTypeID6.ToString()
                ));
            rptDay1.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID1).ToList();
            rptDay1.DataBind();
            rptDay2.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID2).ToList();
            rptDay2.DataBind();
            rptDay3.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID3).ToList();
            rptDay3.DataBind();
            rptDay4.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID4).ToList();
            rptDay4.DataBind();
            rptDay5.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID5).ToList();
            rptDay5.DataBind();
            rptDay6.DataSource = lstEntityDt.Where(p => p.DailyScheduleTypeID == entity.DailyScheduleTypeID6).ToList();
            rptDay6.DataBind();

            cboScheduleType1.Value = entity.DailyScheduleTypeID1.ToString();
            cboScheduleType2.Value = entity.DailyScheduleTypeID2.ToString();
            cboScheduleType3.Value = entity.DailyScheduleTypeID3.ToString();
            cboScheduleType4.Value = entity.DailyScheduleTypeID4.ToString();
            cboScheduleType5.Value = entity.DailyScheduleTypeID5.ToString();
            cboScheduleType6.Value = entity.DailyScheduleTypeID6.ToString();            
        }

        protected void cbpScheduleType1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType1.Value));
            rptDay1.DataSource = lstEntityDt;
            rptDay1.DataBind();
        }

        protected void cbpScheduleType2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType2.Value));
            rptDay2.DataSource = lstEntityDt;
            rptDay2.DataBind();
        }

        protected void cbpScheduleType3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType3.Value));
            rptDay3.DataSource = lstEntityDt;
            rptDay3.DataBind();
        }

        protected void cbpScheduleType4_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType4.Value));
            rptDay4.DataSource = lstEntityDt;
            rptDay4.DataBind();
        }

        protected void cbpScheduleType5_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType5.Value));
            rptDay5.DataSource = lstEntityDt;
            rptDay5.DataBind();
        }

        protected void cbpScheduleType6_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<DailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetDailyScheduleTypeDtList(string.Format("DailyScheduleTypeID = {0}", cboScheduleType6.Value));
            rptDay6.DataSource = lstEntityDt;
            rptDay6.DataBind();
        }

        private void ControlToEntity(DailySchedulePackage entity)
        {
            entity.DailySchedulePackageCode = txtDailySchedulePackageCode.Text;
            entity.DailySchedulePackageName = txtDailySchedulePackageName.Text;

            if (cboScheduleType1.Value != null && cboScheduleType1.Value.ToString() != "0")
                entity.DailyScheduleTypeID1 = Convert.ToInt32(cboScheduleType1.Value);
            else
                entity.DailyScheduleTypeID1 = null;

            if (cboScheduleType2.Value != null && cboScheduleType2.Value.ToString() != "0")
                entity.DailyScheduleTypeID2 = Convert.ToInt32(cboScheduleType2.Value);
            else
                entity.DailyScheduleTypeID2 = null;

            if (cboScheduleType3.Value != null && cboScheduleType3.Value.ToString() != "0")
                entity.DailyScheduleTypeID3 = Convert.ToInt32(cboScheduleType3.Value);
            else
                entity.DailyScheduleTypeID3 = null;

            if (cboScheduleType4.Value != null && cboScheduleType4.Value.ToString() != "0")
                entity.DailyScheduleTypeID4 = Convert.ToInt32(cboScheduleType4.Value);
            else
                entity.DailyScheduleTypeID4 = null;

            if (cboScheduleType5.Value != null && cboScheduleType5.Value.ToString() != "0")
                entity.DailyScheduleTypeID5 = Convert.ToInt32(cboScheduleType5.Value);
            else
                entity.DailyScheduleTypeID5 = null;

            if (cboScheduleType6.Value != null && cboScheduleType6.Value.ToString() != "0")
                entity.DailyScheduleTypeID6 = Convert.ToInt32(cboScheduleType6.Value);
            else
                entity.DailyScheduleTypeID6 = null;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("DailySchedulePackageCode = '{0}'", txtDailySchedulePackageCode.Text);
            List<DailySchedulePackage> lst = BusinessLayer.GetDailySchedulePackageList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Ruangan Dengan Kode " + txtDailySchedulePackageCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("DailySchedulePackageCode = '{0}' AND DailySchedulePackageID != {1}", txtDailySchedulePackageCode.Text, hdnID.Value);
            List<DailySchedulePackage> lst = BusinessLayer.GetDailySchedulePackageList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Ruangan Dengan Kode " + txtDailySchedulePackageCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            DailySchedulePackageDao entityDao = new DailySchedulePackageDao(ctx);
            bool result = false;
            try
            {
                DailySchedulePackage entity = new DailySchedulePackage();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetDailySchedulePackageMaxID(ctx).ToString();
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
                DailySchedulePackage entity = BusinessLayer.GetDailySchedulePackage(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateDailySchedulePackage(entity);
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