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
    public partial class OrganizationStructureCompEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.ORGANIZATION_DEPARTMENT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                //OrganizationDepartment entity = BusinessLayer.GetOrganizationDepartment(Convert.ToInt32(ID));
                vOrganizationDepartment entity = BusinessLayer.GetvOrganizationDepartmentList(string.Format("OrganizationDepartmentID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtOrganizationDepartmentCode.Focus();
        }

      


        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtOrganizationDepartmentCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtOrganizationDepartmentName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(tacOrganizationDepartmentParentID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vOrganizationDepartment entity)
        {
            txtOrganizationDepartmentCode.Text = entity.OrganizationDepartmentCode;
            txtOrganizationDepartmentName.Text = entity.OrganizationDepartmentName;
            tacOrganizationDepartmentParentID.Value = entity.ParentID.ToString();
            tacOrganizationDepartmentParentID.Text = entity.ParentName.ToString();
            txtRemarks.Text = entity.Remarks;
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(OrganizationDepartment entity)
        {
            entity.OrganizationDepartmentCode = txtOrganizationDepartmentCode.Text;
            entity.OrganizationDepartmentName = txtOrganizationDepartmentName.Text;
            if (tacOrganizationDepartmentParentID.Value == "" || tacOrganizationDepartmentParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(tacOrganizationDepartmentParentID.Value);
            entity.Remarks = txtRemarks.Text;
            entity.IsHeader = chkIsHeader.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("OrganizationDepartmentCode = '{0}'", txtOrganizationDepartmentCode.Text);
            List<OrganizationDepartment> lst = BusinessLayer.GetOrganizationDepartmentList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Organization Structure With Code " + txtOrganizationDepartmentCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("OrganizationDepartmentCode = '{0}' AND OrganizationDepartmentID != {1}", txtOrganizationDepartmentCode.Text, hdnID.Value);
            List<OrganizationDepartment> lst = BusinessLayer.GetOrganizationDepartmentList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Organization Structure With Code " + txtOrganizationDepartmentCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            OrganizationDepartmentDao entityDao = new OrganizationDepartmentDao(ctx);
            bool result = false;
            try
            {
                OrganizationDepartment entity = new OrganizationDepartment();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                retval = entityDao.Insert(entity).ToString();
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
                OrganizationDepartment entity = BusinessLayer.GetOrganizationDepartment(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateOrganizationDepartment(entity);
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