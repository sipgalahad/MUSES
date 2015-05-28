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
    public partial class ScholarshipDtViewCtl : BaseViewPopupCtl
    {
        List<StudentFeeCompType> lstComp = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;

            BindGridView();

            rptStudentFeeCompTypeView.DataSource = lstComp;
            rptStudentFeeCompTypeView.DataBind();

            rptStudentFeeCompTypeView2.DataSource = lstComp;
            rptStudentFeeCompTypeView2.DataBind();

            thFeeComp.ColSpan = lstComp.Count * 2;
        }

        List<ScholarshipComp> lstScholarshipDtComp = null;
        private void BindGridView()
        {
            string filterExpression = string.Format("ScholarshipID = {0} AND IsDeleted = 0", hdnID.Value);
            List<Scholarship> lstEntity = BusinessLayer.GetScholarshipList(filterExpression);
            if (lstEntity.Count > 0)
            {
                string lstID = string.Join(",", lstEntity.Select(p => p.ScholarshipID).ToList());
                lstScholarshipDtComp = BusinessLayer.GetScholarshipCompList(string.Format("ScholarshipID IN ({0})", lstID));
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
                Scholarship entity = (Scholarship)e.Item.DataItem;
                Repeater rptViewDt = (Repeater)e.Item.FindControl("rptViewDt");
                List<ScholarshipComp> lstDt = lstScholarshipDtComp.Where(p => p.ScholarshipID == entity.ScholarshipID).ToList();
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

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }
    }
}