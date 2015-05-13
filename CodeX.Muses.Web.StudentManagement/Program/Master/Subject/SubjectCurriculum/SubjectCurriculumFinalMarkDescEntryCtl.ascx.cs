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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCurriculumFinalMarkDescEntryCtl : BaseEntryPopupCtl
    {
        List<CurriculumSchoolPeriodSection> lstPeriodSection = null;
        List<SubjectCurriculumFinalMarkDescription> lstFinalMarkDesc = null;
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
            hdnID.Value = param;
            SubjectCurriculum entitySubjectCurriculum = BusinessLayer.GetSubjectCurriculum(Convert.ToInt32(hdnID.Value));
            txtSubjectCurriculumName.Text = entitySubjectCurriculum.SubjectCurriculumName;

            lstPeriodSection = BusinessLayer.GetCurriculumSchoolPeriodSectionList(string.Format("CurriculumID = {0} AND IsDeleted = 0", entitySubjectCurriculum.CurriculumID));

            lstFinalMarkDesc = BusinessLayer.GetSubjectCurriculumFinalMarkDescriptionList(string.Format("SubjectCurriculumID = {0} AND IsDeleted = 0", hdnID.Value));

            List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND GCCompetencyDescriptionType = '{1}' AND IsDeleted = 0", entitySubjectCurriculum.CurriculumID, Constant.CompetencyDescriptionType.SEMESTER));
            rptMarkType.DataSource = lstMarkType;
            rptMarkType.DataBind();
        }

        protected void rptMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CurriculumMarkType entity = (CurriculumMarkType)e.Item.DataItem;
                Repeater rptPeriodSection = (Repeater)e.Item.FindControl("rptPeriodSection");
                rptPeriodSection.DataSource = lstPeriodSection;
                rptPeriodSection.DataBind();
            }
        }

        protected void rptPeriodSection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CurriculumSchoolPeriodSection entity = (CurriculumSchoolPeriodSection)e.Item.DataItem;
                CurriculumMarkType markType = ((RepeaterItem)e.Item.Parent.Parent).DataItem as CurriculumMarkType;
                TextBox txtDescriptionText = (TextBox)e.Item.FindControl("txtDescriptionText");
                txtDescriptionText.Attributes.Add("curriculumschoolperiodsectionid", entity.CurriculumSchoolPeriodSectionID.ToString());
                txtDescriptionText.Attributes.Add("curriculummarktypeid", markType.CurriculumMarkTypeID.ToString());

                SubjectCurriculumFinalMarkDescription desc = lstFinalMarkDesc.FirstOrDefault(p => p.CurriculumMarkTypeID == markType.CurriculumMarkTypeID && p.CurriculumSchoolPeriodSectionID == entity.CurriculumSchoolPeriodSectionID);
                if (desc != null)
                    txtDescriptionText.Text = desc.DescriptionText;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumFinalMarkDescriptionDao entityDao = new SubjectCurriculumFinalMarkDescriptionDao(ctx);
            bool result = false;
            try
            {
                int SubjectCurriculumID = Convert.ToInt32(hdnID.Value);

                List<SubjectCurriculumFinalMarkDescription> lstFinalMarkDesc = BusinessLayer.GetSubjectCurriculumFinalMarkDescriptionList(string.Format("SubjectCurriculumID = {0} AND IsDeleted = 0", hdnID.Value), ctx);
                if (hdnSaveValue.Value != "")
                {
                    string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(';');
                        int CurriculumSchoolPeriodSectionID = Convert.ToInt32(temp[0]);
                        int CurriculumMarkTypeID = Convert.ToInt32(temp[1]);
                        string DescriptionText = temp[2];

                        SubjectCurriculumFinalMarkDescription entity = lstFinalMarkDesc.FirstOrDefault(p => p.CurriculumMarkTypeID == CurriculumMarkTypeID && p.CurriculumSchoolPeriodSectionID == CurriculumSchoolPeriodSectionID);
                        if (entity != null)
                        {
                            entity.DescriptionText = DescriptionText;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDao.Update(entity);

                            lstFinalMarkDesc.Remove(entity);
                        }
                        else
                        {
                            entity = new SubjectCurriculumFinalMarkDescription();
                            entity.CurriculumMarkTypeID = CurriculumMarkTypeID;
                            entity.CurriculumSchoolPeriodSectionID = CurriculumSchoolPeriodSectionID;
                            entity.SubjectCurriculumID = SubjectCurriculumID;
                            entity.DescriptionText = DescriptionText;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            entityDao.Insert(entity);
                        }
                    }
                }
                foreach (SubjectCurriculumFinalMarkDescription entity in lstFinalMarkDesc)
                {
                    entity.IsDeleted = true;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDao.Update(entity);
                }
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
    }
}