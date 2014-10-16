using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Common;

namespace CodeX.Report
{
    public partial class BNotaKredit : BaseDailyPortraitRpt
    {
        public BNotaKredit()
        {
            InitializeComponent();
        }

        private void lblterbilang_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Int64 amount = Convert.ToInt64(GetCurrentColumnValue("totalValue"));
            ((XRLabel)sender).Text = Helper.NumberInWords(amount, true);
        }
    }
}
