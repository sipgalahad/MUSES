using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;

namespace CodeX.Report
{
    public partial class BPenggantianReturPembelian : BaseDailyPortraitRpt
    {
        public BPenggantianReturPembelian()
        {
            InitializeComponent();
        }


        public override void InitializeReport(string[] param)
        {
            vPurchaseReplacementHd entityHd = BusinessLayer.GetvPurchaseReplacementHdList(param[0])[0];
            lblExchangeNo.Text = entityHd.PurchaseReplacementNo;
            lblExchangeDate.Text = entityHd.ReplacementDateInString;
            lblReturnNo.Text = entityHd.PurchaseReturnNo;
            lblSupplier.Text = String.Format("{0} - {1}", entityHd.BusinessPartnerCode, entityHd.BusinessPartnerName);
            lblWarehouseCode.Text = String.Format("{0} - {1}",entityHd.LocationCode,entityHd.LocationName);
            base.InitializeReport(param);
        }
    }
}
