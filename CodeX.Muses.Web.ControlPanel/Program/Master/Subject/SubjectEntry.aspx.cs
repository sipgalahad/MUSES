using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class SubjectEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.EXTRACURRICULAR)
                return Constant.MenuCode.ControlPanel.EXTRACURRICULAR_SUBJECT;
            if (hdnGCClassStudyType.Value == Constant.ClassStudyType.PERSONALITY)
                return Constant.MenuCode.ControlPanel.PERSONALITY;
            return Constant.MenuCode.ControlPanel.SUBJECT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0 && Page.Request.QueryString["id"] != "ex" && Page.Request.QueryString["id"] != "pr")
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Subject entity = BusinessLayer.GetSubject(Convert.ToInt32(ID));
                hdnGCClassStudyType.Value = entity.GCClassStudyType;
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                if (Page.Request.QueryString["id"] == "ex")
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.EXTRACURRICULAR;
                else if (Page.Request.QueryString["id"] == "pr")
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.PERSONALITY;
                else
                    hdnGCClassStudyType.Value = Constant.ClassStudyType.REGULAR;
                IsAdd = true;
            }
            txtSubjectCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSubjectCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSubjectName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        protected override void SetControlProperties()
        {
        }

        private void EntityToControl(Subject entity)
        {
            txtSubjectCode.Text = entity.SubjectCode;
            txtSubjectName.Text = entity.SubjectName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Subject entity)
        {
            entity.SubjectCode = txtSubjectCode.Text;
            entity.SubjectName = txtSubjectName.Text;
            entity.GCClassStudyType = hdnGCClassStudyType.Value;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("SubjectCode = '{0}'", txtSubjectCode.Text);
            List<Subject> lst = BusinessLayer.GetSubjectList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Pelajaran Dengan Kode " + txtSubjectCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("SubjectCode = '{0}' AND SubjectID != {1}", txtSubjectCode.Text, hdnID.Value);
            List<Subject> lst = BusinessLayer.GetSubjectList(FilterExpression);

            if (lst.Count > 0)
                errMessage = "Pelajaran Dengan Kode " + txtSubjectCode.Text + " Sudah Ada!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SubjectDao entityDao = new SubjectDao(ctx);
            bool result = false;
            try
            {
                Subject entity = new Subject();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetSubjectMaxID(ctx).ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                Subject entity = BusinessLayer.GetSubject(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSubject(entity);
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