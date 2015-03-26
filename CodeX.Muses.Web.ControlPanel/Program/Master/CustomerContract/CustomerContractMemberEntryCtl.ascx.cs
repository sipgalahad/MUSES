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

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public partial class CustomerContractMemberEntryCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            CustomerContract entity = BusinessLayer.GetCustomerContract(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0}", entity.ContractNo);

            BindGridView();

            Helper.SetControlEntrySetting(tacCoverageType, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected string OnGetStudentFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND GCStudentStatus = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.StudentStatus.ACTIVE);
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvCustomerContractMemberCustomList(string.Format("ContractID = {0}", hdnID.Value));
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

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CustomerContractMemberDao entityDao = new CustomerContractMemberDao(ctx);
            try
            {
                int contractID = Convert.ToInt32(hdnID.Value);
                int coverageTypeID = Convert.ToInt32(hdnCoverageTypeID.Value);

                string[] lstStudentID = hdnStudentSave.Value.Split(',');
                foreach (string studentID in lstStudentID)
                {
                    CustomerContractMember entity = new CustomerContractMember();
                    entity.ContractID = contractID;
                    entity.CoverageTypeID = coverageTypeID;
                    entity.StudentID = Convert.ToInt32(studentID);
                    entityDao.Insert(entity);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CustomerContractMemberDao entityDao = new CustomerContractMemberDao(ctx);
            try
            {
                int contractID = Convert.ToInt32(hdnID.Value);
                int coverageTypeID = Convert.ToInt32(hdnCoverageTypeID.Value);

                List<CustomerContractMember> lstEntityDt = BusinessLayer.GetCustomerContractMemberList(string.Format("ContractID = {0} AND CoverageTypeID = {1}", contractID, coverageTypeID), ctx);
                if (hdnStudentSave.Value != "")
                {
                    string[] lstStudentID = hdnStudentSave.Value.Split(',');
                    foreach (string studentID in lstStudentID)
                    {
                        CustomerContractMember entity = lstEntityDt.FirstOrDefault(p => p.StudentID == Convert.ToInt32(studentID));
                        if (entity == null)
                        {
                            entity = new CustomerContractMember();
                            entity.ContractID = contractID;
                            entity.CoverageTypeID = coverageTypeID;
                            entity.StudentID = Convert.ToInt32(studentID);
                            entityDao.Insert(entity);
                        }
                        else
                            lstEntityDt.Remove(entity);
                    }
                }

                foreach (CustomerContractMember entity in lstEntityDt)
                {
                    entityDao.Delete(entity.ContractID, entity.CoverageTypeID, entity.StudentID);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CustomerContractMemberDao entityDao = new CustomerContractMemberDao(ctx);
            try
            {
                int contractID = Convert.ToInt32(hdnID.Value);
                int coverageTypeID = Convert.ToInt32(hdnCoverageTypeID.Value);

                List<CustomerContractMember> lstEntityDt = BusinessLayer.GetCustomerContractMemberList(string.Format("ContractID = {0} AND CoverageTypeID = {1}", contractID, coverageTypeID), ctx);
                foreach (CustomerContractMember entity in lstEntityDt)
                {
                    entityDao.Delete(entity.ContractID, entity.CoverageTypeID, entity.StudentID);
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
    }
}