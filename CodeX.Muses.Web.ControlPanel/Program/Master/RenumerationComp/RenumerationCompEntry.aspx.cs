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
    public partial class RenumerationCompEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ControlPanel.RENUMERATION_COMP;
        }

        protected string OnGetRenumerationCompTypeDeduction()
        {
            return Constant.RenumerationCompType.DEDUCTION;
        }

        protected string OnGetRenumerationCompSourcePerformanceIndicator()
        {
            return Constant.RenumerationCompSource.PERFORMANCE_INDICATOR;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                RenumerationComp entity = BusinessLayer.GetRenumerationComp(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            ctlEntityCode.InitializeMasterCodingControl(Constant.MasterCode.RENUMERATION_COMP);
            ctlEntityCode.SetControlVisibility(IsAdd);
            txtRenumerationCompName.Focus();
        }

        public override void OnAddRecord()
        {
            ctlEntityCode.SetControlVisibility(true);
        }

        protected override void SetControlProperties()
        {
            List<PerformanceIndicatorHd> lstIndicator = BusinessLayer.GetPerformanceIndicatorHdList("IsDeleted = 0");
            Methods.SetComboBoxField<PerformanceIndicatorHd>(ddlPerformanceIndicator, lstIndicator, "PerformanceIndicatorName", "PerformanceIndicatorID");

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_COMP_TYPE, Constant.StandardCode.RENUMERATION_COMP_SOURCE));
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompType, lstSc.Where(p => p.ParentID == Constant.StandardCode.RENUMERATION_COMP_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboRenumerationCompSource, lstSc.Where(p => p.ParentID == Constant.StandardCode.RENUMERATION_COMP_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(ddlRenumerationCompSource, lstSc.Where(p => p.ParentID == Constant.StandardCode.RENUMERATION_COMP_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRenumerationCompName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationCompSource, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(RenumerationComp entity)
        {
            ctlEntityCode.SetText(entity.RenumerationCompCode);
            txtRenumerationCompName.Text = entity.RenumerationCompName;
            cboRenumerationCompType.Value = entity.GCRenumerationCompType;
            cboRenumerationCompSource.Value = entity.GCRenumerationCompSource;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(RenumerationComp entity, IDbContext ctx)
        {
            entity.RenumerationCompName = txtRenumerationCompName.Text;
            entity.GCRenumerationCompType = "";
            entity.IsApplyToAll = chkIsApllyToAll.Checked;
            entity.GCRenumerationCompSource = null;
            entity.Remarks = txtRemarks.Text;
            entity.RenumerationCompCode = ctlEntityCode.GetCode(entity.RenumerationCompName, ctx);
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            IDbContext ctx = DbFactory.Configure(true);
            RenumerationCompDao entityDao = new RenumerationCompDao(ctx);
            RenumerationCompSourceDao entityDtDao = new RenumerationCompSourceDao(ctx);
            bool result = false;
            try
            {
                RenumerationComp entity = new RenumerationComp();
                ControlToEntity(entity, ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.RenumerationCompID = entityDao.Insert(entity);

                string[] lstSaveValue = hdnLstSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    RenumerationCompSource entityDt = new RenumerationCompSource();
                    entityDt.RenumerationCompID = entity.RenumerationCompID;
                    entityDt.GCRenumerationCompSource = temp[0];
                    if (entityDt.GCRenumerationCompSource == Constant.RenumerationCompSource.PERFORMANCE_INDICATOR)
                        entityDt.PerformanceIndicatorID = Convert.ToInt32(temp[1]);
                    else
                        entityDt.PerformanceIndicatorID = null;
                    entityDtDao.Insert(entityDt);
                }

                retval = entity.RenumerationCompID.ToString();

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
            RenumerationCompDao entityDao = new RenumerationCompDao(ctx);
            RenumerationCompSourceDao entityDtDao = new RenumerationCompSourceDao(ctx);
            bool result = false;
            try
            {
                RenumerationComp entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity, ctx);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                string[] lstSaveValue = hdnLstSaveValue.Value.Split('|');
                List<RenumerationCompSource> lstEntityDt = BusinessLayer.GetRenumerationCompSourceList(String.Format("RenumerationCompID = {0}", entity.RenumerationCompID), ctx);

                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    string GCRenumerationCompSource = temp[0];
                    RenumerationCompSource entityDt = lstEntityDt.FirstOrDefault(p => p.GCRenumerationCompSource == GCRenumerationCompSource);
                    if (entityDt == null)
                    {
                        entityDt = new RenumerationCompSource();
                        entityDt.RenumerationCompID = entity.RenumerationCompID;
                        entityDt.GCRenumerationCompSource = GCRenumerationCompSource;
                        if (entityDt.GCRenumerationCompSource == Constant.RenumerationCompSource.PERFORMANCE_INDICATOR)
                            entityDt.PerformanceIndicatorID = Convert.ToInt32(temp[1]);
                        else
                            entityDt.PerformanceIndicatorID = null;
                        entityDtDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }
                foreach (RenumerationCompSource entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.RenumerationCompID, entityDt.GCRenumerationCompSource);
                }
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