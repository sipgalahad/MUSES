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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class SubjectMeetingPlanIndicatorEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnSubjectMeetingPlanHdID.Value = param;

            SubjectMeetingPlanHd entityHd = BusinessLayer.GetSubjectMeetingPlanHd(Convert.ToInt32(hdnSubjectMeetingPlanHdID.Value));
            txtMeetingNo.Text = entityHd.MeetingNo.ToString();

            if (param != "")
            {
                List<vSubjectMeetingPlanIndicator> lstSelected = BusinessLayer.GetvSubjectMeetingPlanIndicatorList(string.Format("SubjectMeetingPlanID = {0}", hdnSubjectMeetingPlanHdID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.SubjectIndicatorID).ToList());
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
            string filterExpression = string.Format("SubjectIndicatorName LIKE '%{0}%' AND SubjectBasicCompetencyID IN (SELECT SubjectBasicCompetencyID FROM SubjectMeetingPlanBasicCompetency WHERE SubjectMeetingPlanID = {1}) AND IsDeleted = 0", hdnFilterItemName.Value, hdnSubjectMeetingPlanHdID.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvSubjectIndicatorRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vSubjectIndicator> lstEntity = BusinessLayer.GetvSubjectIndicatorList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vSubjectIndicator entity = e.Row.DataItem as vSubjectIndicator;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.SubjectIndicatorID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectMeetingPlanIndicatorDao entityDtDao = new SubjectMeetingPlanIndicatorDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int SubjectMeetingPlanHdID = Convert.ToInt32(hdnSubjectMeetingPlanHdID.Value);

                List<SubjectMeetingPlanIndicator> lstSubjectMeetingPlanIndicator = BusinessLayer.GetSubjectMeetingPlanIndicatorList(string.Format("SubjectMeetingPlanID = {0}", SubjectMeetingPlanHdID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int SubjectIndicatorID = Convert.ToInt32(lstSelectedMember[ct]);
                        SubjectMeetingPlanIndicator entityDt = lstSubjectMeetingPlanIndicator.FirstOrDefault(p => p.SubjectIndicatorID == SubjectIndicatorID);
                        if (entityDt == null)
                        {
                            entityDt = new SubjectMeetingPlanIndicator();
                            entityDt.SubjectMeetingPlanID = SubjectMeetingPlanHdID;
                            entityDt.SubjectIndicatorID = SubjectIndicatorID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (SubjectMeetingPlanIndicator entity in lstSubjectMeetingPlanIndicator)
                {
                    if (!lstSelectedMember.Contains(entity.SubjectIndicatorID.ToString()))
                        entityDtDao.Delete(SubjectMeetingPlanHdID, entity.SubjectIndicatorID);
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
    }
}