using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;

namespace CodeX.Report
{
    public partial class BStockOpnamePersediaan : BaseDailyPortraitRpt
    {
        public BStockOpnamePersediaan()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            vStockTakingHd entityHd = BusinessLayer.GetvStockTakingHdList(param[0])[0];
            lblStockOpnameNo.Text = entityHd.StockTakingNo;
            lblStockOpnameDate.Text = entityHd.FormDateInString;
            lblLocation.Text = String.Format("{0} - {1}", entityHd.LocationCode, entityHd.LocationName);
            base.InitializeReport(param);
        }

    }
}
