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
using System.Data;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Reflection;

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class UpdateRenumerationCompEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.HumanResources.UPDATE_RENUMERATION_COMP;
        }

        #region Html Getter
        protected string OnGetRenumerationSourceAmountFixed()
        {
            return Constant.RenumerationAmountSource.FIXED;
        }
        protected string OnGetRenumerationSourceAmountRenumerationCompPercentage()
        {
            return Constant.RenumerationAmountSource.RENUMERATION_COMP_PERCENTAGE;
        }
        protected string OnGetFromRenumerationCompFilterExpression()
        {
            return string.Format("GCRenumerationCompType = '{0}' AND IsDeleted = 0", Constant.RenumerationCompType.MONTHLY);
        }
        #endregion

        protected override void InitializeDataControl()
        {
            hdnRowCountPerPage.Value = Constant.GridViewPageSize.GRID_MASTER.ToString();

            SetControlProperties();
            hdnIsEditable.Value = "1";
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.RENUMERATION_AMOUNT_SOURCE));
            Methods.SetComboBoxField<StandardCode>(cboRenumerationAmountSource, lstSc, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnTransactionID, new ControlEntrySetting(false, false, false, "0"));
            SetControlEntrySetting(txtTransactionNo, new ControlEntrySetting(false, false, false, ""));
            SetControlEntrySetting(txtTransactionDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(txtStartEffectiveDate, new ControlEntrySetting(true, true, false, Constant.DefaultValueEntry.DATE_NOW));
            SetControlEntrySetting(tacRenumerationComp, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(chkIsApplyWhenLeave, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsAllowChange, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(tacFromRenumerationComp, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboRenumerationAmountSource, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtAmount, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPercentage, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false, ""));
        }

        #region Load Entity
        public override void OnAddRecord()
        {
            hdnIsEditable.Value = "1";
        }

        protected string IsEditable()
        {
            return hdnIsEditable.Value;
            
        }

        protected string GetFilterExpression()
        {
            string filterExpression = String.Format("");
            return filterExpression;
            
        }
        public override int OnGetRowCount()
        {
            string filterExpression = GetFilterExpression();
            return BusinessLayer.GetvTransRenumerationCompHdRowCount(filterExpression);
        }

        protected override void OnLoadEntity(int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            vTransRenumerationCompHd entity = BusinessLayer.GetvTransRenumerationCompHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        protected override void OnLoadEntity(string keyValue, ref int PageIndex, ref bool isShowWatermark, ref string watermarkText)
        {
            string filterExpression = GetFilterExpression();
            PageIndex = BusinessLayer.GetvTransRenumerationCompHdRowIndex(filterExpression, keyValue, "TransactionID DESC");
            vTransRenumerationCompHd entity = BusinessLayer.GetvTransRenumerationCompHd(filterExpression, PageIndex, "TransactionID DESC");
            EntityToControl(entity, ref isShowWatermark, ref watermarkText);
        }

        private void EntityToControl(vTransRenumerationCompHd entity, ref bool isShowWatermark, ref string watermarkText)
        {
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN)
            {
                isShowWatermark = true;
                watermarkText = entity.TransactionStatusWatermark;
                hdnIsEditable.Value = "0";
            }
            else
                hdnIsEditable.Value = "1";
            if (entity.GCTransactionStatus != Constant.TransactionStatus.OPEN || entity.GCTransactionStatus != Constant.TransactionStatus.VOID)
                hdnPrintStatus.Value = "true";
            else
                hdnPrintStatus.Value = "false";

            hdnTransactionID.Value = entity.TransactionID.ToString();
            txtTransactionNo.Text = entity.TransactionNo;
            txtStartEffectiveDate.Text = entity.StartEffectiveDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtTransactionDate.Text = entity.TransactionDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            tacRenumerationComp.Value = entity.RenumerationCompID.ToString();
            tacRenumerationComp.Text = entity.RenumerationCompName;

            cboRenumerationAmountSource.Value = entity.GCRenumerationAmountSource;
            tacFromRenumerationComp.Value = entity.FromRenumerationCompID.ToString();
            tacFromRenumerationComp.Text = entity.FromRenumerationCompName;
            trRenumerationComp.Style.Add("display", "none");
            trPercentage.Style.Add("display", "none");
            trAmount.Style.Add("display", "none");
            chkIsAllowChange.Checked = entity.IsAllowChange;
            chkIsApplyWhenLeave.Checked = entity.IsApplyWhenLeave;
            txtRemarks.Text = entity.Remarks;
            BindGridView();
        }

        private void BindGridView()
        {
            if (tacRenumerationComp.Value != "")
            {
                RenumerationComp entityComp = BusinessLayer.GetRenumerationComp(Convert.ToInt32(tacRenumerationComp.Value));
                if (!entityComp.IsApplyToAll)
                {
                    List<vRenumerationCompSource> lstCompSource = BusinessLayer.GetvRenumerationCompSourceList(string.Format("RenumerationCompID = {0}", tacRenumerationComp.Value));

                    string propIDComp1 = "";
                    string propComp1 = "";
                    string propIDComp2 = "";
                    string propComp2 = "";
                    IEnumerable<object> lstEntity1 = null;
                    IEnumerable<object> lstEntity2 = null;
                    if (lstCompSource.Count > 0)
                    {
                        int ctr = 0;
                        foreach (vRenumerationCompSource compSource in lstCompSource)
                        {
                            IEnumerable<object> lstEntity = null;
                            string propIDComp = "";
                            string propComp = "";
                            string compHeader = "";

                            if (compSource.GCRenumerationCompSource == Constant.RenumerationCompSource.JOB_LEVEL)
                            {
                                lstEntity = BusinessLayer.GetJobLevelList("IsDeleted = 0");
                                propComp = "JobLevelName";
                                propIDComp = "JobLevelID";
                                compHeader = "Golongan";
                            }
                            else if (compSource.GCRenumerationCompSource == Constant.RenumerationCompSource.WORKING_YEARS)
                            {
                                List<Variable> lstVar = new List<Variable>();
                                for (int i = 0; i <= 35; ++i)
                                {
                                    lstVar.Add(new Variable { Code = i.ToString(), Value = i.ToString() });
                                }
                                lstEntity = lstVar;
                                propComp = "Code";
                                propIDComp = "Code";
                                compHeader = "Masa Kerja";
                            }
                            else if (compSource.GCRenumerationCompSource == Constant.RenumerationCompSource.JOB_LEVEL)
                            {
                                lstEntity = BusinessLayer.GetOrganizationPositionList("IsDeleted = 0");
                                propComp = "OrganizationPositionName";
                                propIDComp = "OrganizationPositionID";
                                compHeader = "Posisi";
                            }
                            else if (compSource.GCRenumerationCompSource == Constant.RenumerationCompSource.FAMILY_STATUS)
                            {
                                lstEntity = BusinessLayer.GetFamilyStatusList("IsDeleted = 0");
                                propComp = "FamilyStatusName";
                                propIDComp = "FamilyStatusID";
                                compHeader = "Status Keluarga";
                            }
                            else if (compSource.GCRenumerationCompSource == Constant.RenumerationCompSource.PERFORMANCE_INDICATOR && compSource.GCIndicatorMarkType == Constant.IndicatorMarkType.CUSTOM)
                            {
                                lstEntity = BusinessLayer.GetPerformanceIndicatorDtList(string.Format("PerformanceIndicatorID = {0} AND IsDeleted = 0 ORDER BY DisplayOrder", compSource.PerformanceIndicatorID));
                                propComp = "PerformanceIndicatorDtName";
                                propIDComp = "PerformanceIndicatorDtID";
                                compHeader = "Nilai";
                            }
                            if (lstEntity != null)
                            {
                                if (ctr == 0)
                                {
                                    lstEntity1 = lstEntity;
                                    propComp1 = propComp;
                                    propIDComp1 = propIDComp;
                                    thComp1.InnerHtml = compHeader;
                                }
                                else if (ctr == 1)
                                {
                                    lstEntity2 = lstEntity;
                                    propComp2 = propComp;
                                    propIDComp2 = propIDComp;
                                    thComp2.InnerHtml = compHeader;
                                }
                                ctr++;
                            }
                        }
                    }


                    List<CCustomGridView> lstBindGrid = new List<CCustomGridView>();
                    if (lstEntity1 != null)
                    {
                        pnlView.Visible = true;
                        hdnIsApplyToAll.Value = "0";
                        foreach (object obj in lstEntity1)
                        {
                            if (lstEntity2 != null)
                            {
                                foreach (object obj2 in lstEntity2)
                                {
                                    CCustomGridView bindGrid = new CCustomGridView();
                                    bindGrid.Comp1 = GetPropValue(obj, propComp1).ToString();
                                    bindGrid.Comp1ID = GetPropValue(obj, propIDComp1).ToString();
                                    bindGrid.Comp1Name = propIDComp1;
                                    bindGrid.Comp2 = GetPropValue(obj2, propComp2).ToString();
                                    bindGrid.Comp2ID = GetPropValue(obj2, propIDComp2).ToString();
                                    bindGrid.Comp2Name = propIDComp2;
                                    lstBindGrid.Add(bindGrid);
                                }
                            }
                            else
                            {
                                CCustomGridView bindGrid = new CCustomGridView();
                                bindGrid.Comp1 = GetPropValue(obj, propComp1).ToString();
                                bindGrid.Comp1ID = GetPropValue(obj, propIDComp1).ToString();
                                bindGrid.Comp1Name = propIDComp1;
                                bindGrid.Comp2 = "";
                                bindGrid.Comp2ID = "";
                                bindGrid.Comp2Name = "";
                                lstBindGrid.Add(bindGrid);
                            }
                        }
                    }
                    else
                    {
                        pnlView.Visible = false;
                        hdnIsApplyToAll.Value = "1";
                    }

                    if (hdnTransactionID.Value != "")
                        lstEntityDt = BusinessLayer.GetTransRenumerationCompDtList(string.Format("TransactionID = {0} AND IsDeleted = 0", hdnTransactionID.Value));
                    
                    rptView.DataSource = lstBindGrid;
                    rptView.DataBind();
                    hdnIsApplyToAll.Value = "1";
                }
                else
                {
                    List<CCustomGridView> lstBindGrid = new List<CCustomGridView>();
                    rptView.DataSource = lstBindGrid;
                    rptView.DataBind();

                    pnlView.Visible = false;
                    hdnIsApplyToAll.Value = "0";
                }
            }
        }

        List<TransRenumerationCompDt> lstEntityDt = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CCustomGridView entity = (CCustomGridView)e.Item.DataItem;
                TextBox txtValue = (TextBox)e.Item.FindControl("txtValue");

                TransRenumerationCompDt entityDt = null;
                if (entity.Comp1Name != "")
                {
                    if (entity.Comp2Name == "")
                        entityDt = lstEntityDt.FirstOrDefault(p => p.GetType().GetProperty(entity.Comp1Name).GetValue(p, null).ToString() == entity.Comp1ID);
                    else
                        entityDt = lstEntityDt.FirstOrDefault(p => p.GetType().GetProperty(entity.Comp1Name).GetValue(p, null).ToString() == entity.Comp1ID
                            && p.GetType().GetProperty(entity.Comp2Name).GetValue(p, null).ToString() == entity.Comp2ID);
                }
                if (entityDt != null)
                    txtValue.Text = entityDt.Amount.ToString();
            }
        }

        public object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public void SetPropValue(object src, string propName, object value)
        {
            Type myType = src.GetType();
            PropertyInfo myPropInfo = myType.GetProperty(propName);
            myPropInfo.SetValue(src, value, null);
        }

        public class CCustomGridView
        {
            public String Comp1 { get; set; }
            public String Comp1ID { get; set; }
            public String Comp1Name { get; set; }
            public String Comp2 { get; set; }
            public String Comp2ID { get; set; }
            public String Comp2Name { get; set; }
        }
        #endregion

        #region Save Header
        private void ControlToEntity(TransRenumerationCompHd entityHd)
        {
            entityHd.TransactionDate = Helper.GetDatePickerValue(Request.Form[txtTransactionDate.UniqueID]);
            entityHd.StartEffectiveDate = Helper.GetDatePickerValue(Request.Form[txtStartEffectiveDate.UniqueID]);
            entityHd.RenumerationCompID = Convert.ToInt32(tacRenumerationComp.Value);
            entityHd.GCRenumerationAmountSource = cboRenumerationAmountSource.Value.ToString();
            if (entityHd.GCRenumerationAmountSource == Constant.RenumerationAmountSource.FIXED)
            {
                entityHd.FromRenumerationCompID = null;
                entityHd.Amount = Convert.ToDecimal(Request.Form[txtAmount.UniqueID]);
            }
            else if (entityHd.GCRenumerationAmountSource == Constant.RenumerationAmountSource.RENUMERATION_COMP_PERCENTAGE)
            {
                entityHd.FromRenumerationCompID = Convert.ToInt32(tacFromRenumerationComp.Value);
                entityHd.Amount = Convert.ToDecimal(Request.Form[txtPercentage.UniqueID]);
            }
            else
            {
                entityHd.FromRenumerationCompID = Convert.ToInt32(tacFromRenumerationComp.Value);
                entityHd.Amount = 0;
            }
            entityHd.IsAllowChange = chkIsAllowChange.Checked;
            entityHd.IsApplyWhenLeave = chkIsApplyWhenLeave.Checked;
            //entityHd.IsUseFormula = chkIsUseFormula.Checked;
            entityHd.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao entityHdDao = new TransRenumerationCompHdDao(ctx);
            TransRenumerationCompDtDao entityDtDao = new TransRenumerationCompDtDao(ctx);
            try
            {
                TransRenumerationCompHd entityHd = new TransRenumerationCompHd();
                ControlToEntity(entityHd);
                entityHd.TransactionNo = BusinessLayer.GenerateTransactionNo(Constant.TransactionCode.RENUMERATION_COMP, entityHd.TransactionDate, ctx);
                entityHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;

                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHd.TransactionID = entityHdDao.Insert(entityHd);

                if (hdnListSaveValue.Value != "")
                {
                    string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(';');
                        if (temp[4] != "")
                        {
                            TransRenumerationCompDt entityDt = new TransRenumerationCompDt();
                            entityDt.TransactionID = entityHd.TransactionID;
                            if (temp[0] != "")
                                SetPropValue(entityDt, temp[0], Convert.ToInt32(temp[1]));
                            if (temp[2] != "")
                                SetPropValue(entityDt, temp[2], Convert.ToInt32(temp[3]));
                            entityDt.Amount = Convert.ToDecimal(temp[4]);
                            entityDt.CreatedBy = AppSession.UserLogin.UserID;
                            entityDtDao.Insert(entityDt);
                        }
                    }
                }

                retval = entityHd.TransactionID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao entityHdDao = new TransRenumerationCompHdDao(ctx);
            TransRenumerationCompDtDao entityDtDao = new TransRenumerationCompDtDao(ctx);
            try
            {
                TransRenumerationCompHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<TransRenumerationCompDt> lstEntityDt = BusinessLayer.GetTransRenumerationCompDtList(string.Format("TransactionID = {0} AND IsDeleted = 0", entityHd.TransactionID), ctx);
                if (hdnListSaveValue.Value != "")
                {
                    string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSaveValue)
                    {
                        string[] temp = saveValue.Split(';');

                        string Comp1Name = temp[0];
                        string Comp1ID = temp[1];
                        string Comp2Name = temp[2];
                        string Comp2ID = temp[1];
                        TransRenumerationCompDt entityDt = null;
                        if (Comp1Name != "")
                        {
                            if (Comp2Name == "")
                                entityDt = lstEntityDt.FirstOrDefault(p => p.GetType().GetProperty(Comp1Name).GetValue(p, null).ToString() == Comp1ID);
                            else
                                entityDt = lstEntityDt.FirstOrDefault(p => p.GetType().GetProperty(Comp1Name).GetValue(p, null).ToString() == Comp1ID
                                    && p.GetType().GetProperty(Comp2Name).GetValue(p, null).ToString() == Comp2ID);
                        }

                        if (temp[4] != "")
                        {
                            if (entityDt != null)
                            {
                                entityDt.Amount = Convert.ToDecimal(temp[4]);
                                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityDtDao.Update(entityDt);
                                lstEntityDt.Remove(entityDt);
                            }
                            else
                            {
                                entityDt = new TransRenumerationCompDt();
                                entityDt.TransactionID = entityHd.TransactionID;
                                if (temp[0] != "")
                                    SetPropValue(entityDt, Comp1Name, Convert.ToInt32(Comp1ID));
                                if (temp[2] != "")
                                    SetPropValue(entityDt, Comp2Name, Convert.ToInt32(Comp2ID));
                                entityDt.Amount = Convert.ToDecimal(temp[4]);
                                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                entityDtDao.Insert(entityDt);

                            }
                        }
                    }
                }
                foreach (TransRenumerationCompDt entityDt in lstEntityDt)
                {
                    entityDt.IsDeleted = true;
                    entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Update(entityDt);
                }

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

        protected override bool OnApproveRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao transRenumerationHdDao = new TransRenumerationCompHdDao(ctx);
            RenumerationCompDao renumerationCompDao = new RenumerationCompDao(ctx);
            try
            {
                TransRenumerationCompHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.APPROVED;
                transRenumerationHd.Remarks = txtRemarks.Text;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);

                if (String.Compare(transRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    RenumerationComp renumerationComp = renumerationCompDao.Get(transRenumerationHd.RenumerationCompID);
                    renumerationComp.CurrentTransactionID = Convert.ToInt32(hdnTransactionID.Value);
                    renumerationComp.LastProcessedDate = DateTime.Now;
                    renumerationCompDao.Update(renumerationComp);
                }
               
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }

        protected override bool OnProposeRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao transRenumerationHdDao = new TransRenumerationCompHdDao(ctx);
            TransRenumerationCompDtDao transRenumerationDtDao = new TransRenumerationCompDtDao(ctx);
            try
            {
                TransRenumerationCompHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.WAIT_FOR_APPROVAL;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }

        protected override bool OnReopenRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao transRenumerationHdDao = new TransRenumerationCompHdDao(ctx);
            TransRenumerationCompDtDao transRenumerationDtDao = new TransRenumerationCompDtDao(ctx);
            try
            {
                TransRenumerationCompHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                if (String.Compare(transRenumerationHd.StartEffectiveDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd")) <=0)
                {
                    result = false;
                    errMessage = "Transaksi Sudah Diproses, Tidak Dapat Diubah";
                }
                else 
                {
                    transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.OPEN;
                    transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationHdDao.Update(transRenumerationHd);
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }

        protected override bool OnVoidRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TransRenumerationCompHdDao transRenumerationHdDao = new TransRenumerationCompHdDao(ctx);
            TransRenumerationCompDtDao transRenumerationDtDao = new TransRenumerationCompDtDao(ctx);
            try
            {
                TransRenumerationCompHd transRenumerationHd = transRenumerationHdDao.Get(Convert.ToInt32(hdnTransactionID.Value));
                transRenumerationHd.GCTransactionStatus = Constant.TransactionStatus.VOID;
                transRenumerationHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                transRenumerationHdDao.Update(transRenumerationHd);

                string filterExpressionPurchaseOrderHd = String.Format("TransactionID = {0} AND isDeleted = 0", hdnTransactionID.Value);
                List<TransRenumerationCompDt> lstTransRenumerationCompDt = BusinessLayer.GetTransRenumerationCompDtList(filterExpressionPurchaseOrderHd, ctx);
                foreach (TransRenumerationCompDt transRenumerationDt in lstTransRenumerationCompDt)
                {
                    transRenumerationDt.IsDeleted = true;
                    transRenumerationDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                    transRenumerationDtDao.Update(transRenumerationDt);
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;

        }

        #endregion

        #region Callback
        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion
    }
}