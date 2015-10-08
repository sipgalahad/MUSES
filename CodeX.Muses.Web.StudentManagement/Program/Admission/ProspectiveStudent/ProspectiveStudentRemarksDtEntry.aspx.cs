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
    public partial class ProspectiveStudentRemarksDtEntry : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String ID = Request.QueryString["id"];
                Registration registration = BusinessLayer.GetRegistration(Convert.ToInt32(ID));
                hdnID.Value = registration.ProspectiveStudentID.ToString();

                SetControlProperties();
                ProspectiveStudent entity = BusinessLayer.GetProspectiveStudent(Convert.ToInt32(hdnID.Value));
                StudentAttribute attr = BusinessLayer.GetStudentAttributeList(String.Format("ProspectiveStudentID = {0}", entity.ProspectiveStudentID)).FirstOrDefault();
                EntityToControl(entity, attr);
                cboBloodType.Focus();
            }
        }

        private void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0",
                Constant.StandardCode.BLOOD_TYPE, Constant.StandardCode.LANGUAGE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            Methods.SetComboBoxField(cboBloodType, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.BLOOD_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboLanguage, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.LANGUAGE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        private void EntityToControl(ProspectiveStudent entity, StudentAttribute stdAtt)
        {
            cboBloodType.Value = entity.GCBloodType;
            cboLanguage.Value = entity.GCLanguage;
            txtHomeDistance.Text = entity.HomeDistance.ToString();
            txtMedicalHistory.Text = entity.MedicalHistory;

            if (stdAtt != null)
            {
                txtReasonRegister.Text = stdAtt.ReasonRegister;
                cboTransportToSchool.Value = stdAtt.GCTransportToSchool;
                cboStateInWomb.Value = stdAtt.GCStateInWomb;
                cboStateAtBirth.Value = stdAtt.GCStateAtBirth;
                chkIsDisablity.Checked = stdAtt.IsDisability;
                cboHouseStatus.Value = stdAtt.GCLivingWith;
                txtHouseHolderAdult.Text = stdAtt.HouseHolderAdult.ToString();
                txtHouseHolderChild.Text = stdAtt.HouseHolderChild.ToString(); ;
                chkIsPlaygroundInHouse.Checked = stdAtt.IsPlaygroundInHouse;
                cboChanceToHangout.Value = stdAtt.GCChanceToHangout;
                chkIsFailInSchool.Checked = stdAtt.IsFailInSchool;
                txtGradeFail.Text = stdAtt.GCFailInSchoolGrade;
                chkIsFatherless.Checked = stdAtt.IsFatherless;
                chkIsMotherless.Checked = stdAtt.IsMotherless;
                txtTalentOrInterest.Text = stdAtt.TalentOrInterest;
                txtStateEnterKinderGarten.Text = stdAtt.StateWhenEnterKindergarten;
                cboAppetiteAtBreakfast.Value = stdAtt.GCAppetiteAtBreakfast;
                cboAppetiteAtLunch.Value = stdAtt.GCAppetiteAtLunch;
                cboAppetiteAtDinner.Value = stdAtt.GCAppetiteAtDinner;
                cboAppetiteAtOtherTime.Value = stdAtt.GCAppetiteAtOtherTime;
                cboRealtionshipWithFather.Value = stdAtt.GCRelationshipWithFather;
                cboRealtionshipWithMother.Value = stdAtt.GCRelationshipWithMother;
                cboRealtionshipWithBrother.Value = stdAtt.GCRelationshipWithBrother;
                cboUrinateStatus.Value = stdAtt.GCUrinateStatus;
                txtSleepingAtNight.Text = stdAtt.SleepingAtNight;
                txtWakeUp.Text = stdAtt.WakeUpAtMorning;
                chkSleepingAtRandomTime.Checked = stdAtt.SleepingAtRandomTime;
                txtBreastfedDuration.Text = stdAtt.BreastfedDuration.ToString();
                chkIsBreastfed.Checked = stdAtt.IsBreastfed;
                txtAdditionalFood.Text = stdAtt.AnotherFood;
            }
        }

        private void ControlToEntity(ProspectiveStudent entity, StudentAttribute stdAtt)
        {
            entity.GCBloodType = cboBloodType.Value == null ? "" : cboBloodType.Value.ToString();
            entity.GCLanguage = cboLanguage.Value == null ? "" : cboLanguage.Value.ToString();
            entity.HomeDistance = Convert.ToDecimal(txtHomeDistance.Text);
            entity.MedicalHistory = txtMedicalHistory.Text;

            if (stdAtt != null)
            {
                stdAtt.ReasonRegister = txtReasonRegister.Text;

                if (cboTransportToSchool.Value != null)
                    stdAtt.GCTransportToSchool = cboTransportToSchool.Value.ToString();
                else
                    stdAtt.GCTransportToSchool = null;

                if (cboStateInWomb.Value != null)
                    stdAtt.GCStateInWomb = cboStateInWomb.Value.ToString();
                else
                    stdAtt.GCStateInWomb = null;

                if (cboStateAtBirth.Value != null)
                    stdAtt.GCStateAtBirth = cboStateAtBirth.Value.ToString();
                else
                    stdAtt.GCStateAtBirth = null;
                stdAtt.IsDisability = chkIsDisablity.Checked;
                if (cboHouseStatus.Value != null)
                    stdAtt.GCLivingWith = cboHouseStatus.Value.ToString();
                else
                    stdAtt.GCLivingWith = null;
                stdAtt.HouseHolderAdult = Convert.ToInt32(txtHouseHolderAdult.Text);
                stdAtt.HouseHolderChild = Convert.ToInt32(txtHouseHolderChild.Text);
                stdAtt.IsPlaygroundInHouse = chkIsPlaygroundInHouse.Checked;
                if (cboChanceToHangout.Value != null)
                    stdAtt.GCChanceToHangout = cboChanceToHangout.Value.ToString();
                else
                    stdAtt.GCChanceToHangout = null;
                stdAtt.IsFailInSchool = chkIsFailInSchool.Checked;
                stdAtt.GCFailInSchoolGrade = txtGradeFail.Text;
                stdAtt.IsFatherless = chkIsFatherless.Checked;
                stdAtt.IsMotherless = chkIsMotherless.Checked;
                stdAtt.TalentOrInterest = txtTalentOrInterest.Text;
                stdAtt.StateWhenEnterKindergarten = txtStateEnterKinderGarten.Text;
                if (cboAppetiteAtBreakfast.Value != null)
                    stdAtt.GCAppetiteAtBreakfast = cboAppetiteAtBreakfast.Value.ToString();
                else
                    stdAtt.GCAppetiteAtBreakfast = null;
                if (cboAppetiteAtLunch.Value != null)
                    stdAtt.GCAppetiteAtLunch = cboAppetiteAtLunch.Value.ToString();
                else
                    stdAtt.GCAppetiteAtLunch = null;
                if (cboAppetiteAtDinner.Value != null)
                    stdAtt.GCAppetiteAtDinner = cboAppetiteAtDinner.Value.ToString();
                else
                    stdAtt.GCAppetiteAtDinner = null;
                if (cboAppetiteAtOtherTime.Value != null)
                    stdAtt.GCAppetiteAtOtherTime = cboAppetiteAtOtherTime.Value.ToString();
                else
                    stdAtt.GCAppetiteAtOtherTime = null;
                if (cboRealtionshipWithFather.Value != null)
                    stdAtt.GCRelationshipWithFather = cboRealtionshipWithFather.Value.ToString();
                else
                    stdAtt.GCRelationshipWithFather = null;
                if (cboRealtionshipWithMother.Value != null)
                    stdAtt.GCRelationshipWithMother = cboRealtionshipWithMother.Value.ToString();
                else
                    stdAtt.GCRelationshipWithMother = null;
                if (cboRealtionshipWithBrother.Value != null)
                    stdAtt.GCRelationshipWithBrother = cboRealtionshipWithBrother.Value.ToString();
                else
                    stdAtt.GCRelationshipWithBrother = null;
                if (cboUrinateStatus.Value != null)
                    stdAtt.GCUrinateStatus = cboUrinateStatus.Value.ToString();
                else
                    stdAtt.GCUrinateStatus = null;
                stdAtt.SleepingAtNight = txtSleepingAtNight.Text;
                stdAtt.WakeUpAtMorning = txtWakeUp.Text;
                stdAtt.SleepingAtRandomTime = chkSleepingAtRandomTime.Checked;
                stdAtt.BreastfedDuration = Convert.ToInt32(txtBreastfedDuration.Text);
                stdAtt.IsBreastfed = chkIsBreastfed.Checked;
                stdAtt.AnotherFood = txtAdditionalFood.Text;
            }
        }

        private bool OnSaveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentDao entityDao = new ProspectiveStudentDao(ctx);
            StudentAttributeDao attributeDao = new StudentAttributeDao(ctx);
            try
            {
                ProspectiveStudent entity = BusinessLayer.GetProspectiveStudent(Convert.ToInt32(hdnID.Value));
                StudentAttribute attr = BusinessLayer.GetStudentAttributeList(String.Format("ProspectiveStudentID = {0}",entity.ProspectiveStudentID)).FirstOrDefault();
                ControlToEntity(entity, attr);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                attr.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                attributeDao.Update(attr);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private void OnBtnSaveClick(ref string result)
        {
            string errMessage = "";
            string retval = "";
            result = "save|";
            if (OnSaveRecord(ref errMessage))
                result += string.Format("success|{0}", retval);
            else
                result += string.Format("fail|{0}", errMessage);
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string[] param = e.Parameter.Split('|');
            if (param[0] == "save")
            {
                OnBtnSaveClick(ref result);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}