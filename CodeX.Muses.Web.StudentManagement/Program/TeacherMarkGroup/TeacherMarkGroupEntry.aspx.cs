using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Common;
using System.Web.UI.HtmlControls;
using CodeX.Data.Core.Dal;
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class TeacherMarkGroupEntry : BasePageList
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.TEACHER_MARK_GROUP;
        }

        #region html getter
        public string OnGetTeacherFilterExpression() 
        {
            return String.Format("SiteID = '{0}' AND GCEmployeeType = '{1}' AND IsDeleted = 0", AppSession.UserLogin.SiteID, Constant.EmployeeType.TEACHER);
        }
        #endregion

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            
            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();
            

            List<PeriodSection> lstPeriodSection = BusinessLayer.GetPeriodSectionList(string.Format("'{0}' BETWEEN StartDate AND EndDate", DateTime.Now.ToString("yyyyMMdd")));
            if (lstPeriodSection.Count > 0)
            {
                PeriodSection periodSection = lstPeriodSection.FirstOrDefault();
                tacPeriodSection.Value = periodSection.PeriodSectionID.ToString();
                tacPeriodSection.Text = periodSection.PeriodSectionName;
            }
            BindGridView();
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            //filterExpression += "IsDeleted = 0";
            filterExpression += String.Format("SchoolPeriodID = {0} AND PeriodSectionID = {1} AND IsDeleted = 0",cboSchoolPeriod.Value, tacPeriodSection.Value);
            return filterExpression;
        }

        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            
            List<vTeacherMarkTypeGroup> lstEntity = BusinessLayer.GetvTeacherMarkTypeGroupList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            String teacherMarkTypeGroupLst = String.Join(",", lstEntity.Select(x => x.TeacherMarkTypeGroupID).ToList());
            lstTypeItem = BusinessLayer.GetvTeacherMarkTypeItemList(String.Format("TeacherMarkTypeGroupID IN ({0}) AND IsDeleted = 0", teacherMarkTypeGroupLst));

            rptTeacerMarkGroup.DataSource = lstEntity;
            rptTeacerMarkGroup.DataBind();
        }
        List<vTeacherMarkTypeItem> lstTypeItem;
        String TeacherMarkTypeGroupID;

        protected void rptTeacerMarkGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vTeacherMarkTypeGroup entity = e.Item.DataItem as vTeacherMarkTypeGroup;
                List<vTeacherMarkTypeItem> lstItem = lstTypeItem.Where(x => x.TeacherMarkTypeGroupID == entity.TeacherMarkTypeGroupID).ToList();

                TextBox txtMark = (TextBox)e.Item.FindControl("txtMark");
                TeacherMarkTypeGroupID = entity.TeacherMarkTypeGroupID.ToString();
                txtMark.CssClass += String.Format(" score{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtTotalMark = (TextBox)e.Item.FindControl("txtTotalMark");
                txtTotalMark.CssClass += String.Format(" txtTotalMark{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtConvertion = (TextBox)e.Item.FindControl("txtConvertion");
                txtConvertion.CssClass += String.Format(" txtConvertion{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtTotalConvertion = (TextBox)e.Item.FindControl("txtTotalConvertion");
                txtTotalConvertion.CssClass += String.Format(" txtTotalConvertion{0}", entity.TeacherMarkTypeGroupID);

                HtmlTableCell tdTeacherMarkTypeGroupName = (HtmlTableCell)e.Item.FindControl("tdTeacherMarkTypeGroupName");
                tdTeacherMarkTypeGroupName.RowSpan = lstItem.Count();
                HtmlTableCell tdNote = (HtmlTableCell)e.Item.FindControl("tdNote");
                tdNote.RowSpan = lstItem.Count() + 1;


                HtmlTableCell tdItemFinalMarkPercentage = (HtmlTableCell)e.Item.FindControl("tdItemFinalMarkPercentage");
                HtmlTableCell tdTotalItemFinalMark = (HtmlTableCell)e.Item.FindControl("tdTotalItemFinalMark");
                HtmlTableCell tdTeacherMarkTypeItemName = (HtmlTableCell)e.Item.FindControl("tdTeacherMarkTypeItemName");

                HtmlInputHidden hdnFinalItemMark = (HtmlInputHidden)e.Item.FindControl("hdnFinalItemMark");
                
                tdTeacherMarkTypeItemName.InnerHtml = lstItem[0].TeacherMarkTypeItemName;
                tdItemFinalMarkPercentage.InnerHtml = lstItem[0].FinalMarkPercentage.ToString();

                String total = lstItem.Sum(x => x.FinalMarkPercentage).ToString();
                tdTotalItemFinalMark.InnerHtml = String.Format("<b>{0}</b>", total);
                hdnFinalItemMark.Value = total;

                lstItem.RemoveAt(0);
                Repeater rptTeacherMarkItem = (Repeater)e.Item.FindControl("rptTeacherMarkItem");
                rptTeacherMarkItem.DataSource = lstItem;
                rptTeacherMarkItem.DataBind();
            }
        }

        protected void rptTeacherMarkItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtMark = (TextBox)e.Item.FindControl("txtMark");
                txtMark.CssClass += String.Format(" score{0}", TeacherMarkTypeGroupID);

                TextBox txtConvertion = (TextBox)e.Item.FindControl("txtConvertion");
                txtConvertion.CssClass += String.Format(" txtConvertion{0}", TeacherMarkTypeGroupID);
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}