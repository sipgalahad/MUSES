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
    public partial class GLProductLineEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Accounting.GL_PRODUCT_LINE;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            if (param[0] == "edit")
            {
                IsAdd = false;
                String ID = param[1];
                hdnID.Value = ID;
                ProductLine entity = BusinessLayer.GetProductLineList(String.Format("ProductLineID = {0}", hdnID.Value))[0];

                SetControlProperties();
                EntityToControl(entity);
                hdnGCItemType.Value = entity.GCItemType;
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
                hdnGCItemType.Value = param[1];
            }

            txtProductLineCode.Focus();
        }

        protected override void SetControlProperties()
        {
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtProductLineCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProductLineName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ProductLine entity)
        {
            txtProductLineCode.Text = entity.ProductLineCode;
            txtProductLineName.Text = entity.ProductLineName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(ProductLine entity)
        {
            entity.ProductLineCode = txtProductLineCode.Text;
            entity.ProductLineName = txtProductLineName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ProductLineDao productLineDao = new ProductLineDao(ctx);
            bool result = true;
            try
            {
                ProductLine entity = new ProductLine();
                ControlToEntity(entity);
                entity.GCItemType = hdnGCItemType.Value;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                productLineDao.Insert(entity);
                entity.ProductLineID = BusinessLayer.GetProductLineMaxID(ctx);

                retval = entity.ProductLineID.ToString();
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
            ProductLineDao productLineDao = new ProductLineDao(ctx);
            bool result = true;
            try
            {
                ProductLine entity = productLineDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                productLineDao.Update(entity);                
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