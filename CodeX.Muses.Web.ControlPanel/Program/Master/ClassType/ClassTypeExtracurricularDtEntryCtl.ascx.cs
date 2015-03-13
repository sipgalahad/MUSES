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
    public partial class ClassTypeExtracurricularDtEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnClassTypeID.Value = param;

            ClassType entityHd = BusinessLayer.GetClassType(Convert.ToInt32(hdnClassTypeID.Value));
            txtClassTypeName.Text = entityHd.ClassTypeName;

            if (param != "")
            {
                List<vClassTypeExtracurricular> lstSelected = BusinessLayer.GetvClassTypeExtracurricularList(string.Format("ExtracurricularClassTypeID = {0}", hdnClassTypeID.Value));
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
            string filterExpression = string.Format("ClassTypeCode LIKE '%{0}%' AND ClassTypeName LIKE '%{1}%' AND GCClassStudyType = '{2}' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value, Constant.ClassStudyType.REGULAR);
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
            ClassTypeExtracurricularDao entityDtDao = new ClassTypeExtracurricularDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int ExtracurricularClassTypeID = Convert.ToInt32(hdnClassTypeID.Value);

                List<ClassTypeExtracurricular> lstClassTypeExtracurricular = BusinessLayer.GetClassTypeExtracurricularList(string.Format("ExtracurricularClassTypeID = {0}", ExtracurricularClassTypeID), ctx);
                int ct = 0;
                if (hdnSelectedMember.Value != "")
                {
                    foreach (String itemID in lstSelectedMember)
                    {
                        int ClassTypeID = Convert.ToInt32(lstSelectedMember[ct]);
                        ClassTypeExtracurricular entityDt = lstClassTypeExtracurricular.FirstOrDefault(p => p.ClassTypeID == ClassTypeID);
                        if (entityDt == null)
                        {
                            entityDt = new ClassTypeExtracurricular();
                            entityDt.ExtracurricularClassTypeID = ExtracurricularClassTypeID;
                            entityDt.ClassTypeID = ClassTypeID;
                            entityDtDao.Insert(entityDt);
                        }
                        ct++;
                    }
                }
                foreach (ClassTypeExtracurricular entity in lstClassTypeExtracurricular)
                {
                    if (!lstSelectedMember.Contains(entity.ClassTypeID.ToString()))
                        entityDtDao.Delete(ExtracurricularClassTypeID, entity.ClassTypeID);
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