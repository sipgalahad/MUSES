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
    public partial class CurriculumClassTypeExtracurricularDtEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnCurriculumClassTypeID.Value = param;

            CurriculumClassType entityHd = BusinessLayer.GetCurriculumClassType(Convert.ToInt32(hdnCurriculumClassTypeID.Value));
            txtCurriculumClassTypeName.Text = entityHd.CurriculumClassTypeName;

            if (param != "")
            {
                List<vCurriculumClassTypeExtracurricular> lstSelected = BusinessLayer.GetvCurriculumClassTypeExtracurricularList(string.Format("ExtracurricularCurriculumClassTypeID = {0}", hdnCurriculumClassTypeID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.CurriculumClassTypeID).ToList());
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
            string filterExpression = string.Format("CurriculumID = {0} AND CurriculumClassTypeCode LIKE '%{1}%' AND CurriculumClassTypeName LIKE '%{2}%' AND GCClassStudyType = '{3}' AND IsDeleted = 0", AppSession.CurriculumID, hdnFilterItemCode.Value, hdnFilterItemName.Value, Constant.ClassStudyType.REGULAR);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetCurriculumClassTypeRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<CurriculumClassType> lstEntity = BusinessLayer.GetCurriculumClassTypeList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CurriculumClassType entity = e.Row.DataItem as CurriculumClassType;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.CurriculumClassTypeID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CurriculumClassTypeExtracurricularDao entityDtDao = new CurriculumClassTypeExtracurricularDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int ExtracurricularCurriculumClassTypeID = Convert.ToInt32(hdnCurriculumClassTypeID.Value);

                List<CurriculumClassTypeExtracurricular> lstCurriculumClassTypeExtracurricular = BusinessLayer.GetCurriculumClassTypeExtracurricularList(string.Format("ExtracurricularCurriculumClassTypeID = {0}", ExtracurricularCurriculumClassTypeID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int CurriculumClassTypeID = Convert.ToInt32(lstSelectedMember[ct]);
                        CurriculumClassTypeExtracurricular entityDt = lstCurriculumClassTypeExtracurricular.FirstOrDefault(p => p.CurriculumClassTypeID == CurriculumClassTypeID);
                        if (entityDt == null)
                        {
                            entityDt = new CurriculumClassTypeExtracurricular();
                            entityDt.ExtracurricularCurriculumClassTypeID = ExtracurricularCurriculumClassTypeID;
                            entityDt.CurriculumClassTypeID = CurriculumClassTypeID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (CurriculumClassTypeExtracurricular entity in lstCurriculumClassTypeExtracurricular)
                {
                    if (!lstSelectedMember.Contains(entity.CurriculumClassTypeID.ToString()))
                        entityDtDao.Delete(ExtracurricularCurriculumClassTypeID, entity.CurriculumClassTypeID);
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