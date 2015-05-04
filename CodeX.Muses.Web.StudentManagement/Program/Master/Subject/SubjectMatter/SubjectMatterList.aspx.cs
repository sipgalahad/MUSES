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
    public partial class SubjectMatterList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SBM_SUBJECT_MATTER;
        }
        protected override void InitializeDataControl()
        {
            Repeater rptClassType = (Repeater)ddeClassType.FindControl("rptClassType");
            List<ClassType> lstClassType = BusinessLayer.GetClassTypeList(string.Format("ClassTypeID IN (SELECT ClassTypeID FROM SubjectClassType WHERE SubjectID = {0})", AppSession.SubjectID));
            rptClassType.DataSource = lstClassType;
            rptClassType.DataBind();

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PERIOD_SECTION));
            rptPeriodSection.DataSource = lstSc;
            rptPeriodSection.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(txtSubjectMatterCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtSubjectMatterName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        protected void rptPeriodSection_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtSummaryName = (TextBox)e.Item.FindControl("txtSummaryName");
                Helper.SetControlEntrySetting(txtSummaryName, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        protected void rptClassType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClassType obj = (ClassType)e.Item.DataItem;
                CheckBox chkClassType = (CheckBox)e.Item.FindControl("chkClassType");
                chkClassType.Attributes.Add("classtypename", obj.ClassTypeName);
                chkClassType.Attributes.Add("classtypeid", obj.ClassTypeID.ToString());
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            //string filterExpression = string.Format("SubjectMatterID = {0} AND IsDeleted = 0", AppSession.SubjectCurriculumID);
            //grdView.DataSource = BusinessLayer.GetvSubjectMatterHdList(filterExpression);
            //grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(SubjectMatterHd entity)
        {
            entity.SubjectMatterCode = txtSubjectMatterCode.Text;
            entity.SubjectMatterName = txtSubjectMatterName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectMatterHdDao entityDao = new SubjectMatterHdDao(ctx);
            SubjectMatterClassTypeDao entityClassTypeDao = new SubjectMatterClassTypeDao(ctx);
            SubjectCompetencyStandardSummaryDao entitySummaryDao = new SubjectCompetencyStandardSummaryDao(ctx);
            try
            {
                SubjectMatterHd entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<SubjectCompetencyStandardSummary> lstEntityComp = BusinessLayer.GetSubjectCompetencyStandardSummaryList(string.Format("SubjectMatterID = {0}", entity.SubjectMatterID), ctx);
                string[] lstSaveValue = hdnLstPeriodSectionSummary.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    string GCPeriodSection = temp[0];
                    SubjectCompetencyStandardSummary entityDt = lstEntityComp.FirstOrDefault(p => p.GCPeriodSection == GCPeriodSection);
                    if (entityDt == null)
                    {
                        entityDt = new SubjectCompetencyStandardSummary();
                        entityDt.SubjectMatterID = entity.SubjectMatterID;
                        entityDt.GCPeriodSection = GCPeriodSection;
                        entityDt.SummaryName = temp[1];
                        entitySummaryDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.SummaryName = temp[1];
                        entitySummaryDao.Update(entityDt);
                    }
                }

                List<SubjectMatterClassType> lstEntityDt = BusinessLayer.GetSubjectMatterClassTypeList(string.Format("SubjectMatterID = {0}", entity.SubjectMatterID), ctx);
                string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                foreach (string classTypeID in lstClassTypeID)
                {
                    SubjectMatterClassType entityDt = lstEntityDt.FirstOrDefault(p => p.ClassTypeID.ToString() == classTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new SubjectMatterClassType();
                        entityDt.SubjectMatterID = entity.SubjectMatterID;
                        entityDt.ClassTypeID = Convert.ToInt32(classTypeID);
                        entityClassTypeDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }

                foreach (SubjectMatterClassType entityDt in lstEntityDt)
                {
                    entityClassTypeDao.Delete(entityDt.SubjectMatterID, entityDt.ClassTypeID);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
    }
}