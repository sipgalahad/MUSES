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
using System.Globalization;

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

        public String OnGetScoreGrade() 
        {
            return String.Join("|", lstGrade.Select(x => x.TagProperty + "," + x.StandardCodeName).ToList());
        }

        #endregion

        protected string OnGetPeriodSectionFilterExpression()
        {
            return string.Format("GCPeriodSectionStatus != '{0}'", Constant.SchoolPeriodStatus.VOID);
        }
        List<StandardCode> lstGrade = null;

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);

            lstGrade = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.SCORE_GRADE));

            if (selectedSchoolPeriod == null)
                cboSchoolPeriod.SelectedIndex = 0;
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();
            
            cboMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a.ToString("00")
            });
            cboMonth.TextField = "MonthName";
            cboMonth.ValueField = "MonthNumber";
            cboMonth.EnableCallbackMode = false;
            cboMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboMonth.DataBind();
            cboMonth.Value = DateTime.Now.Month.ToString("00");

            PeriodSection ps = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = {0} AND {1} BETWEEN Month(StartDate) AND Month(EndDate)",cboSchoolPeriod.Value, cboMonth.Value))[0];
            hdnPeriodSectionID.Value = ps.PeriodSectionID.ToString();
            hdnStartDate.Value = ps.StartDate.Year.ToString();
            BindGridView();
        }

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (tacTeacher.Value != "")
                filterExpression += String.Format("SchoolPeriodID = {0} AND PeriodNo = '{1}{2}' AND TeacherID = {3} AND IsDeleted = 0", cboSchoolPeriod.Value, hdnStartDate.Value, cboMonth.Value, tacTeacher.Value);
            else filterExpression = "1 = 0";
            return filterExpression;
        }

        private void BindGridView()
        {
            string filterExpression = GetFilterExpression();
            TeacherMark tm = BusinessLayer.GetTeacherMarkList(filterExpression).FirstOrDefault();
            if (tm != null)
            {
                hdnTeacherMarkID.Value = tm.TeacherMarkID.ToString();
                List<vTeacherMarkGroup> lstEntity = BusinessLayer.GetvTeacherMarkGroupList(String.Format("TeacherMarkID = {0}", tm.TeacherMarkID));
                if (lstEntity.Count > 0)
                {
                    String lstTeacherMarkGroup = String.Join(",", lstEntity.Select(x => x.TeacherMarkGroupID).ToList());
                    lstTeacherMarkItem = BusinessLayer.GetvTeacherMarkItemList(String.Format("TeacherMarkGroupID IN ({0}) AND IsDeleted = 0", lstTeacherMarkGroup));
                    Int32 itemTypeFMP = lstTeacherMarkItem.Sum(x => x.FinalMarkPercentage);
                    Int32 groupCount = lstEntity.Count();
                    tdTotalAllItemFinalMark.InnerHtml = (itemTypeFMP / groupCount).ToString();

                    rptTeacerMarkGroup.DataSource = lstEntity;
                    rptTeacerMarkGroup.DataBind();
                }
            }
            else 
            {
                hdnTeacherMarkID.Value = "";
            }
        }
        List<vTeacherMarkItem> lstTeacherMarkItem;
        String TeacherMarkTypeGroupID;

        protected void rptTeacerMarkGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vTeacherMarkGroup entity = e.Item.DataItem as vTeacherMarkGroup;
                List<vTeacherMarkItem> lstItem = lstTeacherMarkItem.Where(x => x.TeacherMarkGroupID == entity.TeacherMarkGroupID).ToList();

                TextBox txtItemMark = (TextBox)e.Item.FindControl("txtItemMark");
                TeacherMarkTypeGroupID = entity.TeacherMarkTypeGroupID.ToString();
                txtItemMark.CssClass += String.Format(" score{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtItemMarkInString = (TextBox)e.Item.FindControl("txtItemMarkInString");
                txtItemMarkInString.CssClass += String.Format(" txtItemMarkInString{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtTotalItemMark = (TextBox)e.Item.FindControl("txtTotalItemMark");
                txtTotalItemMark.CssClass += String.Format(" txtTotalItemMark{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtTotalItemMarkInString = (TextBox)e.Item.FindControl("txtTotalItemMarkInString");
                txtTotalItemMarkInString.CssClass += String.Format(" txtTotalItemMarkInString{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtConvertion = (TextBox)e.Item.FindControl("txtConvertion");
                txtConvertion.CssClass += String.Format(" txtConvertion{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtTotalConvertion = (TextBox)e.Item.FindControl("txtTotalConvertion");
                txtTotalConvertion.CssClass += String.Format(" txtTotalConvertion{0}", entity.TeacherMarkTypeGroupID);

                TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
                txtRemarks.CssClass += String.Format(" txtRemarks{0}", entity.TeacherMarkTypeGroupID);
                
                HtmlInputHidden hdnItemMarkKeyField = (HtmlInputHidden)e.Item.FindControl("hdnItemMarkKeyField");
                hdnItemMarkKeyField.Attributes.Add("class", String.Format("hdnItemMarkKeyField{0}", entity.TeacherMarkTypeGroupID));

                HtmlTableCell tdGroupFinalMarkPercentage = (HtmlTableCell)e.Item.FindControl("tdGroupFinalMarkPercentage");
                tdGroupFinalMarkPercentage.Attributes.Add("class", String.Format("tdGroupFinalMarkPercentage{0}", entity.TeacherMarkTypeGroupID));

                HtmlTableCell tdTeacherMarkTypeGroupName = (HtmlTableCell)e.Item.FindControl("tdTeacherMarkTypeGroupName");
                tdTeacherMarkTypeGroupName.RowSpan = lstItem.Count();
                HtmlTableCell tdNote = (HtmlTableCell)e.Item.FindControl("tdNote");
                tdNote.RowSpan = lstItem.Count() + 1;

                HtmlTableCell tdItemFinalMarkPercentage = (HtmlTableCell)e.Item.FindControl("tdItemFinalMarkPercentage");
                tdItemFinalMarkPercentage.Attributes.Add("class", String.Format("tdItemFinalMarkPercentage{0}", entity.TeacherMarkTypeGroupID));

                HtmlTableCell tdTotalItemFinalMark = (HtmlTableCell)e.Item.FindControl("tdTotalItemFinalMark");
                HtmlTableCell tdTeacherMarkTypeItemName = (HtmlTableCell)e.Item.FindControl("tdTeacherMarkTypeItemName");

                HtmlInputHidden hdnFinalItemMark = (HtmlInputHidden)e.Item.FindControl("hdnFinalItemMark");

                txtItemMark.Text = lstItem[0].Mark.ToString();
                txtItemMarkInString.Text = lstItem[0].MarkInString;
                tdTeacherMarkTypeItemName.InnerHtml = lstItem[0].TeacherMarkTypeItemName;
                tdItemFinalMarkPercentage.InnerHtml = lstItem[0].FinalMarkPercentage.ToString();
                hdnItemMarkKeyField.Value = lstItem[0].TeacherMarkItemID.ToString();
                txtRemarks.Text = lstItem[0].Remarks;

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
                HtmlInputHidden hdnItemMarkKeyField = (HtmlInputHidden)e.Item.FindControl("hdnItemMarkKeyField");
                hdnItemMarkKeyField.Attributes.Add("class", String.Format("hdnItemMarkKeyField{0}", TeacherMarkTypeGroupID));

                HtmlTableCell tdItemFinalMarkPercentage = (HtmlTableCell)e.Item.FindControl("tdItemFinalMarkPercentage");
                tdItemFinalMarkPercentage.Attributes.Add("class", String.Format("tdItemFinalMarkPercentage{0}", TeacherMarkTypeGroupID));

                TextBox txtItemMark = (TextBox)e.Item.FindControl("txtItemMark");
                txtItemMark.CssClass += String.Format(" score{0}", TeacherMarkTypeGroupID);

                TextBox txtItemMarkInString = (TextBox)e.Item.FindControl("txtItemMarkInString");
                txtItemMarkInString.CssClass += String.Format(" txtItemMarkInString{0}", TeacherMarkTypeGroupID);

                TextBox txtRemarks = (TextBox)e.Item.FindControl("txtRemarks");
                txtRemarks.CssClass += String.Format(" txtRemarks{0}", TeacherMarkTypeGroupID);
                
                TextBox txtConvertion = (TextBox)e.Item.FindControl("txtConvertion");
                txtConvertion.CssClass += String.Format(" txtConvertion{0}", TeacherMarkTypeGroupID);
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            PeriodSection ps = BusinessLayer.GetPeriodSectionList(String.Format("SchoolPeriodID = {0} AND {1} BETWEEN Month(StartDate) AND Month(EndDate)", cboSchoolPeriod.Value, cboMonth.Value))[0];
            hdnPeriodSectionID.Value = ps.PeriodSectionID.ToString();
            hdnStartDate.Value = ps.StartDate.Year.ToString();
            BindGridView();
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "generate")
            {
                if (OnGenerateTeacherMark(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else if (param[0] == "update") 
            {
                if (OnUpdateTeacherMark(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool OnGenerateTeacherMark(ref String errMessage) 
        {
            Boolean result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkGroupDao tmgDao = new TeacherMarkGroupDao(ctx);
            TeacherMarkItemDao tmiDao = new TeacherMarkItemDao(ctx);
            TeacherMarkDao tmDao = new TeacherMarkDao(ctx);
            try
            {

                TeacherMark tm = BusinessLayer.GetTeacherMarkList(String.Format("SchoolPeriodID = {0} AND PeriodNo = '{1}{2}' AND TeacherID = '{3}' AND IsDeleted = 0", cboSchoolPeriod.Value, hdnStartDate.Value, cboMonth.Value, tacTeacher.Value), ctx).FirstOrDefault();
                if (tm == null)
                {
                    tm = new TeacherMark();
                    tm.TeacherID = Convert.ToInt32(tacTeacher.Value);
                    tm.SchoolPeriodID = Convert.ToInt32(cboSchoolPeriod.Value);
                    tm.PeriodSectionID = Convert.ToInt32(hdnPeriodSectionID.Value);
                    tm.PeriodNo = String.Format("{0}{1}", hdnStartDate.Value, cboMonth.Value);
                    tm.FinalMark = 0;
                    tm.FinalMarkInString = "E";
                    tm.LastUpdatedBy = tm.CreatedBy = AppSession.UserLogin.UserID;
                    tmDao.Insert(tm);
                    tm.TeacherMarkID = BusinessLayer.GetTeacherMarkMaxID(ctx);

                    List<TeacherMarkTypeGroup> lstTmtg = BusinessLayer.GetTeacherMarkTypeGroupList(String.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID), ctx);
                    String listTmtgID = String.Join(",", lstTmtg.Select(x => x.TeacherMarkTypeGroupID).ToList());
                    List<TeacherMarkTypeItem> lstTmti = BusinessLayer.GetTeacherMarkTypeItemList(String.Format("TeacherMarkTypeGroupID IN ({0}) AND IsDeleted = 0", listTmtgID), ctx);
                    foreach (TeacherMarkTypeGroup tmtg in lstTmtg)
                    {
                        TeacherMarkGroup entityGroup = new TeacherMarkGroup();
                        entityGroup.TeacherMarkID = tm.TeacherMarkID;
                        entityGroup.TeacherMarkTypeGroupID = tmtg.TeacherMarkTypeGroupID;
                        entityGroup.Mark = 0;
                        entityGroup.MarkInString = "E";
                        entityGroup.IsDeleted = false;
                        entityGroup.LastUpdatedBy = entityGroup.CreatedBy = AppSession.UserLogin.UserID;
                        entityGroup.LastUpdatedDate = DateTime.Now;
                        tmgDao.Insert(entityGroup);

                        Int32 teacherMarkGroupID = BusinessLayer.GetTeacherMarkGroupMaxID(ctx);
                        foreach (TeacherMarkTypeItem tmti in lstTmti.Where(x => x.TeacherMarkTypeGroupID == tmtg.TeacherMarkTypeGroupID).ToList())
                        {
                            TeacherMarkItem entityItem = new TeacherMarkItem();
                            entityItem.TeacherMarkGroupID = teacherMarkGroupID;
                            entityItem.TeacherMarkTypeItemID = tmti.TeacherMarkTypeItemID;
                            entityItem.Mark = 0;
                            entityItem.MarkInString = "E";
                            entityItem.IsDeleted = false;
                            entityItem.LastUpdatedBy = entityItem.CreatedBy = AppSession.UserLogin.UserID;
                            entityItem.LastUpdatedDate = DateTime.Now;
                            tmiDao.Insert(entityItem);
                        }
                    }
                    ctx.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
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

        private bool OnUpdateTeacherMark(ref String errMessage) 
        {
            Boolean result = true;
            IDbContext ctx = DbFactory.Configure(true);
            TeacherMarkGroupDao tmgDao = new TeacherMarkGroupDao(ctx);
            TeacherMarkItemDao tmiDao = new TeacherMarkItemDao(ctx);
            TeacherMarkDao tmDao = new TeacherMarkDao(ctx);

            try
            {
                TeacherMark entity = tmDao.Get(Convert.ToInt32(hdnTeacherMarkID.Value));
                String filterExpression = String.Format("TeacherMarkID = {0} AND IsDeleted = 0", entity.TeacherMarkID);
                String[] tempTm = hdnTotalGroupMark.Value.Split(';');
                entity.FinalMark = Convert.ToInt32(tempTm[0]);
                entity.FinalMarkInString = tempTm[1];
                tmDao.Update(entity);

                List<TeacherMarkGroup> lstTmg = BusinessLayer.GetTeacherMarkGroupList(filterExpression,ctx);
                String lstTeacherMarkGroup = String.Join(",", lstTmg.Select(x => x.TeacherMarkGroupID));
                
                filterExpression = String.Format("TeacherMarkGroupID IN ({0}) AND IsDeleted = 0", lstTeacherMarkGroup);
                List<TeacherMarkItem> lstTmi = BusinessLayer.GetTeacherMarkItemList(filterExpression, ctx);

                List<String> lstValue = hdnListValue.Value.Split('|').ToList();
                foreach (String value in lstValue) 
                {
                    List<String> temp = value.Split(';').ToList();
                    String[] valGroup = temp[0].Split(',');
                    TeacherMarkGroup entityTmg = lstTmg.FirstOrDefault(x => x.TeacherMarkGroupID == Convert.ToInt32(valGroup[0]));
                    entityTmg.Mark = Convert.ToInt32(valGroup[1]);
                    temp.RemoveAt(0);
                    tmgDao.Update(entityTmg);

                    foreach (String valItem in temp) 
                    { 
                        String[] val = valItem.Split(',');
                        TeacherMarkItem entityTmi = lstTmi.FirstOrDefault(x => x.TeacherMarkItemID == Convert.ToInt32(val[0]));
                        entityTmi.Mark = Convert.ToInt32(val[1]);
                        entityTmi.MarkInString = val[2];
                        entityTmi.Remarks = val[3];
                        tmiDao.Update(entityTmi);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
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
    }
}