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
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectCurriculumSyllabusEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            if (AppSession.SubjectCurriculumID > 0)
                return Constant.MenuCode.StudentManagement.SBM_SUBJECT_CURRICULUM_SYLLABUS;
            return Constant.MenuCode.StudentManagement.SB_SUBJECT_CURRICULUM_SYLLABUS;
        }

        protected string OnGetSubjectCurriculumFilterExpression()
        {
            return string.Format("SubjectID = {0} AND GCSchoolType = '{1}' AND IsDeleted = 0", AppSession.Subject.SubjectID, AppSession.Subject.GCSchoolType);
        }

        protected override void InitializeDataControl()
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            hdnSubjectID.Value = AppSession.Subject.SubjectID.ToString();

            if (AppSession.SubjectCurriculumID > 0)
            {
                SubjectCurriculum entityHd = BusinessLayer.GetSubjectCurriculum(AppSession.SubjectCurriculumID);
                tacSubjectCurriculum.Value = entityHd.SubjectCurriculumID.ToString();
                tacSubjectCurriculum.Text = entityHd.SubjectCurriculumName;
                tacSubjectCurriculum.Readonly = true;
                hdnCurriculumID.Value = entityHd.CurriculumID.ToString();
                hdnIsPerSchoolPeriodSection.Value = entityHd.IsSyllabusPerSchoolPeriodSection ? "1" : "0";
                if (entityHd.IsSyllabusPerSchoolPeriodSection)
                {
                    trSchoolPeriodSection.Attributes.Remove("style");
                    string optVal = "";
                    List<CurriculumSchoolPeriodSection> lstSchoolPeriodSection = BusinessLayer.GetCurriculumSchoolPeriodSectionList(string.Format("CurriculumID = {0} AND IsDeleted = 0", AppSession.SubjectCurriculumID));
                    foreach (CurriculumSchoolPeriodSection schoolTimeUnit in lstSchoolPeriodSection)
                    {
                        optVal += string.Format("<option value='{0}'>{1}</option>", schoolTimeUnit.CurriculumSchoolPeriodSectionID, schoolTimeUnit.CurriculumSchoolPeriodSectionName);
                    }
                    cboSchoolPeriodSection.InnerHtml = optVal;
                }
                else
                    trSchoolPeriodSection.Attributes.Add("style", "display:none");
            }

            //if (AppSession.SubjectMatterID > 0)
            //{
            //    SubjectCurriculum entityHd = BusinessLayer.GetSubjectCurriculum(AppSession.SubjectMatterID);
            //    tacSubjectCurriculum.Value = entityHd.SubjectCurriculumID.ToString();
            //    tacSubjectCurriculum.Text = entityHd.SubjectCurriculumName;
            //    tacSubjectCurriculum.Readonly = true;
            //}

            Helper.SetControlEntrySetting(tacSubjectCurriculum, new ControlEntrySetting(true, true, true), "mpFilter");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (FileUpload1.HasFile)
            //{
            //    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //    string FolderPath = "Files/";

            //    string FilePath = Server.MapPath(FolderPath + FileName);

            //    FileUpload1.SaveAs(FilePath);
            //    //Import_To_Grid(FilePath, Extension, "Yes");
            //}
        }

        private bool OnUploadFile(string FilePath, string Extension, string isHDR, ref string errMessage)
        {
            bool result = true;

            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Propertiesx='Excel 8.0;HDR={1}'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            List<CurriculumSyllabus> lstCurriculumSyllabus = BusinessLayer.GetCurriculumSyllabusList(string.Format("CurriculumID = {0} AND IsDeleted = 0", hdnCurriculumID.Value));
            List<CurriculumMarkType> lstCurriculumMarkType = BusinessLayer.GetCurriculumMarkTypeList(string.Format("CurriculumID = {0} AND IsDeleted = 0", hdnCurriculumID.Value));
            CurriculumSyllabus mainCompetency = lstCurriculumSyllabus.FirstOrDefault(p => p.GCCurriculumSyllabusType == Constant.CurriculumSyllabusType.MAIN_COMPETENCY);
            CurriculumSyllabus indicator = lstCurriculumSyllabus.FirstOrDefault(p => p.GCCurriculumSyllabusType == Constant.CurriculumSyllabusType.INDICATOR);
            CurriculumSyllabus indicatorDt = lstCurriculumSyllabus.FirstOrDefault(p => p.GCCurriculumSyllabusType == Constant.CurriculumSyllabusType.INDICATOR_DT);

            //bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectCurriculumSyllabusDao entityDao = new SubjectCurriculumSyllabusDao(ctx);
            try
            {
                List<SubjectCurriculumSyllabus> lstOldSubjectCurriculumSyllabus = BusinessLayer.GetSubjectCurriculumSyllabusList(string.Format("SubjectCurriculumID = {0} AND IsDeleted = 0", tacSubjectCurriculum.Value), ctx);

                int currentKIID = 0;
                int currentKDID = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string KINumber = row[0].ToString();
                    string KITeks = row[1].ToString();

                    if (KINumber.Trim() != "")
                    {
                        int curriculumMarkTypeID = lstCurriculumMarkType.FirstOrDefault(p => p.CurriculumMarkTypeCode == KINumber).CurriculumMarkTypeID;
                        SubjectCurriculumSyllabus entity = lstOldSubjectCurriculumSyllabus.FirstOrDefault(p => p.CurriculumSyllabusID == mainCompetency.CurriculumSyllabusID && p.CurriculumMarkTypeID == curriculumMarkTypeID);
                        if (entity == null)
                        {
                            entity = new SubjectCurriculumSyllabus();
                            entity.SubjectCurriculumID = Convert.ToInt32(tacSubjectCurriculum.Value);
                            entity.CurriculumSyllabusID = mainCompetency.CurriculumSyllabusID;
                            entity.SubjectCurriculumSyllabusName = KITeks;
                            entity.CurriculumMarkTypeID = curriculumMarkTypeID;
                            entity.IsAllowTask = true;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            currentKIID = entityDao.Insert(entity);
                        }
                        else
                        {
                            entity.SubjectCurriculumSyllabusName = KITeks;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDao.Update(entity);
                            currentKIID = entity.SubjectCurriculumSyllabusID;
                            lstOldSubjectCurriculumSyllabus.Remove(entity);
                        }
                    }

                    string KDNumber = row[2].ToString();
                    string KDTeks = row[3].ToString();
                    if (KDNumber.Trim() != "")
                    {
                        SubjectCurriculumSyllabus entity = lstOldSubjectCurriculumSyllabus.FirstOrDefault(p => p.CurriculumSyllabusID == indicator.CurriculumSyllabusID && p.SubjectCurriculumSyllabusCode == KDNumber);
                        if (entity == null)
                        {
                            entity = new SubjectCurriculumSyllabus();
                            entity.SubjectCurriculumID = Convert.ToInt32(tacSubjectCurriculum.Value);
                            entity.CurriculumSyllabusID = indicator.CurriculumSyllabusID;
                            entity.SubjectCurriculumSyllabusCode = KDNumber;
                            entity.SubjectCurriculumSyllabusName = KDTeks;
                            entity.CurriculumMarkTypeID = null;
                            entity.ParentID = currentKIID;
                            entity.IsAllowTask = true;
                            entity.CreatedBy = AppSession.UserLogin.UserID;
                            currentKDID = entityDao.Insert(entity);
                        }
                        else
                        {
                            entity.SubjectCurriculumSyllabusName = KDTeks;
                            entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityDao.Update(entity);
                            currentKDID = entity.SubjectCurriculumSyllabusID;
                            lstOldSubjectCurriculumSyllabus.Remove(entity);
                        }
                    }

                    string indicatorTeks = row[4].ToString();
                    if (indicatorTeks.Trim() != "")
                    {
                        SubjectCurriculumSyllabus entity = new SubjectCurriculumSyllabus();
                        entity.SubjectCurriculumID = Convert.ToInt32(tacSubjectCurriculum.Value);
                        entity.CurriculumSyllabusID = indicatorDt.CurriculumSyllabusID;
                        entity.SubjectCurriculumSyllabusName = indicatorTeks;
                        entity.CurriculumMarkTypeID = null;
                        entity.ParentID = currentKDID;
                        entity.IsAllowTask = false;
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(entity);
                    }
                }

                foreach (SubjectCurriculumSyllabus entity in lstOldSubjectCurriculumSyllabus)
                {
                    entity.IsDeleted = true;
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDao.Update(entity);
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

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            string[] param = e.Parameter.Split('|');

            string result = "";
            string errMessage = "";
            result = param[0] + "|";

            if (param[0] == "delete")
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "upload")
            {
                /*HttpPostedFile file = Request.Files[FileUpload1.UniqueID];

                if (file != null && file.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(file.FileName);
                    string Extension = Path.GetExtension(file.FileName);
                    string FolderPath = "Files/";

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    file.SaveAs(FilePath);
                    //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));

                    if (OnUploadFile(FilePath, Extension, "Yes", ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }*/
                string imageData = hdnUploadedFile1.Value;
                if (imageData != "")
                {
                    string[] parts = Regex.Split(imageData, ",").Skip(1).ToArray();
                    imageData = String.Join(",", parts);
                }

                /*byte[] data = Convert.FromBase64String(imageData);
                StreamReader stream = new StreamReader(new MemoryStream(data));
                while (true)
                {
                    string line = stream.ReadLine();
                }*/

                string FileName = hdnFileName.Value;
                string Extension = hdnExtension.Value;
                string FolderPath = Server.MapPath(ResolveUrl("~/Libs/App_Data/TempFiles/"));
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                string FilePath = FolderPath + FileName;

                FileStream fs = new FileStream(FilePath, FileMode.Create);
                BinaryWriter bw = bw = new BinaryWriter(fs);

                byte[] data = Convert.FromBase64String(imageData);
                bw.Write(data);
                bw.Close();

                if (OnUploadFile(FilePath, Extension, "Yes", ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);

                File.Delete(FilePath);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;

        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                SubjectCurriculumSyllabus entity = BusinessLayer.GetSubjectCurriculumSyllabus(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubjectCurriculumSyllabus(entity);
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