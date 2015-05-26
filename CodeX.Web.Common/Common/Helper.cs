using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using System.IO;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Web.CustomControl;

namespace CodeX.Web.Common
{
    public partial class Helper
    {
        public static void ExportExcel(string fileName, string title, Control exportControl, TemplateControl page, bool isShowTitle)
        {
            Control control = exportControl;
            HtmlGenericControl div = new HtmlGenericControl("DIV");
            if (isShowTitle)
            {
                HtmlGenericControl h1Title = new HtmlGenericControl("h1");
                h1Title.InnerHtml = title;
                div.Controls.Add(h1Title);
            }
            div.Controls.Add(control);

            //Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //div.RenderControl(htmlWrite);
            ////Response.Write(stringWrite.ToString());
            //Response.Write("<html><head><style type='text/css'>.grdView > tbody > tr > td {color:green; border:1px solid;}</style></head>" + stringWrite.ToString() + "</html>");
            //Response.End();


            string attachment = string.Format("attachment;filename=\"{0}.xls\"", title);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            div.RenderControl(htextw);
            HttpContext.Current.Response.Write(stw.ToString());
            FileInfo fi = new FileInfo(HttpContext.Current.Request.MapPath(page.ResolveUrl("~/Libs/Styles/excel.css")));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());
            }
            sr.Close();
            HttpContext.Current.Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
            stw = null;
            htextw = null;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        public static String GenerateCode(String formatCode, Int32 ID)
        {
            int count = 0;
            for (int i = 0; i < formatCode.Length; ++i)
                if (formatCode[i] == '*')
                    count++;

            string tempCode = ID.ToString().PadLeft(count, '0');
            StringBuilder result = new StringBuilder();

            int ctrTempCode = 0;
            for (int i = 0; i < formatCode.Length; ++i)
            {
                char c = formatCode[i];
                if (c == '*')
                    result.Append(tempCode[ctrTempCode++]);
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        public static String GetComboBoxValue(ASPxComboBox cbo, bool IsNullable)
        {
            if (IsNullable)
            {
                if (cbo.Value != null && cbo.Value.ToString() != "")
                    return cbo.Value.ToString();
                else
                    return null;
            }
            return cbo.Value.ToString();
        }

        public static DateTime InitializeDateTimeNull()
        {
            return new DateTime(1900, 1, 1);
        }

        public static XDocument LoadXMLFile(TemplateControl page, string xmlFileName)
        {
            string[] param = HttpContext.Current.Request.MapPath("~").Split('\\');
            var remStrings = param.Take(param.Length - 1);
            //string myXml = string.Format("{0}\\App_Data\\{1}", HttpContext.Current.Request.MapPath("~"), xmlFileName);
            //string myXml = string.Format("{0}\\CodeX.Web.CommonLibs\\App_Data\\{1}", string.Join("\\", remStrings), xmlFileName);
            string myXml = page.ResolveUrl("~/Libs/App_Data/") + xmlFileName;
            string physicalPath = HttpContext.Current.Request.MapPath(myXml);
            if (File.Exists(physicalPath))
            {
                XDocument xdoc = XDocument.Load(physicalPath);
                return xdoc;
            }
            return null;
        }

        public static string[] LoadTextFile(TemplateControl page, string textFileName)
        {
            string myText = page.ResolveUrl("~/Libs/App_Data/") + textFileName;
            return System.IO.File.ReadAllLines(HttpContext.Current.Request.MapPath(myText), Encoding.GetEncoding("windows-1250"));
        }

        #region Language
        public static string GetWordsLabel(List<Words> words, string code)
        {
            if (words == null)
                return code;
            Words word = words.FirstOrDefault(w => w.Code == code);
            return word == null ? code : word.Text;
        }

        public static List<Words> LoadWords(TemplateControl page)
        {
            XDocument xdoc = LoadXMLFile(page, "config.xml");
            var config = (from pg in xdoc.Descendants("page")
                          select new
                          {
                              Lang = pg.Attribute("lang").Value
                          }).FirstOrDefault();

            List<Words> words = new List<Words>();
            string[] tempWords = Helper.LoadTextFile(page, string.Format("lang/{0}.txt", config.Lang));
            foreach (string word in tempWords)
            {
                string[] param = word.Split(';');
                words.Add(new Words { Code = param[0], Text = param[1] });
            }
            return words;
        }
        #endregion

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }

            return null;
        }

        public static void AddCssClass(WebControl ctrl, string classname)
        {
            ctrl.CssClass = String.Join(" ", ctrl.CssClass.Split(' ').Except(new string[] { "", classname }).Concat(new string[] { classname }).ToArray());
        }

        public static void AddCssClass(HtmlGenericControl ctrl, string classname)
        {
            string cssClass = ctrl.Attributes["class"];
            ctrl.Attributes.Add("class", String.Join(" ", cssClass.Split(' ').Except(new string[] { "", classname }).Concat(new string[] { classname }).ToArray()));
        }

        public static void SetDropDownListValue(DropDownList ddl, object value)
        {
            if (value != null)
            {
                if (ddl.Items.FindByValue(value.ToString()) != null)
                {
                    ddl.ClearSelection();
                    ddl.Items.FindByValue(value.ToString()).Selected = true;
                }
            }
        }

        //public static String GetAge(List<Words> words, DateTime DoB)
        //{
        //    int ageInYear = Function.GetPatientAgeInYear(DoB, DateTime.Now);
        //    int ageInMonth = Function.GetPatientAgeInMonth(DoB, DateTime.Now);
        //    int ageInDay = Function.GetPatientAgeInDay(DoB, DateTime.Now);

        //    return string.Format("{0} {3}  {1} {4}  {2} {5}", ageInYear, ageInMonth, ageInDay, GetWordsLabel(words, "yr"), GetWordsLabel(words, "mo"), GetWordsLabel(words, "day"));
        //}

        #region Date
        public static DateTime GetDatePickerValue(TextBox txt)
        {
            return GetDatePickerValue(txt.Text);
        }

        public static DateTime GetDatePickerValue(String text)
        {
            if (text != "")
            {
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                return DateTime.ParseExact(text, "dd-MM-yyyy", culture);
            }
            return new DateTime(1900, 1, 1);
        }

        public static DateTime ConvertDateToString(string val, string format)
        {
            if (val != "")
            {
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                return DateTime.ParseExact(val, format, culture);
            }
            return new DateTime(1900, 1, 1);
        }
        #endregion

        #region Module
        public static string GetModuleName()
        {
            string[] param = HttpContext.Current.Request.ApplicationPath.Split('/');
            return param.Last();
        }
        #endregion

        public static void SetControlEntrySetting(Control ctrl, ControlEntrySetting setting, string ValidationGroup)
        {
            if (ctrl is ASPxEdit)
            {
                ASPxEdit ctl = ctrl as ASPxEdit;
                ctl.ValidationSettings.RequiredField.IsRequired = setting.IsRequired;
                ctl.ValidationSettings.RequiredField.ErrorText = "";
                ctl.ValidationSettings.CausesValidation = true;
                ctl.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.None;
                ctl.ValidationSettings.ErrorFrameStyle.Paddings.Padding = new System.Web.UI.WebControls.Unit(0);

                //if (setting.IsRequired)
                ctl.ValidationSettings.ValidationGroup = ValidationGroup;
            }
            else if (ctrl is CodeXAutoCompleteTextBox)
            {
                CodeXAutoCompleteTextBox tac = (CodeXAutoCompleteTextBox)ctrl;
                tac.ValidationGroup = ValidationGroup;
                tac.IsRequired = setting.IsRequired;
            }
            else if (ctrl is WebControl)
            {
                if (setting.IsRequired)
                    Helper.AddCssClass(((WebControl)ctrl), "required");
                ((WebControl)ctrl).Attributes.Add("validationgroup", ValidationGroup);
                if (setting.IsEditAbleInEditMode)
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
            else if (ctrl is HtmlGenericControl)
            {
                if (setting.IsEditAbleInEditMode)
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
        }

        private void SetControlAttribute(Control ctrl, bool isEnabled)
        {
            if (ctrl is ASPxEdit)
            {
                ((ASPxEdit)ctrl).ClientEnabled = isEnabled;
            }
            else if (ctrl is TextBox)
            {
                if (isEnabled)
                    ((TextBox)ctrl).ReadOnly = false;
                else
                    ((TextBox)ctrl).ReadOnly = true;
            }
            else if (ctrl is DropDownList)
            {
                ((DropDownList)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is HtmlGenericControl)
            {
                HtmlGenericControl lbl = ctrl as HtmlGenericControl;
                if (!isEnabled)
                    lbl.Attributes.Add("class", "lblDisabled");
            }
        }

        public static String GetHTMLEditorText(TextBox txt)
        {
            return HttpUtility.HtmlDecode(txt.Text);
        }

        public static int GetPageCount(int RowCount, double pageSize = 16.0)
        {
            double pageCount = RowCount / pageSize;
            return (int)Math.Ceiling(pageCount);
        }

        public static DateTime DateInStringToDateTime(string value)
        {
            DateTime theTime = DateTime.ParseExact(value,
                                        "yyyyMMdd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
            return theTime;
        }

        public static String GetErrorMessageText(TemplateControl page, string code)
        {
            XDocument xdoc = LoadXMLFile(page, "config.xml");
            var config = (from pg in xdoc.Descendants("page")
                          select new
                          {
                              Lang = pg.Attribute("lang").Value
                          }).FirstOrDefault();

            string[] tempWords = Helper.LoadTextFile(page, string.Format("err_message/{0}.txt", config.Lang));
            foreach (string word in tempWords)
            {
                string[] param = word.Split(';');
                if (param[0] == code)
                    return param[1];
            }
            return "";
        }

        #region Name
        public static String GenerateFullName(String _Name, String _Title, String _Suffix)
        {
            StringBuilder result = new StringBuilder("Title Name, Suffix");
            result = result.Replace("Title", _Title).
                Replace("Name", _Name).
                Replace("Suffix", _Suffix).
                Replace(",  ", "").
                Replace("  ", " ");
            return result.ToString().TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ',', ' ' });
        }

        public static String GenerateName(String _LastName, String _MiddleName, String _FirstName)
        {
            StringBuilder result = new StringBuilder(AppConfigManager.CDXNameFormat);
            result = result.Replace("LastName", _LastName).
                Replace("LASTNAME", _LastName.ToUpper()).
                Replace("MiddleName", _MiddleName).
                Replace("MIDDLENAME", _MiddleName.ToUpper()).
                Replace("FirstName", _FirstName).
                Replace("FIRSTNAME", _FirstName.ToUpper()).
                Replace(",  ", "").
                Replace("  ", " ");
            return result.ToString().TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ',', ' ' });
        }
        #endregion


        #region Error Log
        public static void InsertErrorLog()
        {
            // Code that runs when an unhandled error occurs
            HttpServerUtility server = HttpContext.Current.Server;
            Exception exception = server.GetLastError();
            InsertErrorLog(exception);
        }

        public static void InsertErrorLog(Exception exception)
        {
            // Code that runs when an unhandled error occurs
            Exception baseException = exception.GetBaseException();
            if (baseException != null)
                exception = baseException;
            string userIP = HttpContext.Current.Request.UserHostAddress;
            string appPath = HttpContext.Current.Request.Url.AbsolutePath;
            string trace = RemoveLineEndings(exception.StackTrace);
            string moduleName = Helper.GetModuleName();
            string ModuleID = Helper.GetModuleID(moduleName);

            string message = string.Format("{0}|{1}|{2}|{3}|{4}|{5}{6}", DateTime.Now.ToString(Constant.FormatString.TIME_FORMAT), ModuleID, userIP, appPath, RemoveLineEndings1(exception.Message), trace, Environment.NewLine);

            string path = VirtualPathUtility.ToAbsolute("~/Libs/App_Data/log");
            string physicalPath = HttpContext.Current.Request.MapPath(path);
            if (!Directory.Exists(physicalPath))
                Directory.CreateDirectory(physicalPath);

            string myFile = string.Format("{0}\\{1}.txt", physicalPath, DateTime.Now.ToString("yyyyMMdd"));

            if (!File.Exists(myFile))
                File.WriteAllText(myFile, message);
            else
                File.AppendAllText(myFile, message);
        }

        private static string RemoveLineEndings(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            string replaceChar = "%^%";
            return value.Replace("\r\n", replaceChar).Replace("\n", replaceChar).Replace("\r", replaceChar).Replace(lineSeparator, replaceChar).Replace(paragraphSeparator, replaceChar);
        }

        private static string RemoveLineEndings1(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            string replaceChar = " ";
            return value.Replace("\r\n", replaceChar).Replace("\n", replaceChar).Replace("\r", replaceChar).Replace(lineSeparator, replaceChar).Replace(paragraphSeparator, replaceChar);
        }
        #endregion
    }
}
