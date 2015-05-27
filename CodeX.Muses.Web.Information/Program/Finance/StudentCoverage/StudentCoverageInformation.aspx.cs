using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using DevExpress.Utils;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentCoverageInformation : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_COVERAGE_INFO;
        }

        List<StudentFeeCompType> lstComp = null;
        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<vCustomer> lstCustomer = BusinessLayer.GetvCustomerList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<vCustomer>(cboCustomer, lstCustomer, "BusinessPartnerName", "BusinessPartnerID");
            cboCustomer.SelectedIndex = 0;

            BindGridView();
        }

        #region Bind Grid View
        List<CoverageTypeDtComp> lstCoverageTypeDtComp = null;
        private void BindGridView()
        {
            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 3;

            List<vStudentCoverageTransactionDt> lstStudent = BusinessLayer.GetvStudentCoverageTransactionDtList(string.Format("SchoolPeriodID = {0} AND BusinessPartnerID = {1}", cboSchoolPeriod.Value, cboCustomer.Value));
            if (lstStudent.Count > 0)
            {
                string lstID = string.Join(",", lstStudent.Select(p => p.CoverageTypeDtID).ToList());
                lstCoverageTypeDtComp = BusinessLayer.GetCoverageTypeDtCompList(string.Format("CoverageTypeDtID IN ({0})", lstID));
            }
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vStudentCoverageTransactionDt entity = (vStudentCoverageTransactionDt)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                List<CoverageTypeDtComp> lstDt = lstCoverageTypeDtComp.Where(p => p.CoverageTypeDtID == entity.CoverageTypeDtID).ToList();
                List<CoverageTypeDtComp> lstDt1 = new List<CoverageTypeDtComp>();
                foreach (StudentFeeCompType comp in lstComp)
                {
                    CoverageTypeDtComp entityDt = lstDt.FirstOrDefault(p => p.StudentFeeCompTypeID == comp.StudentFeeCompTypeID);
                    if (entityDt == null)
                        entityDt = new CoverageTypeDtComp();
                    lstDt1.Add(entityDt);
                }
                rptViewDt.DataSource = lstDt1;
                rptViewDt.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        public override Control OnGetExportControl(ref bool isShowTitle)
        {
            isShowTitle = false;
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            HtmlGenericControl div2 = new HtmlGenericControl("DIV");
            HtmlGenericControl h41 = new HtmlGenericControl("DIV");
            HtmlGenericControl h42 = new HtmlGenericControl("DIV");
            HtmlGenericControl h43 = new HtmlGenericControl("DIV");
            h41.InnerHtml = string.Format("DAFTAR PENERIMA DANA BANTUAN PENDIDIKAN {0}", cboCustomer.Text);
            h42.InnerHtml = string.Format("{0}", cboSchoolPeriod.Text);
            h43.InnerHtml = string.Format("Unit {0}", AppSession.UserLogin.SiteName);
            div.Controls.Add(h41);
            div.Controls.Add(h42);
            div.Controls.Add(h43);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(div2);
            return div;
        }
    }
}