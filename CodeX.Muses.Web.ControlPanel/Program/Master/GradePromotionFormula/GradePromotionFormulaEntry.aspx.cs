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
    public partial class GradePromotionFormulaHdEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.GRADE_PROMOTION_FORMULA;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                GradePromotionFormulaHd entity = BusinessLayer.GetGradePromotionFormulaHd(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtGradePromotionFormulaCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtGradePromotionFormulaCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtGradePromotionFormulaName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(GradePromotionFormulaHd entity)
        {
            txtGradePromotionFormulaCode.Text = entity.GradePromotionFormulaCode;
            txtGradePromotionFormulaName.Text = entity.GradePromotionFormulaName;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(GradePromotionFormulaHd entity)
        {
            entity.GradePromotionFormulaCode = txtGradePromotionFormulaCode.Text;
            entity.GradePromotionFormulaName = txtGradePromotionFormulaName.Text;
            entity.Remarks = txtRemarks.Text;

        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("GradePromotionFormulaCode = '{0}'", txtGradePromotionFormulaCode.Text);
            List<GradePromotionFormulaHd> lst = BusinessLayer.GetGradePromotionFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " GradePromotionFormulaHd With Code " + txtGradePromotionFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("GradePromotionFormulaCode = '{0}' AND GradePromotionFormulaID != {1}", txtGradePromotionFormulaCode.Text, hdnID.Value);
            List<GradePromotionFormulaHd> lst = BusinessLayer.GetGradePromotionFormulaHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " GradePromotionFormulaHd With Code " + txtGradePromotionFormulaCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            GradePromotionFormulaHdDao entityDao = new GradePromotionFormulaHdDao(ctx);
            bool result = false;
            try
            {
                GradePromotionFormulaHd entity = new GradePromotionFormulaHd();
                ControlToEntity(entity);
                entity.SiteID = AppSession.UserLogin.SiteID;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
                retval = BusinessLayer.GetGradePromotionFormulaHdMaxID(ctx).ToString();
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
                GradePromotionFormulaHd entity = BusinessLayer.GetGradePromotionFormulaHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateGradePromotionFormulaHd(entity);
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