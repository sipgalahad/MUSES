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
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class PeriodClassTypeSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_SCHOOL_PERIOD_CLASS_TYPE_SUBJECT;
        }

        protected override void InitializeDataControl()
        {
            hdnSiteID.Value = AppSession.UserLogin.SiteID;

            SchoolPeriod entitySchoolPeriod = BusinessLayer.GetSchoolPeriod(AppSession.SchoolPeriodID);
            hdnCurriculumID.Value = entitySchoolPeriod.CurriculumID.ToString();

            List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsAllowTask = 1 AND IsDeleted = 0", entitySchoolPeriod.CurriculumID));
            rptFinalMarkFormula.DataSource = lstMarkType;
            rptFinalMarkFormula.DataBind();

            List<vPeriodClassType> lstClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ClassStudyType.REGULAR));
            Methods.SetComboBoxField<vPeriodClassType>(cboClassType, lstClassType, "CurriculumClassTypeName", "PeriodClassTypeID");
            cboClassType.SelectedIndex = 0;

            List<CurriculumSubjectGroup> lstSubjectGroup = BusinessLayer.GetCurriculumSubjectGroupList(string.Format("CurriculumID = {0} AND IsDeleted = 0", entitySchoolPeriod.CurriculumID));
            Methods.SetComboBoxField<CurriculumSubjectGroup>(cboCurriculumSubjectGroup, lstSubjectGroup, "CurriculumSubjectGroupName", "CurriculumSubjectGroupID");

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboCurriculumSubjectGroup, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(tacSubjectCurriculum, new ControlEntrySetting(true, true, false), "mpTrx");
            Helper.SetControlEntrySetting(tacTeacher, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtNoMeetingHoursInWeek, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtPassingGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptFinalMarkFormula_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CurriculumMarkType entity = (CurriculumMarkType)e.Item.DataItem;

                ASPxComboBox cboCurriculumFinalMarkFormulaID = (ASPxComboBox)e.Item.FindControl("cboCurriculumFinalMarkFormulaID");
                cboCurriculumFinalMarkFormulaID.ClientInstanceName = string.Format("cboCurriculumFinalMarkFormulaID{0}", e.Item.ItemIndex);

                List<CurriculumFinalMarkFormulaHd> lstFormula = BusinessLayer.GetCurriculumFinalMarkFormulaHdList(string.Format("CurriculumMarkTypeID = {0} AND IsDeleted = 0", entity.CurriculumMarkTypeID));
                lstFormula.Insert(0, new CurriculumFinalMarkFormulaHd { CurriculumFinalMarkFormulaID = 0, CurriculumFinalMarkFormulaName = "" });
                Methods.SetComboBoxField<CurriculumFinalMarkFormulaHd>(cboCurriculumFinalMarkFormulaID, lstFormula, "CurriculumFinalMarkFormulaName", "CurriculumFinalMarkFormulaID");
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (cboClassType.Value != null && cboClassType.Value.ToString() != "0")
            {
                filterExpression = string.Format("SchoolPeriodID = {0} AND PeriodClassTypeID = {1} AND GCClassStudyType = '{2}' AND IsDeleted = 0", AppSession.SchoolPeriodID, cboClassType.Value, Constant.ClassStudyType.REGULAR);
                vPeriodClassType entity = BusinessLayer.GetvPeriodClassTypeList(string.Format("PeriodClassTypeID = {0}", cboClassType.Value)).FirstOrDefault();
                hdnClassTypeID.Value = entity.CurriculumClassTypeID.ToString();

                hdnClassRowCount.Value = BusinessLayer.GetSchoolClassRowCount(string.Format("PeriodClassTypeID = {0} AND IsDeleted = 0", cboClassType.Value)).ToString();
            }
            List<vPeriodClassTypeSubject> lstEntity = BusinessLayer.GetvPeriodClassTypeSubjectList(filterExpression);
            grdView.DataSource = lstEntity;
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

        private void ControlToEntity(PeriodClassTypeSubject entity)
        {
            entity.SubjectID = Convert.ToInt32(tacSubject.Value);
            entity.CurriculumSubjectGroupID = Convert.ToInt32(cboCurriculumSubjectGroup.Value);
            if (tacSubjectCurriculum.Value != "" && tacSubjectCurriculum.Value != "0")
                entity.SubjectCurriculumID = Convert.ToInt32(tacSubjectCurriculum.Value);
            else
                entity.SubjectCurriculumID = null;
            entity.TeacherID = Convert.ToInt32(tacTeacher.Value);
            entity.NoMeetingHoursInWeek = Convert.ToInt16(txtNoMeetingHoursInWeek.Text);
            entity.PassingGrade = Convert.ToInt16(txtPassingGrade.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            PeriodClassTypeSubjectDao entityDao = new PeriodClassTypeSubjectDao(ctx);
            PeriodClassTypeSubjectFinalMarkFormulaDao entityFinalMarkDao = new PeriodClassTypeSubjectFinalMarkFormulaDao(ctx);
            bool result = false;
            try
            {
                PeriodClassTypeSubject entity = new PeriodClassTypeSubject();
                ControlToEntity(entity);
                entity.PeriodClassTypeID = Convert.ToInt32(cboClassType.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.PeriodClassTypeSubjectID = BusinessLayer.GetPeriodClassTypeSubjectMaxID(ctx);

                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    PeriodClassTypeSubjectFinalMarkFormula entityFinalMark = new PeriodClassTypeSubjectFinalMarkFormula();
                    entityFinalMark.PeriodClassTypeSubjectID = entity.PeriodClassTypeSubjectID;
                    entityFinalMark.CurriculumMarkTypeID = Convert.ToInt32(temp[0]);
                    if (temp[1] != "")
                        entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                    else
                        entityFinalMark.CurriculumFinalMarkFormulaID = null;
                    entityFinalMarkDao.Insert(entityFinalMark);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            PeriodClassTypeSubjectDao entityDao = new PeriodClassTypeSubjectDao(ctx);
            PeriodClassTypeSubjectFinalMarkFormulaDao entityFinalMarkDao = new PeriodClassTypeSubjectFinalMarkFormulaDao(ctx);
            bool result = false;
            try
            {
                PeriodClassTypeSubject entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<PeriodClassTypeSubjectFinalMarkFormula> lstEntityFinalMark = BusinessLayer.GetPeriodClassTypeSubjectFinalMarkFormulaList(string.Format("PeriodClassTypeSubjectID = {0}", entity.PeriodClassTypeSubjectID), ctx);
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int CurriculumMarkTypeID = Convert.ToInt32(temp[0]);

                    PeriodClassTypeSubjectFinalMarkFormula entityFinalMark = lstEntityFinalMark.FirstOrDefault(p => p.CurriculumMarkTypeID == CurriculumMarkTypeID);
                    if (entityFinalMark == null)
                    {
                        entityFinalMark = new PeriodClassTypeSubjectFinalMarkFormula();
                        entityFinalMark.PeriodClassTypeSubjectID = entity.PeriodClassTypeSubjectID;
                        entityFinalMark.CurriculumMarkTypeID = CurriculumMarkTypeID;
                        if (temp[1] != "")
                            entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                        else
                            entityFinalMark.CurriculumFinalMarkFormulaID = null;
                        entityFinalMarkDao.Insert(entityFinalMark);
                    }
                    else
                    {
                        if (temp[1] != "")
                            entityFinalMark.CurriculumFinalMarkFormulaID = Convert.ToInt32(temp[1]);
                        else
                            entityFinalMark.CurriculumFinalMarkFormulaID = null;
                        entityFinalMarkDao.Update(entityFinalMark);
                        lstEntityFinalMark.Remove(entityFinalMark);
                    }
                }

                foreach (PeriodClassTypeSubjectFinalMarkFormula entityFinalMark in lstEntityFinalMark)
                {
                    entityFinalMarkDao.Delete(entityFinalMark.PeriodClassTypeSubjectID, entityFinalMark.CurriculumMarkTypeID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                PeriodClassTypeSubject entity = BusinessLayer.GetPeriodClassTypeSubject(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdatePeriodClassTypeSubject(entity);
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