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
        vTeacherSubject ts = null;
        List<vTeacherSubject> lstTeacheSubject = null;
        List<vTransTeacherProfileDtItem> lstProfileAllItem = null;
        List<vTransTeacherProfileDtItem> lstProfileItem = null;
        List<vEmployee> lstEmployee = null;
        List<EmployeeAttendanceSummary> lstEAS = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        public override void Bind(string filterExpression, string[] param)
        {
            List<vTransTeacherProfileDt> lstTtpdt = BusinessLayer.GetvTransTeacherProfileDtList(filterExpression);
            String lstTeacherID = String.Join(",", lstTtpdt.Select(x => x.TeacherID));
            lstTeacheSubject = BusinessLayer.GetvTeacherSubjectList(String.Format("TeacherID IN ({0})", lstTeacherID));
            lstProfileAllItem = BusinessLayer.GetvTransTeacherProfileDtItemList(String.Format("TransTeacherProfileDtID IN ({0})", String.Join(",", lstTtpdt.Select(x => x.ID))));
            String lstEmployeeID = String.Join(",",lstTtpdt.Select(x => x.TeacherID));
            lstEmployee = BusinessLayer.GetvEmployeeList(String.Format("EmployeeID IN ({0})", lstEmployeeID));
            lstEAS = BusinessLayer.GetEmployeeAttendanceSummaryList(String.Format("EmployeeID IN ({0}) AND IsDeleted = 0", lstEmployeeID));
            rptMainBody.DataSource = lstTtpdt;
            rptMainBody.DataBind();
        }

        protected void rptMainBody_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item) 
            {
                vTransTeacherProfileDt ttpdt = e.Item.DataItem as vTransTeacherProfileDt;
                HtmlGenericControl divPersonalityType = e.Item.FindControl("divPersonalityType") as HtmlGenericControl;
                HtmlGenericControl divPersonal = e.Item.FindControl("divPersonal") as HtmlGenericControl;
                HtmlGenericControl divPersonalDesc = e.Item.FindControl("divPersonalDesc") as HtmlGenericControl;
                HtmlGenericControl divEmploymentStatus = e.Item.FindControl("divEmploymentStatus") as HtmlGenericControl;
                HtmlGenericControl divEmployeeAttendanceSummary = e.Item.FindControl("divEmployeeAttendanceSummary") as HtmlGenericControl;

                Repeater rptReportBody = e.Item.FindControl("rptReportBody") as Repeater;

                String text = divPersonalityType.InnerHtml;
                divPersonalityType.InnerHtml = text.Replace("{PersonalityType}", ttpdt.PersonalityTypeName);
                vEmployee emp = lstEmployee.FirstOrDefault(x => x.EmployeeID == ttpdt.TeacherID);

                if (emp != null)
                {
                    text = divEmploymentStatus.InnerHtml;
                    if (emp.HiredDate.ToString(Constant.FormatString.DATE_FORMAT) != "01-Jan-1900")
                        text = text.Replace("{HiredDate}", emp.HiredDate.ToString(Constant.FormatString.DATE_FORMAT));
                    else
                        text = text.Replace("{HiredDate}", "-");
                    if (emp.TerminatedDate.ToString(Constant.FormatString.DATE_FORMAT) != "01-Jan-1900")
                        text = text.Replace("{TerminatedDate}", emp.TerminatedDate.ToString(Constant.FormatString.DATE_FORMAT));
                    else
                        text = text.Replace("{TerminatedDate}", "-");
                    text = text.Replace("{EmployeeStatus}", emp.EmployeeStatus);
                    text = text.Replace("{NIK}", emp.EmployeeCode);
                    divEmploymentStatus.InnerHtml = text;
                }
                else 
                {
                    text = divEmploymentStatus.InnerHtml;
                    text = text.Replace("{HiredDate}", "-");
                    text = text.Replace("{TerminatedDate}", "-");
                    divEmploymentStatus.InnerHtml = text;
                }

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
                text = text.Replace("{Adventages}", ttpdt.Advantages.Replace("<br>", "<br/>"));
                text = text.Replace("{Weakness}", ttpdt.Weakness.Replace("<br>", "<br/>"));
                divPersonalDesc.InnerHtml = text;

                EmployeeAttendanceSummary eas = lstEAS.FirstOrDefault(x => x.EmployeeID == emp.EmployeeID);
                if (eas != null) 
                {
                    text = divEmployeeAttendanceSummary.InnerHtml;
                    text = text.Replace("{EffectiveDays}",eas.EfectiveDays.ToString());
                    text = text.Replace("{SickDays}",eas.SickDays.ToString());
                    text = text.Replace("{PermitDays}",eas.PermitDays.ToString());
                    text = text.Replace("{AlphaDays}",eas.AlphaDays.ToString());
                    text = text.Replace("{EffectiveDaysInPercentage}", (Convert.ToInt32((eas.EfectiveDays / (Decimal) eas.WorkDays) * 100)).ToString());
                    text = text.Replace("{SickDaysInPercentage}", (Convert.ToInt32((eas.SickDays / (Decimal)eas.WorkDays) * 100)).ToString());
                    text = text.Replace("{PermitDaysInPercentage}", (Convert.ToInt32((eas.PermitDays / (Decimal)eas.WorkDays) * 100)).ToString());
                    text = text.Replace("{AlphaDaysInPercentage}", (Convert.ToInt32((eas.AlphaDays / (Decimal)eas.WorkDays) * 100)).ToString());
                    divEmployeeAttendanceSummary.InnerHtml = text;
                }else
                {
                    text = divEmployeeAttendanceSummary.InnerHtml;
                    text = text.Replace("{EffectiveDays}", "-");
                    text = text.Replace("{SickDays}", "-");
                    text = text.Replace("{PermitDays}", "-");
                    text = text.Replace("{AlphaDays}", "-");
                    text = text.Replace("{EffectiveDaysInPercentage}", "-");
                    text = text.Replace("{SickDaysInPercentage}", "-");
                    text = text.Replace("{PermitDaysInPercentage}", "-");
                    text = text.Replace("{AlphaDaysInPercentage}", "-");
                    divEmployeeAttendanceSummary.InnerHtml = text;
                }
                
                lstProfileItem = lstProfileAllItem.Where(x => x.TransTeacherProfileDtID == ttpdt.ID).ToList();
                ts = lstTeacheSubject.Where(x => x.TeacherID == ttpdt.TeacherID).FirstOrDefault();

                List<Variable> lstGroup = (from grp in lstProfileItem group grp by new { grp.TeacherProfileGroupID, grp.TeacherProfileGroupDisplayText } into NewGrp select new Variable { Code = NewGrp.Key.TeacherProfileGroupID.ToString(), Value = NewGrp.Key.TeacherProfileGroupDisplayText }).ToList();
                rptReportBody.DataSource = lstGroup;
                rptReportBody.DataBind();
            }
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
                    case "13":
                        divHeader.Style.Remove("display");
                        divHeader.InnerHtml = "Pendapat dan Kesan Siswa Secara Terbuka terhadap Profil Kepribadian Guru";
                        tdHeaderPercentage.InnerHtml = "Jawaban";
                        tdHeaderPercentage.ColSpan = 2;
                        tdHeaderPercentage.Width = "50%";
                        tdHeaderMutu.Visible = false;
                        break;
                    case "14":
                        divHeader.Style.Remove("display");
                        divHeader.InnerHtml = "D. PENDAPAT DAN KESAN SECARA TERBUKA DARI KOLEGA :";
                        tdHeaderPercentage.InnerHtml = "Jawaban";
                        tdHeaderPercentage.ColSpan = 2;
                        tdHeaderPercentage.Width = "50%";
                        tdHeaderMutu.Visible = false;
                        break;
                    default : 
                        divHeader.Style.Add("display","none"); 
                        break;
                }

                List<vTransTeacherProfileDtItem> lstTemp = lstProfileItem.Where(x => x.TeacherProfileGroupID == Convert.ToInt32(group.Code)).OrderBy(s => s.DisplayOrder).ToList();
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
                if (lstTemp.Count() > 1 && group.Code != "12" && group.Code != "13" && group.Code != "14")
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

                if (entity.TeacherProfileGroupID < 7 && entity.GCTeacherProfileMarkType != Constant.TeacherProfileMarkType.TEXT)
                {
                    tdMutu.InnerHtml = GetMutu(percentage);
                }
                else if (entity.GCTeacherProfileMarkType == Constant.TeacherProfileMarkType.NUMBER)
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
