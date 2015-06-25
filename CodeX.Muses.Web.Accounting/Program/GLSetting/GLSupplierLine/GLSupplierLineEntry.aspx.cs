using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using CodeX.Data.Model;
using DevExpress.Web.ASPxEditors;
using System.Reflection;
using System.Collections;
using CodeX.Data.Core.Dal;
using CodeX.Common;

namespace CodeX.Muses.Web.Accounting.Program
{
    public partial class GLSupplierLineEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.GL_SUPPLIER_LINE;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                SupplierLine entity = BusinessLayer.GetSupplierLineList(String.Format("SupplierLineID = {0}", hdnID.Value))[0];
                SetControlProperties();
                EntityToControl(entity);
                hdnGCSupplierType.Value = entity.GCSupplierType;
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
                hdnGCSupplierType.Value = param[1];
            }

            txtSupplierLineCode.Focus();
        }

        protected override void SetControlProperties()
        {
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtSupplierLineCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtSupplierLineName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(SupplierLine entity)
        {
            txtSupplierLineCode.Text = entity.SupplierLineCode;
            txtSupplierLineName.Text = entity.SupplierLineName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(SupplierLine entity)
        {
            entity.SupplierLineCode = txtSupplierLineCode.Text;
            entity.SupplierLineName = txtSupplierLineName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            SupplierLineDao supplierLineDao = new SupplierLineDao(ctx);
            bool result = true;
            try
            {
                SupplierLine entity = new SupplierLine();
                ControlToEntity(entity);
                entity.GCSupplierType = hdnGCSupplierType.Value;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                supplierLineDao.Insert(entity);
                entity.SupplierLineID = BusinessLayer.GetSupplierLineMaxID(ctx);
                retval = entity.SupplierLineID.ToString();

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
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
            SupplierLineDao supplierLineDao = new SupplierLineDao(ctx);
            bool result = true;
            try
            {
                SupplierLine entity = supplierLineDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                supplierLineDao.Update(entity);                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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
    }
}