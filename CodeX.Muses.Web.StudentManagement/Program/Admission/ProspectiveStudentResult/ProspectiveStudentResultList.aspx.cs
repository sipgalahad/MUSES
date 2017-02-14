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
using DevExpress.Web.ASPxEditors;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ProspectiveStudentResultList : BasePageTrx
    {
        protected int PageCount = 1;
        protected int RowCount = 1;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.PA_PROSPECTIVE_STUDENT_RESULT;
        }
        List<AdmissionSelection> lstAdmissionSelection = null;

        protected override void InitializeDataControl()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID != '{1}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.REGISTRATION_STATUS, Constant.RegistrationStatus.VOID));
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboProspectiveStudentStatus, lstSc, "StandardCodeName", "StandardCodeID");
            cboProspectiveStudentStatus.SelectedIndex = 0;

            hdnSchoolPeriodID.Value = BusinessLayer.GetPeriodAdmission(AppSession.PeriodAdmissionID).SchoolPeriodID.ToString();

            lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
            rptHeader.DataSource = lstAdmissionSelection;
            rptHeader.DataBind();

            rptHeader2.DataSource = lstAdmissionSelection;
            rptHeader2.DataBind();

            if (lstAdmissionSelection.Count == 0)
                thMarkHeader.Style.Add("display", "none");
            else
                thMarkHeader.ColSpan = lstAdmissionSelection.Count * 2;

            RowCountPerPage = Constant.GridViewPageSize.GRID_MASTER;
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);
        }

        List<vPeriodClassType> lstPeriodClassType = null;
        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = string.Format("PeriodAdmissionID = {0} AND GCRegistrationStatus NOT IN ('{1}','{2}') AND RegistrationNo LIKE '%{3}%' AND ProspectiveStudentName LIKE '%{4}%'", AppSession.PeriodAdmissionID, Constant.RegistrationStatus.VOID, Constant.RegistrationStatus.CLOSED, hdnFilterCode.Value, hdnFilterName.Value);
            if (cboProspectiveStudentStatus.Value != null && cboProspectiveStudentStatus.Value.ToString() != "")
                filterExpression += string.Format(" AND GCRegistrationStatus = '{0}'", cboProspectiveStudentStatus.Value.ToString());
            else
                filterExpression += string.Format(" AND GCRegistrationStatus != '{0}'", Constant.RegistrationStatus.VOID);
            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstPeriodClassType = BusinessLayer.GetvPeriodClassTypeList(string.Format("SchoolPeriodID = {0} AND IsDeleted = 0", hdnSchoolPeriodID.Value));

            List<vRegistration> lstStudent = BusinessLayer.GetvRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "FinalMark DESC");
            if (lstStudent.Count > 0)
            {
                string lstRegistrationID = string.Join(",", lstStudent.Select(p => p.RegistrationID).ToList());
                if (lstAdmissionSelection == null)
                    lstAdmissionSelection = BusinessLayer.GetAdmissionSelectionList(string.Format("PeriodAdmissionID = {0} AND IsDeleted = 0", AppSession.PeriodAdmissionID));
                lstStudentMark = BusinessLayer.GetRegistrationMarkList(string.Format("RegistrationID IN ({0})", lstRegistrationID));
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
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
        private string[] lstSelectedMember = null;
        protected void rptStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vRegistration entity = (vRegistration)e.Item.DataItem;

                Repeater rptStudentMark = (Repeater)e.Item.FindControl("rptStudentMark");
                rptStudentMark.DataSource = lstAdmissionSelection;
                rptStudentMark.DataBind();

                HtmlInputHidden hdnIsSelectMajor = (HtmlInputHidden)e.Item.FindControl("hdnIsSelectMajor");
                ASPxComboBox cboGCMajor = (ASPxComboBox)e.Item.FindControl("cboGCMajor");
                cboGCMajor.ClientInstanceName = string.Format("cboGCMajor{0}", e.Item.ItemIndex);
                List<vPeriodClassType> lst = lstPeriodClassType.Where(p => p.GCGrade == entity.GCGrade && p.GCMajor != "").ToList();
                Methods.SetComboBoxField<vPeriodClassType>(cboGCMajor, lst, "Major", "GCMajor");
                cboGCMajor.Value = entity.GCMajor;
                if (lst.Count > 0)
                {
                    Helper.SetControlEntrySetting(cboGCMajor, new ControlEntrySetting(true, true, true), "mpEntry");
                    hdnIsSelectMajor.Value = "1";
                }
                else
                {
                    cboGCMajor.ClientVisible = false;
                    hdnIsSelectMajor.Value = "0";
                }

                CheckBox chkIsSelected = e.Item.FindControl("chkIsSelected") as CheckBox;
                if (entity.GCRegistrationStatus == Constant.RegistrationStatus.AR_PROCESSED || entity.GCRegistrationStatus == Constant.RegistrationStatus.PAID || entity.GCRegistrationStatus == Constant.RegistrationStatus.SETTLED || entity.GCRegistrationStatus == Constant.RegistrationStatus.CLOSED)
                    chkIsSelected.Visible = false;
                if (lstSelectedMember.Contains(entity.ProspectiveStudentID.ToString()))
                    chkIsSelected.Checked = true;
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
                    HtmlGenericControl divStudentMark = (HtmlGenericControl)e.Item.FindControl("divStudentMark");
                    HtmlGenericControl divStudentMarkRemarks = (HtmlGenericControl)e.Item.FindControl("divStudentMarkRemarks");
                    divStudentMark.InnerHtml = entity.Mark.ToString();
                    divStudentMarkRemarks.InnerHtml = entity.Remarks;
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
            RegistrationDao entityDao = new RegistrationDao(ctx);
            try
            {
                if (type == "accept")
                {
                    string[] lstRegistrationID = hdnSelectedMember.Value.Substring(1).Split(',');
                    string[] lstGCMajor = hdnLstGCMajor.Value.Substring(1).Split(',');
                    string filterExpression = String.Format("RegistrationID IN ({0})", hdnSelectedMember.Value.Substring(1));
                    List<Registration> lstEntity = BusinessLayer.GetRegistrationList(filterExpression, ctx);
                    for (int i = 0; i < lstRegistrationID.Length;++i)
                    {
                        Registration entity = lstEntity.FirstOrDefault(p => p.RegistrationID == Convert.ToInt32(lstRegistrationID[i]));
                        entity.GCRegistrationStatus = Constant.RegistrationStatus.ACCEPTED;
                        entity.GCMajor = lstGCMajor[i];
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                else if (type == "reject")
                {
                    string[] lstRegistrationID = hdnSelectedMember.Value.Substring(1).Split(',');
                    string[] lstGCMajor = hdnLstGCMajor.Value.Substring(1).Split(',');
                    string filterExpression = String.Format("RegistrationID IN ({0})", hdnSelectedMember.Value.Substring(1));
                    List<Registration> lstEntity = BusinessLayer.GetRegistrationList(filterExpression, ctx);
                    for (int i = 0; i < lstRegistrationID.Length; ++i)
                    {
                        Registration entity = lstEntity.FirstOrDefault(p => p.RegistrationID == Convert.ToInt32(lstRegistrationID[i]));
                        entity.GCRegistrationStatus = Constant.RegistrationStatus.REJECTED;
                        entity.GCMajor = lstGCMajor[i];
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                else
                {
                    string[] lstRegistrationID = hdnSelectedMember.Value.Substring(1).Split(',');
                    string[] lstGCMajor = hdnLstGCMajor.Value.Substring(1).Split(',');
                    string filterExpression = String.Format("RegistrationID IN ({0})", hdnSelectedMember.Value.Substring(1));
                    List<Registration> lstEntity = BusinessLayer.GetRegistrationList(filterExpression, ctx);
                    for (int i = 0; i < lstRegistrationID.Length; ++i)
                    {
                        Registration entity = lstEntity.FirstOrDefault(p => p.RegistrationID == Convert.ToInt32(lstRegistrationID[i]));
                        entity.GCRegistrationStatus = Constant.RegistrationStatus.OPEN;
                        entity.GCMajor = lstGCMajor[i];
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
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