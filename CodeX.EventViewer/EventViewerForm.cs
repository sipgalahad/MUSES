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

namespace CodeX.EventViewerApp
{
    public partial class EventViewerForm : Form
    {
        List<ListViewItem> lstViewItem = new List<ListViewItem>();
        List<StandardCode> lstStandardCode = null;
        bool IsClose = false;

        public EventViewerForm()
        {
            InitializeComponent();
            Dictionary<String, String> list = new Dictionary<string, string>();

            lstStandardCode = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0 AND IsActive = 1", Constant.StandardCode.DB_SYNC_INFO_TYPE));

            foreach (StandardCode obj in lstStandardCode) 
            {
                lstService.Items.Add(obj.StandardCodeID);
                list.Add(obj.StandardCodeID, obj.StandardCodeName);
            }
            
            if (list.Count() > 0) 
            {
                lstService.DataSource = new BindingSource(list, null);
                lstService.ValueMember = "Key";
                lstService.DisplayMember = "Value";
            }
            this.Visible = false;
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
                        lvi = new ListViewItem(new[] { eventStruct.ServiceCode, eventStruct.EventName, eventStruct.EventDate.ToString(), eventStruct.Message, eventStruct.status ? "Success" : "Failure", eventStruct.EID });
                        lstViewItem.Add(lvi);
                        KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
                        if (eventStruct.ServiceCode == result.Key) lstEvent.Items.Add(lvi);
                    }
                    else
                    {
                        int idx = lstEvent.Items.IndexOf(lvi);
                        lstEvent.Items.Remove(lvi);
                        lvi.SubItems[3].Text = eventStruct.Message;
                        KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
                        if (eventStruct.ServiceCode == result.Key) lstEvent.Items.Insert(idx, lvi);
                    }
                }
            }
            base.WndProc(ref m);
        }

        public void CloseForm()
        {
            Application.Exit();
            IsClose = true;
        }
        
        public void OpenForm()
        {
            this.Visible = true;
        }

        private void lstService_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<String, String> result = (KeyValuePair<String, String>)lstService.SelectedItem;
            lstEvent.Items.Clear();
            lstEvent.Items.AddRange(lstViewItem.Where(x => x.SubItems[0].Text == result.Key).ToArray());
        }

        private void EventViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsClose) 
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
    }
}
