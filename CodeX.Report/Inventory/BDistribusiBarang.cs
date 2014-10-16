using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CodeX.Data.Model;
using CodeX.Common;

namespace CodeX.Report
{
    public partial class BDistribusiBarang : BaseDailyPortraitRpt
    {
        public BDistribusiBarang()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param) 
        {
            vItemDistributionHd entityHd = BusinessLayer.GetvItemDistributionHdList(param[0])[0];
            lblDistributionNo.Text = entityHd.DistributionNo;
            lblDistributionDate.Text = entityHd.DeliveryDate.ToString(Constant.FormatString.DATE_FORMAT);
            lblWarehouseCode.Text = String.Format("{0} - {1}", entityHd.FromLocationCode, entityHd.FromLocationName);
            lblOtherWarehouseCode.Text = String.Format("{0} - {1}", entityHd.ToLocationCode, entityHd.ToLocationName);
            base.InitializeReport(param);
        }
    }
}
