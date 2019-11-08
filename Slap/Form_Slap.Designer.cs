namespace Slap
{
    partial class Form_Slap
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
            this.panel_User = new System.Windows.Forms.Panel();
            this.pb_FedexLogo = new System.Windows.Forms.PictureBox();
            this.panel_Tab = new System.Windows.Forms.Panel();
            this.btn_MenuSortHistory = new System.Windows.Forms.Button();
            this.btn_MenuNewSort = new System.Windows.Forms.Button();
            this.ctrl_SortHistory1 = new Slap.ctrl_SortHistory();
            this.ctrl_NewSort_Input1 = new Slap.ctrl_NewSort_Input();
            this.panel_User.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FedexLogo)).BeginInit();
            this.panel_Tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_User
            // 
            this.panel_User.BackColor = System.Drawing.Color.White;
            this.panel_User.Controls.Add(this.pb_FedexLogo);
            this.panel_User.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_User.Location = new System.Drawing.Point(0, 0);
            this.panel_User.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_User.Name = "panel_User";
            this.panel_User.Size = new System.Drawing.Size(1200, 100);
            this.panel_User.TabIndex = 0;
            // 
            // pb_FedexLogo
            // 
            this.pb_FedexLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_FedexLogo.Image = global::Slap.Properties.Resources.fedexLogo;
            this.pb_FedexLogo.Location = new System.Drawing.Point(453, 0);
            this.pb_FedexLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_FedexLogo.Name = "pb_FedexLogo";
            this.pb_FedexLogo.Size = new System.Drawing.Size(249, 100);
            this.pb_FedexLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_FedexLogo.TabIndex = 0;
            this.pb_FedexLogo.TabStop = false;
            // 
            // panel_Tab
            // 
            this.panel_Tab.BackColor = System.Drawing.Color.White;
            this.panel_Tab.Controls.Add(this.btn_MenuSortHistory);
            this.panel_Tab.Controls.Add(this.btn_MenuNewSort);
            this.panel_Tab.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Tab.Location = new System.Drawing.Point(0, 100);
            this.panel_Tab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_Tab.Name = "panel_Tab";
            this.panel_Tab.Size = new System.Drawing.Size(1200, 75);
            this.panel_Tab.TabIndex = 1;
            // 
            // btn_MenuSortHistory
            // 
            this.btn_MenuSortHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_MenuSortHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_MenuSortHistory.FlatAppearance.BorderSize = 0;
            this.btn_MenuSortHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_MenuSortHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_MenuSortHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MenuSortHistory.Font = new System.Drawing.Font("DengXian", 15F, System.Drawing.FontStyle.Bold);
            this.btn_MenuSortHistory.Location = new System.Drawing.Point(600, 0);
            this.btn_MenuSortHistory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_MenuSortHistory.Name = "btn_MenuSortHistory";
            this.btn_MenuSortHistory.Size = new System.Drawing.Size(600, 75);
            this.btn_MenuSortHistory.TabIndex = 3;
            this.btn_MenuSortHistory.Text = "SORT HISTORY";
            this.btn_MenuSortHistory.UseVisualStyleBackColor = false;
            this.btn_MenuSortHistory.Click += new System.EventHandler(this.btn_SortHistory_Click);
            // 
            // btn_MenuNewSort
            // 
            this.btn_MenuNewSort.BackColor = System.Drawing.Color.BlueViolet;
            this.btn_MenuNewSort.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_MenuNewSort.FlatAppearance.BorderSize = 0;
            this.btn_MenuNewSort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.BlueViolet;
            this.btn_MenuNewSort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet;
            this.btn_MenuNewSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MenuNewSort.Font = new System.Drawing.Font("DengXian", 15F, System.Drawing.FontStyle.Bold);
            this.btn_MenuNewSort.Location = new System.Drawing.Point(0, 0);
            this.btn_MenuNewSort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_MenuNewSort.Name = "btn_MenuNewSort";
            this.btn_MenuNewSort.Size = new System.Drawing.Size(600, 75);
            this.btn_MenuNewSort.TabIndex = 2;
            this.btn_MenuNewSort.Text = "NEW SORT";
            this.btn_MenuNewSort.UseVisualStyleBackColor = false;
            this.btn_MenuNewSort.Click += new System.EventHandler(this.btn_NewSort_Click);
            // 
            // ctrl_SortHistory1
            // 
            this.ctrl_SortHistory1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ctrl_SortHistory1.Location = new System.Drawing.Point(0, 190);
            this.ctrl_SortHistory1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_SortHistory1.Name = "ctrl_SortHistory1";
            this.ctrl_SortHistory1.Size = new System.Drawing.Size(1200, 700);
            this.ctrl_SortHistory1.TabIndex = 3;
            // 
            // ctrl_NewSort_Input1
            // 
            this.ctrl_NewSort_Input1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ctrl_NewSort_Input1.Location = new System.Drawing.Point(0, 190);
            this.ctrl_NewSort_Input1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl_NewSort_Input1.Name = "ctrl_NewSort_Input1";
            this.ctrl_NewSort_Input1.Size = new System.Drawing.Size(1200, 700);
            this.ctrl_NewSort_Input1.TabIndex = 2;
            // 
            // Form_Slap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.ctrl_SortHistory1);
            this.Controls.Add(this.ctrl_NewSort_Input1);
            this.Controls.Add(this.panel_Tab);
            this.Controls.Add(this.panel_User);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_Slap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FedEx SLaP - Sorting Large Parcels";
            this.Resize += new System.EventHandler(this.Form_Slap_Resize);
            this.panel_User.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_FedexLogo)).EndInit();
            this.panel_Tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_User;
        private System.Windows.Forms.Panel panel_Tab;
        private System.Windows.Forms.Button btn_MenuNewSort;
        private System.Windows.Forms.Button btn_MenuSortHistory;
        private System.Windows.Forms.PictureBox pb_FedexLogo;
        private ctrl_NewSort_Input ctrl_NewSort_Input1;
        private ctrl_SortHistory ctrl_SortHistory1;
    }
}

