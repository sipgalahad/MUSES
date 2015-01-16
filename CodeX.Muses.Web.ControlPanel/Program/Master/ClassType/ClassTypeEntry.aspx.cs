using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class ClassTypeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CLASS_TYPE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                ClassType entity = BusinessLayer.GetClassType(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtClassTypeCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<vSchoolGrade> lstGrade = BusinessLayer.GetvSchoolGradeList(string.Format("SiteID = '{0}' ORDER BY DisplayOrder", AppSession.UserLogin.SiteID));
            List<vSchoolMajor> lstMajor = BusinessLayer.GetvSchoolMajorList(string.Format("SiteID = '{0}'", AppSession.UserLogin.SiteID));
            lstMajor.Insert(0, new vSchoolMajor { GCMajor = "", Major = "" });
            Methods.SetComboBoxField<vSchoolGrade>(cboGrade, lstGrade, "Grade", "GCGrade");
            Methods.SetComboBoxField<vSchoolMajor>(cboMajor, lstMajor, "Major", "GCMajor");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtClassTypeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtClassTypeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGrade, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboMajor, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ClassType entity)
        {
            txtClassTypeCode.Text = entity.ClassTypeCode;
            txtClassTypeName.Text = entity.ClassTypeName;
            cboGrade.Value = entity.GCGrade;
            cboMajor.Value = entity.GCMajor;
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

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ClassTypeCode = '{0}'", txtClassTypeCode.Text);
            List<ClassType> lst = BusinessLayer.GetClassTypeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Tipe Kelas Dengan Kode " + txtClassTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ClassTypeCode = '{0}' AND ClassTypeID != {1}", txtClassTypeCode.Text, hdnID.Value);
            List<ClassType> lst = BusinessLayer.GetClassTypeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Tipe Kelas Dengan Kode " + txtClassTypeCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ClassTypeDao entityDao = new ClassTypeDao(ctx);
            bool result = false;
            try
            {
                ClassType entity = new ClassType();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetClassTypeMaxID(ctx).ToString();
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
            try
            {
                ClassType entity = BusinessLayer.GetClassType(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
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
    }
}