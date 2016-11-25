using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Globalization;
using CodeX.Data.Core.Dal;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace CodeX.Muses.Web.HumanResource.Program
{
    public partial class EmployeeDailyAttendanceEntry : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.EMPLOYEE_DAILY_ATTENDANCE;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtDate.Text = DateTime.Now.AddDays(-1).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        public void UploadFile(String data, ref string errMessage) 
        {
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            String data = GetDataFromFile();
            UploadFile(data, ref errMessage);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpErrorMessage"] = errMessage;
        }

        public String GetDataFromFile() 
        {
            string imageData = hdnUploadedFile1.Value;
            if (imageData != "")
            {
                string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                imageData = String.Join(",", parts);
            }

            byte[] data = Convert.FromBase64String(imageData);
            var stream = new StreamReader(new MemoryStream(data));
            string text = stream.ReadToEnd();
            return text;
        }

        private String ChangeSpace(String Data) 
        {
            //String temp = "";
            Data = Data.Replace("\r\n", "|");
            Char[] tempChar = Data.ToCharArray();
            for (int i = 0; i < tempChar.Count(); i++) 
            {
                if ((i > 0 && (tempChar[i - 1] == ' ' || tempChar[i - 1] == '_') && tempChar[i] == ' ') || (i < tempChar.Count() - 1 && tempChar[i + 1] == ' ' && tempChar[i] == ' ')) 
                {
                    tempChar[i] = '_';
                }
            }
            return new String(tempChar);
        }
    }
}