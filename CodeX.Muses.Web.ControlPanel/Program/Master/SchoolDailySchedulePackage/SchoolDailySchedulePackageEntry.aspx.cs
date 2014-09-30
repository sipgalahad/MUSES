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
    public partial class SchoolDailySchedulePackageEntry : BasePageEntry
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
                SchoolDailySchedulePackage entity = BusinessLayer.GetSchoolDailySchedulePackage(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            txtSchoolDailySchedulePackageCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOOL_DAILY_SCHEDULE_TYPE));
            rptRemarks.DataSource = lstSc;
            rptRemarks.DataBind();

            List<SchoolDailyScheduleTypeHd> lstEntityHd = BusinessLayer.GetSchoolDailyScheduleTypeHdList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            lstEntityHd.Insert(0, new SchoolDailyScheduleTypeHd { SchoolDailyScheduleTypeID = 0, SchoolDailyScheduleTypeName = "" });
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType1, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType2, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType3, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType4, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType5, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
            Methods.SetComboBoxField<SchoolDailyScheduleTypeHd>(cboScheduleType6, lstEntityHd, "SchoolDailyScheduleTypeName", "SchoolDailyScheduleTypeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSchoolDailySchedulePackageCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSchoolDailySchedulePackageName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(SchoolDailySchedulePackage entity)
        {
            txtSchoolDailySchedulePackageCode.Text = entity.SchoolDailySchedulePackageCode;
            txtSchoolDailySchedulePackageName.Text = entity.SchoolDailySchedulePackageName;

            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID IN ({0},{1},{2},{3},{4},{5})",
                entity.SchoolDailyScheduleTypeID1 == null ? "0" : entity.SchoolDailyScheduleTypeID1.ToString(),
                entity.SchoolDailyScheduleTypeID2 == null ? "0" : entity.SchoolDailyScheduleTypeID2.ToString(),
                entity.SchoolDailyScheduleTypeID3 == null ? "0" : entity.SchoolDailyScheduleTypeID3.ToString(),
                entity.SchoolDailyScheduleTypeID4 == null ? "0" : entity.SchoolDailyScheduleTypeID4.ToString(),
                entity.SchoolDailyScheduleTypeID5 == null ? "0" : entity.SchoolDailyScheduleTypeID5.ToString(),
                entity.SchoolDailyScheduleTypeID6 == null ? "0" : entity.SchoolDailyScheduleTypeID6.ToString()
                ));
            rptDay1.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID1).ToList();
            rptDay1.DataBind();
            rptDay2.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID2).ToList();
            rptDay2.DataBind();
            rptDay3.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID3).ToList();
            rptDay3.DataBind();
            rptDay4.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID4).ToList();
            rptDay4.DataBind();
            rptDay5.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID5).ToList();
            rptDay5.DataBind();
            rptDay6.DataSource = lstEntityDt.Where(p => p.SchoolDailyScheduleTypeID == entity.SchoolDailyScheduleTypeID6).ToList();
            rptDay6.DataBind();

            cboScheduleType1.Value = entity.SchoolDailyScheduleTypeID1.ToString();
            cboScheduleType2.Value = entity.SchoolDailyScheduleTypeID2.ToString();
            cboScheduleType3.Value = entity.SchoolDailyScheduleTypeID3.ToString();
            cboScheduleType4.Value = entity.SchoolDailyScheduleTypeID4.ToString();
            cboScheduleType5.Value = entity.SchoolDailyScheduleTypeID5.ToString();
            cboScheduleType6.Value = entity.SchoolDailyScheduleTypeID6.ToString();            
        }

        protected void cbpScheduleType1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType1.Value));
            rptDay1.DataSource = lstEntityDt;
            rptDay1.DataBind();
        }

        protected void cbpScheduleType2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType2.Value));
            rptDay2.DataSource = lstEntityDt;
            rptDay2.DataBind();
        }

        protected void cbpScheduleType3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType3.Value));
            rptDay3.DataSource = lstEntityDt;
            rptDay3.DataBind();
        }

        protected void cbpScheduleType4_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType4.Value));
            rptDay4.DataSource = lstEntityDt;
            rptDay4.DataBind();
        }

        protected void cbpScheduleType5_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType5.Value));
            rptDay5.DataSource = lstEntityDt;
            rptDay5.DataBind();
        }

        protected void cbpScheduleType6_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            List<SchoolDailyScheduleTypeDt> lstEntityDt = BusinessLayer.GetSchoolDailyScheduleTypeDtList(string.Format("SchoolDailyScheduleTypeID = {0}", cboScheduleType6.Value));
            rptDay6.DataSource = lstEntityDt;
            rptDay6.DataBind();
        }

        private void ControlToEntity(SchoolDailySchedulePackage entity)
        {
            entity.SchoolDailySchedulePackageCode = txtSchoolDailySchedulePackageCode.Text;
            entity.SchoolDailySchedulePackageName = txtSchoolDailySchedulePackageName.Text;

            if (cboScheduleType1.Value != null && cboScheduleType1.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID1 = Convert.ToInt32(cboScheduleType1.Value);
            else
                entity.SchoolDailyScheduleTypeID1 = null;

            if (cboScheduleType2.Value != null && cboScheduleType2.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID2 = Convert.ToInt32(cboScheduleType2.Value);
            else
                entity.SchoolDailyScheduleTypeID2 = null;

            if (cboScheduleType3.Value != null && cboScheduleType3.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID3 = Convert.ToInt32(cboScheduleType3.Value);
            else
                entity.SchoolDailyScheduleTypeID3 = null;

            if (cboScheduleType4.Value != null && cboScheduleType4.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID4 = Convert.ToInt32(cboScheduleType4.Value);
            else
                entity.SchoolDailyScheduleTypeID4 = null;

            if (cboScheduleType5.Value != null && cboScheduleType5.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID5 = Convert.ToInt32(cboScheduleType5.Value);
            else
                entity.SchoolDailyScheduleTypeID5 = null;

            if (cboScheduleType6.Value != null && cboScheduleType6.Value.ToString() != "0")
                entity.SchoolDailyScheduleTypeID6 = Convert.ToInt32(cboScheduleType6.Value);
            else
                entity.SchoolDailyScheduleTypeID6 = null;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SchoolDailySchedulePackageCode = '{0}'", txtSchoolDailySchedulePackageCode.Text);
            List<SchoolDailySchedulePackage> lst = BusinessLayer.GetSchoolDailySchedulePackageList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Ruangan Dengan Kode " + txtSchoolDailySchedulePackageCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SchoolDailySchedulePackageCode = '{0}' AND SchoolDailySchedulePackageID != {1}", txtSchoolDailySchedulePackageCode.Text, hdnID.Value);
            List<SchoolDailySchedulePackage> lst = BusinessLayer.GetSchoolDailySchedulePackageList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Ruangan Dengan Kode " + txtSchoolDailySchedulePackageCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolDailySchedulePackageDao entityDao = new SchoolDailySchedulePackageDao(ctx);
            bool result = false;
            try
            {
                SchoolDailySchedulePackage entity = new SchoolDailySchedulePackage();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetSchoolDailySchedulePackageMaxID(ctx).ToString();
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
                SchoolDailySchedulePackage entity = BusinessLayer.GetSchoolDailySchedulePackage(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSchoolDailySchedulePackage(entity);
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