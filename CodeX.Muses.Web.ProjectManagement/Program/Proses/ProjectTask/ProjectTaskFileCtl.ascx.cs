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

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class ProjectTaskFileCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            ProjectTask entity = BusinessLayer.GetProjectTask(Convert.ToInt32(hdnID.Value));
            hdnTeamDtID.Value = entity.TeamDtID.ToString();
            txtProjectTaskName.Text = string.Format("{0} - {1}", entity.ProjectTaskCode, entity.ProjectTaskName);

            BindGridView();
        }

        protected string OnGetFilterExpression()
        {
            string filterExpression = String.Format("ProjectTaskID = {0} AND IsDeleted = 0",hdnID.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            string filterExpression = OnGetFilterExpression();
            grdPopupView.DataSource = BusinessLayer.GetvProjectTaskFileList(filterExpression);
            grdPopupView.DataBind();
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

            if (param[0] == "upload")
            {
                if (OnUploadFile(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteFile(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        #region Upload File

        public class ListUploadedFile
        {
            public FileData[] ListData { get; set; }
        }

        public class FileData
        {
            public String filename { get; set; }
            public Byte[] data { get; set; }
        }

        private String GetNewFileName(String filename) 
        {
            if (File.Exists(String.Format(@"D:\Upload\{0}", filename)))
            {
                String name = Path.GetFileNameWithoutExtension(filename);
                return GetNewFileName(String.Format("{0}_1{1}", name, Path.GetExtension(filename)));
            }
            else { return filename; }
        }

        private bool OnUploadFile(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProjectTaskFileDao ptfDao = new ProjectTaskFileDao(ctx);
            try
            {
                string json = hdnUploadedFile.Value.ToString();
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                ListUploadedFile lstFile = serializer.Deserialize<ListUploadedFile>(json);
                foreach (FileData file in lstFile.ListData)
                {
                    ProjectTaskFile ptf = new ProjectTaskFile();
                    ptf.Filename = GetNewFileName(file.filename);
                    String path = String.Format(@"D:\Upload\{0}", ptf.Filename);
                    ptf.FilePath = path;
                    ptf.ProjectTaskID = Convert.ToInt32(hdnID.Value);
                    ptf.CreatedBy = AppSession.UserLogin.UserID;
                    ptfDao.Insert(ptf);
                    File.WriteAllBytes(path, file.data);
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                ctx.RollBackTransaction();
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        #region Download
        /// <summary>
        /// Gets the MIME type of the file name specified based on the file name's
        /// extension.  If the file's extension is unknown, returns "octet-stream"
        /// generic for streaming file bytes.
        /// </summary>
        /// <param name="sFileName">The name of the file for which the MIME type
        /// refers to.</param>
        public string GetMimeTypeByFileName(string sFileName)
        {
            string sMime = "application/octet-stream";

            string sExtension = Path.GetExtension(sFileName);
            if (!string.IsNullOrEmpty(sExtension))
            {
                sExtension = sExtension.Replace(".", "");
                sExtension = sExtension.ToLower();

                if (sExtension == "xls" || sExtension == "xlsx")
                {
                    sMime = "application/ms-excel";
                }
                else if (sExtension == "doc" || sExtension == "docx")
                {
                    sMime = "application/msword";
                }
                else if (sExtension == "ppt" || sExtension == "pptx")
                {
                    sMime = "application/ms-powerpoint";
                }
                else if (sExtension == "rtf")
                {
                    sMime = "application/rtf";
                }
                else if (sExtension == "zip")
                {
                    sMime = "application/zip";
                }
                else if (sExtension == "mp3")
                {
                    sMime = "audio/mpeg";
                }
                else if (sExtension == "bmp")
                {
                    sMime = "image/bmp";
                }
                else if (sExtension == "gif")
                {
                    sMime = "image/gif";
                }
                else if (sExtension == "jpg" || sExtension == "jpeg")
                {
                    sMime = "image/jpeg";
                }
                else if (sExtension == "png")
                {
                    sMime = "image/png";
                }
                else if (sExtension == "tiff" || sExtension == "tif")
                {
                    sMime = "image/tiff";
                }
                else if (sExtension == "txt")
                {
                    sMime = "text/plain";
                }
            }

            return sMime;
        }

        /// <summary>
        /// Streams the bytes specified as a file with the name specified using HTTP to the 
        /// calling browser.
        /// </summary>
        /// <param name="sFileName">The name of the file as it will apear when the user
        /// clicks either open or save as in their browser to accept the file
        /// download.</param>
        /// <param name="fileBytes">The file as a byte array to be streamed.</param>
        public void StreamFileToBrowser(FileInfo file)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.AppendHeader("content-length", file.Length.ToString());
            context.Response.ContentType = GetMimeTypeByFileName(file.Name);
            context.Response.AppendHeader("content-disposition", "attachment; filename=\"" + file.Name +"\"");
            context.Response.TransmitFile(file.FullName);
            context.Response.Flush();
            
            // use this instead of response.end to avoid thread aborted exception (known issue):
            // http://support.microsoft.com/kb/312629/EN-US
            context.ApplicationInstance.CompleteRequest();
        }

        private bool OnDownloadFile(ref String errMessage) 
        {
            bool result = true;
            try
            {
                ProjectTaskFile ptf = BusinessLayer.GetProjectTaskFile(Convert.ToInt32(hdnEntryID.Value));
                String path = ptf.FilePath;
                FileInfo file = new FileInfo(ptf.FilePath);

                StreamFileToBrowser(file);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            String errMessage = "";
            String result = "";

            if (hdnEntryID.Value != "")
            {
                if (OnDownloadFile(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            
        }
        #endregion

        private bool OnDeleteFile(ref String errMessage) 
        {
            bool result = true;
            try
            {
                ProjectTaskFile ptf = BusinessLayer.GetProjectTaskFile(Convert.ToInt32(hdnEntryID.Value));
                ptf.IsDeleted = true;
                ptf.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProjectTaskFile(ptf);
                File.Delete(ptf.FilePath);
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }
        #endregion
    }
}