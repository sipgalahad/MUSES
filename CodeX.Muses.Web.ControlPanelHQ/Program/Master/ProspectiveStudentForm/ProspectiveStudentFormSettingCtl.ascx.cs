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
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.ControlPanelHQ.Program
{
    public partial class ProspectiveStudentFormSettingCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            string[] temp = param.Split('|');
            hdnFormID.Value = temp[0];
            
            ProspectiveStudentForm entityHd = BusinessLayer.GetProspectiveStudentForm(Convert.ToInt32(hdnFormID.Value));
            txtFormCode.Text = entityHd.FormCode;
            txtFormName.Text = entityHd.FormName;

            if (param != "")
            {
                List<vProspectiveStudentFolder> lstSelected = BusinessLayer.GetvProspectiveStudentFolderList(string.Format("FormID = {0} AND IsDeleted = 0", hdnFormID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.SiteID).ToList());
            }

            BindGridView(1, true, ref PageCount);
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private string GetFilterExpression()
        {
            string filterExpression = "";
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("SiteID IN (SELECT SiteID FROM fnGetSiteBranch('{0}'))", AppSession.UserLogin.SiteID);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvSiteRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 8);
            }

            List<vSite> lstEntity = BusinessLayer.GetvSiteList(filterExpression, 8, pageIndex, "SiteID ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentFolderDao entityDtDao = new ProspectiveStudentFolderDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                List<ProspectiveStudentFolder> lstProspectiveStudentFolder = BusinessLayer.GetProspectiveStudentFolderList(String.Format("FormID = {0}",hdnFormID.Value), ctx);
                foreach (String member in lstSelectedMember)
                {
                    ProspectiveStudentFolder entityDt = lstProspectiveStudentFolder.FirstOrDefault(x => x.SiteID == member);
                    if (entityDt == null)
                    {
                        entityDt = new ProspectiveStudentFolder();
                        entityDt.FormID = Convert.ToInt32(hdnFormID.Value);
                        entityDt.SiteID = member;
                        entityDtDao.Insert(entityDt);
                    }
                    else 
                    {
                        lstProspectiveStudentFolder.Remove(entityDt);
                    }
                    foreach (ProspectiveStudentFolder psf in lstProspectiveStudentFolder) 
                    {
                        entityDtDao.Delete(psf.SiteID, psf.FormID);
                    }
                }
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                //Helper.InsertErrorLog(ex);
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