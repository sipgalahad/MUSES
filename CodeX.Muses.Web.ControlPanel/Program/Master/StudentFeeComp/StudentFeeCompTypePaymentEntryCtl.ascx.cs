using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Web.Common;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using System.Globalization;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class StudentFeeCompTypePaymentEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            StudentFeeCompType entity = BusinessLayer.GetStudentFeeCompType(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.StudentFeeCompTypeName);
            hdnGCAdmissionPaymentPeriod.Value = entity.GCAdmissionPaymentPeriod;

            if (entity.GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.SEKALI_BAYAR)
            {
                tdDay.Style.Add("display", "none");
                tdMonth.Style.Add("display", "none");
                tdMonthValue.Style.Add("display", "none");
            }
            else if (entity.GCAdmissionPaymentPeriod == Constant.AdmissionPaymentPeriod.BULANAN)
            {
                tdMonth.Style.Add("display", "none");
                tdMonthValue.Style.Add("display", "none");
            }

            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString();

            BindGridView();

            Helper.SetControlEntrySetting(txtDisplayOrder, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtDay, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(cboMonth, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtPaymentAmountPercentage, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptTaskType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                CheckBox chkTaskType = (CheckBox)e.Item.FindControl("chkTaskType");
                chkTaskType.Attributes.Add("tasktypename", obj.StandardCodeName);
                chkTaskType.Attributes.Add("tasktypeid", obj.StandardCodeID);
            }
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetStudentFeeCompTypePaymentList(string.Format("StudentFeeCompTypeID = {0} AND IsDeleted = 0 ORDER BY DisplayOrder ASC", hdnID.Value));
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(StudentFeeCompTypePayment entity)
        {
            entity.DisplayOrder = Convert.ToInt16(txtDisplayOrder.Text);
            entity.PaymentAmountPercentage = Convert.ToDecimal(txtPaymentAmountPercentage.Text);
            if (hdnGCAdmissionPaymentPeriod.Value != Constant.AdmissionPaymentPeriod.SEKALI_BAYAR) entity.PaymentDate = Convert.ToInt32(txtDay.Text);
            else entity.PaymentDate = null;
            if (hdnGCAdmissionPaymentPeriod.Value == Constant.AdmissionPaymentPeriod.TAHUNAN) entity.PaymentMonth = Convert.ToInt32(cboMonth.Value);
            else entity.PaymentMonth = null;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentFeeCompTypePaymentDao entityDao = new StudentFeeCompTypePaymentDao(ctx);
            try
            {
                StudentFeeCompTypePayment entity = new StudentFeeCompTypePayment();
                ControlToEntity(entity);
                entity.StudentFeeCompTypeID = Convert.ToInt32(hdnID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            StudentFeeCompTypePaymentDao entityDao = new StudentFeeCompTypePaymentDao(ctx);
            try
            {
                StudentFeeCompTypePayment entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                StudentFeeCompTypePayment entity = BusinessLayer.GetStudentFeeCompTypePayment(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateStudentFeeCompTypePayment(entity);
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