using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class SendEmailCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            List<TemplateText> lstTemplateText = BusinessLayer.GetTemplateTextList(String.Format("IsDeleted = 0 AND GCTemplateGroup = '{0}'", Constant.TemplateGroup.EMAIL));
            Methods.SetComboBoxField(cboTemplate, lstTemplateText, "TemplateName", "TemplateID");
            cboTemplate.SelectedIndex = 0;
            
            List<Employee> lstEmployee = BusinessLayer.GetEmployeeList(String.Format("EmployeeID IN ({0})", param));

            txtTo.Text = String.Join(",",lstEmployee.Select(x => x.EmailAddress1));
            txtContent.Text = lstTemplateText[0].TemplateContent;
        }

        protected string OnGetFilterExpression()
        {
            string filterExpression = String.Format("ProjectTaskID = {0} AND IsDeleted = 0",hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            string filterExpression = OnGetFilterExpression();
            //grdPopupView.DataSource = BusinessLayer.GetvProjectTaskFileList(filterExpression);
            //grdPopupView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";

            if (param[0] == "email")
            {
                if (OnPopupSendEmail(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnPopupSendEmail(ref string errMessage)
        {
            bool result = true;
            try
            {
                string emailFrom = "agireza@yahoo.com";
                string password = "";
                Employee user = BusinessLayer.GetEmployee(Convert.ToInt32(AppSession.UserLogin.EmployeeID));
                if (user != null)
                {
                    emailFrom = user.EmailAddress1 != "" ? user.EmailAddress1 : user.EmailAddress2;
                    password = "";
                }

                //string emailTo = String.Join(";", lstEmployee.Select(x => x.EmailAddress1));
                string subject = txtSubject.Text;
                string body = hdnEmailMessage.Value;

                string smtpAddress = GetSmtpAddress(emailFrom);
                int portNumber = GetPort(emailFrom);
                bool enableSSL = true;

                #region Send Email
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    String[] lstTo = txtTo.Text.Split(',');
                    foreach(string email in lstTo)
                        mail.To.Add(email);

                    mail.Subject = subject;
                    mail.Body = body;//ConvertMessage(emailTo, body);
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        public String GetSmtpAddress(String email)
        {
            String[] data = email.Split('@');
            String SmtpAddress = "";
            switch (data[1])
            {
                case "hotmail.com": SmtpAddress = string.Format("smtp.live.com"); break;
                case "gmail.com": SmtpAddress = string.Format("smtp.gmail.com"); break;
                case "yahoo.com": SmtpAddress = string.Format("smtp.mail.yahoo.com"); break;
                default: SmtpAddress = String.Format("smtp.{0}", data[1]); break;
            }
            return SmtpAddress;
        }

        public Int32 GetPort(String email)
        {
            String[] data = email.Split('@');
            Int32 port = 0;
            switch (data[1])
            {
                case "hotmail.com": port = 587; break;
                case "gmail.com": port = 587; break;
                case "yahoo.com": port = 587; break;
                default: port = 25; break;
            }
            return port;
        }
        #endregion
    }
}