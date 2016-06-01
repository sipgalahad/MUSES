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
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Net;
using System.Globalization;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherStudentFinalMarkEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_STUDENT_FINAL_MARK;
        }

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnEmployeeID.Value = AppSession.UserLogin.EmployeeID.ToString();

            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}' AND SchoolPeriodID IN (SELECT SchoolPeriodID FROM vSchoolClass WHERE TeacherID = {2} AND IsDeleted = 0)", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID, AppSession.UserLogin.EmployeeID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            if (cboSchoolPeriod.Value != null)
            {
                List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("SchoolPeriodID = {0} AND '{1}' BETWEEN StartDate AND EndDate", cboSchoolPeriod.Value, DateTime.Now.ToString("yyyyMMdd")));
                if (lstPeriodSection.Count > 0)
                {
                    PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                    tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                    tacPeriodSection.Text = periodSection.PeriodSectionName;
                }
            }

            if (cboSchoolPeriod.Value != null)
            {
                List<vSchoolClass> lstSchoolClass = BusinessLayer.GetvSchoolClassList(string.Format("SchoolPeriodID = {0} AND TeacherID = {1} AND IsDeleted = 0", cboSchoolPeriod.Value, AppSession.UserLogin.EmployeeID));
                if (lstSchoolClass != null)
                {
                    vSchoolClass schoolClass = lstSchoolClass.FirstOrDefault();
                    hdnClassID.Value = schoolClass.SchoolClassID.ToString();
                    hdnClassCode.Value = schoolClass.SchoolClassCode;
                    txtClassName.Text = schoolClass.SchoolClassName;
                }
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            if (hdnClassID.Value == "")
                return "1 = 0";
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("SchoolClassID = {0}", hdnClassID.Value);
            return filterExpression;
        }

        List<ClassStudentMark> lstStudentMark = null;
        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            if (hdnClassID.Value != "")
                lstStudentMark = BusinessLayer.GetClassStudentMarkList(string.Format("SchoolClassID = {0} AND PeriodSectionID = {1}", hdnClassID.Value, tacPeriodSection.Value));
            List<vClassStudent> lstEntity = BusinessLayer.GetvClassStudentList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassStudent entity = (vClassStudent)e.Row.DataItem;
                ClassStudentMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);
                if (studentMark != null)
                {
                    HtmlGenericControl lblFinalMark = (HtmlGenericControl)e.Row.FindControl("lblFinalMark");
                    lblFinalMark.InnerHtml = studentMark.FinalMark.ToString();
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        private string CurrentDomain()
        {
            string currDomain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
            if (Request.Url.Port != 80 && Request.Url.Port != 443)
                currDomain += (":" + Request.Url.Port);
            return currDomain;
        }

        protected void btnCreatePDF_Click(object sender, EventArgs e)
        {
            // Create a Document object
            var document = new Document(PageSize.A4, 25, 25, 25, 25);

            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            // Read in the contents of the Receipt.htm HTML template file

            string url = CurrentDomain() + ResolveUrl(string.Format("~/Report/VID_RCI/StudentManagement/BRapor2013.aspx?id={0}", hdnStudentID.Value));

            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentLength = 0;

            WebResponse response = webrequest.GetResponse();
            string contents = "";
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                contents = stream.ReadToEnd();
            }

            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);
            foreach (var htmlElement in parsedHtmlElements)
            {
                if (((IElement)htmlElement).Chunks.Count > 0)
                {
                    string content = ((IElement)htmlElement).Chunks[0].ToString();
                    if (content.Trim() == "\\p")
                        document.NewPage();
                    else if (content.StartsWith("img"))
                    {
                        string[] temp = contents.Split(' ');
                        string imgUrl = "";
                        float posX = 0;
                        float posY = 0;
                        foreach (string s in temp)
                        {
                            if (s.StartsWith("src="))
                                imgUrl = s.Substring(4);
                            else if (s.StartsWith("posX="))
                                posX = float.Parse(s.Substring(5), CultureInfo.InvariantCulture.NumberFormat);
                            else if (s.StartsWith("posY="))
                                posY = float.Parse(s.Substring(5), CultureInfo.InvariantCulture.NumberFormat);
                        }

                        var logo = iTextSharp.text.Image.GetInstance(Server.MapPath(imgUrl));
                        logo.SetAbsolutePosition(posX, posY);
                        document.Add(logo);

                    }
                    else
                        document.Add(htmlElement as IElement);
                }
                else
                    document.Add(htmlElement as IElement);
            }

            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}_{1}_{2}.pdf", hdnClassCode.Value, hdnStudentCode.Value, hdnStudentName.Value));
            Response.BinaryWrite(output.ToArray());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}