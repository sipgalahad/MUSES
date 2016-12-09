using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common;
using CodeX.Data.Model;
using System.Web.Security;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common.UI;

namespace CodeX.Muses.Web.Mobile.Program
{
    public partial class StudentBillInformation : BasePageContent
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.Mobile.STUDENT_BILL_INFO;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnStudentID.Value = "5";
                GetARStudentPerDate entity = BusinessLayer.GetARStudentPerDate(false, hdnStudentID.Value, DateTime.Now).FirstOrDefault();
                divUpemb.InnerHtml = entity.Col1.ToString("N");
                divUsek.InnerHtml = entity.Col2.ToString("N");
                divUkeg.InnerHtml = entity.Col3.ToString("N");

                if (entity.Col1 != 0 || entity.Col2 != 0 || entity.Col3 != 0)
                    divEmptyBill.Style.Add("display", "none");
                else
                    divBill.Style.Add("display", "none");
            }
        }
    }
}