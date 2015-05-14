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
    public partial class CoverageTypeDtEntryCtl : BaseViewPopupCtl
    {
        List<StudentFeeCompType> lstComp = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            CoverageType entity = BusinessLayer.GetCoverageType(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.CoverageTypeCode, entity.CoverageTypeName);

            Repeater rptClassType = (Repeater)ddeClassType.FindControl("rptClassType");
            List<ClassType> lstClassType = BusinessLayer.GetClassTypeList(string.Format("GCClassStudyType = '{0}' AND IsDeleted = 0", Constant.ClassStudyType.REGULAR));
            rptClassType.DataSource = lstClassType;
            rptClassType.DataBind();

            lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            rptStudentFeeCompType.DataSource = lstComp;
            rptStudentFeeCompType.DataBind();

            BindGridView();

            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 3;

            Helper.SetControlEntrySetting(txtCoverageTypeDtName, new ControlEntrySetting(true, true, true), "mpTrxPopup");
            Helper.SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, true), "mpTrxPopup");
        }

        protected void rptClassType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ClassType obj = (ClassType)e.Item.DataItem;
                CheckBox chkClassType = (CheckBox)e.Item.FindControl("chkClassType");
                chkClassType.Attributes.Add("classtypename", obj.ClassTypeName);
                chkClassType.Attributes.Add("classtypeid", obj.ClassTypeID.ToString());
            }
        }

        protected void rptStudentFeeCompType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                TextBox txtDiscountAmount = (TextBox)e.Item.FindControl("txtDiscountAmount");
                TextBox txtCoverageAmount = (TextBox)e.Item.FindControl("txtCoverageAmount");
                TextBox txtNoOfPeriod = (TextBox)e.Item.FindControl("txtNoOfPeriod");
                Helper.SetControlEntrySetting(txtDiscountAmount, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtCoverageAmount, new ControlEntrySetting(true, true, true), "mpTrx");
                Helper.SetControlEntrySetting(txtNoOfPeriod, new ControlEntrySetting(true, true, true), "mpTrx");
            }
        }

        List<CoverageTypeDtComp> lstCoverageTypeDtComp = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("CoverageTypeID = {0} AND IsDeleted = 0", hdnID.Value);
            List<vCoverageTypeDt> lstEntity = BusinessLayer.GetvCoverageTypeDtList(filterExpression);
            if (lstEntity.Count > 0)
            {
                string lstID = string.Join(",", lstEntity.Select(p => p.CoverageTypeDtID).ToList());
                lstCoverageTypeDtComp = BusinessLayer.GetCoverageTypeDtCompList(string.Format("CoverageTypeDtID IN ({0})", lstID));
                if (lstComp == null)
                    lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID));
            }
            rptView.DataSource = lstEntity;
            rptView.DataBind();
        }

        protected void rptView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                vCoverageTypeDt entity = (vCoverageTypeDt)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                List<CoverageTypeDtComp> lstDt = lstCoverageTypeDtComp.Where(p => p.CoverageTypeDtID == entity.CoverageTypeDtID).ToList();
                List<CoverageTypeDtComp> lstDt1 = new List<CoverageTypeDtComp>();
                foreach (StudentFeeCompType comp in lstComp)
                {
                    CoverageTypeDtComp entityDt = lstDt.FirstOrDefault(p => p.StudentFeeCompTypeID == comp.StudentFeeCompTypeID);
                    if (entityDt == null)
                        entityDt = new CoverageTypeDtComp();
                    lstDt1.Add(entityDt);
                }
                rptViewDt.DataSource = lstDt1;
                rptViewDt.DataBind();
            }
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        #region Process Detail
        protected void cbpProcessPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private void ControlToEntity(CoverageTypeDt entity)
        {
            entity.CoverageTypeDtName = txtCoverageTypeDtName.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecordEntityDt(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CoverageTypeDtDao entityCoverageTypeDtDao = new CoverageTypeDtDao(ctx);
            CoverageTypeDtCompDao entityCoverageTypeDtCompDao = new CoverageTypeDtCompDao(ctx);
            CoverageTypeDtClassTypeDao entityCoverageTypeDtClassTypeDao = new CoverageTypeDtClassTypeDao(ctx);
            try
            {
                CoverageTypeDt entityCoverageTypeDt = new CoverageTypeDt();
                ControlToEntity(entityCoverageTypeDt);
                entityCoverageTypeDt.CoverageTypeID = Convert.ToInt32(hdnID.Value);
                entityCoverageTypeDt.CreatedBy = AppSession.UserLogin.UserID;
                entityCoverageTypeDtDao.Insert(entityCoverageTypeDt);

                entityCoverageTypeDt.CoverageTypeDtID = BusinessLayer.GetCoverageTypeDtMaxID(ctx);

                string[] lstSaveCompValue = hdnStudentFeeCompTypeSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveCompValue)
                {
                    string[] temp = saveValue.Split(';');

                    CoverageTypeDtComp entityDt = new CoverageTypeDtComp();
                    entityDt.CoverageTypeDtID = entityCoverageTypeDt.CoverageTypeDtID;
                    entityDt.StudentFeeCompTypeID = Convert.ToInt32(temp[0]);
                    entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                    entityDt.IsDiscountInPercentage = temp[2] == "1";
                    entityDt.CoverageAmount = Convert.ToDecimal(temp[3]);
                    entityDt.IsCoverageInPercentage = temp[4] == "1";
                    entityDt.NoOfPeriod = Convert.ToInt16(temp[5]);
                    entityCoverageTypeDtCompDao.Insert(entityDt);
                }

                string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                foreach (string classTypeID in lstClassTypeID)
                {
                    CoverageTypeDtClassType entityDt = new CoverageTypeDtClassType();
                    entityDt.CoverageTypeDtID = entityCoverageTypeDt.CoverageTypeDtID;
                    entityDt.ClassTypeID = Convert.ToInt32(classTypeID);
                    entityCoverageTypeDtClassTypeDao.Insert(entityDt);
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
            CoverageTypeDtDao entityCoverageTypeDtDao = new CoverageTypeDtDao(ctx);
            CoverageTypeDtCompDao entityCoverageTypeDtCompDao = new CoverageTypeDtCompDao(ctx);
            CoverageTypeDtClassTypeDao entityCoverageTypeDtClassTypeDao = new CoverageTypeDtClassTypeDao(ctx);
            try
            {
                CoverageTypeDt entityCoverageTypeDt = entityCoverageTypeDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entityCoverageTypeDt);
                entityCoverageTypeDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityCoverageTypeDtDao.Update(entityCoverageTypeDt);

                List<CoverageTypeDtComp> lstEntityComp = BusinessLayer.GetCoverageTypeDtCompList(string.Format("CoverageTypeDtID = {0}", entityCoverageTypeDt.CoverageTypeDtID), ctx);
                string[] lstSaveValue = hdnStudentFeeCompTypeSaveValue.Value.Split('|');
                foreach (string saveValue in lstSaveValue)
                {
                    string[] temp = saveValue.Split(';');
                    int StudentFeeCompTypeID = Convert.ToInt32(temp[0]);
                    CoverageTypeDtComp entityDt = lstEntityComp.FirstOrDefault(p => p.StudentFeeCompTypeID == StudentFeeCompTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new CoverageTypeDtComp();
                        entityDt.CoverageTypeDtID = entityCoverageTypeDt.CoverageTypeDtID;
                        entityDt.StudentFeeCompTypeID = StudentFeeCompTypeID;
                        entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                        entityDt.IsDiscountInPercentage = temp[2] == "1";
                        entityDt.CoverageAmount = Convert.ToDecimal(temp[3]);
                        entityDt.IsCoverageInPercentage = temp[4] == "1";
                        entityDt.NoOfPeriod = Convert.ToInt16(temp[5]);
                        entityCoverageTypeDtCompDao.Insert(entityDt);
                    }
                    else
                    {
                        entityDt.DiscountAmount = Convert.ToDecimal(temp[1]);
                        entityDt.IsDiscountInPercentage = temp[2] == "1";
                        entityDt.CoverageAmount = Convert.ToDecimal(temp[3]);
                        entityDt.IsCoverageInPercentage = temp[4] == "1";
                        entityDt.NoOfPeriod = Convert.ToInt16(temp[5]);
                        entityCoverageTypeDtCompDao.Update(entityDt);
                    }
                }

                List<CoverageTypeDtClassType> lstEntityDt = BusinessLayer.GetCoverageTypeDtClassTypeList(string.Format("CoverageTypeDtID = {0}", entityCoverageTypeDt.CoverageTypeDtID), ctx);
                string[] lstClassTypeID = hdnLstClassTypeID.Value.Split(',');
                foreach (string classTypeID in lstClassTypeID)
                {
                    CoverageTypeDtClassType entityDt = lstEntityDt.FirstOrDefault(p => p.ClassTypeID.ToString() == classTypeID);
                    if (entityDt == null)
                    {
                        entityDt = new CoverageTypeDtClassType();
                        entityDt.CoverageTypeDtID = entityCoverageTypeDt.CoverageTypeDtID;
                        entityDt.ClassTypeID = Convert.ToInt32(classTypeID);
                        entityCoverageTypeDtClassTypeDao.Insert(entityDt);
                    }
                    else
                        lstEntityDt.Remove(entityDt);
                }

                foreach (CoverageTypeDtClassType entityDt in lstEntityDt)
                {
                    entityCoverageTypeDtClassTypeDao.Delete(entityDt.CoverageTypeDtID, entityDt.ClassTypeID);
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
                CoverageTypeDt entity = BusinessLayer.GetCoverageTypeDt(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCoverageTypeDt(entity);
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