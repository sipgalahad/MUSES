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
    public partial class OrganizationMarkList : BasePageTrx
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.StudentManagement.CS_ORGANIZATION_MARK;
        }

        protected override void InitializeDataControl()
        {
            List<Variable> lstEntity = new List<Variable>();
            string lstOrganizationID = "";

            List<vOrganizationDt> lstOrganization = BusinessLayer.GetvOrganizationDtList(string.Format("StudentCoordinatorID = {0}", AppSession.ClassStudent.StudentID));
            foreach (vOrganizationDt entity in lstOrganization)
            {
                if (lstOrganizationID != "")
                    lstOrganizationID += ",";
                lstOrganizationID += entity.OrganizationID.ToString();

                Variable newEntity = new Variable();
                newEntity.Code = entity.OrganizationName;
                newEntity.Value = entity.Position;
                lstEntity.Add(newEntity);
            }

            List<vOrganizationDtStudent> lstOrganizationStudent = BusinessLayer.GetvOrganizationDtStudentList(string.Format("StudentID = {0}", AppSession.ClassStudent.StudentID));
            foreach (vOrganizationDtStudent entity in lstOrganizationStudent)
            {
                if (lstOrganizationID != "")
                    lstOrganizationID += ",";
                lstOrganizationID += entity.OrganizationID.ToString();

                Variable newEntity = new Variable();
                newEntity.Code = entity.OrganizationName;
                newEntity.Value = entity.Position;
                lstEntity.Add(newEntity);
            }

            string filterExpression = "IsAllStudentAsMember = 1 AND IsDeleted = 0";
            if (lstOrganizationID != "")
                filterExpression = string.Format("OrganizationID NOT IN ({0}) AND IsAllStudentAsMember = 1 AND IsDeleted = 0", lstOrganizationID);
            List<OrganizationHd> lstOrganizationHd = BusinessLayer.GetOrganizationHdList(filterExpression);
            foreach (OrganizationHd entity in lstOrganizationHd)
            {
                Variable newEntity = new Variable();
                newEntity.Code = entity.OrganizationName;
                newEntity.Value = GetLabel("Anggota");
                lstEntity.Add(newEntity);
            }

            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
    }
}