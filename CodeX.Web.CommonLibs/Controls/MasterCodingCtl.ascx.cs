using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using CodeX.Data.Model;
using CodeX.Common;
using CodeX.Data.Core.Dal;
using CodeX.Web.Common;
using System.Data;

namespace CodeX.Web.CommonLibs.Controls
{
    public partial class MasterCodingCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {

        }
        public void InitializeMasterCodingControl(String MasterType)
        {
            hdnMasterType.Value = MasterType;
            MasterCoding masterCoding = BusinessLayer.GetMasterCoding(hdnMasterType.Value);
            hdnDefaultPrefix.Value = masterCoding.DefaultPrefix;
            txtCodeInitial.MaxLength = masterCoding.PrefixLength;
            Helper.SetControlEntrySetting(txtCode, new ControlEntrySetting(true, true, true), "mpEntry");
            Helper.SetControlEntrySetting(txtCodeInitial, new ControlEntrySetting(true, true, true), "mpEntry");

            if (masterCoding.GCPrefixType == Constant.PrefixType.N_FIRST_DIGIT || !masterCoding.IsAllowChangeInitial)
                txtCodeInitial.ReadOnly = true;
            if (!masterCoding.IsEditable)
                txtCode.ReadOnly = true;
        }

        public void SetControlVisibility(bool IsAdd)
        {
            hdnIsAdd.Value = IsAdd ? "1" : "0";
            if (!IsAdd)
            {
                divEditCode.Style.Remove("display");
                divAddCode.Style.Add("display", "none");
            }
            else
            {
                divAddCode.Style.Remove("display");
                divEditCode.Style.Add("display", "none");
                txtCodeInitial.Text = hdnDefaultPrefix.Value;
            }
        }

        public void SetFocus()
        {
            if (hdnIsAdd.Value == "0")
                txtCode.Focus();
            else
                txtCodeInitial.Focus();
        }

        public void SetText(string Text)
        {
            txtCode.Text = Text;
        }

        public string GetCode(string entityName, IDbContext ctx = null)
        {
            string result = "";
            if (hdnIsAdd.Value == "0")
            {
                result = Request.Form[txtCode.UniqueID];
            }
            else
            {
                result = BusinessLayer.GenerateMasterCode(hdnMasterType.Value, txtCodeInitial.Text, AppSession.UserLogin.SiteID, entityName, ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();
            }
            return result;
        }
    }
}