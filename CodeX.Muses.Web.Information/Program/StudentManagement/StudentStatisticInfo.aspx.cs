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


namespace CodeX.Muses.Web.Information.Program
{
    public partial class StudentStatisticInfo : BasePageList
    {
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;     
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_STATISTIC_INFO;
        }

        protected string OnGetClassStudyTypeRegular()
        {
            return Constant.ClassStudyType.REGULAR;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            BindGridView();
        }

        List<vPeriodClassType> lstPeriodClassType = null;
        List<vPeriodClassTypeStudentPerGender> lstPeriodClassTypeStudentPerGender = null;
        List<vPeriodClassTypeStudentPerReligion> lstPeriodClassTypeStudentPerReligion = null;
        List<StandardCode> lstRegion = null;
        #region Bind Grid View
        private void BindGridView()
        {
            if (tacPeriodClassType.Value != "")
            {

                lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", cboSchoolPeriod.Value, Constant.ClassStudyType.REGULAR));
                string lstPeriodClassTypeID = string.Join(",", lstPeriodClassType.Select(p => p.PeriodClassTypeID).ToList());
                lstPeriodClassTypeStudentPerGender = BusinessLayer.GetvPeriodClassTypeStudentPerGenderList(string.Format("PeriodClassTypeID IN ({0})", lstPeriodClassTypeID));
                lstPeriodClassTypeStudentPerReligion = BusinessLayer.GetvPeriodClassTypeStudentPerReligionList(string.Format("PeriodClassTypeID IN ({0})", lstPeriodClassTypeID));

                List<DateTime> lstPeriod = new List<DateTime>();
                lstRegion = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RELIGION));

                SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(Convert.ToInt32(cboSchoolPeriod.Value));
                DateTime startDate = entitySchoolPeriod.StartDate;
                while (startDate < entitySchoolPeriod.EndDate)
                {
                    lstPeriod.Add(startDate);
                    startDate = startDate.AddMonths(1);
                }
                rptPeriod.DataSource = lstPeriod;
                rptPeriod.DataBind();
                hdnTempExportTitle.Value = String.Format("DATA STATISTIK SISWA UNIT {0} {1}", AppSession.UserLogin.SiteName, entitySchoolPeriod.SchoolPeriodName);
            }            
        }

        protected void rptPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DateTime entity = (DateTime)e.Item.DataItem;
                HtmlTableCell thPeriodName = (HtmlTableCell)e.Item.FindControl("thPeriodName");
                thPeriodName.InnerHtml = string.Format("BULAN : {0}", entity.ToString("MMMM yyyy"));
                thPeriodName.ColSpan = 4 + lstRegion.Count;

                HtmlTableCell thReligion = (HtmlTableCell)e.Item.FindControl("thReligion");
                thReligion.ColSpan = lstRegion.Count;

                Repeater rptReligion = (Repeater)e.Item.FindControl("rptReligion");
                rptReligion.DataSource = lstRegion;
                rptReligion.DataBind();

                Repeater rptClassType = (Repeater)e.Item.FindControl("rptClassType");
                rptClassType.DataSource = lstPeriodClassType;
                rptClassType.DataBind();                
            }
        }

        protected void rptClassType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vPeriodClassType entity = (vPeriodClassType)e.Item.DataItem;
                HtmlTableCell tdStudentCount = (HtmlTableCell)e.Item.FindControl("tdStudentCount");
                HtmlTableCell tdStudentMaleCount = (HtmlTableCell)e.Item.FindControl("tdStudentMaleCount");
                HtmlTableCell tdStudentFemaleCount = (HtmlTableCell)e.Item.FindControl("tdStudentFemaleCount");

                int maleCount = 0;
                int femaleCount = 0;
                vPeriodClassTypeStudentPerGender entityMale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.MALE);
                vPeriodClassTypeStudentPerGender entityFemale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.FEMALE);
                if (entityMale != null)
                    maleCount = entityMale.StudentCount;
                if (entityFemale != null)
                    femaleCount = entityFemale.StudentCount;

                tdStudentMaleCount.InnerHtml = maleCount.ToString();
                tdStudentFemaleCount.InnerHtml = femaleCount.ToString();
                tdStudentCount.InnerHtml = (maleCount + femaleCount).ToString();

                Repeater rptStudentReligion = (Repeater)e.Item.FindControl("rptStudentReligion");
                rptStudentReligion.DataSource = lstRegion;
                rptStudentReligion.DataBind();
            }
        }

        protected void rptStudentReligion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = (StandardCode)e.Item.DataItem;
                vPeriodClassType classType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vPeriodClassType;
                HtmlTableCell tdReligion = (HtmlTableCell)e.Item.FindControl("tdReligion");

                vPeriodClassTypeStudentPerReligion entityReligion = lstPeriodClassTypeStudentPerReligion.FirstOrDefault(p => p.PeriodClassTypeID == classType.PeriodClassTypeID && p.GCReligion == entity.StandardCodeID);
                if (entityReligion != null)
                    tdReligion.InnerHtml = entityReligion.StudentCount.ToString();
                else
                    tdReligion.InnerHtml = "0";
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
            HtmlGenericControl h4 = new HtmlGenericControl("h4");
            h4.InnerHtml = hdnExportTitle.Value;
            div.Controls.Add(h4);
            div2.InnerHtml = hdnExportControl.Value;
            div.Controls.Add(div2);
            return div;
        }
    }
}