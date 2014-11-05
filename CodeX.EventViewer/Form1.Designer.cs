namespace CodeX.EventViewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstService = new System.Windows.Forms.ListBox();
            this.lstEvent = new System.Windows.Forms.ListView();
            this.colServiceCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colService = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstService);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstEvent);
            this.splitContainer1.Size = new System.Drawing.Size(648, 262);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 3;
            // 
            // lstService
            // 
            this.lstService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstService.FormattingEnabled = true;
            this.lstService.Location = new System.Drawing.Point(0, 0);
            this.lstService.Name = "lstService";
            this.lstService.Size = new System.Drawing.Size(156, 262);
            this.lstService.TabIndex = 0;
            this.lstService.Click += new System.EventHandler(this.lstService_Click);
            // 
            // lstEvent
            // 
            this.lstEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colServiceCode,
            this.colService,
            this.colDateTime,
            this.colMessages,
            this.colStatus,
            this.colEID});
            this.lstEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEvent.FullRowSelect = true;
            this.lstEvent.GridLines = true;
            this.lstEvent.Location = new System.Drawing.Point(0, 0);
            this.lstEvent.Name = "lstEvent";
            this.lstEvent.Size = new System.Drawing.Size(488, 262);
            this.lstEvent.TabIndex = 1;
            this.lstEvent.UseCompatibleStateImageBehavior = false;
            this.lstEvent.View = System.Windows.Forms.View.Details;
            // 
            // colServiceCode
            // 
            this.colServiceCode.Text = "ServiceCode";
            this.colServiceCode.Width = 0;
            // 
            // colService
            // 
            this.colService.Text = "Service";
            // 
            // colDateTime
            // 
            this.colDateTime.Text = "Date Time";
            this.colDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDateTime.Width = 75;
            // 
            // colMessages
            // 
            this.colMessages.Text = "Message";
            this.colMessages.Width = 235;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 149;
            // 
            // colEID
            // 
            this.colEID.Text = "Event ID";
            this.colEID.Width = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 262);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Event Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstService;
        private System.Windows.Forms.ListView lstEvent;
        private System.Windows.Forms.ColumnHeader colServiceCode;
        private System.Windows.Forms.ColumnHeader colService;
        private System.Windows.Forms.ColumnHeader colDateTime;
        private System.Windows.Forms.ColumnHeader colMessages;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colEID;
    }
}

