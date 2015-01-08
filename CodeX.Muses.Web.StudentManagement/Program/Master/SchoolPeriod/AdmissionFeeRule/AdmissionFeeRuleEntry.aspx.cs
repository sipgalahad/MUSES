using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class AdmissionFeeRuleEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_ADMISSION_FEE_RULE;
        }
        List<vAdmissionFeeComp> lstComp = null;
        List<PeriodAdmission> lstAdmission = null;
        protected override void InitializeDataControl()
        {
            lstComp = BusinessLayer.GetvAdmissionFeeCompList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
            rptAdmissionFeeComp.DataSource = lstComp;
            rptAdmissionFeeComp.DataBind();

            lstAdmission = BusinessLayer.GetPeriodAdmissionList(string.Format("SchoolPeriodID = {0} AND GCPeriodAdmissionStatus != '{1}'", AppSession.SchoolPeriodID, Constant.SchoolPeriodStatus.VOID));
            rptPeriodAdmission.DataSource = lstAdmission;
            rptPeriodAdmission.DataBind();

            rptAdmissionFeeCompView.DataSource = lstComp;
            rptAdmissionFeeCompView.DataBind();

            List<PeriodAdmission> lstCompDt = new List<PeriodAdmission>();
            for (int i = 0; i < lstComp.Where(p => p.IsFixedAmount == false).Count(); ++i)
            {
                foreach (PeriodAdmission entity in lstAdmission)
                {
                    lstCompDt.Add(entity);
                }
            }
            rptAdmissionFeeCompViewDt.DataSource = lstCompDt;
            rptAdmissionFeeCompViewDt.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(txtAdmissionFeeRuleName, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptAdmissionFeeCompView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vAdmissionFeeComp entity = (vAdmissionFeeComp)e.Item.DataItem;
                HtmlTableCell thAdmissionFeeCompType = (HtmlTableCell)e.Item.FindControl("thAdmissionFeeCompType");
                if (entity.IsFixedAmount)
                {
                    thAdmissionFeeCompType.RowSpan = 2;
                    thAdmissionFeeCompType.Width = "150px";
                }
                else
                    thAdmissionFeeCompType.ColSpan = lstAdmission.Count;
            }
        }

        protected void rptPeriodAdmission_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PeriodAdmission entity = (PeriodAdmission)e.Item.DataItem;
                Repeater rptAdmissionFeeCompDt = (Repeater)e.Item.FindControl("rptAdmissionFeeCompDt");
                rptAdmissionFeeCompDt.DataSource = lstComp;
                rptAdmissionFeeCompDt.DataBind();
            }
        }

        protected void rptAdmissionFeeCompDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vAdmissionFeeComp entity = (vAdmissionFeeComp)e.Item.DataItem;
                TextBox txtAdmissionFeeAmount = (TextBox)e.Item.FindControl("txtAdmissionFeeAmount");
                if (entity.IsFixedAmount)
                {
                    txtAdmissionFeeAmount.ReadOnly = true;
                    txtAdmissionFeeAmount.Text = entity.TotalAmount.ToString();
                }
                else
                    Helper.SetControlEntrySetting(txtAdmissionFeeAmount, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        List<AdmissionFeeRuleDt> lstEntityDt = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID);
            List<AdmissionFeeRuleHd> lstEntity = BusinessLayer.GetAdmissionFeeRuleHdList(filterExpression);
            string lstFeeRuleID = string.Join(",", lstEntity.Select(p => p.AdmissionFeeRuleID).ToList());
            lstEntityDt = BusinessLayer.GetAdmissionFeeRuleDtList(string.Format("AdmissionFeeRuleID IN ({0})", lstFeeRuleID));
            if (lstComp == null && lstEntity.Count > 0)
            {
                lstComp = BusinessLayer.GetvAdmissionFeeCompList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
                lstAdmission = BusinessLayer.GetPeriodAdmissionList(string.Format("SchoolPeriodID = {0} AND GCPeriodAdmissionStatus != '{1}'", AppSession.SchoolPeriodID, Constant.SchoolPeriodStatus.VOID));
            }

            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                AdmissionFeeRuleHd entity = (AdmissionFeeRuleHd)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");

                List<AdmissionFeeRuleDt> lstEntityDt1 = new List<AdmissionFeeRuleDt>();
                foreach (vAdmissionFeeComp entityComp in lstComp)
                {
                    if (entityComp.IsFixedAmount)
                    {
                        AdmissionFeeRuleDt entityDt = new AdmissionFeeRuleDt();
                        entityDt.AdmissionFeeCompID = entityComp.AdmissionFeeCompID;
                        entityDt.TotalAmount = entityComp.TotalAmount;
                        lstEntityDt1.Add(entityDt);
                    }
                    else
                    {
                        foreach (PeriodAdmission entityAdmission in lstAdmission)
                        {
                            AdmissionFeeRuleDt entityDt = lstEntityDt.FirstOrDefault(p => p.AdmissionFeeRuleID == entity.AdmissionFeeRuleID && p.PeriodAdmissionID == entityAdmission.PeriodAdmissionID && p.AdmissionFeeCompID == entityComp.AdmissionFeeCompID);
                            lstEntityDt1.Add(entityDt);
                        }
                    }
                }
                rptViewDt.DataSource = lstEntityDt1;
                rptViewDt.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(AdmissionFeeRuleHd entityHd)
        {
            entityHd.AdmissionFeeRuleName = txtAdmissionFeeRuleName.Text;
            entityHd.IsFeeder = chkIsFeeder.Checked;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            AdmissionFeeRuleHdDao entityHdDao = new AdmissionFeeRuleHdDao(ctx);
            AdmissionFeeRuleDtDao entityDtDao = new AdmissionFeeRuleDtDao(ctx);
            try
            {
                AdmissionFeeRuleHd entityHd = new AdmissionFeeRuleHd();
                ControlToEntity(entityHd);
                entityHd.SchoolPeriodID = AppSession.SchoolPeriodID;
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);

                entityHd.AdmissionFeeRuleID = BusinessLayer.GetAdmissionFeeRuleHdMaxID(ctx);

                string[] lstSaveValue = hdnAdmissionFeeRuleDtSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    AdmissionFeeRuleDt entityDt = new AdmissionFeeRuleDt();
                    entityDt.AdmissionFeeRuleID = entityHd.AdmissionFeeRuleID;
                    entityDt.PeriodAdmissionID = Convert.ToInt32(temp[0]);
                    entityDt.AdmissionFeeCompID = Convert.ToInt32(temp[1]);
                    entityDt.TotalAmount = Convert.ToDecimal(temp[2]);
                    entityDtDao.Insert(entityDt);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            AdmissionFeeRuleHdDao entityHdDao = new AdmissionFeeRuleHdDao(ctx);
            AdmissionFeeRuleDtDao entityDtDao = new AdmissionFeeRuleDtDao(ctx);
            try
            {
                AdmissionFeeRuleHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<AdmissionFeeRuleDt> lstEntityDt = BusinessLayer.GetAdmissionFeeRuleDtList(string.Format("AdmissionFeeRuleID = {0}", entityHd.AdmissionFeeRuleID), ctx);
                string[] lstSaveValue = hdnAdmissionFeeRuleDtSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int PeriodAdmissionID = Convert.ToInt32(temp[0]);
                    int AdmissionFeeCompID = Convert.ToInt32(temp[1]);
                    AdmissionFeeRuleDt entityDt = lstEntityDt.FirstOrDefault(p => p.PeriodAdmissionID == PeriodAdmissionID && p.AdmissionFeeCompID == AdmissionFeeCompID);
                    if (entityDt == null)
                    {
                        entityDt = new AdmissionFeeRuleDt();
                        entityDt.AdmissionFeeRuleID = entityHd.AdmissionFeeRuleID;
                        entityDt.PeriodAdmissionID = Convert.ToInt32(temp[0]);
                        entityDt.AdmissionFeeCompID = Convert.ToInt32(temp[1]);
                        entityDt.TotalAmount = Convert.ToDecimal(temp[2]);
                        entityDtDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.TotalAmount = Convert.ToDecimal(temp[2]);
                        entityDtDao.Update(entityDt);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                result = false;
                errMessage = ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                AdmissionFeeRuleHd entityHd = BusinessLayer.GetAdmissionFeeRuleHd(Convert.ToInt32(hdnEntryID.Value));
                entityHd.IsDeleted = true;
                entityHd.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateAdmissionFeeRuleHd(entityHd);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}