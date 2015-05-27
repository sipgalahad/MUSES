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
    public partial class CurriculumMarkTypeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_MARK_TYPE;
        }
        protected override void InitializeDataControl()
        {
            Repeater rptClassStudyType = (Repeater)ddeClassStudyType.FindControl("rptClassStudyType");
            List<StandardCode> lstClassStudyType = BusinessLayer.GetStandardCodeList(string.Format("StandardCodeID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.ClassStudyType.REGULAR, Constant.ClassStudyType.EXTRACURRICULAR));
            rptClassStudyType.DataSource = lstClassStudyType;
            rptClassStudyType.DataBind();

            List<MarkTypeHd> lstMark = BusinessLayer.GetMarkTypeHdList(string.Format("IsDeleted = 0"));
            Methods.SetComboBoxField<MarkTypeHd>(cboCompetencyMarkType, lstMark, "MarkTypeName", "MarkTypeID");
            lstMark.Insert(0, new MarkTypeHd { MarkTypeID = 0, MarkTypeName = "" });
            Methods.SetComboBoxField<MarkTypeHd>(cboTaskMarkType, lstMark, "MarkTypeName", "MarkTypeID");
            Methods.SetComboBoxField<MarkTypeHd>(cboFinalMarkType, lstMark, "MarkTypeName", "MarkTypeID");
            Methods.SetComboBoxField<MarkTypeHd>(cboPredicateMarkType, lstMark, "MarkTypeName", "MarkTypeID");

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.COMPETENCY_DESCRIPTION_TYPE, Constant.StandardCode.STUDENT_MARK_GROUP));
            Methods.SetComboBoxField<StandardCode>(cboCompetencyDescriptionType, lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPETENCY_DESCRIPTION_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboStudentMarkGroup, lstSc.Where(p => p.ParentID == Constant.StandardCode.STUDENT_MARK_GROUP).ToList(), "StandardCodeName", "StandardCodeID");

            BindGridView();

            Helper.SetControlEntrySetting(txtCurriculumMarkTypeCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtCurriculumMarkTypeName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboStudentMarkGroup, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCompetencyDescriptionType, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCompetencyMarkType, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptClassStudyType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkClassStudyType = (CheckBox)e.Item.FindControl("chkClassStudyType");
                chkClassStudyType.Attributes.Add("classstudytype", obj.StandardCodeName);
                chkClassStudyType.Attributes.Add("gcclassstudytype", obj.StandardCodeID.ToString());
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID);
            grdView.DataSource = BusinessLayer.GetvCurriculumMarkTypeList(filterExpression);
            grdView.DataBind();
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

        private void ControlToEntity(CurriculumMarkType entity)
        {
            entity.CurriculumMarkTypeCode = txtCurriculumMarkTypeCode.Text;
            entity.CurriculumMarkTypeName = txtCurriculumMarkTypeName.Text;
            entity.GCStudentMarkGroup = cboStudentMarkGroup.Value.ToString();
            if (cboTaskMarkType.Value != null && cboTaskMarkType.Value.ToString() != "0")
                entity.TaskMarkTypeID = Convert.ToInt32(cboTaskMarkType.Value);
            else
                entity.TaskMarkTypeID = null;
            if (cboFinalMarkType.Value != null && cboFinalMarkType.Value.ToString() != "0")
                entity.FinalMarkTypeID = Convert.ToInt32(cboFinalMarkType.Value);
            else
                entity.FinalMarkTypeID = null;
            if (cboPredicateMarkType.Value != null && cboPredicateMarkType.Value.ToString() != "0")
                entity.PredicateMarkTypeID = Convert.ToInt32(cboPredicateMarkType.Value);
            else
                entity.PredicateMarkTypeID = null;
            entity.IsAllowTask = chkIsAllowTask.Checked;
            entity.IsShowCompetencyDescription = chkIsShowCompetencyDescription.Checked;
            if (entity.IsShowCompetencyDescription)
            {
                entity.GCCompetencyDescriptionType = cboCompetencyDescriptionType.Value.ToString();
                entity.CompetencyMarkTypeID = Convert.ToInt32(cboCompetencyMarkType.Value);
            }
            else
            {
                entity.GCCompetencyDescriptionType = null;
                entity.CompetencyMarkTypeID = null;
            }
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumMarkTypeDao entityDao = new CurriculumMarkTypeDao(ctx);
            CurriculumMarkTypeClassStudyTypeDao entityClassStudyTypeDao = new CurriculumMarkTypeClassStudyTypeDao(ctx);
            try
            {
                CurriculumMarkType entity = new CurriculumMarkType();
                ControlToEntity(entity);
                entity.CurriculumID = AppSession.CurriculumID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.CurriculumMarkTypeID = BusinessLayer.GetCurriculumMarkTypeMaxID(ctx);

                if (hdnLstClassStudyTypeID.Value != "")
                {
                    string[] lstClassStudyTypeID = hdnLstClassStudyTypeID.Value.Split(',');
                    foreach (string GCClassStudyType in lstClassStudyTypeID)
                    {
                        CurriculumMarkTypeClassStudyType entityClassStudyType = new CurriculumMarkTypeClassStudyType();
                        entityClassStudyType.CurriculumMarkTypeID = entity.CurriculumMarkTypeID;
                        entityClassStudyType.GCClassStudyType = GCClassStudyType;
                        entityClassStudyTypeDao.Insert(entityClassStudyType);
                    }
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
            CurriculumMarkTypeDao entityDao = new CurriculumMarkTypeDao(ctx);
            CurriculumMarkTypeClassStudyTypeDao entityClassStudyTypeDao = new CurriculumMarkTypeClassStudyTypeDao(ctx);
            try
            {
                CurriculumMarkType entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<CurriculumMarkTypeClassStudyType> lstEntityClassStudyType = BusinessLayer.GetCurriculumMarkTypeClassStudyTypeList(string.Format("CurriculumMarkTypeID = {0}", entity.CurriculumMarkTypeID), ctx);
                if (hdnLstClassStudyTypeID.Value != "")
                {
                    string[] lstClassStudyTypeID = hdnLstClassStudyTypeID.Value.Split(',');
                    foreach (string GCClassStudyType in lstClassStudyTypeID)
                    {
                        CurriculumMarkTypeClassStudyType entityClassStudyType = lstEntityClassStudyType.FirstOrDefault(p => p.GCClassStudyType == GCClassStudyType);
                        if (entityClassStudyType == null)
                        {
                            entityClassStudyType = new CurriculumMarkTypeClassStudyType();
                            entityClassStudyType.CurriculumMarkTypeID = entity.CurriculumMarkTypeID;
                            entityClassStudyType.GCClassStudyType = GCClassStudyType;
                            entityClassStudyTypeDao.Insert(entityClassStudyType);
                        }
                        else
                            lstEntityClassStudyType.Remove(entityClassStudyType);
                    }
                }
                foreach (CurriculumMarkTypeClassStudyType entityClassStudyType in lstEntityClassStudyType)
                {
                    entityClassStudyTypeDao.Delete(entityClassStudyType.CurriculumMarkTypeID, entityClassStudyType.GCClassStudyType);
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
                CurriculumMarkType entity = BusinessLayer.GetCurriculumMarkType(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumMarkType(entity);
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