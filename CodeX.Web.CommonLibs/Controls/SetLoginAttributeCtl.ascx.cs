using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Web.Common;
using System.Web.Security;
using CodeX.Data.Model;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class SetLoginAttributeCtl : BaseEntryPopupCtl
    {
        protected override void OnInit(EventArgs e)
        {
            List<GetLoginAttributeUserList> lst = BusinessLayer.GetLoginAttributeUserList(AppSession.UserLogin.SiteID, AppSession.UserLogin.UserID, "");
            rptLoginAttribute.DataSource = lst;
            rptLoginAttribute.DataBind();
            base.OnInit(e);
        }
        public override void InitializeDataControl(string param)
        {
            IsAdd = true;
        }

        private string GetFilterExpression(string value)
        {
            StringBuilder sbResult = new StringBuilder(value);
            sbResult.Replace("@SiteID", AppSession.UserLogin.SiteID);
            sbResult.Replace("@UserID", AppSession.UserLogin.UserID.ToString());
            return sbResult.ToString();
        }


        protected void rptLoginAttribute_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlInputHidden hdnSessionName = e.Item.FindControl("hdnSessionName") as HtmlInputHidden;
                ASPxComboBox cboLoginAttribute = e.Item.FindControl("cboLoginAttribute") as ASPxComboBox;
                GetLoginAttributeUserList entity = e.Item.DataItem as GetLoginAttributeUserList;
                MethodInfo method = typeof(BusinessLayer).GetMethod(entity.MethodName, new[] { typeof(string) });
                object obj = method.Invoke(null, new string[] { GetFilterExpression(entity.FilterExpression) });
                IList list = (IList)obj;

                hdnSessionName.Value = entity.SessionName;
                cboLoginAttribute.DataSource = list;
                cboLoginAttribute.TextField = entity.TextFieldName;
                cboLoginAttribute.ValueField = entity.ValueFieldName;
                cboLoginAttribute.CallbackPageSize = 50;
                cboLoginAttribute.EnableCallbackMode = false;
                cboLoginAttribute.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                cboLoginAttribute.DropDownStyle = DropDownStyle.DropDownList;
                cboLoginAttribute.DataBind();

                Helper.SetControlEntrySetting(cboLoginAttribute, new ControlEntrySetting(true, true, true), "mpEntryPopup");
                //if (cboLoginAttribute.Value == null)
                //{
                //    string value = AppSession.GetSessionValue(entity.SessionName);
                //    if (value != null && value != "")
                //        cboLoginAttribute.Value = value;
                //}
            }
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            foreach (RepeaterItem itemDt in rptLoginAttribute.Items)
            {
                ASPxComboBox cboLoginAttribute = itemDt.FindControl("cboLoginAttribute") as ASPxComboBox;
                HtmlInputHidden hdnSessionName = itemDt.FindControl("hdnSessionName") as HtmlInputHidden;
                AppSession.SetSessionValue(hdnSessionName.Value, cboLoginAttribute.Value.ToString());
            }
            return true;
        }
    }
}