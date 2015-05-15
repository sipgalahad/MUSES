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
    public partial class SchoolSubjectEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                return Constant.MenuCode.ControlPanel.ST_EXTRACURRICULAR_SUBJECT;
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.PERSONALITY)
                return Constant.MenuCode.ControlPanel.ST_PERSONALITY_SUBJECT;
            return Constant.MenuCode.ControlPanel.ST_SUBJECT;
        }
        protected string OnGetSubjectFilterExpression()
        {
            return string.Format("GCClassStudyType = '{0}' AND IsDeleted = 0 AND SubjectID NOT IN (SELECT SubjectID FROM SchoolSubject WHERE GCSchoolType = '{1}' AND IsDeleted = 0)", hdnGCClassStudyType.Value, AppSession.SchoolTypeID);
        }
        protected override void InitializeDataControl()
        {
            if (Page.Request.QueryString["id"] == "ex")
                hdnGCClassStudyType.Value = Constant.ClassStudyType.EXTRACURRICULAR;
            else if (Page.Request.QueryString["id"] == "pr")
                hdnGCClassStudyType.Value = Constant.ClassStudyType.PERSONALITY;
            else
                hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;

            BindGridView();

            Helper.SetControlEntrySetting(tacSubject, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("GCSchoolType = '{0}' AND GCClassStudyType = '{1}' ORDER BY DisplayOrder", AppSession.SchoolTypeID, hdnGCClassStudyType.Value);
            grdView.DataSource = BusinessLayer.GetvSchoolSubjectList(filterExpression);
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

        private void ControlToEntity(SchoolSubject entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SchoolSubjectDao entityDao = new SchoolSubjectDao(ctx);
            try
            {
                SchoolSubject entity = new SchoolSubject();
                ControlToEntity(entity);
                entity.SubjectID = Convert.ToInt32(tacSubject.Value);
                entity.GCSchoolType = AppSession.SchoolTypeID;
                entityDao.Insert(entity);
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
            SchoolSubjectDao entityDao = new SchoolSubjectDao(ctx);
            try
            {
                SchoolSubject entity = entityDao.Get(AppSession.SchoolTypeID, Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entityDao.Update(entity);
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
                BusinessLayer.DeleteSchoolSubject(AppSession.SchoolTypeID, Convert.ToInt32(hdnEntryID.Value));
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