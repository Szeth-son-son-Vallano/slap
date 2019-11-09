namespace Slap
{
    partial class ctrl_NewSort_Input
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_NewSort_Input));
            this.lbl_ParcelListFile = new System.Windows.Forms.Label();
            this.lbl_RouteListFile = new System.Windows.Forms.Label();
            this.pb_DND_RouteList = new System.Windows.Forms.PictureBox();
            this.pb_DND_ParcelList = new System.Windows.Forms.PictureBox();
            this.lbl_RouteList = new System.Windows.Forms.Label();
            this.lbl_ParcelList = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbl_ErrorMessage = new System.Windows.Forms.Label();
            this.btn_Read = new BrbVideoManager.Controls.RoundedButton();
            this.btn_Clear = new BrbVideoManager.Controls.RoundedButton();
            this.ctrl_NewSort_Output1 = new Slap.ctrl_NewSort_Output();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DND_RouteList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DND_ParcelList)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ParcelListFile
            // 
            this.lbl_ParcelListFile.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ParcelListFile.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Italic);
            this.lbl_ParcelListFile.Location = new System.Drawing.Point(175, 420);
            this.lbl_ParcelListFile.Name = "lbl_ParcelListFile";
            this.lbl_ParcelListFile.Size = new System.Drawing.Size(300, 30);
            this.lbl_ParcelListFile.TabIndex = 3;
            this.lbl_ParcelListFile.Text = "Drag and Drop";
            this.lbl_ParcelListFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RouteListFile
            // 
            this.lbl_RouteListFile.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RouteListFile.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Italic);
            this.lbl_RouteListFile.Location = new System.Drawing.Point(725, 420);
            this.lbl_RouteListFile.Name = "lbl_RouteListFile";
            this.lbl_RouteListFile.Size = new System.Drawing.Size(300, 30);
            this.lbl_RouteListFile.TabIndex = 4;
            this.lbl_RouteListFile.Text = "Drag and Drop";
            this.lbl_RouteListFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_DND_RouteList
            // 
            this.pb_DND_RouteList.Image = ((System.Drawing.Image)(resources.GetObject("pb_DND_RouteList.Image")));
            this.pb_DND_RouteList.Location = new System.Drawing.Point(700, 125);
            this.pb_DND_RouteList.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DND_RouteList.Name = "pb_DND_RouteList";
            this.pb_DND_RouteList.Size = new System.Drawing.Size(350, 350);
            this.pb_DND_RouteList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DND_RouteList.TabIndex = 1;
            this.pb_DND_RouteList.TabStop = false;
            this.pb_DND_RouteList.Click += new System.EventHandler(this.pb_DND_RouteList_Click);
            this.pb_DND_RouteList.DragDrop += new System.Windows.Forms.DragEventHandler(this.pb_DND_RouteList_DragDrop);
            this.pb_DND_RouteList.DragEnter += new System.Windows.Forms.DragEventHandler(this.pb_DND_RouteList_DragEnter);
            // 
            // pb_DND_ParcelList
            // 
            this.pb_DND_ParcelList.Image = global::Slap.Properties.Resources.fileGrayFrame;
            this.pb_DND_ParcelList.Location = new System.Drawing.Point(150, 125);
            this.pb_DND_ParcelList.Margin = new System.Windows.Forms.Padding(0);
            this.pb_DND_ParcelList.Name = "pb_DND_ParcelList";
            this.pb_DND_ParcelList.Size = new System.Drawing.Size(350, 350);
            this.pb_DND_ParcelList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_DND_ParcelList.TabIndex = 0;
            this.pb_DND_ParcelList.TabStop = false;
            this.pb_DND_ParcelList.Click += new System.EventHandler(this.pb_DND_ParcelList_Click);
            this.pb_DND_ParcelList.DragDrop += new System.Windows.Forms.DragEventHandler(this.pb_DND_ParcelList_DragDrop);
            this.pb_DND_ParcelList.DragEnter += new System.Windows.Forms.DragEventHandler(this.pb_DND_ParcelList_DragEnter);
            // 
            // lbl_RouteList
            // 
            this.lbl_RouteList.BackColor = System.Drawing.Color.Transparent;
            this.lbl_RouteList.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_RouteList.Location = new System.Drawing.Point(725, 75);
            this.lbl_RouteList.Name = "lbl_RouteList";
            this.lbl_RouteList.Size = new System.Drawing.Size(300, 30);
            this.lbl_RouteList.TabIndex = 9;
            this.lbl_RouteList.Text = "ROUTE LIST";
            this.lbl_RouteList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ParcelList
            // 
            this.lbl_ParcelList.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ParcelList.Font = new System.Drawing.Font("DengXian", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_ParcelList.Location = new System.Drawing.Point(175, 75);
            this.lbl_ParcelList.Name = "lbl_ParcelList";
            this.lbl_ParcelList.Size = new System.Drawing.Size(300, 30);
            this.lbl_ParcelList.TabIndex = 8;
            this.lbl_ParcelList.Text = "PARCEL LIST";
            this.lbl_ParcelList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbl_ErrorMessage
            // 
            this.lbl_ErrorMessage.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ErrorMessage.Font = new System.Drawing.Font("DengXian", 10F, System.Drawing.FontStyle.Italic);
            this.lbl_ErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMessage.Location = new System.Drawing.Point(350, 500);
            this.lbl_ErrorMessage.Name = "lbl_ErrorMessage";
            this.lbl_ErrorMessage.Size = new System.Drawing.Size(500, 20);
            this.lbl_ErrorMessage.TabIndex = 10;
            this.lbl_ErrorMessage.Text = "Error message";
            this.lbl_ErrorMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Read
            // 
            this.btn_Read.BackColor = System.Drawing.Color.Indigo;
            this.btn_Read.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Read.BorderDownColor = System.Drawing.Color.Empty;
            this.btn_Read.BorderDownWidth = 0F;
            this.btn_Read.BorderOverColor = System.Drawing.Color.Empty;
            this.btn_Read.BorderOverWidth = 0F;
            this.btn_Read.BorderRadius = 30;
            this.btn_Read.BorderWidth = 0F;
            this.btn_Read.FlatAppearance.BorderSize = 0;
            this.btn_Read.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(0)))), ((int)(((byte)(100)))));
            this.btn_Read.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btn_Read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Read.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Read.ForeColor = System.Drawing.Color.White;
            this.btn_Read.Location = new System.Drawing.Point(475, 525);
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.Size = new System.Drawing.Size(250, 60);
            this.btn_Read.TabIndex = 6;
            this.btn_Read.Text = "R E A D";
            this.btn_Read.UseVisualStyleBackColor = false;
            this.btn_Read.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Read_MouseDown);
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
            this.btn_Clear.TabIndex = 7;
            this.btn_Clear.Text = "C L E A R";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Clear_MouseDown);
            // 
            // ctrl_NewSort_Output1
            // 
            this.ctrl_NewSort_Output1.Location = new System.Drawing.Point(0, 0);
            this.ctrl_NewSort_Output1.Name = "ctrl_NewSort_Output1";
            this.ctrl_NewSort_Output1.Size = new System.Drawing.Size(1200, 700);
            this.ctrl_NewSort_Output1.TabIndex = 11;
            // 
            // ctrl_NewSort_Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrl_NewSort_Output1);
            this.Controls.Add(this.lbl_ParcelList);
            this.Controls.Add(this.lbl_RouteList);
            this.Controls.Add(this.lbl_ParcelListFile);
            this.Controls.Add(this.lbl_RouteListFile);
            this.Controls.Add(this.pb_DND_ParcelList);
            this.Controls.Add(this.pb_DND_RouteList);
            this.Controls.Add(this.lbl_ErrorMessage);
            this.Controls.Add(this.btn_Read);
            this.Controls.Add(this.btn_Clear);
            this.Name = "ctrl_NewSort_Input";
            this.Size = new System.Drawing.Size(1200, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pb_DND_RouteList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DND_ParcelList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_DND_ParcelList;
        private System.Windows.Forms.PictureBox pb_DND_RouteList;
        private System.Windows.Forms.Label lbl_ParcelListFile;
        private System.Windows.Forms.Label lbl_RouteListFile;
        private System.Windows.Forms.Label lbl_RouteList;
        private System.Windows.Forms.Label lbl_ParcelList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbl_ErrorMessage;
        private BrbVideoManager.Controls.RoundedButton btn_Read;
        private BrbVideoManager.Controls.RoundedButton btn_Clear;
        private ctrl_NewSort_Output ctrl_NewSort_Output1;
    }
}
