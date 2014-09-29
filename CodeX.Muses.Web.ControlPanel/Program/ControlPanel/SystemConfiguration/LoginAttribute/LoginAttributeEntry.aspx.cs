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
    public partial class LoginAttributeEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.LOGIN_ATTRIBUTE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                LoginAttribute entity = BusinessLayer.GetLoginAttribute(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtLoginAttributeCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtLoginAttributeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLoginAttributeName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSessionName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMethodName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFilterExpression, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtValueFieldName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTextFieldName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDefaultValue, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(LoginAttribute entity)
        {
            txtLoginAttributeCode.Text = entity.LoginAttributeCode;
            txtLoginAttributeName.Text = entity.LoginAttributeName;
            txtSessionName.Text = entity.SessionName;
            txtMethodName.Text = entity.MethodName;
            txtFilterExpression.Text = entity.FilterExpression;
            txtValueFieldName.Text = entity.ValueFieldName;
            txtTextFieldName.Text = entity.TextFieldName;
            txtDefaultValue.Text = entity.DefaultValue;
        }

        private void ControlToEntity(LoginAttribute entity)
        {
            entity.LoginAttributeCode = txtLoginAttributeCode.Text;
            entity.LoginAttributeName = txtLoginAttributeName.Text;
            entity.SessionName = txtSessionName.Text;
            entity.MethodName = txtMethodName.Text;
            entity.FilterExpression = txtFilterExpression.Text;
            entity.ValueFieldName = txtValueFieldName.Text;
            entity.TextFieldName = txtTextFieldName.Text;
            entity.DefaultValue = txtDefaultValue.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("LoginAttributeCode = '{0}'", txtLoginAttributeCode.Text);
            List<LoginAttribute> lst = BusinessLayer.GetLoginAttributeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Login Attribute With Code " + txtLoginAttributeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("LoginAttributeCode = '{0}' AND LoginAttributeID != {1}", txtLoginAttributeCode.Text, hdnID.Value);
            List<LoginAttribute> lst = BusinessLayer.GetLoginAttributeList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Login Attribute With Code " + txtLoginAttributeCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            LoginAttributeDao entityDao = new LoginAttributeDao(ctx);
            bool result = false;
            try
            {
                LoginAttribute entity = new LoginAttribute();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetLoginAttributeMaxID(ctx).ToString();
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
                LoginAttribute entity = BusinessLayer.GetLoginAttribute(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateLoginAttribute(entity);
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