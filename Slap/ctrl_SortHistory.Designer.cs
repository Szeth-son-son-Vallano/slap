namespace Slap
{
    partial class ctrl_SortHistory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_SortHistory));
            this.lbl_RouteList = new System.Windows.Forms.Label();
            this.lbl_ParcelList = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbl_SortPlan = new System.Windows.Forms.Label();
            this.lbl_FloorPlan = new System.Windows.Forms.Label();
            this.pb_DL_SortPlan = new System.Windows.Forms.PictureBox();
            this.pb_DL_FloorPlan = new System.Windows.Forms.PictureBox();
            this.pb_DL_RouteList = new System.Windows.Forms.PictureBox();
            this.pb_DL_ParcelList = new System.Windows.Forms.PictureBox();
            this.dgv_FileData = new System.Windows.Forms.DataGridView();
            this.dtp_SearchDate = new System.Windows.Forms.DateTimePicker();
            this.pb_Search = new System.Windows.Forms.PictureBox();
            this.btn_Clear = new BrbVideoManager.Controls.RoundedButton();
            this.btn_Download = new BrbVideoManager.Controls.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_SortPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_FloorPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_RouteList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_ParcelList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FileData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_RouteList
            // 
            this.lbl_RouteList.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RouteList.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_RouteList.Location = new System.Drawing.Point(1025, 50);
            this.lbl_RouteList.Name = "lbl_RouteList";
            this.lbl_RouteList.Size = new System.Drawing.Size(150, 30);
            this.lbl_RouteList.TabIndex = 9;
            this.lbl_RouteList.Text = "ROUTE LIST";
            this.lbl_RouteList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ParcelList
            // 
            this.lbl_ParcelList.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ParcelList.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_ParcelList.Location = new System.Drawing.Point(875, 50);
            this.lbl_ParcelList.Name = "lbl_ParcelList";
            this.lbl_ParcelList.Size = new System.Drawing.Size(150, 30);
            this.lbl_ParcelList.TabIndex = 8;
            this.lbl_ParcelList.Text = "PARCEL LIST";
            this.lbl_ParcelList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbl_SortPlan
            // 
            this.lbl_SortPlan.BackColor = System.Drawing.Color.Transparent;
            this.lbl_SortPlan.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_SortPlan.Location = new System.Drawing.Point(1025, 275);
            this.lbl_SortPlan.Name = "lbl_SortPlan";
            this.lbl_SortPlan.Size = new System.Drawing.Size(150, 30);
            this.lbl_SortPlan.TabIndex = 13;
            this.lbl_SortPlan.Text = "SORT PLAN";
            this.lbl_SortPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_FloorPlan
            // 
            this.lbl_FloorPlan.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FloorPlan.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_FloorPlan.Location = new System.Drawing.Point(875, 275);
            this.lbl_FloorPlan.Name = "lbl_FloorPlan";
            this.lbl_FloorPlan.Size = new System.Drawing.Size(150, 30);
            this.lbl_FloorPlan.TabIndex = 12;
            this.lbl_FloorPlan.Text = "FLOOR PLAN";
            this.lbl_FloorPlan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_DL_SortPlan
            // 
            this.pb_DL_SortPlan.Image = ((System.Drawing.Image)(resources.GetObject("pb_DL_SortPlan.Image")));
            this.pb_DL_SortPlan.Location = new System.Drawing.Point(1050, 325);
            this.pb_DL_SortPlan.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DL_SortPlan.Name = "pb_DL_SortPlan";
            this.pb_DL_SortPlan.Size = new System.Drawing.Size(100, 100);
            this.pb_DL_SortPlan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DL_SortPlan.TabIndex = 11;
            this.pb_DL_SortPlan.TabStop = false;
            this.pb_DL_SortPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_DL_SortPlan_MouseDown);
            this.pb_DL_SortPlan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_DL_SortPlan_MouseUp);
            // 
            // pb_DL_FloorPlan
            // 
            this.pb_DL_FloorPlan.Image = ((System.Drawing.Image)(resources.GetObject("pb_DL_FloorPlan.Image")));
            this.pb_DL_FloorPlan.Location = new System.Drawing.Point(900, 325);
            this.pb_DL_FloorPlan.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DL_FloorPlan.Name = "pb_DL_FloorPlan";
            this.pb_DL_FloorPlan.Size = new System.Drawing.Size(100, 100);
            this.pb_DL_FloorPlan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DL_FloorPlan.TabIndex = 10;
            this.pb_DL_FloorPlan.TabStop = false;
            this.pb_DL_FloorPlan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_DL_FloorPlan_MouseDown);
            this.pb_DL_FloorPlan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_DL_FloorPlan_MouseUp);
            // 
            // pb_DL_RouteList
            // 
            this.pb_DL_RouteList.Image = ((System.Drawing.Image)(resources.GetObject("pb_DL_RouteList.Image")));
            this.pb_DL_RouteList.Location = new System.Drawing.Point(1050, 100);
            this.pb_DL_RouteList.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DL_RouteList.Name = "pb_DL_RouteList";
            this.pb_DL_RouteList.Size = new System.Drawing.Size(100, 100);
            this.pb_DL_RouteList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DL_RouteList.TabIndex = 1;
            this.pb_DL_RouteList.TabStop = false;
            this.pb_DL_RouteList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_DL_RouteList_MouseDown);
            this.pb_DL_RouteList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_DL_RouteList_MouseUp);
            // 
            // pb_DL_ParcelList
            // 
            this.pb_DL_ParcelList.Image = global::Slap.Properties.Resources.fileGray;
            this.pb_DL_ParcelList.Location = new System.Drawing.Point(900, 100);
            this.pb_DL_ParcelList.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DL_ParcelList.Name = "pb_DL_ParcelList";
            this.pb_DL_ParcelList.Size = new System.Drawing.Size(100, 100);
            this.pb_DL_ParcelList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DL_ParcelList.TabIndex = 0;
            this.pb_DL_ParcelList.TabStop = false;
            this.pb_DL_ParcelList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pb_DL_ParcelList_MouseDown);
            this.pb_DL_ParcelList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pb_DL_ParcelList_MouseUp);
            // 
            // dgv_FileData
            // 
            this.dgv_FileData.AllowUserToAddRows = false;
            this.dgv_FileData.AllowUserToDeleteRows = false;
            this.dgv_FileData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FileData.Location = new System.Drawing.Point(50, 100);
            this.dgv_FileData.Name = "dgv_FileData";
            this.dgv_FileData.ReadOnly = true;
            this.dgv_FileData.RowHeadersWidth = 51;
            this.dgv_FileData.RowTemplate.Height = 24;
            this.dgv_FileData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_FileData.Size = new System.Drawing.Size(800, 400);
            this.dgv_FileData.TabIndex = 14;
            this.dgv_FileData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Cell_Click);
            // 
            // dtp_SearchDate
            // 
            this.dtp_SearchDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.dtp_SearchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_SearchDate.Location = new System.Drawing.Point(50, 50);
            this.dtp_SearchDate.Name = "dtp_SearchDate";
            this.dtp_SearchDate.Size = new System.Drawing.Size(300, 25);
            this.dtp_SearchDate.TabIndex = 15;
            // 
            // pb_Search
            // 
            this.pb_Search.Image = global::Slap.Properties.Resources.search;
            this.pb_Search.Location = new System.Drawing.Point(375, 50);
            this.pb_Search.Name = "pb_Search";
            this.pb_Search.Size = new System.Drawing.Size(25, 25);
            this.pb_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Search.TabIndex = 16;
            this.pb_Search.TabStop = false;
            this.pb_Search.Click += new System.EventHandler(this.pb_Search_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Clear.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Clear.BorderDownColor = System.Drawing.Color.Empty;
            this.btn_Clear.BorderDownWidth = 0F;
            this.btn_Clear.BorderOverColor = System.Drawing.Color.Empty;
            this.btn_Clear.BorderOverWidth = 0F;
            this.btn_Clear.BorderRadius = 30;
            this.btn_Clear.BorderWidth = 0F;
            this.btn_Clear.FlatAppearance.BorderSize = 0;
            this.btn_Clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Clear.ForeColor = System.Drawing.Color.White;
            this.btn_Clear.Location = new System.Drawing.Point(475, 600);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(250, 60);
            this.btn_Clear.TabIndex = 17;
            this.btn_Clear.Text = "C L E A R";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Clear_MouseDown);
            // 
            // btn_Download
            // 
            this.btn_Download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_Download.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Download.BorderDownColor = System.Drawing.Color.Empty;
            this.btn_Download.BorderDownWidth = 0F;
            this.btn_Download.BorderOverColor = System.Drawing.Color.Empty;
            this.btn_Download.BorderOverWidth = 0F;
            this.btn_Download.BorderRadius = 30;
            this.btn_Download.BorderWidth = 0F;
            this.btn_Download.FlatAppearance.BorderSize = 0;
            this.btn_Download.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(98)))), ((int)(((byte)(0)))));
            this.btn_Download.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(158)))), ((int)(((byte)(0)))));
            this.btn_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Download.ForeColor = System.Drawing.Color.White;
            this.btn_Download.Location = new System.Drawing.Point(475, 525);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(250, 60);
            this.btn_Download.TabIndex = 6;
            this.btn_Download.Text = "D O W N L O A D";
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Download_MouseDown);
            // 
            // ctrl_SortHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.pb_Search);
            this.Controls.Add(this.dtp_SearchDate);
            this.Controls.Add(this.lbl_SortPlan);
            this.Controls.Add(this.lbl_FloorPlan);
            this.Controls.Add(this.pb_DL_SortPlan);
            this.Controls.Add(this.pb_DL_FloorPlan);
            this.Controls.Add(this.lbl_RouteList);
            this.Controls.Add(this.lbl_ParcelList);
            this.Controls.Add(this.pb_DL_RouteList);
            this.Controls.Add(this.pb_DL_ParcelList);
            this.Controls.Add(this.dgv_FileData);
            this.Controls.Add(this.btn_Download);
            this.Name = "ctrl_SortHistory";
            this.Size = new System.Drawing.Size(1200, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_SortPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_FloorPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_RouteList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DL_ParcelList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FileData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Search)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_DL_ParcelList;
        private System.Windows.Forms.PictureBox pb_DL_RouteList;
        private BrbVideoManager.Controls.RoundedButton btn_Download;
        private System.Windows.Forms.Label lbl_RouteList;
        private System.Windows.Forms.Label lbl_ParcelList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbl_SortPlan;
        private System.Windows.Forms.Label lbl_FloorPlan;
        private System.Windows.Forms.PictureBox pb_DL_SortPlan;
        private System.Windows.Forms.PictureBox pb_DL_FloorPlan;
        private System.Windows.Forms.DataGridView dgv_FileData;
        private System.Windows.Forms.DateTimePicker dtp_SearchDate;
        private System.Windows.Forms.PictureBox pb_Search;
        private BrbVideoManager.Controls.RoundedButton btn_Clear;
    }
}
