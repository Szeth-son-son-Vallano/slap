namespace Slap
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
            this.panel_User = new System.Windows.Forms.Panel();
            this.panel_Tab = new System.Windows.Forms.Panel();
            this.btn_NewSort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel_User
            // 
            this.panel_User.Location = new System.Drawing.Point(0, 0);
            this.panel_User.Name = "panel_User";
            this.panel_User.Size = new System.Drawing.Size(1200, 100);
            this.panel_User.TabIndex = 0;
            // 
            // panel_Tab
            // 
            this.panel_Tab.Location = new System.Drawing.Point(0, 100);
            this.panel_Tab.Name = "panel_Tab";
            this.panel_Tab.Size = new System.Drawing.Size(1200, 75);
            this.panel_Tab.TabIndex = 1;
            // 
            // btn_NewSort
            // 
            this.btn_NewSort.Location = new System.Drawing.Point(228, 235);
            this.btn_NewSort.Name = "btn_NewSort";
            this.btn_NewSort.Size = new System.Drawing.Size(179, 54);
            this.btn_NewSort.TabIndex = 2;
            this.btn_NewSort.Text = "NEW SORT";
            this.btn_NewSort.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.btn_NewSort);
            this.Controls.Add(this.panel_Tab);
            this.Controls.Add(this.panel_User);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_User;
        private System.Windows.Forms.Panel panel_Tab;
        private System.Windows.Forms.Button btn_NewSort;
    }
}

