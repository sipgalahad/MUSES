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

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class COAGroupEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.COA_GROUP;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                SetControlProperties();
                vCOAGroup entity = BusinessLayer.GetvCOAGroupList(string.Format("COAGroupID = {0}", ID))[0];
                EntityToControl(entity);
                hdnGCCOAType.Value = entity.GCCOAType;
            }
            else
            {
                hdnGCCOAType.Value = param[1];
                SetControlProperties();
                IsAdd = true;
            }
            txtCOAGroupCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtCOAGroupCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCOAGroupName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPrintOrder, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnParentID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtParentCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(chkIsHeader, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vCOAGroup entity)
        {
            txtCOAGroupCode.Text = entity.COAGroupCode;
            txtCOAGroupName.Text = entity.COAGroupName;
            txtPrintOrder.Text = entity.PrintOrder.ToString();
            hdnParentID.Value = entity.ParentID.ToString();
            txtParentCode.Text = entity.ParentCode;
            txtParentName.Text = entity.ParentName;
            chkIsHeader.Checked = entity.IsHeader;
        }

        private void ControlToEntity(COAGroup entity)
        {
            entity.COAGroupCode = txtCOAGroupCode.Text;
            entity.COAGroupName = txtCOAGroupName.Text;
            entity.PrintOrder = Convert.ToInt16(txtPrintOrder.Text);
            if (hdnParentID.Value == "" || hdnParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
            entity.IsHeader = chkIsHeader.Checked;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("COAGroupCode = '{0}'", txtCOAGroupCode.Text);
            List<COAGroup> lst = BusinessLayer.GetCOAGroupList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Item Group with Code " + txtCOAGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("COAGroupCode = '{0}' AND COAGroupID != {1}", txtCOAGroupCode.Text, hdnID.Value);
            List<COAGroup> lst = BusinessLayer.GetCOAGroupList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Item Group with Code " + txtCOAGroupCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            COAGroupDao entityDao = new COAGroupDao(ctx);
            bool result = false;
            try
            {
                COAGroup entity = new COAGroup();
                ControlToEntity(entity);
                entity.GCCOAType = hdnGCCOAType.Value;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetCOAGroupMaxID(ctx).ToString();
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
                COAGroup entity = BusinessLayer.GetCOAGroup(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCOAGroup(entity);
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