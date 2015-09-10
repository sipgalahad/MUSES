using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class DepartmentEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.DEPARTMENT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String departmentID = Request.QueryString["id"];
                hdnID.Value = departmentID;
                Department entity = BusinessLayer.GetDepartment(departmentID);
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtDepartmentCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtDepartmentCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtDepartmentName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtShortName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtInitial, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsActive, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(Department entity)
        {
            txtDepartmentCode.Text = entity.DepartmentID;
            txtDepartmentName.Text = entity.DepartmentName;
            txtShortName.Text = entity.ShortName;
            txtInitial.Text = entity.Initial;
            chkIsActive.Checked = entity.IsActive;
        }

        private void ControlToEntity(Department entity)
        {
            entity.DepartmentName = txtDepartmentName.Text;
            entity.ShortName = txtShortName.Text;
            entity.Initial = txtInitial.Text;
            entity.IsActive = chkIsActive.Checked;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                Department entity = BusinessLayer.GetDepartment(hdnID.Value);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateDepartment(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}