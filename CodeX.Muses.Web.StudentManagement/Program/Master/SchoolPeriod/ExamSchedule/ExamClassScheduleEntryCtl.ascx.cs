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
using CodeX.Web.CustomControl;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ExamClassScheduleEntryCtl : BaseEntryPopupCtl
    {
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetEmployeeFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        List<vSchoolClass> lstSchoolClass = null;
        List<vExamClassSchedule> lstExamClassSchedule = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            vExamScheduleHd entityHd = BusinessLayer.GetvExamScheduleHdList(string.Format("ExamScheduleID = {0}", hdnID.Value)).FirstOrDefault();
            txtHeaderText.Text = entityHd.CurriculumClassTypeName;

            lstSchoolClass = BusinessLayer.GetvSchoolClassList(string.Format("PeriodClassTypeID = {0} AND IsDeleted = 0", entityHd.PeriodClassTypeID));
            rptHeader.DataSource = lstSchoolClass;
            rptHeader.DataBind();

            rptHeaderDt.DataSource = lstSchoolClass;
            rptHeaderDt.DataBind();

            List<vExamScheduleDt> lstEntityDt = BusinessLayer.GetvExamScheduleDtList(string.Format("ExamScheduleID = {0} AND IsDeleted = 0 ORDER BY ExamDate,StartTime", hdnID.Value));
            string lstExamScheduleDtID = string.Join(",", lstEntityDt.Select(p => p.ExamScheduleDtID).ToList());
            lstExamClassSchedule = BusinessLayer.GetvExamClassScheduleList(string.Format("ExamScheduleDtID IN ({0}) AND IsDeleted = 0", lstExamScheduleDtID));
            rptView.DataSource = lstEntityDt;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                rptViewDt.DataSource = lstSchoolClass;
                rptViewDt.DataBind();
            }
        }

        protected void rptViewDt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vExamScheduleDt entityParent = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vExamScheduleDt;
                vSchoolClass entityClass = (vSchoolClass)e.Item.DataItem;
                vExamClassSchedule entitySchedule = lstExamClassSchedule.FirstOrDefault(p => p.ExamScheduleDtID == entityParent.ExamScheduleDtID && p.SchoolClassID == entityClass.SchoolClassID);

                HtmlInputHidden hdnRoomID = (HtmlInputHidden)e.Item.FindControl("hdnRoomID");
                HtmlGenericControl lblRoom = (HtmlGenericControl)e.Item.FindControl("lblRoom");
                HtmlInputHidden hdnEmployeeID = (HtmlInputHidden)e.Item.FindControl("hdnEmployeeID");
                HtmlGenericControl lblEmployee = (HtmlGenericControl)e.Item.FindControl("lblEmployee");
                if (entitySchedule != null)
                {
                    lblRoom.InnerHtml = entitySchedule.RoomName;
                    hdnRoomID.Value = entitySchedule.RoomID.ToString();

                    hdnEmployeeID.Value = entitySchedule.EmployeeID.ToString();
                    if (entitySchedule.EmployeeID > 0)
                        lblEmployee.InnerHtml = entitySchedule.EmployeeName;
                    else
                        lblEmployee.InnerHtml = GetLabel("Pilih Pengawas");
                }
                else
                {
                    lblRoom.InnerHtml = entityClass.RoomName;
                    hdnRoomID.Value = entityClass.RoomID.ToString();

                    hdnEmployeeID.Value = "0";
                    lblEmployee.InnerHtml = GetLabel("Pilih Pengawas");
                }
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ExamClassScheduleDao entityDtDao = new ExamClassScheduleDao(ctx);
            try
            {
                List<ExamClassSchedule> lstExamClassSchedule = BusinessLayer.GetExamClassScheduleList(string.Format("ExamScheduleDtID IN ({0})", hdnListID.Value));
                string[] lstSaveValue = hdnSaveValue.Value.Split('|');
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split('^');
                    int ExamScheduleDtID = Convert.ToInt32(temp[0]);
                    string[] lstSaveValue1 = temp[1].Split(';');
                    foreach (String saveValue1 in lstSaveValue1)
                    {
                        if (saveValue1 != "")
                        {
                            string[] temp1 = saveValue1.Split(',');
                            int schoolClassID = Convert.ToInt32(temp1[0]);

                            ExamClassSchedule entityDt = lstExamClassSchedule.FirstOrDefault(p => p.ExamScheduleDtID == ExamScheduleDtID && p.SchoolClassID == schoolClassID);
                            if (entityDt == null)
                            {
                                entityDt = new ExamClassSchedule();
                                entityDt.ExamScheduleDtID = ExamScheduleDtID;
                                entityDt.SchoolClassID = schoolClassID;
                                entityDt.RoomID = Convert.ToInt32(temp1[1]);
                                entityDt.EmployeeID = Convert.ToInt32(temp1[2]);
                                entityDt.CreatedBy = AppSession.UserLogin.UserID;
                                entityDtDao.Insert(entityDt);
                            }
                            else
                            {
                                entityDt.RoomID = Convert.ToInt32(temp1[1]);
                                entityDt.EmployeeID = Convert.ToInt32(temp1[2]);
                                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityDtDao.Update(entityDt);
                            }
                        }
                    }
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