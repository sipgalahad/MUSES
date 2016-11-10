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
                vOrganizationDepartment entity = BusinessLayer.GetvOrganizationDepartmentList(string.Format("OrganizationDepartmentID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.ORGANIZATION_DEPARTMENT);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtOrganizationDepartmentName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(tacOrganizationDepartmentParentID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vOrganizationDepartment entity)
        {
            ctlEntityCode.SetText(entity.OrganizationDepartmentCode);
            txtOrganizationDepartmentName.Text = entity.OrganizationDepartmentName;
            tacOrganizationDepartmentParentID.Value = entity.ParentID.ToString();
            tacOrganizationDepartmentParentID.Text = entity.ParentName.ToString();
            txtRemarks.Text = entity.Remarks;
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(OrganizationDepartment entity, IDbContext ctx)
        {
            entity.OrganizationDepartmentName = txtOrganizationDepartmentName.Text;
            if (tacOrganizationDepartmentParentID.Value == "" || tacOrganizationDepartmentParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(tacOrganizationDepartmentParentID.Value);
            entity.Remarks = txtRemarks.Text;
            entity.IsHeader = chkIsHeader.Checked;
            entity.OrganizationDepartmentCode = ctlEntityCode.GetCode(entity.OrganizationDepartmentName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            OrganizationDepartmentDao entityDao = new OrganizationDepartmentDao(ctx);
            bool result = false;
            try
            {
                OrganizationDepartment entity = new OrganizationDepartment();
                ControlToEntity(entity, ctx);
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
            IDbContext ctx = DbFactory.Configure(true);
            OrganizationDepartmentDao entityDao = new OrganizationDepartmentDao(ctx);
            bool result = false;
            try
            {
                OrganizationDepartment entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity, ctx);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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
    }
}