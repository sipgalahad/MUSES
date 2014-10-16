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
    public partial class BPembelianTunai : BaseDailyPortraitRpt
    {
        public BPembelianTunai()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            base.InitializeReport(param);

            vDirectPurchaseHd entity = BusinessLayer.GetvDirectPurchaseHdList(String.Format("DirectPurchaseID IN (SELECT DirectPurchaseID FROM vDirectPurchaseDt WHERE {0})", param[0]))[0];
            lblPPN.Text = (entity.TransactionAmount * entity.VATPercentage / 100).ToString("N");
            lblTotal.Text = (entity.TransactionAmount + (entity.TransactionAmount * entity.VATPercentage / 100)).ToString("N");
        }
    }
}
