using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using System.Web.Security;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CSASPNETReverseAJAX
{
    public partial class Receiver : BasePage
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = tbUserName.Text.Trim();

            // Join into the recipient list.
            if (!string.IsNullOrEmpty(userName))
            {
                ClientAdapter.Instance.Join(userName);

                Session["userName"] = userName;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Activate the JavaScript waiting loop.
            if (Session["userName"] != null)
            {
                string userName = (string)Session["userName"];

                lbNotification.Text = string.Format("Your user name is <b>{0}</b>. It is waiting for new message now.", userName);

                // Disable the login.
                tbUserName.Visible = false;
                btnLogin.Visible = false;
            }
        }
    }
}