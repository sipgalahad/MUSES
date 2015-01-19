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
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
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

            rptHeader2.DataSource = lstAdmissionSelection;
            rptHeader2.DataBind();

            thMarkHeader.ColSpan = lstAdmissionSelection.Count * 2;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus NOT IN ('{1}','{2}') AND RegistrationNo LIKE '%{3}%' AND ProspectiveStudentName LIKE '%{4}%'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID, Constant.RegistrationStatus.CLOSED, hdnFilterCode.Value, hdnFilterName.Value);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vRegistration> lstStudent = BusinessLayer.GetvRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            if (lstStudent.Count > 0)
            {
                string lstRegistrationID = string.Join(",", lstStudent.Select(p => p.RegistrationID).ToList());
                if (lstAdmissionSelection == null)
                    lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
                lstStudentMark = BusinessLayer.GetRegistrationMarkList(string.Format("RegistrationID IN ({0})", lstRegistrationID));
            }
            rptStudent.DataSource = lstStudent;
            rptStudent.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        List<RegistrationMark> lstStudentMark = null;
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
                vRegistration student = ((RepeaterItem)e.Item.Parent.Parent).DataItem as vRegistration;

                RegistrationMark entity = lstStudentMark.FirstOrDefault(p => p.AdmissionSelectionID == admissionSelection.AdmissionSelectionID && p.RegistrationID == student.RegistrationID);
                if (entity != null)
                {
                    TextBox txtStudentMark = (TextBox)e.Item.FindControl("txtStudentMark");
                    TextBox txtStudentMarkRemarks = (TextBox)e.Item.FindControl("txtStudentMarkRemarks");
                    txtStudentMark.Text = entity.Mark.ToString();
                    txtStudentMarkRemarks.Text = entity.Remarks;
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
            RegistrationMarkDao entityDtDao = new RegistrationMarkDao(ctx);
            RegistrationDao entityRegistrationDao = new RegistrationDao(ctx);
            try
            {
                string[] lstSaveValue = hdnListSaveValue.Value.Split('|');
                lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
                List<Registration> lstStudent = BusinessLayer.GetRegistrationList(string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus != '{1}'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID), ctx);
                List<RegistrationMark> lstStudentMark = BusinessLayer.GetRegistrationMarkList(string.Format("PeriodAdmissionID = {0}", AppSession.PeriodAdmissionID), ctx);
                foreach (String saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(',');
                    int registrationID = Convert.ToInt32(temp[0]);

                    Decimal finalMark = 0;
                    for (int ctr = 1; ctr < temp.Length; ++ctr)
                    {
                        AdmissionSelection admissionSelection = lstAdmissionSelection[ctr - 1];
                        RegistrationMark entityDt = lstStudentMark.FirstOrDefault(p => p.RegistrationID == registrationID && p.AdmissionSelectionID == admissionSelection.AdmissionSelectionID);

                        string[] tempValue = temp[ctr].Split(';');
                        if (tempValue[0] != "")
                        {
                            Decimal mark = Convert.ToDecimal(tempValue[0]);
                            if (entityDt == null)
                            {
                                entityDt = new RegistrationMark();
                                entityDt.PeriodAdmissionID = AppSession.PeriodAdmissionID;
                                entityDt.AdmissionSelectionID = admissionSelection.AdmissionSelectionID;
                                entityDt.RegistrationID = registrationID;
                                entityDt.Mark = mark;
                                entityDt.Remarks = tempValue[1];
                                entityDtDao.Insert(entityDt);
                            }
                            else
                            {
                                entityDt.Mark = mark;
                                entityDt.Remarks = tempValue[1];
                                entityDtDao.Update(entityDt);
                            }

                            finalMark += (mark * admissionSelection.FinalMarkPercentage / 100);
                        }
                        else if (entityDt != null)
                        {
                            entityDtDao.Delete(entityDt.PeriodAdmissionID, entityDt.AdmissionSelectionID, entityDt.RegistrationID);
                        }
                    }

                    Registration registration = lstStudent.FirstOrDefault(p => p.RegistrationID == registrationID);
                    registration.FinalMark = finalMark;
                    registration.LastUpdatedBy = AppSession.UserLogin.UserID;
                    entityRegistrationDao.Update(registration);
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