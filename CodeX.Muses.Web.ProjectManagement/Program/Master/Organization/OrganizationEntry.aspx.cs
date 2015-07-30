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
using System.Data;

namespace CodeX.DTRACK.Web.ProjectManagement.Program
{
    public partial class OrganizationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ProjectManagement.ORGANIZATION;
        }

        #region Html Getter
        protected string OnGetEmployeeFilterExpression() 
        {
            return "IsDeleted = 0";
        }
        protected string OnGetProjectFilterExpression() 
        {
            return "IsHeader = 1 AND IsDeleted = 0";
        }
        #endregion

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                String filterExpression = String.Format("TeamID = {0}", Convert.ToInt32(ID));
                TeamHd entity = BusinessLayer.GetTeamHdList(filterExpression)[0];
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }

            txtTeamCode.Focus();
        }

        protected override void SetControlProperties()
        {
            
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTeamCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtTeamName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(TeamHd entity)
        {
            txtTeamCode.Text = entity.TeamCode;
            txtTeamName.Text = entity.TeamName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(TeamHd entity)
        {
            entity.TeamCode = txtTeamCode.Text;
            entity.TeamName = txtTeamName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            TeamHdDao entityDao = new TeamHdDao(ctx);
            bool result = false;
            try
            {
                TeamHd entity = new TeamHd();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
                entityDao.Insert(entity);
                entity.TeamID = BusinessLayer.GetTeamHdMaxID(ctx);
                retval = entity.TeamID.ToString();
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeamHdDao entityDao = new TeamHdDao(ctx);
            try
            {
                TeamHd entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
                ctx.CommitTransaction();
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