using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using CodeX.Web.Common;
using CodeX.Common;

namespace CodeX.Muses.Web.StudentManagement.Report
{
    public partial class BRaporMidSemesterRpt : BaseCustomReportCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        //List<StudentFee> lstRegFee = null;

        public override void Bind(string filterExpression, string[] param)
        {
            
        }

        protected void rptPayment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                //vStudentFeeComp entity = (vStudentFeeComp)e.Item.DataItem;
                //Repeater rptPaymentDt = (Repeater)e.Item.FindControl("rptPaymentDt");
                //rptPaymentDt.DataSource = lstRegFee.Where(x => x.StudentFeeCompID == entity.StudentFeeCompID);
                //rptPaymentDt.DataBind();
            }
        }
    }
}