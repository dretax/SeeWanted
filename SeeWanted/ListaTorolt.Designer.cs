namespace SeeWanted
{
    partial class ListaTorolt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaTorolt));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.Persons = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.Vehicles = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.Persons);
            this.groupBox1.Location = new System.Drawing.Point(18, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archivált Körözések (Személyek)";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(84, 423);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Részletek Megtekintése";
            this.button3.UseSelectable = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Persons
            // 
            this.Persons.FormattingEnabled = true;
            this.Persons.Location = new System.Drawing.Point(7, 20);
            this.Persons.Name = "Persons";
            this.Persons.Size = new System.Drawing.Size(362, 381);
            this.Persons.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.Vehicles);
            this.groupBox2.Location = new System.Drawing.Point(400, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 476);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivált Körözések (Járművek)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(82, 423);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(210, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Részletek Megtekintése";
            this.button4.UseSelectable = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Vehicles
            // 
            this.Vehicles.FormattingEnabled = true;
            this.Vehicles.Location = new System.Drawing.Point(6, 20);
            this.Vehicles.Name = "Vehicles";
            this.Vehicles.Size = new System.Drawing.Size(336, 381);
            this.Vehicles.TabIndex = 0;
            // 
            // ListaTorolt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 551);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListaTorolt";
            this.Text = "Archivált Személy/Jármű Körözések";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox Persons;
        private System.Windows.Forms.ListBox Vehicles;
        private MetroFramework.Controls.MetroButton button3;
        private MetroFramework.Controls.MetroButton button4;
    }
}