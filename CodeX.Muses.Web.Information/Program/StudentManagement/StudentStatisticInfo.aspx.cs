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
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Information.STUDENT_STATISTIC_INFO;
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
        List<vClassStudentRegistration> lstClassStudentRegistration = null;
        List<vClassStudentRegistration> lstClassStudentRegistrationPeriod = null;
        List<vClassStudentRegistration> lstClassStudentRegistrationPeriodType = null;
        List<vStudentMoveOut> lstStudentMoveOut = null;
        List<vStudentMoveOut> lstStudentMoveOutPeriod = null;
        List<vStudentMoveOut> lstStudentMoveOutPeriodType = null;

        class CStudentGenderCount
        {
            public int MaleCount { get; set; }
            public int FemaleCount { get; set; }
        }

        class CStudentReligionCount
        {
            public string GCReligion { get; set; }
            public int StudentCount { get; set; }
        }

        CStudentGenderCount entityStudentGenderCount = null;
        List<CStudentReligionCount> lstStudentReligionCount = null;

        #region Bind Grid View
        private void BindGridView()
        {
            entityStudentGenderCount = new CStudentGenderCount();
            lstStudentReligionCount = new List<CStudentReligionCount>();

            lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", cboSchoolPeriod.Value, Constant.ClassStudyType.REGULAR));
            string lstPeriodClassTypeID = string.Join(",", lstPeriodClassType.Select(p => p.PeriodClassTypeID).ToList());
            lstPeriodClassTypeStudentPerGender = BusinessLayer.GetvPeriodClassTypeStudentPerGenderList(string.Format("PeriodClassTypeID IN ({0})", lstPeriodClassTypeID));
            lstPeriodClassTypeStudentPerReligion = BusinessLayer.GetvPeriodClassTypeStudentPerReligionList(string.Format("PeriodClassTypeID IN ({0})", lstPeriodClassTypeID));

            lstStudentMoveOut = BusinessLayer.GetvStudentMoveOutList(string.Format("PeriodClassTypeID IN ({0})", lstPeriodClassTypeID));
            lstClassStudentRegistration = BusinessLayer.GetvClassStudentRegistrationList(string.Format("PeriodClassTypeID IN ({0}) AND GCPeriodAdmissionType = '{1}'", lstPeriodClassTypeID, Constant.AdmissionType.STUDENT_TRANSFER));

            List<DateTime> lstPeriod = new List<DateTime>();
            lstRegion = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RELIGION));

            foreach (StandardCode religion in lstRegion)
            {
                lstStudentReligionCount.Add(new CStudentReligionCount { GCReligion = religion.StandardCodeID });
            }

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

        protected void rptPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                entityStudentGenderCount.FemaleCount = 0;
                entityStudentGenderCount.MaleCount = 0;
                foreach (CStudentReligionCount religionCount in lstStudentReligionCount)
                    religionCount.StudentCount = 0;

                DateTime entity = (DateTime)e.Item.DataItem;
                HtmlTableCell thPeriodName = (HtmlTableCell)e.Item.FindControl("thPeriodName");
                thPeriodName.InnerHtml = string.Format("BULAN : {0}", entity.ToString("MMMM yyyy"));
                thPeriodName.ColSpan = 4 + lstRegion.Count;

                lstClassStudentRegistrationPeriod = lstClassStudentRegistration.Where(p => p.SchoolDate.Month == entity.Month && p.SchoolDate.Year == entity.Year).ToList();
                lstStudentMoveOutPeriod = lstStudentMoveOut.Where(p => p.MoveOutDate.Month == entity.Month && p.MoveOutDate.Year == entity.Year).ToList();

                HtmlTableCell thReligion = (HtmlTableCell)e.Item.FindControl("thReligion");
                thReligion.ColSpan = lstRegion.Count;

                Repeater rptReligion = (Repeater)e.Item.FindControl("rptReligion");
                rptReligion.DataSource = lstRegion;
                rptReligion.DataBind();

                Repeater rptClassType2 = (Repeater)e.Item.FindControl("rptClassType2");
                rptClassType2.DataSource = lstPeriodClassType;
                rptClassType2.DataBind();

                Repeater rptClassType = (Repeater)e.Item.FindControl("rptClassType");
                rptClassType.DataSource = lstPeriodClassType;
                rptClassType.DataBind();

                HtmlGenericControl bClassCount = (HtmlGenericControl)e.Item.FindControl("bClassCount");
                HtmlGenericControl bTotalStudentCount = (HtmlGenericControl)e.Item.FindControl("bTotalStudentCount");
                HtmlGenericControl bTotalMaleCount = (HtmlGenericControl)e.Item.FindControl("bTotalMaleCount");
                HtmlGenericControl bTotalFemaleCount = (HtmlGenericControl)e.Item.FindControl("bTotalFemaleCount");

                bClassCount.InnerHtml = lstPeriodClassType.Sum(p => p.NoOfClass).ToString();
                bTotalStudentCount.InnerHtml = (entityStudentGenderCount.MaleCount + entityStudentGenderCount.FemaleCount).ToString();
                bTotalMaleCount.InnerHtml = entityStudentGenderCount.MaleCount.ToString();
                bTotalFemaleCount.InnerHtml = entityStudentGenderCount.FemaleCount.ToString();

                Repeater rptReligionTotal = (Repeater)e.Item.FindControl("rptReligionTotal");
                rptReligionTotal.DataSource = lstRegion;
                rptReligionTotal.DataBind();                
            }
        }

        protected void rptReligionTotal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                StandardCode entity = (StandardCode)e.Item.DataItem;
                HtmlGenericControl bTotalReligionCount = (HtmlGenericControl)e.Item.FindControl("bTotalReligionCount");
                bTotalReligionCount.InnerHtml = lstStudentReligionCount.FirstOrDefault(p => p.GCReligion == entity.StandardCodeID).StudentCount.ToString();
            }
        }

        protected void rptClassType2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vPeriodClassType entity = (vPeriodClassType)e.Item.DataItem;
                HtmlTableCell tdBeginStudentCount = (HtmlTableCell)e.Item.FindControl("tdBeginStudentCount");
                HtmlTableCell tdBeginStudentMaleCount = (HtmlTableCell)e.Item.FindControl("tdBeginStudentMaleCount");
                HtmlTableCell tdBeginStudentFemaleCount = (HtmlTableCell)e.Item.FindControl("tdBeginStudentFemaleCount");

                HtmlTableCell tdInStudentCount = (HtmlTableCell)e.Item.FindControl("tdInStudentCount");
                HtmlTableCell tdInStudentMaleCount = (HtmlTableCell)e.Item.FindControl("tdInStudentMaleCount");
                HtmlTableCell tdInStudentFemaleCount = (HtmlTableCell)e.Item.FindControl("tdInStudentFemaleCount");

                HtmlTableCell tdOutStudentCount = (HtmlTableCell)e.Item.FindControl("tdOutStudentCount");
                HtmlTableCell tdOutStudentMaleCount = (HtmlTableCell)e.Item.FindControl("tdOutStudentMaleCount");
                HtmlTableCell tdOutStudentFemaleCount = (HtmlTableCell)e.Item.FindControl("tdOutStudentFemaleCount");

                HtmlTableCell tdEndStudentCount = (HtmlTableCell)e.Item.FindControl("tdEndStudentCount");
                HtmlTableCell tdEndStudentMaleCount = (HtmlTableCell)e.Item.FindControl("tdEndStudentMaleCount");
                HtmlTableCell tdEndStudentFemaleCount = (HtmlTableCell)e.Item.FindControl("tdEndStudentFemaleCount");

                HtmlTableCell tdStudentMoveOutReason = (HtmlTableCell)e.Item.FindControl("tdStudentMoveOutReason"); 

                lstClassStudentRegistrationPeriodType = lstClassStudentRegistrationPeriod.Where(p => p.PeriodClassTypeID == entity.PeriodClassTypeID).ToList();
                lstStudentMoveOutPeriodType = lstStudentMoveOutPeriod.Where(p => p.PeriodClassTypeID == entity.PeriodClassTypeID).ToList();

                int maleCount = 0;
                int femaleCount = 0;
                vPeriodClassTypeStudentPerGender entityMale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.MALE);
                vPeriodClassTypeStudentPerGender entityFemale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.FEMALE);

                int newStudentMale = lstClassStudentRegistrationPeriodType.Where(p => p.GCGender == Constant.Gender.MALE).Count();
                int newStudentFemale = lstClassStudentRegistrationPeriodType.Where(p => p.GCGender == Constant.Gender.FEMALE).Count();
                int studentMoveOutMale = lstStudentMoveOutPeriodType.Where(p => p.GCGender == Constant.Gender.MALE).Count();
                int studentMoveOutFemale = lstStudentMoveOutPeriodType.Where(p => p.GCGender == Constant.Gender.FEMALE).Count();

                if (entityMale != null)
                    maleCount = entityMale.StudentCount;
                if (entityFemale != null)
                    femaleCount = entityFemale.StudentCount;

                tdBeginStudentMaleCount.InnerHtml = maleCount.ToString();
                tdBeginStudentFemaleCount.InnerHtml = femaleCount.ToString();
                tdBeginStudentCount.InnerHtml = (maleCount + femaleCount).ToString();

                tdInStudentMaleCount.InnerHtml = newStudentMale.ToString();
                tdInStudentFemaleCount.InnerHtml = newStudentFemale.ToString();
                tdInStudentCount.InnerHtml = (newStudentMale + newStudentFemale).ToString();

                tdOutStudentMaleCount.InnerHtml = studentMoveOutMale.ToString();
                tdOutStudentFemaleCount.InnerHtml = studentMoveOutFemale.ToString();
                tdOutStudentCount.InnerHtml = (studentMoveOutMale + studentMoveOutFemale).ToString();

                tdEndStudentMaleCount.InnerHtml = (maleCount + newStudentMale - studentMoveOutMale).ToString();
                tdEndStudentFemaleCount.InnerHtml = (femaleCount + newStudentFemale - studentMoveOutFemale).ToString();
                tdEndStudentCount.InnerHtml = ((maleCount + newStudentMale - studentMoveOutMale) + (femaleCount + newStudentFemale - studentMoveOutFemale)).ToString();

                tdStudentMoveOutReason.InnerHtml = string.Join(",", lstStudentMoveOutPeriodType.Select(p => p.MoveOutReason).ToList());
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

                lstClassStudentRegistrationPeriodType = lstClassStudentRegistrationPeriod.Where(p => p.PeriodClassTypeID == entity.PeriodClassTypeID).ToList();
                lstStudentMoveOutPeriodType = lstStudentMoveOutPeriod.Where(p => p.PeriodClassTypeID == entity.PeriodClassTypeID).ToList();

                int maleCount = 0;
                int femaleCount = 0;
                vPeriodClassTypeStudentPerGender entityMale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.MALE);
                vPeriodClassTypeStudentPerGender entityFemale = lstPeriodClassTypeStudentPerGender.FirstOrDefault(p => p.PeriodClassTypeID == entity.PeriodClassTypeID && p.GCGender == Constant.Gender.FEMALE);

                int newStudentMale = lstClassStudentRegistrationPeriodType.Where(p => p.GCGender == Constant.Gender.MALE).Count();
                int newStudentFemale = lstClassStudentRegistrationPeriodType.Where(p => p.GCGender == Constant.Gender.FEMALE).Count();
                int studentMoveOutMale = lstStudentMoveOutPeriodType.Where(p => p.GCGender == Constant.Gender.MALE).Count();
                int studentMoveOutFemale = lstStudentMoveOutPeriodType.Where(p => p.GCGender == Constant.Gender.FEMALE).Count();

                if (entityMale != null)
                {
                    maleCount = entityMale.StudentCount;
                    if (newStudentMale > 0)
                        entityMale.StudentCount += newStudentMale;
                    if (studentMoveOutMale > 0)
                        entityMale.StudentCount -= studentMoveOutMale;
                }
                else if (newStudentMale > 0 || studentMoveOutMale > 0)
                {
                    entityMale = new vPeriodClassTypeStudentPerGender { GCGender = Constant.Gender.MALE, PeriodClassTypeID = entity.PeriodClassTypeID, StudentCount = newStudentMale - studentMoveOutMale };
                    lstPeriodClassTypeStudentPerGender.Add(entityMale);
                }
                if (entityFemale != null)
                {
                    femaleCount = entityFemale.StudentCount;
                    if (newStudentFemale > 0)
                        entityFemale.StudentCount += newStudentFemale;
                    if (studentMoveOutFemale > 0)
                        entityFemale.StudentCount -= studentMoveOutFemale;
                }
                else if (newStudentFemale > 0 || studentMoveOutFemale > 0)
                {
                    entityFemale = new vPeriodClassTypeStudentPerGender { GCGender = Constant.Gender.FEMALE, PeriodClassTypeID = entity.PeriodClassTypeID, StudentCount = newStudentFemale - studentMoveOutFemale };
                    lstPeriodClassTypeStudentPerGender.Add(entityFemale);
                }

                maleCount += newStudentMale - studentMoveOutMale;
                femaleCount += newStudentFemale - studentMoveOutFemale;

                entityStudentGenderCount.FemaleCount += femaleCount;
                entityStudentGenderCount.MaleCount += maleCount;
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

                int newStudentReligion = lstClassStudentRegistrationPeriodType.Where(p => p.GCReligion == entity.StandardCodeID).Count();
                int studentMoveOutReligion = lstStudentMoveOutPeriodType.Where(p => p.GCReligion == entity.StandardCodeID).Count();
                vPeriodClassTypeStudentPerReligion entityReligion = lstPeriodClassTypeStudentPerReligion.FirstOrDefault(p => p.PeriodClassTypeID == classType.PeriodClassTypeID && p.GCReligion == entity.StandardCodeID);
                if (entityReligion != null)
                {
                    if (newStudentReligion > 0 || studentMoveOutReligion > 0)
                        entityReligion.StudentCount += newStudentReligion - studentMoveOutReligion;
                    tdReligion.InnerHtml = entityReligion.StudentCount.ToString();
                }
                else
                {
                    if (newStudentReligion > 0 || studentMoveOutReligion > 0)
                    {
                        entityReligion = new vPeriodClassTypeStudentPerReligion { GCReligion = entity.StandardCodeID, PeriodClassTypeID = classType.PeriodClassTypeID, StudentCount = newStudentReligion - studentMoveOutReligion };
                        lstPeriodClassTypeStudentPerReligion.Add(entityReligion);
                        tdReligion.InnerHtml = entityReligion.StudentCount.ToString();
                    }
                    else
                        tdReligion.InnerHtml = "0";
                }
                if (entityReligion != null)
                    lstStudentReligionCount.FirstOrDefault(p => p.GCReligion == entity.StandardCodeID).StudentCount += entityReligion.StudentCount;
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