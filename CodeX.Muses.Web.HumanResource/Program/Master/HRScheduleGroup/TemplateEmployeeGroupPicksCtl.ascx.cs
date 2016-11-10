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

namespace CodeX.Muses.Web.Inventory.Program
{
    public partial class TemplateEmployeeGroupPicksCtl : BaseEntryPopupCtl
    {
        protected string OnGetTemplateEmployeeFilterExpression()
        {
            return string.Format("IsDeleted = 0");
        }

        private HRScheduleGroupEntry DetailPage
        {
            get { return (HRScheduleGroupEntry)Page; }
        }

        #region Html Getter
        protected string OnGetGCScheduleTypeFromComponent()
        {
            return Constant.RenumerationSheduleType.FIXED;
        }
        #endregion

        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            hdnTemplateID.Value = "0";
            //TemplateEmployeeGroupHd entity = BusinessLayer.GetTemplateEmployeeGroupHd(Convert.ToInt32(hdnID.Value));
            //txtHeaderText.Text = string.Format("{0} - {1}", entity.TemplateCode, entity.TemplateName);

            BindGridView();


            IsAdd = true;           
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(tacTemplateEmployee, new ControlEntrySetting(true, true, true));
        }

        private void BindGridView()
        {
            string filterExpression = "1 = 0";
            if (hdnTemplateID.Value != "")
                filterExpression = string.Format("TemplateID = {0} ORDER BY EmployeeName ASC", hdnTemplateID.Value);
            grdView.DataSource = BusinessLayer.GetvTemplateEmployeeGroupDtList(filterExpression);
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            HRScheduleGroupEmployeeDao entityDtDao = new HRScheduleGroupEmployeeDao(ctx);
            try
            {
                int TransactionID = 0;
                DetailPage.SaveHRScheduleGroupHd(ctx, ref TransactionID);

                string filterExpression = string.Format("TemplateID = {0} AND EmployeeID NOT IN (SELECT EmployeeID FROM HRScheduleGroupEmployee WHERE TransactionID = {1})", hdnTemplateID.Value, TransactionID);
                List<TemplateEmployeeGroupDt> lsTemp =  BusinessLayer.GetTemplateEmployeeGroupDtList(filterExpression, ctx);

                //HRScheduleGroupEmployee entityDt = new HRScheduleGroupEmployee();
                foreach(TemplateEmployeeGroupDt templateEmp in lsTemp){
                    HRScheduleGroupEmployee entityDt = new HRScheduleGroupEmployee();
                    entityDt.TransactionID = Convert.ToInt32(TransactionID);
                    entityDt.EmployeeID = templateEmp.EmployeeID;
                    entityDtDao.Insert(entityDt);
                }
                //ControlToEntity(entityDt);
                //entityDt.TransactionID = TransactionID;
                //entityDtDao.Insert(entityDt);
                retval = TransactionID.ToString();
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
    }
}