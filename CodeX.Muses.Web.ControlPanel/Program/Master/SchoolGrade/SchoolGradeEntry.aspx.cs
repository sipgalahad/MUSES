using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SchoolGradeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SCHOOL_GRADE;
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstGrade = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1",Constant.StandardCode.SCHOOL_GRADE));
            Methods.SetComboBoxField(cboGrade, lstGrade, "StandardCodeName", "StandardCodeID");

            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                String filterExpression = String.Format("GCGrade = '{0}'", ID);
                vSchoolGrade entity = BusinessLayer.GetvSchoolGradeList(filterExpression)[0];
                cboGrade.Enabled = false;
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            
            txtDisplayOrder.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vSchoolGrade entity)
        {
            cboGrade.Value = entity.GCGrade;
            txtDisplayOrder.Text = entity.DisplayOrder.ToString();
        }

        private void ControlToEntity(SchoolGrade entity)
        {
            entity.GCGrade = cboGrade.Value.ToString();
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("GCGrade = '{0}'", cboGrade.Value);
            List<SchoolGrade> lst = BusinessLayer.GetSchoolGradeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Grade with Code " + cboGrade.Value + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("GCGrade = '{0}' AND DisplayOrder != {1}", cboGrade.Value, txtDisplayOrder.Text);
            List<SchoolGrade> lst = BusinessLayer.GetSchoolGradeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Display Order with order " + txtDisplayOrder.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolGradeDao entityDao = new SchoolGradeDao(ctx);
            
            bool result = false;
            try
            {
                SchoolGrade entity = new SchoolGrade();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entityDao.Insert(entity);

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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SchoolGradeDao entityDao = new SchoolGradeDao(ctx);
            try
            {
                SchoolGrade entity = entityDao.Get(AppSession.UserLogin.SiteID,cboGrade.Value.ToString());
                ControlToEntity(entity);
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}