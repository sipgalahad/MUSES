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

namespace CodeX.Muses.Web.Finance.Program
{
    public partial class CoverageTypeDtViewCtl : BaseViewPopupCtl
    {
        List<StudentFeeCompType> lstComp = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            CoverageType entity = BusinessLayer.GetCoverageType(Convert.ToInt32(hdnID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.CoverageTypeCode, entity.CoverageTypeName);

            BindGridView();

            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 3;
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
                    lstComp = BusinessLayer.GetStudentFeeCompTypeList(string.Format("IsDeleted = 0"));
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
    }
}