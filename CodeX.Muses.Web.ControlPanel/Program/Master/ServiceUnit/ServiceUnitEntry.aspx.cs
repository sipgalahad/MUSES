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
    public partial class ServiceUnitEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.SERVICE_UNIT;
        }

        protected override void InitializeDataControl()
        {
            String[] param = Request.QueryString["id"].Split('|');
            hdnDepartmentID.Value = param[0];
            if (param.Length > 1)
            {
                IsAdd = false;
                Int32 serviceUnitID = Convert.ToInt32(param[1]);
                hdnID.Value = serviceUnitID.ToString();
                ServiceUnitMaster entity = BusinessLayer.GetServiceUnitMaster(Convert.ToInt32(serviceUnitID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtServiceUnitCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtServiceUnitCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtServiceUnitName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtShortName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(ServiceUnitMaster entity)
        {
            txtServiceUnitCode.Text = entity.ServiceUnitCode;
            txtServiceUnitName.Text = entity.ServiceUnitName;
            txtShortName.Text = entity.ShortName;
        }

        private void ControlToEntity(ServiceUnitMaster entity)
        {
            entity.ServiceUnitCode = txtServiceUnitCode.Text;
            entity.ServiceUnitName = txtServiceUnitName.Text;
            entity.ShortName = txtShortName.Text;
            entity.DepartmentID = hdnDepartmentID.Value;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ServiceUnitCode = '{0}'", txtServiceUnitCode.Text);
            List<ServiceUnitMaster> lst = BusinessLayer.GetServiceUnitMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Service Unit with Code " + txtServiceUnitCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 ID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("ServiceUnitCode = '{0}' AND ServiceUnitID != {1}", txtServiceUnitCode.Text, ID);
            List<ServiceUnitMaster> lst = BusinessLayer.GetServiceUnitMasterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Service Unit with Code " + txtServiceUnitCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            ServiceUnitMasterDao entityDao = new ServiceUnitMasterDao(ctx);
            bool result = false;
            try
            {
                ServiceUnitMaster entity = new ServiceUnitMaster();
                ControlToEntity(entity);
                entity.DepartmentID = hdnDepartmentID.Value;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetServiceUnitMasterMaxID(ctx).ToString();
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
                ServiceUnitMaster entity = BusinessLayer.GetServiceUnitMaster(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateServiceUnitMaster(entity);
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