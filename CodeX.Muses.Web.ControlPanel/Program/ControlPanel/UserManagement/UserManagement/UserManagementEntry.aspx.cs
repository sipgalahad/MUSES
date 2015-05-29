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
using System.Web.Security;
using System.Web.UI.HtmlControls;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class UserManagementEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.USER_ACCOUNTS;
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String userID = Request.QueryString["id"];
                hdnID.Value = userID;
                vUser entity = BusinessLayer.GetvUserList(string.Format("UserID = {0}", userID))[0];
                UserTagField entityTagField = BusinessLayer.GetUserTagField(entity.UserID);
                EntityToControl(entity, entityTagField);
                divCopyUser.Visible = false;
            }
            else
            {
                IsAdd = true;
                divCopyUser.Visible = true;
            }
            txtUserName.Focus();
        }

        public override void OnAddRecord()
        {
            divCopyUser.Visible = true;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtUserName, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtFullName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmail, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPassword, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtConfirmPassword, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtMobilePIN, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtConfirmMobilePIN, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSecurityQuestion, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSecurityAnswer, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(tacEmployee, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacCopyFromUser, new ControlEntrySetting(true, false, false));
        }

        protected override void OnReInitControl()
        {
            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                txt.Text = "";
            }
            #endregion
        }

        private void EntityToControl(vUser entity, UserTagField entityTagField)
        {
            txtUserName.Text = entity.UserName;
            txtFullName.Text = entity.FullName;
            txtEmail.Text = entity.Email;
            txtPassword.Text = "hidden";
            txtConfirmPassword.Text = "hidden";
            txtMobilePIN.Text = "hidden";
            txtMobilePIN.Text = "hidden";
            txtSecurityQuestion.Text = entity.PasswordQuestion;
            txtSecurityAnswer.Text = "hidden";
            tacEmployee.Value = entity.EmployeeID.ToString();
            tacEmployee.Text = entity.EmployeeName;

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                txt.Text = entityTagField.GetType().GetProperty("TagField" + hdn.Value).GetValue(entityTagField, null).ToString();
            }
            #endregion
        }

        private void ControlToEntity(User entity, UserAttribute entityAttribute, UserTagField entityTagField)
        {
            entity.UserName = txtUserName.Text;
            entity.LoweredUserName = entity.UserName.ToLower();
            entityAttribute.FullName = txtFullName.Text;
            entity.Email = txtEmail.Text;
            entity.LoweredEmail = entity.Email.ToLower();
            entity.PasswordQuestion = txtSecurityQuestion.Text;
            if (tacEmployee.Value == "" || tacEmployee.Value == "0")
                entityAttribute.EmployeeID = null;
            else
                entityAttribute.EmployeeID = Convert.ToInt32(tacEmployee.Value);

            #region Custom Attribute
            foreach (RepeaterItem item in rptCustomAttribute.Items)
            {
                TextBox txt = (TextBox)item.FindControl("txtTagField");
                HtmlInputHidden hdn = (HtmlInputHidden)item.FindControl("hdnTagFieldCode");
                entityTagField.GetType().GetProperty("TagField" + hdn.Value).SetValue(entityTagField, txt.Text, null);
            }
            #endregion
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("UserName = '{0}'", txtUserName.Text);
            List<User> lst = BusinessLayer.GetUserList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " User Name is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserDao entityDao = new UserDao(ctx);
            UserAttributeDao entityAttributeDao = new UserAttributeDao(ctx);
            UserTagFieldDao entityTagFieldDao = new UserTagFieldDao(ctx);
            try
            {
                User entity = new User();
                UserAttribute entityAttribute = new UserAttribute();
                UserTagField entityTagField = new UserTagField();
                ControlToEntity(entity, entityAttribute, entityTagField);

                entity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                entity.MobilePIN = FormsAuthentication.HashPasswordForStoringInConfigFile(txtMobilePIN.Text, "sha1");
                entity.PasswordAnswer = FormsAuthentication.HashPasswordForStoringInConfigFile(txtSecurityAnswer.Text, "sha1");

                entityDao.Insert(entity);

                entityAttribute.UserID = BusinessLayer.GetUserMaxID(ctx);
                entityAttribute.CreatedBy = AppSession.UserLogin.UserID;
                entityAttributeDao.Insert(entityAttribute);

                entityTagField.UserID = entityAttribute.UserID;
                entityTagField.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityTagFieldDao.Insert(entityTagField);

                retval = entityAttribute.UserID.ToString();

                #region copyUser
                if (tacCopyFromUser.Value != "")
                {
                    #region UserInRole
                    List<UserInRole> entityUserInRoleFromlst = BusinessLayer.GetUserInRoleList(string.Format("UserID = {0}", tacCopyFromUser.Value));
                    UserInRoleDao entityUserInRoleDao = new UserInRoleDao(ctx);
                    foreach (UserInRole entityNew in entityUserInRoleFromlst)
                    {
                        entityNew.UserID = entityAttribute.UserID;
                        entityUserInRoleDao.Insert(entityNew);
                    }
                    #endregion

                    #region UserMenu
                    List<UserMenu> entityUserMenuFromlst = BusinessLayer.GetUserMenuList(string.Format("UserID = {0}", tacCopyFromUser.Value));
                    UserMenuDao entityUserMenuDao = new UserMenuDao(ctx);
                    foreach (UserMenu entityNew in entityUserMenuFromlst)
                    {
                        entityNew.UserID = entityAttribute.UserID;
                        entityNew.CreatedBy = AppSession.UserLogin.UserID;
                        entityUserMenuDao.Insert(entityNew);
                    }
                    #endregion

                    #region LoginAttribute
                    List<UserLoginAttribute> entityUserLoginAttributeFromlst = BusinessLayer.GetUserLoginAttributeList(string.Format("UserID = {0}", tacCopyFromUser.Value));
                    UserLoginAttributeDao entityUserLoginAttributeDao = new UserLoginAttributeDao(ctx);
                    foreach (UserLoginAttribute entityNew in entityUserLoginAttributeFromlst)
                    {
                        entityNew.UserID = entityAttribute.UserID;
                        entityUserLoginAttributeDao.Insert(entityNew);
                    }
                    #endregion

                    //#region ServiceUnitUser
                    //List<ServiceUnitUser> entityServiceUnitFromlst = BusinessLayer.GetServiceUnitUserList(string.Format("UserID = {0}", tacCopyFromUser.Value));
                    //ServiceUnitUserDao entityServiceUnitUserDao = new ServiceUnitUserDao(ctx);
                    //foreach (ServiceUnitUser entityNew in entityServiceUnitFromlst)
                    //{
                    //    entityNew.UserID = entityAttribute.UserID;
                    //    entityNew.CreatedBy = AppSession.UserLogin.UserID;
                    //    entityServiceUnitUserDao.Insert(entityNew);
                    //}
                    //#endregion

                    #region LocationUser
                    List<LocationUser> entityLocationUserFromlst = BusinessLayer.GetLocationUserList(string.Format("UserID = {0}", tacCopyFromUser.Value));
                    LocationUserDao entityLocationUserDao = new LocationUserDao(ctx);
                    foreach (LocationUser entityNew in entityLocationUserFromlst)
                    {
                        entityNew.UserID = entityAttribute.UserID;
                        entityNew.CreatedBy = AppSession.UserLogin.UserID;
                        entityLocationUserDao.Insert(entityNew);
                    }
                    #endregion
                }
                #endregion

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
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
            UserDao entityDao = new UserDao(ctx);
            UserAttributeDao entityAttributeDao = new UserAttributeDao(ctx);
            UserTagFieldDao entityTagFieldDao = new UserTagFieldDao(ctx);
            try
            {
                Int32 UserID = Convert.ToInt32(hdnID.Value);
                User entity = entityDao.Get(UserID);
                UserAttribute entityAttribute = entityAttributeDao.Get(UserID);
                UserTagField entityTagField = entityTagFieldDao.Get(UserID);
                ControlToEntity(entity, entityAttribute, entityTagField);
                entityDao.Update(entity);

                entityAttribute.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityAttributeDao.Update(entityAttribute);

                entityTagField.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityTagFieldDao.Update(entityTagField);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (pnlCustomAttribute.Visible)
            {
                List<Variable> ListCustomAttribute = initListCustomAttribute();
                if (ListCustomAttribute.Count == 0)
                    pnlCustomAttribute.Visible = false;
                else
                {
                    rptCustomAttribute.DataSource = ListCustomAttribute;
                    rptCustomAttribute.DataBind();
                }
            }
        }

        private List<Variable> initListCustomAttribute()
        {
            List<Variable> ListCustomAttribute = new List<Variable>();
            TagField tagField = BusinessLayer.GetTagField(Constant.BusinessObjectType.USER);
            if (tagField != null)
            {
                if (tagField.TagField1 != "") { ListCustomAttribute.Add(new Variable { Code = "1", Value = tagField.TagField1 }); }
                if (tagField.TagField2 != "") { ListCustomAttribute.Add(new Variable { Code = "2", Value = tagField.TagField2 }); }
                if (tagField.TagField3 != "") { ListCustomAttribute.Add(new Variable { Code = "3", Value = tagField.TagField3 }); }
                if (tagField.TagField4 != "") { ListCustomAttribute.Add(new Variable { Code = "4", Value = tagField.TagField4 }); }
                if (tagField.TagField5 != "") { ListCustomAttribute.Add(new Variable { Code = "5", Value = tagField.TagField5 }); }
                if (tagField.TagField6 != "") { ListCustomAttribute.Add(new Variable { Code = "6", Value = tagField.TagField6 }); }
                if (tagField.TagField7 != "") { ListCustomAttribute.Add(new Variable { Code = "7", Value = tagField.TagField7 }); }
                if (tagField.TagField8 != "") { ListCustomAttribute.Add(new Variable { Code = "8", Value = tagField.TagField8 }); }
                if (tagField.TagField9 != "") { ListCustomAttribute.Add(new Variable { Code = "9", Value = tagField.TagField9 }); }
                if (tagField.TagField10 != "") { ListCustomAttribute.Add(new Variable { Code = "10", Value = tagField.TagField10 }); }
                if (tagField.TagField11 != "") { ListCustomAttribute.Add(new Variable { Code = "11", Value = tagField.TagField11 }); }
                if (tagField.TagField12 != "") { ListCustomAttribute.Add(new Variable { Code = "12", Value = tagField.TagField12 }); }
                if (tagField.TagField13 != "") { ListCustomAttribute.Add(new Variable { Code = "13", Value = tagField.TagField13 }); }
                if (tagField.TagField14 != "") { ListCustomAttribute.Add(new Variable { Code = "14", Value = tagField.TagField14 }); }
                if (tagField.TagField15 != "") { ListCustomAttribute.Add(new Variable { Code = "15", Value = tagField.TagField15 }); }
                if (tagField.TagField16 != "") { ListCustomAttribute.Add(new Variable { Code = "16", Value = tagField.TagField16 }); }
                if (tagField.TagField17 != "") { ListCustomAttribute.Add(new Variable { Code = "17", Value = tagField.TagField17 }); }
                if (tagField.TagField18 != "") { ListCustomAttribute.Add(new Variable { Code = "18", Value = tagField.TagField18 }); }
                if (tagField.TagField19 != "") { ListCustomAttribute.Add(new Variable { Code = "19", Value = tagField.TagField19 }); }
                if (tagField.TagField20 != "") { ListCustomAttribute.Add(new Variable { Code = "20", Value = tagField.TagField20 }); }
            }
            return ListCustomAttribute;
        }
    }
}