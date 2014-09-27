using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using System.Globalization;

namespace QIS.Medinfras.Web.EMR.Program
{
    public partial class CurrentMedicationList : BasePagePatientPageListEntry
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.PATIENT_PAGE.CURRENT_MEDICATION;
        }

        #region List
        protected override void InitializeDataControl()
        {
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("MRN = {0} AND IsDeleted = 0", AppSession.RegisteredPatient.MRN);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvPatientAllergyRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vPatientAllergy> lstEntity = BusinessLayer.GetvPatientAllergyList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value != "")
            {
                PatientAllergy entity = BusinessLayer.GetPatientAllergy(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePatientAllergy(entity);
                return true;
            }
            return false;
        }
        #endregion

        #region Entry
        protected override void SetControlProperties()
        {
            string filterExpression = string.Format("ParentID IN ('{0}','{1}','{2}')",
                Constant.StandardCode.ALLERGEN_TYPE, Constant.StandardCode.ALLERGY_INFORMATION_SOURCE, Constant.StandardCode.ALLERGY_SEVERITY);

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);
            Methods.SetComboBoxField<StandardCode>(ddlAllergenType, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ALLERGEN_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(ddlFindingSource, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ALLERGY_INFORMATION_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(ddlSeverity, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.ALLERGY_SEVERITY).ToList(), "StandardCodeName", "StandardCodeID");

            fillDate();
        }

        private void fillDate()
        {
            ddlMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "MonthNumber";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem { Value = "", Text = "" });

            ddlYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem { Value = "", Text = "" });

            ddlDate.DataSource = Enumerable.Range(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            ddlDate.DataBind();
            ddlDate.Items.Insert(0, new ListItem { Value = "", Text = "" });
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtAllergenName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(ddlAllergenType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(ddlFindingSource, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(ddlYear, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlMonth, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlDate, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(ddlSeverity, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtReaction, new ControlEntrySetting(true, true, false));
        }

        private void ControlToEntity(PatientAllergy entity)
        {
            entity.GCAllergenType = Request.Form[ddlAllergenType.UniqueID];
            entity.GCAllergySource = Request.Form[ddlFindingSource.UniqueID];
            entity.GCAllergySeverity = Request.Form[ddlSeverity.UniqueID];

            String year = Request.Form[ddlYear.UniqueID];
            String month = "";
            String date = "";
            if (year != "")
            {
                month = Request.Form[ddlMonth.UniqueID];
                if (month != "")
                {
                    month = String.Format("{0:00}", Convert.ToInt32(month));
                    date = Request.Form[ddlDate.UniqueID];
                    if (date != "")
                        date = String.Format("{0:00}", Convert.ToInt32(date));
                }
            }

            entity.KnownDate = year + month + date;
            entity.Allergen = txtAllergenName.Text;
            entity.Reaction = txtReaction.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                PatientAllergy entity = new PatientAllergy();
                ControlToEntity(entity);
                entity.MRN = AppSession.RegisteredPatient.MRN;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertPatientAllergy(entity);
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
                PatientAllergy entity = BusinessLayer.GetPatientAllergy(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdatePatientAllergy(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}