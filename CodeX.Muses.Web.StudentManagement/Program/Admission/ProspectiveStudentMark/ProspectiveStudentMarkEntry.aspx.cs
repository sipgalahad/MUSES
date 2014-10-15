using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Web.Common;
using System.Data;
using CodeX.Data.Core.Dal;
using CodeX.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ProspectiveStudentMarkEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_PROSPECTIVE_STUDENT_MARK;
        }
        List<AdmissionSelection> lstAdmissionSelection = null;
        protected override void InitializeDataControl()
        {
            lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
            rptHeader.DataSource = lstAdmissionSelection;
            rptHeader.DataBind();

            lstStudentMark = BusinessLayer.GetProspectiveStudentMarkList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID));

            List<vProspectiveStudent> lstStudent = BusinessLayer.GetvProspectiveStudentList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID));
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        List<ProspectiveStudentMark> lstStudentMark = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstAdmissionSelection;
                rptStudentMark.DataBind();
            }
        }

        protected void rptStudentMark_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                AdmissionSelection admissionSelection = (AdmissionSelection)e.Item.DataItem;
                vProspectiveStudent student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vProspectiveStudent;

                ProspectiveStudentMark entity = lstStudentMark.FirstOrDefault(p => p.AdmissionSelectionID == admissionSelection.AdmissionSelectionID && p.ProspectiveStudentID == student.ProspectiveStudentID);
                if (entity != null)
                {
                    TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                    txtStudentMark.Text = entity.Mark.ToString();
                }
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProspectiveStudentMarkDao entityDtDao = new ProspectiveStudentMarkDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
                List<ProspectiveStudentMark> lstStudentMark = BusinessLayer.GetProspectiveStudentMarkList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int studentID = Convert.ToInt32(temp[0]);

                    for (int ctr = 1; ctr < temp.Length; ++ctr)
                    {
                        int admissionSelectionID = lstAdmissionSelection[ctr - 1].AdmissionSelectionID;
                        ProspectiveStudentMark entityDt = lstStudentMark.FirstOrDefault(p => p.ProspectiveStudentID == studentID && p.AdmissionSelectionID == admissionSelectionID);
                        if (temp[ctr] != "")
                        {
                            Int16 mark = Convert.ToInt16(temp[ctr]);
                            if (entityDt == null)
                            {
                                entityDt = new ProspectiveStudentMark();
                                entityDt.PeriodAdmissionID = AppSession.PeriodAdmissionID;
                                entityDt.AdmissionSelectionID = admissionSelectionID;
                                entityDt.ProspectiveStudentID = studentID;
                                entityDt.Mark = mark;
                                entityDtDao.Insert(entityDt);
                            }
                            else
                            {
                                entityDt.Mark = mark;
                                entityDtDao.Update(entityDt);
                            }
                        }
                        else if (entityDt != null)
                        {
                            entityDtDao.Delete(entityDt.PeriodAdmissionID, entityDt.AdmissionSelectionID, entityDt.ProspectiveStudentID);
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