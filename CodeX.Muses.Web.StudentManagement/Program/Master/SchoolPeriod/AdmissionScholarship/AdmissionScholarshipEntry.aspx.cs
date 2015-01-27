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
    public partial class AdmissionScholarshipEntry : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.SP_ADMISSION_SCHOLARSHIP;
        }

        List<StudentFeeCompType> lstComp = null;
        List<PeriodAdmission> lstAdmission = null;
        protected override void InitializeDataControl()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsActive = 1 AND IsDeleted = 0", Constant.StandardCode.FROM_SCHOOL_TYPE));
            lstStandardCode.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "- All -" });
            Methods.SetComboBoxField<StandardCode>(cboFromSchoolType, lstStandardCode, "StandardCodeName", "StandardCodeID");

            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            rptStudentFeeCompType.DataSource = lstComp;
            rptStudentFeeCompType.DataBind();

            lstAdmission = BusinessLayer.GetPeriodAdmissionList(string.Format("SchoolPeriodID = {0} AND GCPeriodAdmissionStatus != '{1}'", AppSession.SchoolPeriodID, Constant.SchoolPeriodStatus.VOID));
            rptPeriodAdmission.DataSource = lstAdmission;
            rptPeriodAdmission.DataBind();

            BindGridView();

            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 2;
            thPeriodAdmission.ColSpan = lstAdmission.Count;

            rptPeriodAdmissionView.DataSource = lstAdmission;
            rptPeriodAdmissionView.DataBind();

            Helper.SetControlEntrySetting(txtScholarshipName, new ControlEntrySetting(true, true, true), "mpTrx");
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
            string filterExpression = string.Format("SchoolPeriodID = {0} AND GCScholarshipType = '{1}' AND IsDeleted = 0", AppSession.SchoolPeriodID, Constant.ScholarshipType.ADMISSION);
            List<Scholarship> lstEntity = BusinessLayer.GetScholarshipList(filterExpression);
            if (lstEntity.Count > 0)
            {
                string lstID = string.Join(",", lstEntity.Select(p => p.ScholarshipID).ToList());
                lstScholarshipComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID IN ({0})", lstID));
                lstScholarshipPeriodAdmission = BusinessLayer.GetScholarshipPeriodAdmissionList(string.Format("ScholarshipID IN ({0})", lstID));
                if (lstComp == null)
                {
                    lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
                    lstAdmission = BusinessLayer.GetPeriodAdmissionList(string.Format("SchoolPeriodID = {0} AND GCPeriodAdmissionStatus != '{1}'", AppSession.SchoolPeriodID, Constant.SchoolPeriodStatus.VOID));
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
                Scholarship entity = (Scholarship)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                Repeater rptViewDtAdmission = (Repeater)e.Item.FindControl("rptViewDtAdmission");
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

                rptViewDtAdmission.DataSource = lstAdmission;
                rptViewDtAdmission.DataBind();
            }
        }

        protected void rptViewDtAdmission_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PeriodAdmission entity = (PeriodAdmission)e.Item.DataItem;
                var repeater = (Repeater)sender;
                var parentItem = (RepeaterItem)repeater.NamingContainer;
                Scholarship entityHd = (Scholarship)parentItem.DataItem;
                ScholarshipPeriodAdmission entityPeriodAdmission = lstScholarshipPeriodAdmission.FirstOrDefault(p => p.ScholarshipID == entityHd.ScholarshipID && p.PeriodAdmissionID == entity.PeriodAdmissionID);
                if (entityPeriodAdmission != null)
                {
                    CheckBox chkPeriodAdmissionView = (CheckBox)e.Item.FindControl("chkPeriodAdmissionView");
                    chkPeriodAdmissionView.Checked = true;
                }
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
            if (cboFromSchoolType.Value == null || cboFromSchoolType.Value.ToString() == "")
                entity.GCFromSchoolType = null;
            else
                entity.GCFromSchoolType = cboFromSchoolType.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ScholarshipDao entityScholarshipDao = new ScholarshipDao(ctx);
            ScholarshipCompDao entityScholarshipCompDao = new ScholarshipCompDao(ctx);
            ScholarshipPeriodAdmissionDao entityScholarshipPeriodAdmissionDao = new ScholarshipPeriodAdmissionDao(ctx);
            try
            {
                Scholarship entityScholarship = new Scholarship();
                ControlToEntity(entityScholarship);
                entityScholarship.SiteID = AppSession.UserLogin.SiteID;
                entityScholarship.SchoolPeriodID = AppSession.SchoolPeriodID;
                entityScholarship.GCScholarshipType = Constant.ScholarshipType.ADMISSION;
                entityScholarship.CreatedBy = AppSession.UserLogin.UserID;
                entityScholarshipDao.Insert(entityScholarship);

                entityScholarship.ScholarshipID = BusinessLayer.GetScholarshipMaxID(ctx);

                if (hdnPeriodAdmissionSaveValue.Value != "")
                {
                    string[] lstSavePeriodAdmissionValue = hdnPeriodAdmissionSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSavePeriodAdmissionValue)
                    {
                        ScholarshipPeriodAdmission entityDt = new ScholarshipPeriodAdmission();
                        entityDt.ScholarshipID = entityScholarship.ScholarshipID;
                        entityDt.PeriodAdmissionID = Convert.ToInt32(saveValue);
                        entityScholarshipPeriodAdmissionDao.Insert(entityDt);
                    }
                }

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
            ScholarshipPeriodAdmissionDao entityScholarshipPeriodAdmissionDao = new ScholarshipPeriodAdmissionDao(ctx);
            try
            {
                Scholarship entityScholarship = entityScholarshipDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityScholarship);
                entityScholarship.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityScholarshipDao.Update(entityScholarship);

                List<ScholarshipPeriodAdmission> lstEntityPeriodAdmission = BusinessLayer.GetScholarshipPeriodAdmissionList(string.Format("ScholarshipID = {0}", entityScholarship.ScholarshipID), ctx);
                if (hdnPeriodAdmissionSaveValue.Value != "")
                {
                    string[] lstSavePeriodAdmissionValue = hdnPeriodAdmissionSaveValue.Value.Split('|');
                    foreach (string saveValue in lstSavePeriodAdmissionValue)
                    {
                        int PeriodAdmissionID = Convert.ToInt32(saveValue);
                        ScholarshipPeriodAdmission entityDt = lstEntityPeriodAdmission.FirstOrDefault(p => p.PeriodAdmissionID == PeriodAdmissionID);
                        if (entityDt == null)
                        {
                            entityDt = new ScholarshipPeriodAdmission();
                            entityDt.ScholarshipID = entityScholarship.ScholarshipID;
                            entityDt.PeriodAdmissionID = PeriodAdmissionID;
                            entityScholarshipPeriodAdmissionDao.Insert(entityDt);
                        }
                        else
                            lstEntityPeriodAdmission.Remove(entityDt);
                    }
                }
                foreach (ScholarshipPeriodAdmission entityDt in lstEntityPeriodAdmission)
                {
                    entityScholarshipPeriodAdmissionDao.Delete(entityDt.ScholarshipID, entityDt.PeriodAdmissionID);
                }

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