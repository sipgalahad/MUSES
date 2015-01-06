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
using CodeX.Web.CustomControl;

namespace CodeX.Muses.Web.StudentManagement.Program
{
    public partial class GenerateSchoolClassEntryCtl : BaseEntryPopupCtl
    {
        protected string OnGetRoomFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        protected string OnGetTeacherFilterExpression()
        {
            return string.Format("SiteID = '{0}' AND IsDeleted = 0", AppSession.UserLogin.SiteID);
        }

        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            vPeriodClassType entity = BusinessLayer.GetvPeriodClassTypeList(string.Format("PeriodClassTypeID = {0}", hdnID.Value)).FirstOrDefault();
            txtHeaderText.Text = entity.ClassTypeName;
            hdnNoOfClass.Value = entity.NoOfClass.ToString();
            hdnClassTypeCode.Value = entity.ClassTypeCode;
            hdnClassTypeName.Value = entity.ClassTypeName;

            hdnMaxStudent.Value = BusinessLayer.GetSiteParameter(AppSession.UserLogin.SiteID, Constant.SiteParameter.MAX_STUDENT).ParameterValue;

            BindGridView();
        }

        private void BindGridView()
        {
            List<Variable> lstVariable = new List<Variable>();
            int noOfClass = Convert.ToInt32(hdnNoOfClass.Value);
            for (int i = 1; i <= noOfClass; ++i)
            {
                lstVariable.Add(new Variable { Code = string.Format("{0}-{1}", hdnClassTypeCode.Value, i), Value = string.Format("{0}-{1}", hdnClassTypeName.Value, i) });
            }

            grdView.DataSource = lstVariable;
            grdView.DataBind();
        }

        protected void cbpViewPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindGridView();
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            SchoolClassDao entityDtDao = new SchoolClassDao(ctx);
            try
            {
                string[] lstSchoolClassCode = hdnListSchoolClassCode.Value.Split(',');
                string[] lstSchoolClassName = hdnListSchoolClassName.Value.Split(',');
                string[] lstRoomID = hdnListRoomID.Value.Split(',');
                string[] lstTeacherID = hdnListTeacherID.Value.Split(',');
                string[] lstMaxStudent = hdnListMaxStudent.Value.Split(',');
                int PeriodClassTypeID = Convert.ToInt32(hdnID.Value);
                int ct = 0;
                foreach (String itemID in lstSchoolClassCode)
                {
                    SchoolClass entityDt = new SchoolClass();
                    entityDt.PeriodClassTypeID = PeriodClassTypeID;
                    entityDt.SchoolClassCode = lstSchoolClassCode[ct];
                    entityDt.SchoolClassName = lstSchoolClassName[ct];
                    entityDt.RoomID = Convert.ToInt32(lstRoomID[ct]);
                    entityDt.TeacherID = Convert.ToInt32(lstTeacherID[ct]);
                    entityDt.MaxStudent = Convert.ToInt16(lstMaxStudent[ct]);
                    entityDt.CreatedBy = AppSession.UserLogin.UserID;
                    entityDtDao.Insert(entityDt);
                    ct++;
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
    }
}