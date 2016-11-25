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
    public partial class FamilyStatusEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.FAMILY_STATUS;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                FamilyStatus entity = BusinessLayer.GetFamilyStatus(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.FAMILY_STATUS);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.EMPLOYEE_MARTIAL_STATUS));
            Methods.SetComboBoxField<StandardCode>(cboGCMaritalStatus, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtFamilyStatusName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCMaritalStatus, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFromNoOfChilds, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtToNoOfChilds, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
           
        }

        private void EntityToControl(FamilyStatus entity)
        {
            ctlEntityCode.SetText(entity.FamilyStatusCode);
            txtFamilyStatusName.Text = entity.FamilyStatusName;
            cboGCMaritalStatus.Value = entity.GCMaritalStatus.ToString();
            txtFromNoOfChilds.Text = entity.FromNoOfChilds.ToString();
            txtToNoOfChilds.Text = entity.ToNoOfChilds.ToString();
            txtRemarks.Text = entity.Remarks;
      
        }

        private void ControlToEntity(FamilyStatus entity, IDbContext ctx)
        {
            entity.FamilyStatusName = txtFamilyStatusName.Text;
            entity.GCMaritalStatus = cboGCMaritalStatus.Value.ToString();
            entity.FromNoOfChilds = Convert.ToInt16(txtFromNoOfChilds.Text);
            entity.ToNoOfChilds = Convert.ToInt16(txtToNoOfChilds.Text);
            entity.Remarks = txtRemarks.Text;
            entity.FamilyStatusCode = ctlEntityCode.GetCode(entity.FamilyStatusName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            FamilyStatusDao entityDao = new FamilyStatusDao(ctx);
            bool result = false;
            try
            {
                FamilyStatus entity = new FamilyStatus();
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
            FamilyStatusDao entityDao = new FamilyStatusDao(ctx);
            bool result = false;
            try
            {
                FamilyStatus entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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