namespace SeeWanted
{
    partial class NyilvantartasArch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyilvantartasArch));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.Szemelyek = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.Szemelyek);
            this.groupBox1.Location = new System.Drawing.Point(23, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 278);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archivált Nyílvántartások";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(342, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Nyílvántartás Megtekintése";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Szemelyek
            // 
            this.Szemelyek.FormattingEnabled = true;
            this.Szemelyek.Location = new System.Drawing.Point(7, 20);
            this.Szemelyek.Name = "Szemelyek";
            this.Szemelyek.Size = new System.Drawing.Size(342, 199);
            this.Szemelyek.TabIndex = 0;
            // 
            // NyilvantartasArch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 346);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NyilvantartasArch";
            this.Text = "Archivált Nyílvántartások";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox Szemelyek;
        private MetroFramework.Controls.MetroButton button1;
    }
}