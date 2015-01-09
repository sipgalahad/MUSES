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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class AdmissionPaymentEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_ADMISSION_PAYMENT;
        }
        List<vAdmissionFeeComp> lstComp = null;
        protected override void InitializeDataControl()
        {
            lstComp = BusinessLayer.GetvAdmissionFeeCompList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID));
            rptAdmissionFeeComp.DataSource = lstComp;
            rptAdmissionFeeComp.DataBind();

            BindGridView();

            Helper.SetControlEntrySetting(txtPaymentName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false), "mpTrx");
        }

        protected void rptAdmissionFeeComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtPaymentDate = (TextBox)e.Item.FindControl("txtPaymentDate");
                TextBox txtPaymentAmount = (TextBox)e.Item.FindControl("txtPaymentAmount");
                TextBox txtNoOfPayment = (TextBox)e.Item.FindControl("txtNoOfPayment");
                Helper.SetControlEntrySetting(txtPaymentDate, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtPaymentAmount, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtNoOfPayment, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", AppSession.SchoolPeriodID);
            List<AdmissionPaymentHd> lstEntity = BusinessLayer.GetAdmissionPaymentHdList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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

        private void ControlToEntity(AdmissionPaymentHd entity)
        {
            entity.PaymentName = txtPaymentName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            AdmissionPaymentHdDao entityHdDao = new AdmissionPaymentHdDao(ctx);
            AdmissionPaymentDtDao entityDtDao = new AdmissionPaymentDtDao(ctx);
            try
            {
                AdmissionPaymentHd entityHd = new AdmissionPaymentHd();
                ControlToEntity(entityHd);
                entityHd.SchoolPeriodID = AppSession.SchoolPeriodID;
                entityHd.CreatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Insert(entityHd);

                entityHd.PaymentID = BusinessLayer.GetAdmissionPaymentHdMaxID(ctx);

                string[] lstSaveValue = hdnPaymentDtSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    Int32 AdmissionFeeCompID = Convert.ToInt32(temp[0]);

                    string[] lstSaveValue2 = temp[1].Split(';');
                    short ctr = 1;
                    foreach (string saveValue2 in lstSaveValue2)
                    {
                        string[] temp2 = saveValue2.Split('^');
                        AdmissionPaymentDt entityDt = new AdmissionPaymentDt();
                        entityDt.PaymentID = entityHd.PaymentID;
                        entityDt.AdmissionFeeCompID = AdmissionFeeCompID;
                        entityDt.DisplayOrder = ctr;
                        if (temp2[0] == "1")
                            entityDt.PaymentDate = Helper.InitializeDateTimeNull();
                        else
                            entityDt.PaymentDate = Helper.GetDatePickerValue(temp2[1]);
                        entityDt.PaymentAmount = Convert.ToDecimal(temp2[2]);
                        entityDt.IsPaymentAmountInPercentage = temp2[3] == "1";
                        entityDt.NoOfPayment = Convert.ToInt16(temp2[4]);
                        entityDtDao.Insert(entityDt);
                        ctr++;
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            AdmissionPaymentHdDao entityHdDao = new AdmissionPaymentHdDao(ctx);
            AdmissionPaymentDtDao entityDtDao = new AdmissionPaymentDtDao(ctx);
            try
            {
                AdmissionPaymentHd entityHd = entityHdDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityHd);
                entityHd.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityHdDao.Update(entityHd);

                List<AdmissionPaymentDt> lstEntityDt = BusinessLayer.GetAdmissionPaymentDtList(string.Format("PaymentID = {0}", entityHd.PaymentID), ctx);
                string[] lstSaveValue = hdnPaymentDtSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    Int32 AdmissionFeeCompID = Convert.ToInt32(temp[0]);

                    string[] lstSaveValue2 = temp[1].Split(';');
                    short ctr = 1;
                    foreach (string saveValue2 in lstSaveValue2)
                    {
                        string[] temp2 = saveValue2.Split('^');
                        AdmissionPaymentDt entityDt = lstEntityDt.FirstOrDefault(p => p.AdmissionFeeCompID == AdmissionFeeCompID && p.DisplayOrder == ctr);
                        if (entityDt == null)
                        {
                            entityDt = new AdmissionPaymentDt();
                            entityDt.PaymentID = entityHd.PaymentID;
                            entityDt.AdmissionFeeCompID = AdmissionFeeCompID;
                            entityDt.DisplayOrder = ctr;
                            if (temp2[0] == "1")
                                entityDt.PaymentDate = Helper.InitializeDateTimeNull();
                            else
                                entityDt.PaymentDate = Helper.GetDatePickerValue(temp2[1]);
                            entityDt.PaymentAmount = Convert.ToDecimal(temp2[2]);
                            entityDt.IsPaymentAmountInPercentage = temp2[3] == "1";
                            entityDt.NoOfPayment = Convert.ToInt16(temp2[4]);
                            entityDtDao.Insert(entityDt);
                        }
                        else
                        {
                            if (temp2[0] == "1")
                                entityDt.PaymentDate = Helper.InitializeDateTimeNull();
                            else
                                entityDt.PaymentDate = Helper.GetDatePickerValue(temp2[1]);
                            entityDt.PaymentAmount = Convert.ToDecimal(temp2[2]);
                            entityDt.IsPaymentAmountInPercentage = temp2[3] == "1";
                            entityDt.NoOfPayment = Convert.ToInt16(temp2[4]);
                            entityDtDao.Update(entityDt);

                            lstEntityDt.Remove(entityDt);
                        }
                        ctr++;
                    }
                }

                foreach (AdmissionPaymentDt entityDt in lstEntityDt)
                {
                    entityDtDao.Delete(entityDt.PaymentID, entityDt.AdmissionFeeCompID, entityDt.DisplayOrder);
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
                AdmissionPaymentHd entity = BusinessLayer.GetAdmissionPaymentHd(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateAdmissionPaymentHd(entity);
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