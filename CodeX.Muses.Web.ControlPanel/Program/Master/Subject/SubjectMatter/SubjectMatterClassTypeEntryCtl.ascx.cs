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
    public partial class SubjectMatterClassTypeEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnSubjectMatterID.Value = param;

            SubjectMatterHd entityHd = BusinessLayer.GetSubjectMatterHd(Convert.ToInt32(hdnSubjectMatterID.Value));
            txtSubjectMatterName.Text = entityHd.SubjectMatterName;

            if (param != "")
            {
                List<ClassType> lstSelected = BusinessLayer.GetClassTypeList(string.Format("ClassTypeID IN (SELECT ClassTypeID FROM SubjectMatterClassType WHERE SubjectMatterID = {0})", hdnSubjectMatterID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.ClassTypeID).ToList());
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
            string filterExpression = string.Format("ClassTypeCode LIKE '%{0}%' AND ClassTypeName LIKE '%{1}%' AND SiteID = '{2}' AND GCClassStudyType = '{3}' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value, AppSession.UserLogin.SiteID, Constant.ClassStudyType.REGULAR);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetClassTypeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<ClassType> lstEntity = BusinessLayer.GetClassTypeList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ClassType entity = e.Row.DataItem as ClassType;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.ClassTypeID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SubjectMatterClassTypeDao entityDtDao = new SubjectMatterClassTypeDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int SubjectMatterID = Convert.ToInt32(hdnSubjectMatterID.Value);

                List<SubjectMatterClassType> lstSubjectMatterClassType = BusinessLayer.GetSubjectMatterClassTypeList(string.Format("SubjectMatterID = {0}", SubjectMatterID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int ClassTypeID = Convert.ToInt32(lstSelectedMember[ct]);
                        SubjectMatterClassType entityDt = lstSubjectMatterClassType.FirstOrDefault(p => p.ClassTypeID == ClassTypeID);
                        if (entityDt == null)
                        {
                            entityDt = new SubjectMatterClassType();
                            entityDt.SubjectMatterID = SubjectMatterID;
                            entityDt.ClassTypeID = ClassTypeID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (SubjectMatterClassType entity in lstSubjectMatterClassType)
                {
                    if (!lstSelectedMember.Contains(entity.ClassTypeID.ToString()))
                        entityDtDao.Delete(SubjectMatterID, entity.ClassTypeID);
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