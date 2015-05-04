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
    public partial class CurriculumEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.CURRICULUM;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Curriculum entity = BusinessLayer.GetCurriculum(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtCurriculumCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtCurriculumCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCurriculumName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Curriculum entity)
        {
            txtCurriculumCode.Text = entity.CurriculumCode;
            txtCurriculumName.Text = entity.CurriculumName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Curriculum entity)
        {
            entity.CurriculumCode = txtCurriculumCode.Text;
            entity.CurriculumName = txtCurriculumName.Text;
            entity.Remarks = txtRemarks.Text;

        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("CurriculumCode = '{0}'", txtCurriculumCode.Text);
            List<Curriculum> lst = BusinessLayer.GetCurriculumList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Curriculum With Code " + txtCurriculumCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("CurriculumCode = '{0}' AND CurriculumID != {1}", txtCurriculumCode.Text, hdnID.Value);
            List<Curriculum> lst = BusinessLayer.GetCurriculumList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Curriculum With Code " + txtCurriculumCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumDao entityDao = new CurriculumDao(ctx);
            bool result = false;
            try
            {
                Curriculum entity = new Curriculum();
                ControlToEntity(entity);
                //entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetCurriculumMaxID(ctx).ToString();
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
                Curriculum entity = BusinessLayer.GetCurriculum(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCurriculum(entity);
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