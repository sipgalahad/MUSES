using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Data.Service;

namespace QIS.Medinfras.Web.EMR.Program
{
    public partial class PatientDataView : BasePageContent
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.EMR.REGISTERED_PATIENT;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }
    }
}