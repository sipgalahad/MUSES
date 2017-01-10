using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Web.Security;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;

namespace CodeX.Muses.Web.Mobile.Controls
{
    public partial class ChangePasswordCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            txtOldPassword.Attributes.Add("validationgroup", "mpChangePassword");
            txtNewPassword.Attributes.Add("validationgroup", "mpChangePassword");
            txtConfirmPassword.Attributes.Add("validationgroup", "mpChangePassword");
        }

        protected void cbpChangePasswordProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string oldPassword = txtOldPassword.Text;
            Student user = BusinessLayer.GetStudent(AppSession.StudentLogin.UserID);
            if (user.Password.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "sha1"))
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtConfirmPassword.Text, "sha1");
                BusinessLayer.UpdateStudent(user);
                result = "success";
            }
            else
                result = "fail|Old Password is invalid, try again";
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}