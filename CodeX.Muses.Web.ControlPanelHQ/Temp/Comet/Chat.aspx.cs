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
    public partial class Chat : BasePage
    {
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter == "send")
            {
                // Create a message entity to contain all necessary data.
                Message message = new Message();
                message.SenderName = txtSenderName.Text.Trim();
                message.RecipientName = tbRecipientName.Text.Trim();
                message.MessageContent = tbMessageContent.Text.Trim();

                if (!string.IsNullOrWhiteSpace(message.RecipientName) && !string.IsNullOrEmpty(message.MessageContent))
                {
                    // Call the client adapter to send the message to the particular recipient instantly.
                    ClientAdapter.Instance.SendMessage(message);

                    // Display a timestamp.
                }
            }
            else
            {
                string userName = txtSenderName.Text.Trim();

                // Join into the recipient list.
                if (!string.IsNullOrEmpty(userName))
                {
                    ClientAdapter.Instance.Join(userName);
                }
            }

            ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
            panel.JSProperties["cpResult"] = e.Parameter;
        }
    }
}