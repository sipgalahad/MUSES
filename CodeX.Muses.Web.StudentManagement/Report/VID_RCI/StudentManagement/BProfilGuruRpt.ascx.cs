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
    public partial class BProfilGuruRpt : BaseCustomReportCtl
    {
        List<vTransTeacherProfileDtItem> lstProfileItem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        vTeacherSubject ts = null;
        public override void Bind(string filterExpression, string[] param)
        {
            String text = divPersonalityType.InnerHtml;
            vTransTeacherProfileDt ttpdt = BusinessLayer.GetvTransTeacherProfileDtList(filterExpression)[0];
            divRBHeader.InnerHtml = divRBHeader.InnerHtml.Replace("{TeacherName}", ttpdt.TeacherName);
            divPersonalityType.InnerHtml = text.Replace("{PersonalityType}", ttpdt.PersonalityTypeName);

            text = divPersonal.InnerHtml;
            text = text.Replace("{TeacherName}", ttpdt.TeacherName);
            text = text.Replace("{IQ}", ttpdt.IQScore.ToString());
            text = text.Replace("{IQInPercentage}", GetIQScore(ttpdt.IQScore));
            text = text.Replace("{Drive}", ttpdt.DScore.ToString("N"));
            text = text.Replace("{Komunikasi}", ttpdt.KScore.ToString("N"));
            text = text.Replace("{Loyalitas}", ttpdt.LScore.ToString("N"));
            text = text.Replace("{Ketelitian}", ttpdt.TScore.ToString("N"));
            text = text.Replace("{Konsistensi}", ttpdt.KonsScoreInPercentage.ToString("N"));
            divPersonal.InnerHtml = text;

            text = divPersonalDesc.InnerHtml;
            text = text.Replace("{Adventages}", ttpdt.Advantages.Replace("<br>","<br/>"));
            text = text.Replace("{Weakness}", ttpdt.Weakness.Replace("<br>", "<br/>"));
            divPersonalDesc.InnerHtml = text;

            filterExpression = String.Format("TransTeacherProfileDtID = {0}",ttpdt.ID);
            lstProfileItem = BusinessLayer.GetvTransTeacherProfileDtItemList(filterExpression);

            ts = BusinessLayer.GetvTeacherSubjectList(String.Format("TeacherID = {0}",ttpdt.TeacherID)).FirstOrDefault();
            
            List<Variable> lstGroup = (from grp in lstProfileItem group grp by new { grp.TeacherProfileGroupID, grp.TeacherProfileGroupDisplayText } into NewGrp select new Variable { Code = NewGrp.Key.TeacherProfileGroupID.ToString(), Value = NewGrp.Key.TeacherProfileGroupDisplayText }).ToList();
            rptReportBody.DataSource = lstGroup;
            rptReportBody.DataBind();

            
        }

        protected void rptReportBody_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                Repeater rptGroupItem = e.Item.FindControl("rptGroupItem") as Repeater;
                HtmlGenericControl divHeader = e.Item.FindControl("divHeader") as HtmlGenericControl;
                HtmlTableCell tdHeaderPercentage = e.Item.FindControl("tdHeaderPercentage") as HtmlTableCell;
                HtmlTableCell tdHeaderMutu = e.Item.FindControl("tdHeaderMutu") as HtmlTableCell;

                Variable group = e.Item.DataItem as Variable;

                switch (group.Code) 
                { 
                    case "2" :
                        divHeader.Style.Remove("display");
                        divHeader.InnerHtml = "B. KOMPETENSI PEDAGOGIK & PROFESIONAL";
                        break;
                    case "5":
                        divHeader.Style.Remove("display");
                        divHeader.InnerHtml = "B. KOMPETENSI PEDAGOGIK & PROFESIONAL"; 
                        break;
                    case "8":
                        divHeader.Style.Remove("display");
                        divHeader.InnerHtml = "C. PROFIL MENURUT SISWA";
                        break;
                    case "12":
                        tdHeaderPercentage.InnerHtml = "Jawaban";
                        tdHeaderPercentage.ColSpan = 2;
                        tdHeaderPercentage.Width = "50%";
                        tdHeaderMutu.Visible = false;
                        break;
                    default : 
                        divHeader.Style.Add("display","none"); 
                        break;
                }

                List<vTransTeacherProfileDtItem> lstTemp = lstProfileItem.Where(x => x.TeacherProfileGroupID == Convert.ToInt32(group.Code)).ToList();
                if (lstTemp.Count() == 1 && ts != null)
                {
                    lstTemp[0].TeacherProfileItemName = lstTemp[0].TeacherProfileItemName.Replace("{SubjectName}", ts.SubjectName);
                }
                else 
                {
                    lstTemp[0].TeacherProfileItemName = lstTemp[0].TeacherProfileItemName.Replace("{SubjectName}", "");
                }
                rptGroupItem.ItemDataBound += new RepeaterItemEventHandler(rptGroupItem_ItemDataBound);
                rptGroupItem.DataSource = lstTemp;
                rptGroupItem.DataBind();
                HtmlTableRow trMutu = e.Item.FindControl("trMutu") as HtmlTableRow;
                if (lstTemp.Count() > 1 && group.Code != "12")
                {
                    HtmlTableCell tdFinalScore = e.Item.FindControl("tdFinalScore") as HtmlTableCell;
                    HtmlTableCell tdQualityScore = e.Item.FindControl("tdQualityScore") as HtmlTableCell;
                    Decimal TotScore = lstTemp.Sum(x => x.Score);
                    Int32 QualityPercentage = lstTemp.Sum(x => x.QualityPercentage);
                    Decimal DynamicQualityPercentage = lstTemp.Sum(x => x.DynamicQualityPercentage);
                    Decimal FinalScore = 0;
                    if (QualityPercentage == 0)
                    {
                        FinalScore = (TotScore / DynamicQualityPercentage * 100);
                    }
                    else
                    {
                        FinalScore = (TotScore / QualityPercentage * 100);
                    }
                    tdFinalScore.InnerHtml = FinalScore.ToString("N2");
                    if(Convert.ToInt32(group.Code) < 7)
                        tdQualityScore.InnerHtml = GetMutu(FinalScore);
                    else
                        tdQualityScore.InnerHtml = GetPetaUmpanBalik(FinalScore);
                }
                else 
                {
                    trMutu.Visible = false;
                }
            }
        }

        void rptGroupItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item)
            {
                vTransTeacherProfileDtItem entity = e.Item.DataItem as vTransTeacherProfileDtItem;
                HtmlTableCell tdPercentage = e.Item.FindControl("tdPercentage") as HtmlTableCell;
                HtmlTableCell tdMutu = e.Item.FindControl("tdMutu") as HtmlTableCell;

                Decimal percentage = 0;
                if (entity.QualityPercentage == 0 && entity.DynamicQualityPercentage != 0)
                {
                    percentage = entity.Score / entity.DynamicQualityPercentage * 100;
                    tdPercentage.InnerHtml = percentage.ToString("N2");
                }
                else if (entity.QualityPercentage != 0)
                {
                    percentage = entity.Score / entity.QualityPercentage * 100;
                    tdPercentage.InnerHtml = percentage.ToString("N2");
                }
                else 
                {
                    tdPercentage.InnerHtml = entity.Remarks;
                }

                if (entity.TeacherProfileGroupID < 7 && entity.TeacherProfileGroupID != 12)
                {
                    tdMutu.InnerHtml = GetMutu(percentage);
                }
                else if (entity.TeacherProfileGroupID != 12)
                {
                    tdMutu.InnerHtml = GetPetaUmpanBalik(percentage);
                }
                else 
                {
                    tdPercentage.ColSpan = 2;
                    tdPercentage.Align = "Left";
                    tdMutu.Visible = false;
                }
            }
        }

        protected String GetPetaUmpanBalik(decimal percentage) 
        {
            if (percentage > Convert.ToDecimal(4.1)) return "SB";
            else if (percentage > Convert.ToDecimal(3.34)) return "B";
            else if (percentage > Convert.ToDecimal(2.5)) return "KB";
            else return "SK";
        }

        protected String GetIQScore(Int32 iq) 
        { 
            if(iq > 129) return "Very Superior";
            else if(iq > 119) return "Superior";
            else if(iq > 109) return "High Average";
            else if(iq > 89) return "Average";
            else if (iq > 79) return "Low Average";
            else return "Extremely Low";
        }

        protected String GetMutu(decimal percentage) 
        {
            if (percentage >= 80 && percentage <= 100) return "Amat Baik";
            else if (percentage >= 65 && percentage <= 79) return "Baik";
            else if (percentage >= 55 && percentage <= 64) return "Sedang";
            else if (percentage >= 40 && percentage <= 54) return "Kurang";
            else return "Kurang Sekali";
        }
    }
}
