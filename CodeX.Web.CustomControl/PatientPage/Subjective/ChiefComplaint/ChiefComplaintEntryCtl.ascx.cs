using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Data.Service;
using QIS.Medinfras.Web.Common;

namespace QIS.Medinfras.Web.EMR.Program.PatientPage
{
    public partial class ChiefComplaintEntryCtl : BasePagePatientPageEntryCtl
    {
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                IsAdd = false;
                hdnID.Value = param;
                ChiefComplaint entity = BusinessLayer.GetChiefComplaint(Convert.ToInt32(param));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                hdnID.Value = "";
                IsAdd = true;
                SetControlProperties();
            }
        }

        protected void SetControlProperties()
        {
            string filterExpression = string.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}','{5}')", Constant.StandardCode.ONSET, Constant.StandardCode.EXACERBATED, Constant.StandardCode.QUALITY,
                Constant.StandardCode.SEVERITY, Constant.StandardCode.COURSE_TIMING, Constant.StandardCode.RELIEVED_BY);
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(filterExpression);
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField(ddlOnset, lstSc.Where(p => p.ParentID == Constant.StandardCode.ONSET || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(ddlProvocation, lstSc.Where(p => p.ParentID == Constant.StandardCode.EXACERBATED || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(ddlQuality, lstSc.Where(p => p.ParentID == Constant.StandardCode.QUALITY || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(ddlSeverity, lstSc.Where(p => p.ParentID == Constant.StandardCode.SEVERITY || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(ddlTime, lstSc.Where(p => p.ParentID == Constant.StandardCode.COURSE_TIMING || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(ddlRelievedBy, lstSc.Where(p => p.ParentID == Constant.StandardCode.RELIEVED_BY || p.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
        }


        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtObservationDate, new ControlEntrySetting(true, true, true, AppSession.RegisteredPatient.VisitDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT)));
            SetControlEntrySetting(txtObservationTime, new ControlEntrySetting(true, true, true, AppSession.RegisteredPatient.VisitTime));
            SetControlEntrySetting(txtChiefComplaint, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLocation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlOnset, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtOnset, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlProvocation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvocation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlQuality, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtQuality, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlSeverity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtSeverity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlTime, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTime, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlRelievedBy, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRelievedBy, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ChiefComplaint entity)
        {
            txtObservationDate.Text = entity.ObservationDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtObservationTime.Text = entity.ObservationTime;
            txtChiefComplaint.Text = entity.ChiefComplaintText;
            txtLocation.Text = entity.Location;
            ddlQuality.SelectedValue = entity.GCQuality;
            ddlOnset.SelectedValue = entity.GCOnset;
            ddlRelievedBy.SelectedValue = entity.GCRelieved;
            ddlSeverity.SelectedValue = entity.GCSeverity;
            ddlProvocation.SelectedValue = entity.GCProvocation;
            ddlTime.SelectedValue = entity.GCCourse;
            txtQuality.Text = entity.Quality;
            txtOnset.Text = entity.Onset;
            txtRelievedBy.Text = entity.RelievedBy;
            txtSeverity.Text = entity.Severity;
            txtProvocation.Text = entity.Provocation;
            txtTime.Text = entity.CourseTiming;
        }

        private void ControlToEntity(ChiefComplaint entity)
        {
            entity.ObservationDate = Helper.GetDatePickerValue(txtObservationDate);
            entity.ObservationTime = txtObservationTime.Text;
            entity.ChiefComplaintText = txtChiefComplaint.Text;
            entity.Location = txtLocation.Text;
            entity.GCQuality = Request.Form[ddlQuality.UniqueID];
            entity.GCOnset = Request.Form[ddlOnset.UniqueID];
            entity.GCRelieved = Request.Form[ddlRelievedBy.UniqueID];
            entity.GCSeverity = Request.Form[ddlSeverity.UniqueID];
            entity.GCProvocation = Request.Form[ddlProvocation.UniqueID];
            entity.GCCourse = Request.Form[ddlTime.UniqueID];
            entity.Quality = txtQuality.Text;
            entity.Onset = txtOnset.Text;
            entity.RelievedBy = txtRelievedBy.Text;
            entity.Severity = txtSeverity.Text;
            entity.Provocation = txtProvocation.Text;
            entity.CourseTiming = txtTime.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                ChiefComplaint entity = new ChiefComplaint();
                ControlToEntity(entity);
                entity.VisitID = AppSession.RegisteredPatient.VisitID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertChiefComplaint(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                ChiefComplaint entity = BusinessLayer.GetChiefComplaint(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateChiefComplaint(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}