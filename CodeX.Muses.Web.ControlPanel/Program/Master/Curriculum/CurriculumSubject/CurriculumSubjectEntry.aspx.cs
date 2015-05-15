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
    public partial class CurriculumSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                return Constant.MenuCode.ControlPanel.CR_CURRICULUM_EXTRACURRICULAR;
            return Constant.MenuCode.ControlPanel.CR_CURRICULUM_SUBJECT;
        }

        protected string OnGetSubjectFilterExpression()
        {
            return string.Format("GCClassStudyType = '{0}' AND IsDeleted = 0 AND SubjectID NOT IN (SELECT SubjectID FROM CurriculumSubject WHERE CurriculumID = {1} AND IsDeleted = 0) AND SubjectID IN (SELECT SubjectID FROM SchoolSubject WHERE GCSchoolType = '{2}')", hdnGCClassStudyType.Value, AppSession.CurriculumID, hdnGCSchoolType.Value);
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                if (Page.Request.QueryString["id"] == "ex")
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.EXTRACURRICULAR;
                else
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;
            }
            else
                hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;

            Curriculum entityCurriculum = BusinessLayer.GetCurriculum(AppSession.CurriculumID);
            hdnGCSchoolType.Value = entityCurriculum.GCSchoolType;

            Repeater rptClassType = (Repeater)ddeClassType.FindControl("rptClassType");
            List<CurriculumClassType> lstClassType = BusinessLayer.GetCurriculumClassTypeList(string.Format("CurriculumID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.CurriculumID, hdnGCClassStudyType.Value));
            rptClassType.DataSource = lstClassType;
            rptClassType.DataBind();

            Repeater rptMarkType = (Repeater)ddeMarkType.FindControl("rptMarkType");
            List<CurriculumMarkType> lstMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.CurriculumID));
            rptMarkType.DataSource = lstMarkType;
            rptMarkType.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrx");
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

        protected void rptMarkType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CurriculumMarkType obj = (CurriculumMarkType)e.Item.DataItem;
                CheckBox chkMarkType = (CheckBox)e.Item.FindControl("chkMarkType");
                chkMarkType.Attributes.Add("marktypename", obj.CurriculumMarkTypeName);
                chkMarkType.Attributes.Add("marktypeid", obj.CurriculumMarkTypeID.ToString());
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("CurriculumID = {0} AND GCClassStudyType = '{1}' AND IsDeleted = 0", AppSession.CurriculumID, hdnGCClassStudyType.Value);
            grdView.DataSource = BusinessLayer.GetvCurriculumSubjectList(filterExpression);
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

        private void ControlToEntity(CurriculumSubject entity)
        {
            entity.SubjectID = Convert.ToInt32(tacSubject.Value);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumSubjectDao entityDao = new CurriculumSubjectDao(ctx);
            CurriculumSubjectClassTypeDao entityClassTypeDao = new CurriculumSubjectClassTypeDao(ctx);
            CurriculumSubjectMarkTypeDao entityMarkTypeDao = new CurriculumSubjectMarkTypeDao(ctx);
            try
            {
                CurriculumSubject entity = new CurriculumSubject();
                ControlToEntity(entity);
                entity.CurriculumID = AppSession.CurriculumID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.CurriculumSubjectID = BusinessLayer.GetCurriculumSubjectMaxID(ctx);

                if (hdnLstClassTypeID.Value != "")
                {
                    string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                    foreach (string classTypeID in lstClassTypeID)
                    {
                        CurriculumSubjectClassType entityClassType = new CurriculumSubjectClassType();
                        entityClassType.CurriculumSubjectID = entity.CurriculumSubjectID;
                        entityClassType.CurriculumClassTypeID = Convert.ToInt32(classTypeID);
                        entityClassTypeDao.Insert(entityClassType);
                    }
                }

                if (hdnLstMarkTypeID.Value != "")
                {
                    string[] lstMarkTypeID = hdnLstMarkTypeID.Value.Split(',');
                    foreach (string MarkTypeID in lstMarkTypeID)
                    {
                        CurriculumSubjectMarkType entityMarkType = new CurriculumSubjectMarkType();
                        entityMarkType.CurriculumSubjectID = entity.CurriculumSubjectID;
                        entityMarkType.CurriculumMarkTypeID = Convert.ToInt32(MarkTypeID);
                        entityMarkTypeDao.Insert(entityMarkType);
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
            CurriculumSubjectDao entityDao = new CurriculumSubjectDao(ctx);
            CurriculumSubjectClassTypeDao entityClassTypeDao = new CurriculumSubjectClassTypeDao(ctx);
            CurriculumSubjectMarkTypeDao entityMarkTypeDao = new CurriculumSubjectMarkTypeDao(ctx);
            try
            {
                CurriculumSubject entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                List<CurriculumSubjectClassType> lstEntityClassType = BusinessLayer.GetCurriculumSubjectClassTypeList(string.Format("CurriculumSubjectID = {0}", entity.CurriculumSubjectID), ctx);
                if (hdnLstClassTypeID.Value != "")
                {
                    string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                    foreach (string classTypeID in lstClassTypeID)
                    {
                        CurriculumSubjectClassType entityClassType = lstEntityClassType.FirstOrDefault(p => p.CurriculumClassTypeID.ToString() == classTypeID);
                        if (entityClassType == null)
                        {
                            entityClassType = new CurriculumSubjectClassType();
                            entityClassType.CurriculumSubjectID = entity.CurriculumSubjectID;
                            entityClassType.CurriculumClassTypeID = Convert.ToInt32(classTypeID);
                            entityClassTypeDao.Insert(entityClassType);
                        }
                        else
                            lstEntityClassType.Remove(entityClassType);
                    }
                }
                foreach (CurriculumSubjectClassType entityClassType in lstEntityClassType)
                {
                    entityClassTypeDao.Delete(entityClassType.CurriculumSubjectID, entityClassType.CurriculumClassTypeID);
                }

                List<CurriculumSubjectMarkType> lstEntityMarkType = BusinessLayer.GetCurriculumSubjectMarkTypeList(string.Format("CurriculumSubjectID = {0}", entity.CurriculumSubjectID), ctx);
                if (hdnLstMarkTypeID.Value != "")
                {
                    string[] lstMarkTypeID = hdnLstMarkTypeID.Value.Split(',');
                    foreach (string MarkTypeID in lstMarkTypeID)
                    {
                        CurriculumSubjectMarkType entityMarkType = lstEntityMarkType.FirstOrDefault(p => p.CurriculumMarkTypeID.ToString() == MarkTypeID);
                        if (entityMarkType == null)
                        {
                            entityMarkType = new CurriculumSubjectMarkType();
                            entityMarkType.CurriculumSubjectID = entity.CurriculumSubjectID;
                            entityMarkType.CurriculumMarkTypeID = Convert.ToInt32(MarkTypeID);
                            entityMarkTypeDao.Insert(entityMarkType);
                        }
                        else
                            lstEntityMarkType.Remove(entityMarkType);
                    }
                }
                foreach (CurriculumSubjectMarkType entityMarkType in lstEntityMarkType)
                {
                    entityMarkTypeDao.Delete(entityMarkType.CurriculumSubjectID, entityMarkType.CurriculumMarkTypeID);
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
                CurriculumSubject entity = BusinessLayer.GetCurriculumSubject(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculumSubject(entity);
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