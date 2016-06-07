using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using CodeX.Web.CustomControl;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class PeriodClassTypeSubjectIndicatorEntryDtCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            string[] temp = param.Split('|');
            hdnPeriodClassTypeSubjectID.Value = temp[0];
            hdnSubjectCurriculumID.Value = temp[1];

            vPeriodClassTypeSubject subject = BusinessLayer.GetvPeriodClassTypeSubjectList(string.Format("PeriodClassTypeSubjectID = {0}", hdnPeriodClassTypeSubjectID.Value)).FirstOrDefault();
            txtHeaderText.Text = subject.CurriculumClassTypeName;
            txtHeaderText2.Text = subject.SubjectName;
            txtHeaderText3.Text = subject.SubjectCurriculumName;

            BindGridView();
        }

        private void BindGridView()
        {
            lstPeriodSection = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            lstPeriodSection.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "-- Tidak Digunakan --" });

            lstIndicator = BusinessLayer.GetPeriodClassTypeSubjectIndicatorList(string.Format("PeriodClassTypeSubjectID = {0}", hdnPeriodClassTypeSubjectID.Value));

            string filterExpression = string.Format("SubjectCurriculumID = {0} AND GCCurriculumSyllabusType = '{1}' AND IsAllowTask = 1 AND IsDeleted = 0", hdnSubjectCurriculumID.Value, Constant.CurriculumSyllabusType.INDICATOR);
            List<vSubjectCurriculumSyllabus> lstEntity = BusinessLayer.GetvSubjectCurriculumSyllabusList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        List<StandardCode> lstPeriodSection = null;
        List<PeriodClassTypeSubjectIndicator> lstIndicator = null;
        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSubjectCurriculumSyllabus entity = e.Row.DataItem as vSubjectCurriculumSyllabus;
                ASPxComboBox cboPeriodSection = e.Row.FindControl("cboPeriodSection") as ASPxComboBox;
                cboPeriodSection.ClientInstanceName = string.Format("cboPeriodSection{0}", e.Row.DataItemIndex);
                Methods.SetComboBoxField<StandardCode>(cboPeriodSection, lstPeriodSection, "StandardCodeName", "StandardCodeID");

                PeriodClassTypeSubjectIndicator indicator = lstIndicator.FirstOrDefault(p => p.SubjectIndicatorID == entity.SubjectCurriculumSyllabusID);
                if (indicator != null)
                    cboPeriodSection.Value = indicator.GCPeriodSection;
                else
                    cboPeriodSection.SelectedIndex = 0;
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            PeriodClassTypeSubjectIndicatorDao entityDao = new PeriodClassTypeSubjectIndicatorDao(ctx);
            try
            {
                List<PeriodClassTypeSubjectIndicator> lstIndicator = BusinessLayer.GetPeriodClassTypeSubjectIndicatorList(string.Format("PeriodClassTypeSubjectID = {0}", hdnPeriodClassTypeSubjectID.Value), ctx);
                string[] lstSaveValue = hdnLstSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int indicatorID = Convert.ToInt32(temp[0]);
                    string GCPeriodSection = temp[1];
                    PeriodClassTypeSubjectIndicator indicator = lstIndicator.FirstOrDefault(p => p.SubjectIndicatorID == indicatorID);
                    if (GCPeriodSection != "")
                    {
                        if (indicator != null)
                        {
                            if (indicator.GCPeriodSection != GCPeriodSection)
                            {
                                indicator.GCPeriodSection = GCPeriodSection;
                                entityDao.Update(indicator);
                            }
                        }
                        else
                        {
                            indicator = new PeriodClassTypeSubjectIndicator();
                            indicator.PeriodClassTypeSubjectID = Convert.ToInt32(hdnPeriodClassTypeSubjectID.Value);
                            indicator.SubjectIndicatorID = indicatorID;
                            indicator.GCPeriodSection = GCPeriodSection;
                            entityDao.Insert(indicator);
                        }
                    }
                    else
                    {
                        if (indicator != null)
                            entityDao.Delete(indicator.PeriodClassTypeSubjectID, indicator.SubjectIndicatorID);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
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