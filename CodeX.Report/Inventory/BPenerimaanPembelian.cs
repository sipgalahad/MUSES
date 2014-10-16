using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Report;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Report
{
    public partial class BPenerimaanPembelian : BaseDailyPortraitRpt
    {
        public BPenerimaanPembelian()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            base.InitializeReport(param);
            vPurchaseReceiveHd entityHd = BusinessLayer.GetvPurchaseReceiveHdList(param[0])[0];
            
            lblPurchaseReceiveNo.Text = entityHd.PurchaseReceiveNo;
            lblPurchaseReceiveDate.Text = entityHd.ReceivedDate.ToString(Constant.FormatString.DATE_FORMAT);
            lblSupplierCode.Text = String.Format("{0} - {1}",entityHd.SupplierCode,entityHd.SupplierName);

            decimal ppn = ((entityHd.TransactionAmount - entityHd.FinalDiscount) * entityHd.VATPercentage / 100);

            lblTotal.Text = entityHd.TransactionAmount.ToString("N");
            lblPPN.Text = ppn.ToString("N");
            lblDiskon.Text = entityHd.FinalDiscount.ToString("N");
            lblChargesType.Text = entityHd.ChargesType;
            lblCharges.Text = entityHd.ChargesAmount.ToString("N");
            Decimal totalPPN = entityHd.TransactionAmount - entityHd.FinalDiscount + ppn + entityHd.ChargesAmount;
            lblTotalPPN.Text = totalPPN.ToString("N");
        }
    }
}
