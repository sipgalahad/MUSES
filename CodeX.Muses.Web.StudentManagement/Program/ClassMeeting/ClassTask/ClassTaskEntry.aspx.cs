using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Xml;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassTaskEntry : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 5;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            string id = Request.QueryString["id"];
            if(id == "tcs")
                return Constant.MenuCode.StudentManagement.TCS_CLASS_TASK;
            return Constant.MenuCode.StudentManagement.WS_CLASS_TASK;
        }
        protected string OnGetSubjectMarkTypeNumber()
        {
            return Constant.SubjectMarkType.NUMBER;
        }
        protected string OnGetSubjectMarkTypeOption()
        {
            return Constant.SubjectMarkType.OPTION;
        }
        protected string OnGetSubjectMarkTypeText()
        {
            return Constant.SubjectMarkType.TEXT;
        }

        protected override void InitializeDataControl()
        {
            vClassSubject entity = BusinessLayer.GetvClassSubjectList(string.Format("ClassSubjectID = {0}", AppSession.ClassSubject.ClassSubjectID)).FirstOrDefault();
            txtPassingGrade.Text = entity.PassingGrade.ToString();
            hdnSchoolClassID.Value = entity.SchoolClassID.ToString();

            List<vCurriculumSubjectMarkType> lstCurriculumMarkType = BusinessLayer.GetvCurriculumSubjectMarkTypeList(string.Format("CurriculumID = {0} AND SubjectID = {1} AND IsAllowTask = 1 AND IsDeleted = 0", AppSession.ClassSubject.CurriculumID, entity.SubjectID));
            lstCurriculumMarkType.Insert(0, new vCurriculumSubjectMarkType { CurriculumMarkTypeID = 0, CurriculumMarkTypeName = " -- Semua -- " });
            Methods.SetComboBoxField<vCurriculumSubjectMarkType>(cboFilterTaskType, lstCurriculumMarkType, "CurriculumMarkTypeName", "CurriculumMarkTypeID");
            cboFilterTaskType.SelectedIndex = 0;

            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("PeriodSectionID = {0} AND ClassSubjectID = {1}", AppSession.ClassSubject.PeriodSectionID, AppSession.ClassSubject.ClassSubjectID);
            if (cboFilterTaskType.Value != null && cboFilterTaskType.Value.ToString() != "0")
                filterExpression += string.Format(" AND CurriculumMarkTypeID = {0}", cboFilterTaskType.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvClassSubjectTaskRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, RowCountPerPage);
            }

            List<vClassSubjectTask> lstEntity = BusinessLayer.GetvClassSubjectTaskList(filterExpression, RowCountPerPage, pageIndex, "TaskDate DESC, StartTime DESC");
            rptMeetingView.DataSource = lstEntity;
            rptMeetingView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpMeetingDetail_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string filterExpression = "1 = 0";
            if (hdnClassSubjectTaskID.Value != "")
                filterExpression = string.Format("ClassSubjectTaskID = {0}", hdnClassSubjectTaskID.Value);
            lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(filterExpression);
            lstOption = BusinessLayer.GetMarkTypeDtList(string.Format("MarkTypeID = {0} AND IsDeleted = 0", hdnMarkTypeID.Value));
            lstOption.Insert(0, new MarkTypeDt { MarkTypeDtID = 0, MarkTypeDtName = "" });

            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", hdnSchoolClassID.Value));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<ClassStudentSubjectTaskMark> lstStudentMark = null;
        List<MarkTypeDt> lstOption = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vClassStudent entity = (vClassStudent)e.Item.DataItem;
                ClassStudentSubjectTaskMark studentMark = lstStudentMark.FirstOrDefault(p => p.StudentID == entity.StudentID);

                TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                ASPxComboBox cboStudentMarkOption = (ASPxComboBox)e.Item.FindControl("cboStudentMarkOption");
                TextBox txtStudentMarkDescription = (TextBox)e.Item.FindControl("txtStudentMarkDescription");
                HtmlGenericControl bIsRemedial = (HtmlGenericControl)e.Item.FindControl("bIsRemedial");
                HtmlGenericControl divMark = (HtmlGenericControl)e.Item.FindControl("divMark");

                cboStudentMarkOption.ClientInstanceName = string.Format("cboStudentMarkOption{0}", e.Item.ItemIndex);
                switch (hdnGCSubjectMarkType.Value)
                {
                    case Constant.SubjectMarkType.NUMBER: cboStudentMarkOption.ClientVisible = false; txtStudentMarkDescription.Style.Add("display", "none");
                        txtStudentMark.Attributes.Add("min", hdnMinValue.Value);
                        txtStudentMark.Attributes.Add("max", hdnMaxValue.Value);
                        txtStudentMark.Attributes.Add("validationgroup", "mpMark");
                        break;
                    case Constant.SubjectMarkType.OPTION:
                        divMark.Style.Add("display", "none"); txtStudentMark.Style.Add("display", "none"); txtStudentMarkDescription.Style.Add("display", "none");
                        Methods.SetComboBoxField<MarkTypeDt>(cboStudentMarkOption, lstOption, "MarkTypeDtName", "MarkTypeDtID");
                        break;
                    case Constant.SubjectMarkType.TEXT: divMark.Style.Add("display", "none"); cboStudentMarkOption.ClientVisible = false; txtStudentMark.Style.Add("display", "none"); break;
                }
                if (studentMark != null)
                {
                    txtStudentMark.Text = studentMark.Mark.ToString();
                    cboStudentMarkOption.Value = studentMark.MarkTypeDtID.ToString();
                    txtStudentMarkDescription.Text = studentMark.DescriptionMark;
                    if (!studentMark.IsRemedial)
                        bIsRemedial.Style.Add("display", "none");
                }
                else
                    bIsRemedial.Style.Add("display", "none");
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassStudentSubjectTaskMarkDao entityDtDao = new ClassStudentSubjectTaskMarkDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                string GCSubjectMarkType = hdnGCSubjectMarkType.Value;
                List<ClassStudentSubjectTaskMark> lstStudentMark = BusinessLayer.GetClassStudentSubjectTaskMarkList(string.Format("ClassSubjectTaskID = {0}", hdnClassSubjectTaskID.Value), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);
                    ClassStudentSubjectTaskMark entityDt = lstStudentMark.FirstOrDefault(p => p.StudentID == studentID);
                    if (temp[1] != "")
                    {
                        if (entityDt == null)
                        {
                            entityDt = new ClassStudentSubjectTaskMark();
                            entityDt.ClassSubjectTaskID = Convert.ToInt32(hdnClassSubjectTaskID.Value);
                            entityDt.StudentID = studentID;
                            switch (GCSubjectMarkType)
                            {
                                case Constant.SubjectMarkType.NUMBER: entityDt.OriginalMark = entityDt.Mark = Convert.ToDecimal(temp[1]); break;
                                case Constant.SubjectMarkType.OPTION: entityDt.MarkTypeDtID = Convert.ToInt32(temp[1]); break;
                                case Constant.SubjectMarkType.TEXT: entityDt.DescriptionMark = temp[1]; break;
                            }
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            switch (GCSubjectMarkType)
                            {
                                case Constant.SubjectMarkType.NUMBER: 
                                    entityDt.Mark = Convert.ToDecimal(temp[1]);
                                    if (!entityDt.IsRemedial) 
                                        entityDt.OriginalMark = entityDt.Mark;
                                    break;
                                case Constant.SubjectMarkType.OPTION: entityDt.MarkTypeDtID = Convert.ToInt32(temp[1]); break;
                                case Constant.SubjectMarkType.TEXT: entityDt.DescriptionMark = temp[1]; break;
                            }
                            entityDtDao.Update(entityDt);
                        }
                    }
                    else if(entityDt != null)
                    {
                        entityDtDao.Delete(entityDt.ClassSubjectTaskID, entityDt.StudentID);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            // XML declaration
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.AppendChild(declaration);

            // Root element: Table
            XmlElement table = doc.CreateElement("Table");
            doc.AppendChild(table);

            string filterExpression = "1 = 0";
            if (hdnClassSubjectTaskID.Value != "")
                filterExpression = string.Format("ClassSubjectTaskID = {0}", hdnClassSubjectTaskID.Value);
            List<vClassStudentSubjectTaskMark> lstStudentTaskMark = BusinessLayer.GetvClassStudentSubjectTaskMarkList(filterExpression);
            List<vClassStudent> lstStudent = BusinessLayer.GetvClassStudentList(string.Format("SchoolClassID = {0}", hdnSchoolClassID.Value));
            foreach(vClassStudent student in lstStudent)
            {
                // Export
                XmlElement export = doc.CreateElement("export");
                table.AppendChild(export);

                // ID Siswa
                XmlElement IDSiswa = doc.CreateElement("id_siswa");
                IDSiswa.InnerText = student.StudentCode;
                export.AppendChild(IDSiswa);

                // Nama
                XmlElement Nama = doc.CreateElement("Nama");
                Nama.InnerText = student.StudentName;
                export.AppendChild(Nama);

                // Nilai
                XmlElement Nilai = doc.CreateElement("Nilai");
                vClassStudentSubjectTaskMark mark = lstStudentTaskMark.FirstOrDefault(p => p.StudentID == student.StudentID);
                if (mark != null)
                {
                    switch (hdnGCSubjectMarkType.Value)
                    {
                        case Constant.SubjectMarkType.NUMBER: Nilai.InnerText = mark.Mark.ToString(); break;
                        case Constant.SubjectMarkType.OPTION: Nilai.InnerText = mark.MarkTypeDtName; break;
                        case Constant.SubjectMarkType.TEXT: Nilai.InnerText = mark.DescriptionMark; break;
                    }
                }
                else
                    Nilai.InnerText = "-";
                
                export.AppendChild(Nilai);
            }

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;
            doc.WriteTo(writer);
            writer.Flush();
            Response.Clear();
            byte[] byteArray = stream.ToArray();
            Response.AppendHeader("Content-Disposition", "filename=NilaiTugas.xml");
            Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(byteArray);
            writer.Close();
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}