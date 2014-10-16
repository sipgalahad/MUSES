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
    public partial class BReturPembelianTunaiDenganNilai :BaseDailyPortraitRpt
    {
        public BReturPembelianTunaiDenganNilai()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            
            vDirectPurchaseReturnHd entityHd = BusinessLayer.GetvDirectPurchaseReturnHdList(param[0])[0];
            lblPPN.Text = (entityHd.TransactionAmount * entityHd.VATPercentage / 100).ToString("N");
            lblTotal.Text = (entityHd.TransactionAmount + (entityHd.TransactionAmount * entityHd.VATPercentage / 100)).ToString("N");
            base.InitializeReport(param);
        }

        
    }
}