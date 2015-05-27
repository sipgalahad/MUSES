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

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class ScholarshipEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SCHOLARSHIP;
        }

        List<StudentFeeCompType> lstComp = null;
        protected override void InitializeDataControl()
        {
            List<SchoolPeriod> lstSchoolPeriod = BusinessLayer.GetSchoolPeriodList(string.Format("SiteID = '{0}' AND GCSchoolPeriodStatus != '{1}'", AppSession.UserLogin.SiteID, Constant.SchoolPeriodStatus.VOID));
            Methods.SetComboBoxField<SchoolPeriod>(cboSchoolPeriod, lstSchoolPeriod, "SchoolPeriodName", "SchoolPeriodID");
            SchoolPeriod selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
            if (selectedSchoolPeriod == null)
            {
                cboSchoolPeriod.SelectedIndex = 0;
                selectedSchoolPeriod = lstSchoolPeriod.FirstOrDefault();
            }
            else
                cboSchoolPeriod.Value = selectedSchoolPeriod.SchoolPeriodID.ToString();

            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID != '{1}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.SCHOLARSHIP_TYPE, Constant.ScholarshipType.ADMISSION));
            Methods.SetComboBoxField<StandardCode>(cboScholarshipType, lstStandardCode, "StandardCodeName", "StandardCodeID");

            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
            rptStudentFeeCompType.DataSource = lstComp;
            rptStudentFeeCompType.DataBind();

            BindGridView();

            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 2;

            Helper.SetControlEntrySetting(txtScholarshipName, new ControlEntrySetting(true, true, true), "mpTrx");
            Helper.SetControlEntrySetting(cboScholarshipType, new ControlEntrySetting(true, true, true), "mpTrx");
        }

        protected void rptStudentFeeCompType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtDiscountAmount = (TextBox)e.Item.FindControl("txtDiscountAmount");
                TextBox txtNoOfPeriod = (TextBox)e.Item.FindControl("txtNoOfPeriod");
                Helper.SetControlEntrySetting(txtDiscountAmount, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtNoOfPeriod, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        public override void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {
            IsAllowSave = IsAllowAdd = IsAllowVoid = IsAllowNextPrev = false;
        }

        #region Bind Grid View
        private void BindGridView()
        {
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCScholarshipType != '{1}' AND IsDeleted = 0", cboSchoolPeriod.Value, Constant.ScholarshipType.ADMISSION);
            List<vScholarship> lstEntity = BusinessLayer.GetvScholarshipList(filterExpression);
            if (lstEntity.Count > 0)
            {
                string lstID = string.Join(",", lstEntity.Select(p => p.ScholarshipID).ToList());
                lstScholarshipComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID IN ({0})", lstID));
                lstScholarshipPeriodAdmission = BusinessLayer.GetScholarshipPeriodAdmissionList(string.Format("ScholarshipID IN ({0})", lstID));
                if (lstComp == null)
                {
                    lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
                }
            }
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        List<ScholarshipComp> lstScholarshipComp = null;
        List<ScholarshipPeriodAdmission> lstScholarshipPeriodAdmission = null;
        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vScholarship entity = (vScholarship)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                List<ScholarshipComp> lstDt = lstScholarshipComp.Where(p => p.ScholarshipID == entity.ScholarshipID).ToList();
                List<ScholarshipComp> lstDt1 = new List<ScholarshipComp>();
                foreach (StudentFeeCompType comp in lstComp)
                {
                    ScholarshipComp entityDt = lstDt.FirstOrDefault(p => p.StudentFeeCompTypeID == comp.StudentFeeCompTypeID);
                    if (entityDt == null)
                        entityDt = new ScholarshipComp();
                    lstDt1.Add(entityDt);
                }
                rptViewDt.DataSource = lstDt1;
                rptViewDt.DataBind();
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
        #endregion

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(Scholarship entity)
        {
            entity.ScholarshipName = txtScholarshipName.Text;
            entity.GCFromSchoolType = null;
            entity.GCScholarshipType = cboScholarshipType.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ScholarshipDao entityScholarshipDao = new ScholarshipDao(ctx);
            ScholarshipCompDao entityScholarshipCompDao = new ScholarshipCompDao(ctx);
            try
            {
                Scholarship entityScholarship = new Scholarship();
                ControlToEntity(entityScholarship);
                entityScholarship.SiteID = AppSession.UserLogin.SiteID;
                entityScholarship.SchoolPeriodID = Convert.ToInt32(cboSchoolPeriod.Value);
                entityScholarship.CreatedBy = AppSession.UserLogin.UserID;
                entityScholarshipDao.Insert(entityScholarship);

                entityScholarship.ScholarshipID = BusinessLayer.GetScholarshipMaxID(ctx);

                string[] lstSaveCompValue = hdnStudentFeeCompTypeSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveCompValue)
                {
                    string[] temp = saveValue.Split(';');

                    ScholarshipComp entityDt = new ScholarshipComp();
                    entityDt.ScholarshipID = entityScholarship.ScholarshipID;
                    entityDt.StudentFeeCompTypeID = Convert.ToInt32(temp[0]);
                    entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                    entityDt.IsDiscountInPercentage = temp[2] == "1";
                    entityDt.NoOfPeriod = Convert.ToInt16(temp[3]);
                    entityScholarshipCompDao.Insert(entityDt);
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

        private bool OnSaveEditRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ScholarshipDao entityScholarshipDao = new ScholarshipDao(ctx);
            ScholarshipCompDao entityScholarshipCompDao = new ScholarshipCompDao(ctx);
            try
            {
                Scholarship entityScholarship = entityScholarshipDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityScholarship);
                entityScholarship.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityScholarshipDao.Update(entityScholarship);

                List<ScholarshipComp> lstEntityComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID = {0}", entityScholarship.ScholarshipID), ctx);
                string[] lstSaveValue = hdnStudentFeeCompTypeSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int StudentFeeCompTypeID = Convert.ToInt32(temp[0]);
                    ScholarshipComp entityDt = lstEntityComp.FirstOrDefault(p => p.StudentFeeCompTypeID == StudentFeeCompTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new ScholarshipComp();
                        entityDt.ScholarshipID = entityScholarship.ScholarshipID;
                        entityDt.StudentFeeCompTypeID = StudentFeeCompTypeID;
                        entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                        entityDt.IsDiscountInPercentage = temp[2] == "1";
                        entityDt.NoOfPeriod = Convert.ToInt16(temp[3]);
                        entityScholarshipCompDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                        entityDt.IsDiscountInPercentage = temp[2] == "1";
                        entityDt.NoOfPeriod = Convert.ToInt16(temp[3]);
                        entityScholarshipCompDao.Update(entityDt);
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

        private bool OnDeleteEntityDt(ref string errMessage)
        {
            try
            {
                Scholarship entity = BusinessLayer.GetScholarship(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedDate = DateTime.Now;
                BusinessLayer.UpdateScholarship(entity);
                return true;
            }
            catch (Exception ex)
            {
                Helper.InsertErrorLog(ex);
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion
    }
}