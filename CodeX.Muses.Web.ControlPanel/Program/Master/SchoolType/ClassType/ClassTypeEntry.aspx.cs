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
    public partial class ClassTypeEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.ST_CLASS_TYPE;
        }
        protected override void InitializeDataControl()
        {
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("GCSchoolType = '{0}' ORDER BY DisplayOrder", AppSession.SchoolTypeID));
            List<vSchoolMajor> lstMajor = BusinessLayer.GetvSchoolMajorList(string.Format("GCSchoolType = '{0}'", AppSession.SchoolTypeID));
            lstMajor.Insert(0, new vSchoolMajor { GCMajor = "", Major = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField<vSchoolMajor>(cboMajor, lstMajor, "Major", "GCMajor");

            BindGridView();

            Helper.SetControlEntrySetting(txtClassTypeCode, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtClassTypeName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("GCSchoolType = '{0}' AND GCClassStudyType = '{1}' AND IsDeleted = 0 ORDER BY ClassTypeCode", AppSession.SchoolTypeID, Constant.ClassStudyType.REGULAR);
            grdView.DataSource = BusinessLayer.GetvClassTypeList(filterExpression);
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

        private void ControlToEntity(ClassType entity)
        {
            entity.ClassTypeCode = txtClassTypeCode.Text;
            entity.ClassTypeName = txtClassTypeName.Text;
            entity.GCGrade = cboGrade.Value.ToString();
            if (cboMajor.Value != null && cboMajor.Value.ToString() != "")
                entity.GCMajor = cboMajor.Value.ToString();
            else
                entity.GCMajor = null;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassTypeDao entityDao = new ClassTypeDao(ctx);
            try
            {
                ClassType entity = new ClassType();
                ControlToEntity(entity);
                entity.GCSchoolType = AppSession.SchoolTypeID;
                entity.GCClassStudyType = Constant.ClassStudyType.REGULAR;
                entity.CreatedBy = AppSession.UserLogin.UserID;
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
            ClassTypeDao entityDao = new ClassTypeDao(ctx);
            try
            {
                ClassType entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
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
                ClassType entity = BusinessLayer.GetClassType(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateClassType(entity);
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