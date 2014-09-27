using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Medinfras.Web.Common;
using QIS.Medinfras.Web.Common.UI;
using QIS.Medinfras.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace QIS.Medinfras.Web.EMR.Program
{
    public partial class DentalChartList : BasePagePatientPageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.PATIENT_PAGE.DENTAL_CHART;
        }

        protected override void InitializeDataControl()
        {
            CreateTableTooth();
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("VisitID = {0} AND IsDeleted = 0", AppSession.RegisteredPatient.VisitID);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvChiefComplaintRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vChiefComplaint> lstEntity = BusinessLayer.GetvChiefComplaintList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnAddRecord(ref string url, ref string errMessage, ref string queryString, ref int popupWidth, ref int popupHeight, ref string popupHeaderText)
        {
            url = ResolveUrl("~/Program/PatientPage/Subjective/ChiefComplaint/ChiefComplaintEntryCtl.ascx");
            queryString = "";
            popupWidth = 700;
            popupHeight = 500;
            popupHeaderText = "Chief Complaint";
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage, ref string queryString, ref int popupWidth, ref int popupHeight, ref string popupHeaderText)
        {
            if (hdnID.Value != "")
            {
                url = ResolveUrl("~/Program/PatientPage/Subjective/ChiefComplaint/ChiefComplaintEntryCtl.ascx");
                queryString = hdnID.Value;
                popupWidth = 700;
                popupHeight = 500;
                popupHeaderText = "Chief Complaint";
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value != "")
            {
                ChiefComplaint entity = BusinessLayer.GetChiefComplaint(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateChiefComplaint(entity);
                return true;
            }
            return false;
        }


        #region Create Table Tooth
        class Tooth
        {
            public Int32 Line;
            public String ImageUrl;
            public Int32 Number;
            public Int32 IndexImage;
        }

        private void CreateTableTooth()
        {
            List<Tooth> lstTooth = new List<Tooth>();

            for (int ctr = 1, i = 18, j = 28; i > 10; --i, --j, ctr++)
            {
                string imgUrl = "";
                if (i > 13)
                    imgUrl = "tooth.png";
                else
                    imgUrl = "tooth_2.png";
                lstTooth.Add(new Tooth { Line = 2, ImageUrl = imgUrl, Number = i, IndexImage = ctr });
                lstTooth.Add(new Tooth { Line = 2, ImageUrl = imgUrl, Number = j, IndexImage = 17 - ctr });

                lstTooth.Add(new Tooth { Line = 3, ImageUrl = imgUrl, Number = i + 30, IndexImage = ctr });
                lstTooth.Add(new Tooth { Line = 3, ImageUrl = imgUrl, Number = j + 10, IndexImage = 17 - ctr });
            }

            for (int ctr = 1, i = 55, j = 65; i > 50; --i, --j, ctr++)
            {
                string imgUrl = "";
                if (i > 53)
                    imgUrl = "tooth.png";
                else
                    imgUrl = "tooth_2.png";
                lstTooth.Add(new Tooth { Line = 1, ImageUrl = imgUrl, Number = i, IndexImage = ctr });
                lstTooth.Add(new Tooth { Line = 1, ImageUrl = imgUrl, Number = j, IndexImage = 11 - ctr });

                lstTooth.Add(new Tooth { Line = 4, ImageUrl = imgUrl, Number = i + 30, IndexImage = ctr });
                lstTooth.Add(new Tooth { Line = 4, ImageUrl = imgUrl, Number = j + 10, IndexImage = 11 - ctr });
            }

            containerTableTooth.Controls.Add(CreateTableTitle("upper right", "upper left"));

            for (int i = 1; i < 5; ++i)
            {
                HtmlTable tbl = new HtmlTable();
                tbl.Attributes.Add("Class", "tblTooth");
                HtmlTableRow row = new HtmlTableRow();
                List<Tooth> lst = lstTooth.Where(p => p.Line == i).OrderBy(p => p.IndexImage).ToList();
                foreach (Tooth tooth in lst)
                {
                    HtmlTableCell cell = new HtmlTableCell();

                    HtmlGenericControl div = new HtmlGenericControl("div");
                    div.InnerHtml = tooth.Number.ToString();

                    HtmlImage img = new HtmlImage();
                    img.Src = string.Format("{0}{1}", Page.ResolveUrl("~/Libs/Images/Medical/"), tooth.ImageUrl);
                    img.Alt = "";

                    if (i < 3)
                    {
                        cell.Controls.Add(div);
                        cell.Controls.Add(img);
                    }
                    else
                    {
                        cell.Controls.Add(img);
                        cell.Controls.Add(div);
                    }

                    row.Cells.Add(cell);
                }
                tbl.Rows.Add(row);

                containerTableTooth.Controls.Add(tbl);
            }
            containerTableTooth.Controls.Add(CreateTableTitle("lower right", "lower left"));
        }

        private HtmlTable CreateTableTitle(string leftTitle, string rightTitle)
        {
            HtmlTable tbl = new HtmlTable();
            tbl.Attributes.Add("Class", "tblToothHeader");

            HtmlTableRow row = new HtmlTableRow();

            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.InnerHtml = leftTitle;
            HtmlTableCell cell2 = new HtmlTableCell();
            cell2.InnerHtml = rightTitle;

            row.Cells.Add(cell1);
            row.Cells.Add(cell2);

            tbl.Rows.Add(row);
            return tbl;
        }
        #endregion
    }
}