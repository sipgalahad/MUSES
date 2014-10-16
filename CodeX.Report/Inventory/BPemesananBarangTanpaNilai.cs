using System;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;

namespace CodeX.Report
{
    public partial class BPemesananBarangTanpaNilai : BaseDailyPortraitRpt
    {
        public BPemesananBarangTanpaNilai()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            base.InitializeReport(param);

            vPurchaseOrderHd entity = BusinessLayer.GetvPurchaseOrderHdList(param[0])[0];
            lblPurchaseOrderNo.Text = entity.PurchaseOrderNo;
            lblPurchaseOrderDate.Text = entity.OrderDateInString;
            lblSupplierCode.Text = entity.BusinessPartnerCode;
            lblSupplierName.Text = entity.BusinessPartnerName;
            lblCreatedByName.Text = entity.CreatedByName;

            //string approvedByName = string.Empty;
            //string noSIPA = string.Empty;
            //if (entity.GCPurchaseOrderType == "X145^001") // Persediaan Farmasi
            //{
            //    lblReportTitle.Text = "SURAT PESANAN OBAT DAN ALKES";
            //    string filterExpression = string.Format(" ParameterCode IN ('{0}','{1}')", "PH0004", "PH0005");
            //    List<SettingParameter> lstParam = BusinessLayer.GetSettingParameterList(filterExpression);
            //    approvedByName = lstParam.Where(lst => lst.ParameterCode == "PH0004").FirstOrDefault().ParameterValue;
            //    noSIPA = lstParam.Where(lst => lst.ParameterCode == "PH0005").FirstOrDefault().ParameterValue;
            //}
            //else
            //{
            //    lblReportTitle.Text = "SURAT PEMESANAN BARANG";
            //}

            //lblApprovedByName.Text = approvedByName;
            //lblSIPANo.Text = noSIPA;


            lblReportTitle.Text = "SURAT PEMESANAN BARANG";


            lblApprovedByName.Text = "Slamet";
            lblSIPANo.Text = "20123021234";
        }
    }
}
