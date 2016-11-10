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
    public partial class TemplateEmployeeGroupEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.TEMPLATE_EMPLOYEE_GROUP;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                TemplateEmployeeGroupHd entity = BusinessLayer.GetTemplateEmployeeGroupHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.TEMPLATE_EMPLOYEE_GROUP);
            ctlEntityCode.SetControlVisibility(IsAdd);
            ctlEntityCode.SetFocus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTemplateName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));           
        }

        private void EntityToControl(TemplateEmployeeGroupHd entity)
        {
            ctlEntityCode.SetText(entity.TemplateCode);
            txtTemplateName.Text = entity.TemplateName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(TemplateEmployeeGroupHd entity, IDbContext ctx)
        {
            entity.TemplateName = txtTemplateName.Text;
            entity.Remarks = txtRemarks.Text;
            entity.TemplateCode = ctlEntityCode.GetCode(entity.TemplateName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TemplateEmployeeGroupHdDao entityDao = new TemplateEmployeeGroupHdDao(ctx);
            bool result = false;
            try
            {
                TemplateEmployeeGroupHd entity = new TemplateEmployeeGroupHd();
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
            TemplateEmployeeGroupHdDao entityDao = new TemplateEmployeeGroupHdDao(ctx);
            bool result = false;
            try
            {
                TemplateEmployeeGroupHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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