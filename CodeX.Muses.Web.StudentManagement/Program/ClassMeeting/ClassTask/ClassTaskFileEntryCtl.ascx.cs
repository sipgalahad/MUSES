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
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.IO;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ClassTaskFileEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;

            ClassSubjectTask entity = BusinessLayer.GetClassSubjectTask(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.ClassTaskCode, entity.Topic);

            BindGridView();
        }

        private void BindGridView()
        {
            string filterExpression = string.Format("ClassSubjectTaskID = {0} AND IsDeleted = 0 ORDER BY CreatedDate DESC", hdnID.Value);
            grdView.DataSource = BusinessLayer.GetvClassSubjectTaskFileList(filterExpression);
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vClassSubjectTaskFile entity = e.Row.DataItem as vClassSubjectTaskFile;
                HtmlInputHidden hdnDownloadedFile = (HtmlInputHidden)e.Row.FindControl("hdnDownloadedFile");
                hdnDownloadedFile.Value = string.Format("{0}Project/{1}/{2}/{3}", AppConfigManager.CDXVirtualDirectory, AppSession.ClassSubject.ClassSubjectID, hdnID.Value, entity.Path);
            }
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
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(ClassSubjectTaskFile entity)
        {
            entity.FileName = txtFileName.Text;
            entity.Remarks = txtFileRemarks.Text;
        }

        public class ListUploadedFile
        {
            public FileData[] ListData { get; set; }
        }

        public class FileData
        {
            public String filename { get; set; }
            public Byte[] data { get; set; }
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ClassSubjectTaskFileDao entityDao = new ClassSubjectTaskFileDao(ctx);
            try
            {
                string json = hdnUploadedFile.Value.ToString();
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                ListUploadedFile lstFile = serializer.Deserialize<ListUploadedFile>(json);
                foreach (FileData file in lstFile.ListData)
                {
                    ClassSubjectTaskFile entity = new ClassSubjectTaskFile();
                    ControlToEntity(entity);

                    string path = string.Format("{0}Project\\{1}\\{2}\\", AppConfigManager.CDXPhysicalDirectory, AppSession.ClassSubject.ClassSubjectID, hdnID.Value);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    entity.Extension = Path.GetExtension(file.filename);
                    entity.Path = string.Format("{0}{1}", entity.FileName.Replace(' ', '_'), entity.Extension);
                    entity.ClassSubjectTaskID = Convert.ToInt32(hdnID.Value);
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    entityDao.Insert(entity);

                    path = string.Format("{0}{1}", path, entity.Path);
                    File.WriteAllBytes(path, file.data);
                }

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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                ClassSubjectTaskFile entity = BusinessLayer.GetClassSubjectTaskFile(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateClassSubjectTaskFile(entity);
                string path = string.Format("{0}Project\\{1}\\{2}\\{3}", AppConfigManager.CDXPhysicalDirectory, AppSession.ClassSubject.ClassSubjectID, hdnID.Value, entity.Path);
                File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}