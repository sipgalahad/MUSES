using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeX.Common;
using System.Runtime.InteropServices;
using CodeX.Data.Model;

namespace CodeX.EventViewer
{
    public partial class Form1 : Form
    {
        List<ListViewItem> lstViewItem = new List<ListViewItem>();
        List<StandardCode> lstStandardCode = null;

        public Form1()
        {
            InitializeComponent();
            Dictionary<String, String> list = new Dictionary<string, string>();
            
            lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.BUSINESS_OBJECT_TYPE));

            foreach (StandardCode obj in lstStandardCode) 
            {
                lstService.Items.Add(obj.StandardCodeID);
                list.Add(obj.StandardCodeID, obj.StandardCodeName);
            }

            lstService.DataSource = new BindingSource(list, null);
            lstService.ValueMember = "Key";
            lstService.DisplayMember = "Value";
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constant.EventViewer.WM_COPYDATA)
            {
                // Get the COPYDATASTRUCT struct from lParam.
                COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));

                // If the size matches
                if (cds.cbData == Marshal.SizeOf(typeof(EventStruct)))
                {
                    // Marshal the data from the unmanaged memory block to a 
                    // MyStruct managed struct.
                    EventStruct eventStruct = (EventStruct)Marshal.PtrToStructure(cds.lpData, typeof(EventStruct));

                    ListViewItem lvi = lstViewItem.FirstOrDefault(x => x.SubItems[0].Text == eventStruct.ServiceCode && x.SubItems[5].Text == eventStruct.EID);
                    if (lvi == null)
                    {
                        String ServiceName = lstStandardCode.FirstOrDefault(x => x.StandardCodeID == eventStruct.ServiceCode).StandardCodeName;
                        lvi = new ListViewItem(new[] { eventStruct.ServiceCode, ServiceName, eventStruct.EventDate.ToString(), eventStruct.Message, eventStruct.status ? "Success" : "Failure", eventStruct.EID });
                        lstViewItem.Add(lvi);
                        KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
                        if (eventStruct.ServiceCode == result.Key) lstEvent.Items.Add(lvi);
                    }
                    else
                    {
                        lstEvent.Items.Remove(lvi);
                        lvi.SubItems[3].Text = eventStruct.Message;
                        KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
                        if (eventStruct.ServiceCode == result.Key) lstEvent.Items.Add(lvi);
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void lstService_Click(object sender, EventArgs e)
        {
            KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
            lstEvent.Items.Clear();
            lstEvent.Items.AddRange(lstViewItem.Where(x => x.SubItems[0].Text == result.Key).ToArray());
        }
    }
}
