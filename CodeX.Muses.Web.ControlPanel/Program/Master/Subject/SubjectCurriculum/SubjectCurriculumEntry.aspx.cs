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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectCurriculumEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SB_SUBJECT_CURRICULUM;
        }
        protected override void InitializeDataControl()
        {
            List<Curriculum> lstCurriculum = BusinessLayer.GetCurriculumList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<Curriculum>(cboCurriculum, lstCurriculum, "CurriculumName", "CurriculumID");
            cboCurriculum.SelectedIndex = 0;

            BindGridView();

            Helper.SetControlEntrySetting(txtSubjectCurriculumName, new ControlEntrySetting(true, true, true), "mpTrx");
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
                CurriculumClassType obj = (CurriculumClassType)e.Item.DataItem;
                CheckBox chkClassType = (CheckBox)e.Item.FindControl("chkClassType");
                chkClassType.Attributes.Add("classtypename", obj.CurriculumClassTypeName);
                chkClassType.Attributes.Add("classtypeid", obj.CurriculumClassTypeID.ToString());
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SubjectID = {0} AND CurriculumID = {1} AND IsDeleted = 0", AppSession.SubjectID, cboCurriculum.Value);
            grdView.DataSource = BusinessLayer.GetvSubjectCurriculumList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected void cbpClassType_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            Repeater rptClassType = (Repeater)ddeClassType.FindControl("rptClassType");
            List<CurriculumClassType> lstClassType = BusinessLayer.GetCurriculumClassTypeList(string.Format("CurriculumID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", cboCurriculum.Value, Constant.ClassStudyType.REGULAR));
            rptClassType.DataSource = lstClassType;
            rptClassType.DataBind();
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
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(SubjectCurriculum entity)
        {
            entity.SubjectCurriculumName = txtSubjectCurriculumName.Text;
            entity.IsSyllabusPerSchoolPeriodSection = chkIsSyllabusPerSchoolPeriodSection.Checked;
            entity.IsMeetingPlanPerSchoolPeriodSection = chkIsMeetingPlanPerSchoolPeriodSection.Checked;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumDao entityDao = new SubjectCurriculumDao(ctx);
            SubjectCurriculumClassTypeDao entityClassTypeDao = new SubjectCurriculumClassTypeDao(ctx);
            try
            {
                SubjectCurriculum entity = new SubjectCurriculum();
                ControlToEntity(entity);
                entity.CurriculumID = Convert.ToInt32(cboCurriculum.Value);
                entity.SubjectID = AppSession.SubjectID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.SubjectCurriculumID = BusinessLayer.GetSubjectCurriculumMaxID(ctx);

                string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                foreach (string classTypeID in lstClassTypeID)
                {
                    SubjectCurriculumClassType entityDt = new SubjectCurriculumClassType();
                    entityDt.SubjectCurriculumID = entity.SubjectCurriculumID;
                    entityDt.CurriculumClassTypeID = Convert.ToInt32(classTypeID);
                    entityClassTypeDao.Insert(entityDt);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumDao entityDao = new SubjectCurriculumDao(ctx);
            SubjectCurriculumClassTypeDao entityClassTypeDao = new SubjectCurriculumClassTypeDao(ctx);
            try
            {
                SubjectCurriculum entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<SubjectCurriculumClassType> lstEntityDt = BusinessLayer.GetSubjectCurriculumClassTypeList(string.Format("SubjectCurriculumID = {0}", entity.SubjectCurriculumID), ctx);
                string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                foreach (string classTypeID in lstClassTypeID)
                {
                    SubjectCurriculumClassType entityDt = lstEntityDt.FirstOrDefault(p => p.CurriculumClassTypeID.ToString() == classTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new SubjectCurriculumClassType();
                        entityDt.SubjectCurriculumID = entity.SubjectCurriculumID;
                        entityDt.CurriculumClassTypeID = Convert.ToInt32(classTypeID);
                        entityClassTypeDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }

                foreach (SubjectCurriculumClassType entityDt in lstEntityDt)
                {
                    entityClassTypeDao.Delete(entityDt.SubjectCurriculumID, entityDt.CurriculumClassTypeID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                SubjectCurriculum entity = BusinessLayer.GetSubjectCurriculum(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCurriculum(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}