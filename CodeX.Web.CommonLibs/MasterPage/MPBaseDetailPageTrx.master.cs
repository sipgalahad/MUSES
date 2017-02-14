using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeX.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using CodeX.Data.Model;
using System.Web.UI.HtmlControls;
using System.Text;
using CodeX.Common;
using CodeX.Web.Common;

namespace CodeX.Web.CommonLibs.MasterPage
{
    public partial class MPBaseDetailPageTrx : BaseMP
    {
        private BasePageTrx _basePageEntry;
        private BasePageTrx BasePageEntry
        {
            get
            {
                if (_basePageEntry == null)
                    _basePageEntry = (BasePageTrx)Page;
                return _basePageEntry;
            }
        }

        public void SetTitleText(string text)
        {
            ((MPBaseDetailPage)Master).SetTitleText(text);
        }

        public void SetSubTitleText(string text)
        {
            ((MPBaseDetailPage)Master).SetSubTitleText(text);
        }

        public void SetSubTitleText2(string text)
        {
            ((MPBaseDetailPage)Master).SetSubTitleText2(text);
        }

        public void SetListMenu(List<GetUserMenuAccess> lstMenu)
        {
            ((MPBaseDetailPage)Master).SetListMenu(lstMenu);
        }

        public void SetParentCode(string parentCode)
        {
            ((MPBaseDetailPage)Master).SetParentCode(parentCode);
        }

        protected string menuCode = "";
        private GetUserMenuAccess menu;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RowCountPerPage = Constant.GridViewPageSize.GRID_POPUP_LIST;
                bool isAdd = !BasePageEntry.IsLoadFirstRecord;
                hdnIsAdd.Value = isAdd ? "1" : "0";
                int rowCount = BasePageEntry.OnGetRowCount();
                hdnRowCount.Value = rowCount.ToString();

                bool IsAllowAdd, IsAllowSave, IsAllowVoid, IsAllowNextPrev, IsAllowEdit;
                IsAllowAdd = IsAllowSave = IsAllowVoid = IsAllowNextPrev = IsAllowEdit = true;
                BasePageEntry.SetToolbarVisibility(ref IsAllowAdd, ref IsAllowSave, ref IsAllowVoid, ref IsAllowNextPrev);
                if (!IsAllowAdd)
                    btnMPEntryNew.Style.Add("display", "none");
                if (!IsAllowSave)
                    btnMPEntrySave.Style.Add("display", "none");
                if (!IsAllowVoid)
                    btnMPEntryVoid.Style.Add("display", "none");
                if (!IsAllowNextPrev)
                {
                    btnMPEntryNext.Style.Add("display", "none");
                    btnMPEntryPrev.Style.Add("display", "none");
                }
                if (BasePageEntry.IsRefreshControlAfterSaveAddRecord())
                    hdnIsRefreshControlAfterSaveAddRecord.Value = "1";
                else
                    hdnIsRefreshControlAfterSaveAddRecord.Value = "0";

                menuCode = BasePageEntry.OnGetMenuCode();
                menu = ((MPBaseDetailPage)Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;

                hdnMenuCaption.Value = menu.MenuCaption;
                if (!IsAllowAdd) CRUDMode = CRUDMode.Replace("C", "");
                if (!IsAllowEdit) CRUDMode = CRUDMode.Replace("U", "");
                if (!IsAllowVoid) CRUDMode = CRUDMode.Replace("D", "");
                //if (!IsAllowPrint) CRUDMode = CRUDMode.Replace("P", "");
                hdnIsAllowEdit.Value = CRUDMode.Contains("U") ? "1" : "0";
                hdnIsAllowNextPrev.Value = IsAllowNextPrev ? "1" : "0";
                hdnIsAllowVoid.Value = CRUDMode.Contains("D") ? "1" : "0";
                hdnIsAllowReopen.Value = CRUDMode.Contains("O") ? "1" : "0";

                if (CRUDMode.Contains('A'))
                {
                    hdnProposeText.Value = GetLabel("Approve");
                    hdnIsAllowApprove.Value = "1";
                }
                else if (CRUDMode.Contains('P'))
                {
                    hdnProposeText.Value = GetLabel("Propose");
                    hdnIsAllowApprove.Value = "0";
                    hdnIsAllowPropose.Value = "1";
                }
                else
                    hdnIsAllowPropose.Value = "0";

                foreach (Control c in ulMPTrxToolbar.Controls)
                {
                    if (c is HtmlControl && ((HtmlControl)c).TagName.ToLower() == "li")
                    {
                        HtmlGenericControl li = c as HtmlGenericControl;
                        SetToolbarButtonVisibility(li, CRUDMode);
                    }
                    else if (c is ContentPlaceHolder)
                    {
                        foreach (Control c2 in c.Controls)
                        {
                            if (c2 is HtmlControl && ((HtmlControl)c2).TagName.ToLower() == "li")
                            {
                                HtmlGenericControl li = c2 as HtmlGenericControl;
                                SetToolbarButtonVisibility(li, CRUDMode);
                            }
                        }
                    }
                }

                if (!CRUDMode.Contains("A") && !CRUDMode.Contains("P"))
                    btnMPEntryPropose.Style.Add("display", "none");

                if (rowCount < 1)
                {
                    btnMPEntryNext.Style.Add("display", "none");
                    btnMPEntryPrev.Style.Add("display", "none");
                }
                if (isAdd)
                {
                    btnMPEntryVoid.Style.Add("display", "none");
                    btnMPEntryPropose.Style.Add("display", "none");
                    btnMPEntryReopen.Style.Add("display", "none");
                }
                else
                {
                    if (!CRUDMode.Contains("U"))
                        btnMPEntrySave.Style.Add("display", "none");
                    if (BasePageEntry.isShowWatermark)
                    {
                        hdnWatermark.Value = "1|" + BasePageEntry.watermarkText;
                        btnMPEntryReopen.Style.Remove("display");
                    }
                    else
                    {
                        hdnWatermark.Value = "0";
                        btnMPEntryReopen.Style.Add("display", "none");
                    }
                    hdnPageIndex.Value = BasePageEntry.PageIndex.ToString();
                }
            }
        }

        private void SetToolbarButtonVisibility(HtmlGenericControl li, string CRUDMode)
        {
            if (li.Attributes["CRUDMode"] != null)
            {
                string liCRUDMode = li.Attributes["CRUDMode"];
                if (!CRUDMode.Contains(liCRUDMode))
                    li.Style.Add("display", "none");
            }
        }

        protected string GetProposeText()
        {
            return hdnProposeText.Value;
        }

        protected void cbpMPEntryContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            int pageIndex = Convert.ToInt32(hdnPageIndex.Value);
            int rowCount = Convert.ToInt32(hdnRowCount.Value);
            bool isShowWatermark = false;
            string watermarkText = "";
            if (param[0] == "new")
            {
                BasePageEntry.AddRecord();
                pageIndex = -1;
            }
            else if (param[0] == "next")
                BasePageEntry.NextPageIndex(rowCount, ref pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "prev")
                BasePageEntry.PrevPageIndex(rowCount, ref pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "load")
                BasePageEntry.LoadPage(pageIndex, ref isShowWatermark, ref watermarkText);
            else if (param[0] == "refresh")
            {
                BasePageEntry.RefreshControl();
                pageIndex = -1;
            }
            else if (param[0] == "loadobject")
                BasePageEntry.LoadPage(param[1], ref pageIndex, ref isShowWatermark, ref watermarkText);

            string cpWatermark = isShowWatermark ? "1" : "0";
            if (isShowWatermark)
                cpWatermark = "1|" + watermarkText;
            else
                cpWatermark = "0";
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param[0];
            panel.JSProperties["cpPageIndex"] = pageIndex;
            panel.JSProperties["cpWatermark"] = cpWatermark;
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string retval = "";
            string result = "";
            string[] param = e.Parameter.Split('|');
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            if (param[0] == "save")
            {
                bool isAdd = (param[1] == "1");
                BasePageEntry.OnBtnSaveClick(ref result, ref retval, isAdd);
            }
            else if (param[0] == "void")
                BasePageEntry.OnBtnVoidClick(ref result);
            else if (param[0] == "approve")
                BasePageEntry.OnBtnApproveClick(ref result);
            else if (param[0] == "propose")
                BasePageEntry.OnBtnProposeClick(ref result);
            else if (param[0] == "reopen")
                BasePageEntry.OnBtnReopenClick(ref result);
            else if (param[0] == "customclick")
            {
                BasePageEntry.OnBtnCustomClick(ref result, param[1], ref retval);
                panel.JSProperties["cpType"] = param[1];
            }

            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpRetval"] = retval;
        }

        #region Popup List
        protected int PageCount = 0;
        protected int RowCount = 0;
        protected int RowCountPerPage = 1;
        protected int CurrPage = 1;
        protected void cbpSearchList_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BasePageEntry.BindSearchList(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BasePageEntry.BindSearchList(1, true, ref pageCount, ref rowCount);
                    result = string.Format("refresh|{0}|{1}", pageCount, rowCount);
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool isShowTitle = true;
            string fileName = "";
            Control controlHtml = BasePageEntry.OnGetExportControl(ref isShowTitle, ref fileName);
            if (controlHtml == null)
                controlHtml = BasePageEntry.OnGetExportControl();
            if (fileName == "")
                fileName = hdnMenuCaption.Value;
            Helper.ExportExcel(hdnMenuCaption.Value, hdnMenuCaption.Value, controlHtml, this, isShowTitle);
            //Control control = BasePageList.OnGetExportControl();

            //HtmlGenericControl div = new HtmlGenericControl("DIV");
            //HtmlGenericControl h1Title = new HtmlGenericControl("h1");
            //h1Title.InnerHtml = hdnMenuCaption.Value;
            //div.Controls.Add(h1Title);
            //div.Controls.Add(control);

            ////Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value));
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////Response.ContentType = "application/vnd.xls";
            ////System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            ////System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            ////div.RenderControl(htmlWrite);
            //////Response.Write(stringWrite.ToString());
            ////Response.Write("<html><head><style type='text/css'>.grdView > tbody > tr > td {color:green; border:1px solid;}</style></head>" + stringWrite.ToString() + "</html>");
            ////Response.End();


            //string attachment = string.Format("attachment;filename=\"{0}.xls\"", hdnMenuCaption.Value);
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //StringWriter stw = new StringWriter();
            //HtmlTextWriter htextw = new HtmlTextWriter(stw);
            //div.RenderControl(htextw);
            //HttpContext.Current.Response.Write(stw.ToString());
            //FileInfo fi = new FileInfo(Server.MapPath(ResolveUrl("~/Libs/Styles/excel.css")));
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //StreamReader sr = fi.OpenText();
            //while (sr.Peek() >= 0)
            //{
            //    sb.Append(sr.ReadLine());
            //}
            //sr.Close();
            //Response.Write("<html><head><style type='text/css'>" + sb.ToString() + "</style></head>" + stw.ToString() + "</html>");
            //stw = null;
            //htextw = null;
            //Response.Flush();
            //Response.End();
        }
    }
}