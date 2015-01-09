using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Model;
using CodeX.Data.Core.Dal;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class TeacherEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEACHER;
        }

        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("TeacherID = {0}", Convert.ToInt32(ID));
                vTeacher entity = BusinessLayer.GetvTeacherList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            
            txtTeacherCode.Focus();
        }

        protected override void SetControlProperties()
        {
            String filterExpression = String.Format("ParentID IN ('{0}','{1}','{2}') AND IsDeleted = 0 AND IsActive = 1",
                Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.TITLE);
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(filterExpression);

            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField(cboGCSalutation, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SALUTATION || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCSuffix, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.SUFFIX || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField(cboGCTitle, lstStandardCode.Where(x => x.ParentID == Constant.StandardCode.TITLE || x.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTeacherCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(cboGCSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGCTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCSuffix, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmailAddress1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhoneNo2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacRoom, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vTeacher entity)
        {
            txtTeacherCode.Text = entity.TeacherCode;
            cboGCSalutation.Value = entity.GCSalutation;
            cboGCSuffix.Value = entity.GCSuffix;
            cboGCTitle.Value = entity.GCTitle;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            txtEmailAddress1.Text = entity.EmailAddress;
            txtMobilePhoneNo1.Text = entity.MobilePhone1;
            txtMobilePhoneNo2.Text = entity.MobilePhone2;
            tacRoom.Value = entity.RoomID.ToString();
            tacRoom.Text = entity.RoomName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Teacher entity)
        {
            #region Teacher
            entity.TeacherCode = txtTeacherCode.Text;
            if (cboGCSalutation.Value != null)
                entity.GCSalutation = cboGCSalutation.Value.ToString();
            else
                entity.GCSalutation = null;
            if (cboGCSuffix.Value != null)
                entity.GCSuffix = cboGCSuffix.Value.ToString();
            else
                entity.GCSuffix = null;
            if (cboGCTitle.Value != null)
                entity.GCTitle = cboGCTitle.Value.ToString();
            else
                entity.GCTitle = null;
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            entity.EmailAddress = txtEmailAddress1.Text;
            entity.MobilePhone1 = txtMobilePhoneNo1.Text;
            entity.MobilePhone2 = txtMobilePhoneNo2.Text;
            if (tacRoom.Value == "" || tacRoom.Value == "0")
                entity.RoomID = null;
            else
                entity.RoomID = Convert.ToInt32(tacRoom.Value);
            entity.Remarks = txtRemarks.Text;

            string suffix = cboGCSuffix.Value == null ? "" : cboGCSuffix.Text;
            string title = cboGCTitle.Value == null ? "" : cboGCTitle.Text;
            string name = Helper.GenerateName(entity.LastName, entity.MiddleName, entity.FirstName);
            entity.TeacherName = Helper.GenerateFullName(name, title, suffix);
            #endregion
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("TeacherCode = '{0}'", txtTeacherCode.Text);
            List<Teacher> lst = BusinessLayer.GetTeacherList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Teacher with Code " + txtTeacherCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 ID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("TeacherCode = '{0}' AND TeacherID != {1}", txtTeacherCode.Text, ID);
            List<Teacher> lst = BusinessLayer.GetTeacherList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Teacher with Code " + txtTeacherCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeacherDao entityDao = new TeacherDao(ctx);
            bool result = false;
            try
            {
                Teacher entity = new Teacher();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetTeacherMaxID(ctx).ToString();
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
            bool result = true;
            try
            {
                Teacher entity = BusinessLayer.GetTeacher(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTeacher(entity);
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
            }
            return result;
        }
    }
}