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

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class MyRProjectTaskDtEntryCtl : BaseViewPopupCtl
    {
        protected string OnGetUserID()
        {
            return AppSession.UserLogin.UserID.ToString();
        }

        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnProjectTaskID.Value = temp[0];
            hdnProjectOrganizationID.Value = temp[1];
            hdnIsVerifiedTask.Value = temp[2];

            RProjectOrganization entityOrganization = BusinessLayer.GetRProjectOrganization(Convert.ToInt32(hdnProjectOrganizationID.Value));
            txtPosition.Text = hdnPosition.Value = entityOrganization.Position;
            hdnProjectID.Value = entityOrganization.ProjectID.ToString();

            RProjectTask entity = BusinessLayer.GetRProjectTask(Convert.ToInt32(hdnProjectTaskID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.ProjectTaskName);
            chkIsVerified.Checked = entity.IsVerified;

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}') AND StandardCodeID NOT IN ('{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.PROJECT_TASK_STATUS, Constant.ProjectTaskStatus.VOID));
            List<StandardCode> lstProjectStatus = lstStandardCode.Where(p => p.ParentID == Constant.StandardCode.PROJECT_TASK_STATUS).ToList();
            Methods.SetComboBoxField(cboStatus, lstProjectStatus, "StandardCodeName", "StandardCodeID");

            if (hdnIsVerifiedTask.Value == "0")
                trIsVerified.Style.Add("display", "none");
            cboStatus.Value = entity.GCProjectTaskStatus;

            BindGridView2();
            BindGridView3();

            Helper.SetControlEntrySetting(cboStatus, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        private void BindGridView2()
        {
            string filterExpression = "1 = 0";
            if (hdnProjectTaskID.Value != "")
                filterExpression = string.Format("ProjectTaskID = {0} AND IsDeleted = 0 ORDER BY CreatedDate DESC", hdnProjectTaskID.Value);
            grdView2.DataSource = BusinessLayer.GetvRProjectTaskLogList(filterExpression);
            grdView2.DataBind();
        }

        protected void cbpViewPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView2();
        }

        private void BindGridView3()
        {
            string filterExpression = "1 = 0";
            if (hdnProjectTaskID.Value != "")
                filterExpression = string.Format("ProjectTaskID = {0} AND IsDeleted = 0 ORDER BY CreatedDate DESC", hdnProjectTaskID.Value);
            grdView3.DataSource = BusinessLayer.GetvRProjectTaskFileList(filterExpression);
            grdView3.DataBind();
        }

        protected void grdView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vRProjectTaskFile entity = e.Row.DataItem as vRProjectTaskFile;
                HtmlInputHidden hdnDownloadedFile = (HtmlInputHidden)e.Row.FindControl("hdnDownloadedFile");
                hdnDownloadedFile.Value = string.Format("{0}Project/{1}/{2}/{3}", AppConfigManager.CDXVirtualDirectory, hdnProjectID.Value, hdnProjectTaskID.Value, entity.Path);
            }
        }

        protected void cbpViewPopup3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView3();
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
                if (OnSaveEditRecordEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskDao entityDao = new RProjectTaskDao(ctx);
            RProjectTaskAssignDao entityDtDao = new RProjectTaskAssignDao(ctx);
            try
            {
                RProjectTask entity = entityDao.Get(Convert.ToInt32(hdnProjectTaskID.Value));
                entity.GCProjectTaskStatus = cboStatus.Value.ToString();
                entity.IsVerified = chkIsVerified.Checked;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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
        #endregion

        #region Process Detail2
        protected void cbpProcessPopup2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntry2ID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt2(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt2(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity2(RProjectTaskLog entity)
        {
            entity.LogDate = Helper.GetDatePickerValue(txtLogDate.Text);
            entity.LogTime = txtLogTime.Text;
            entity.LogText = txtLogText.Text;
        }

        private bool OnSaveAddRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskLogDao entityDao = new RProjectTaskLogDao(ctx);
            try
            {
                RProjectTaskLog entity = new RProjectTaskLog();
                ControlToEntity2(entity);
                entity.ProjectTaskID = Convert.ToInt32(hdnProjectTaskID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                
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

        private bool OnSaveEditRecordEntityDt2(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskLogDao entityDao = new RProjectTaskLogDao(ctx);
            try
            {
                RProjectTaskLog entity = entityDao.Get(Convert.ToInt32(hdnEntry2ID.Value));
                ControlToEntity2(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

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

        private bool OnDeleteEntityDt2(ref string errMessage)
        {
            try
            {
                RProjectTaskLog entity = BusinessLayer.GetRProjectTaskLog(Convert.ToInt32(hdnEntry2ID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectTaskLog(entity);
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

        #region Process Detail3
        protected void cbpProcessPopup3_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (OnSaveAddRecordEntityDt3(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt3(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity3(RProjectTaskFile entity)
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

        private bool OnSaveAddRecordEntityDt3(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            RProjectTaskFileDao entityDao = new RProjectTaskFileDao(ctx);
            try
            {
                string json = hdnUploadedFile.Value.ToString();
                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
                ListUploadedFile lstFile = serializer.Deserialize<ListUploadedFile>(json);
                foreach (FileData file in lstFile.ListData)
                {
                    RProjectTaskFile entity = new RProjectTaskFile();
                    ControlToEntity3(entity);

                    string path = string.Format("{0}Project\\{1}\\{2}\\", AppConfigManager.CDXPhysicalDirectory, hdnProjectID.Value, hdnProjectTaskID.Value);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    entity.Extension = Path.GetExtension(file.filename);
                    entity.Path = string.Format("{0}{1}", entity.FileName.Replace(' ', '_'), entity.Extension);
                    entity.ProjectTaskID = Convert.ToInt32(hdnProjectTaskID.Value);
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

        private bool OnDeleteEntityDt3(ref string errMessage)
        {
            try
            {
                RProjectTaskFile entity = BusinessLayer.GetRProjectTaskFile(Convert.ToInt32(hdnEntry3ID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectTaskFile(entity);
                string path = string.Format("{0}Project\\{1}\\{2}\\{3}", AppConfigManager.CDXPhysicalDirectory, hdnProjectID.Value, hdnProjectTaskID.Value, entity.Path);
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