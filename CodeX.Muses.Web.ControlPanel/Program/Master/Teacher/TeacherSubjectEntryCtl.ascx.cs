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
    public partial class TeacherSubjectEntryCtl : BaseEntryPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnTeacherID.Value = param;

            Employee entityHd = BusinessLayer.GetEmployee(Convert.ToInt32(hdnTeacherID.Value));
            txtTeacherName.Text = entityHd.FullName;

            if (param != "")
            {
                List<vTeacherSubject> lstSelected = BusinessLayer.GetvTeacherSubjectList(string.Format("TeacherID = {0}", hdnTeacherID.Value));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();

                hdnSelectedMember.Value = String.Join(",", lstSelected.Select(p => p.SubjectID).ToList());
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
            string filterExpression = string.Format("SubjectCode LIKE '%{0}%' AND SubjectName LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetSubjectRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 10);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<Subject> lstEntity = BusinessLayer.GetSubjectList(filterExpression, 10, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Subject entity = e.Row.DataItem as Subject;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.SubjectID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherSubjectDao entityDtDao = new TeacherSubjectDao(ctx);
            try
            {
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                int TeacherID = Convert.ToInt32(hdnTeacherID.Value);

                List<TeacherSubject> lstTeacherSubject = BusinessLayer.GetTeacherSubjectList(string.Format("TeacherID = {0}", TeacherID, hdnSelectedMember.Value), ctx);
                int ct = 0;
                foreach (String itemID in lstSelectedMember)
                {
                    int SubjectID = Convert.ToInt32(lstSelectedMember[ct]);
                    TeacherSubject entityDt = lstTeacherSubject.FirstOrDefault(p => p.SubjectID == SubjectID);
                    if (entityDt == null)
                    {
                        entityDt = new TeacherSubject();
                        entityDt.TeacherID = TeacherID;
                        entityDt.SubjectID = SubjectID;
                        entityDtDao.Insert(entityDt);
                    }
                    ct++;
                }
                foreach (TeacherSubject entity in lstTeacherSubject)
                {
                    if (!lstSelectedMember.Contains(entity.SubjectID.ToString()))
                        entityDtDao.Delete(TeacherID, entity.SubjectID);
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