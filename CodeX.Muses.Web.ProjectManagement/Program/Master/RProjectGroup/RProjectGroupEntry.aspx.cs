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

namespace CodeX.Muses.Web.ProjectManagement.Program
{
    public partial class RProjectGroupEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.RPROJECT_GROUP;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                vRProjectGroup entity = BusinessLayer.GetvRProjectGroupList(string.Format("ProjectGroupID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtProjectGroupCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtProjectGroupCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtProjectGroupName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnParentID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtParentCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vRProjectGroup entity)
        {
            txtProjectGroupCode.Text = entity.ProjectGroupCode;
            txtProjectGroupName.Text = entity.ProjectGroupName;
            hdnParentID.Value = entity.ParentID.ToString();
            txtParentCode.Text = entity.ParentCode;
            txtParentName.Text = entity.ParentName;
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(RProjectGroup entity)
        {
            entity.ProjectGroupCode = txtProjectGroupCode.Text;
            entity.ProjectGroupName = txtProjectGroupName.Text;
            if (hdnParentID.Value == "" || hdnParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
            entity.IsHeader = chkIsHeader.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ProjectGroupCode = '{0}'", txtProjectGroupCode.Text);
            List<RProjectGroup> lst = BusinessLayer.GetRProjectGroupList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Project Group with Code " + txtProjectGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ProjectGroupCode = '{0}' AND ProjectGroupID != {1}", txtProjectGroupCode.Text, hdnID.Value);
            List<RProjectGroup> lst = BusinessLayer.GetRProjectGroupList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Project Group with Code " + txtProjectGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RProjectGroupDao entityDao = new RProjectGroupDao(ctx);
            bool result = false;
            try
            {
                RProjectGroup entity = new RProjectGroup();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                entity.ProjectGroupID = BusinessLayer.GetRProjectGroupMaxID(ctx);

                retval = entity.ProjectGroupID.ToString();
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
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
                RProjectGroup entity = BusinessLayer.GetRProjectGroup(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRProjectGroup(entity);
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