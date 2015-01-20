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
    public partial class SchoolMajorEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SCHOOL_MAJOR;
        }

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstGrade = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1",Constant.StandardCode.SCHOOL_MAJOR));
            Methods.SetComboBoxField(cboMajor, lstGrade, "StandardCodeName", "StandardCodeID");

            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                String filterExpression = String.Format("GCMajor = '{0}'", ID);
                vSchoolMajor entity = BusinessLayer.GetvSchoolMajorList(filterExpression)[0];
                cboMajor.Enabled = false;
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboMajor, new ControlEntrySetting(true, false, true));
        }

        private void EntityToControl(vSchoolMajor entity)
        {
            cboMajor.Value = entity.GCMajor;
        }

        private void ControlToEntity(SchoolMajor entity)
        {
            entity.GCMajor = cboMajor.Value.ToString();
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("GCMajor = '{0}'", cboMajor.Value);
            List<SchoolMajor> lst = BusinessLayer.GetSchoolMajorList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Major with Code " + cboMajor.Value + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            //string FilterExpression = string.Format("GCGrade = '{0}' AND DisplayOrder != {1}", cboGrade.Value, txtDisplayOrder.Text);
            //List<SchoolMajor> lst = BusinessLayer.GetSchoolMajorList(FilterExpression);

            //if (lst.Count > 0)
            //    errMessage = " Display Order with order " + txtDisplayOrder.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SchoolMajorDao entityDao = new SchoolMajorDao(ctx);
            
            bool result = false;
            try
            {
                SchoolMajor entity = new SchoolMajor();
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
            SchoolMajorDao entityDao = new SchoolMajorDao(ctx);
            try
            {
                SchoolMajor entity = entityDao.Get(AppSession.UserLogin.SiteID, cboMajor.Value.ToString());
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