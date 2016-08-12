namespace SeeWanted
{
    partial class Lista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lista));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.Persons = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.button2 = new MetroFramework.Controls.MetroButton();
            this.Vehicles = new System.Windows.Forms.ListBox();
            this.button5 = new MetroFramework.Controls.MetroButton();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.Persons);
            this.groupBox1.Location = new System.Drawing.Point(18, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 507);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Körözött Személyek";
            // 
            // textBox1
            // 
            // 
            // 
            // 
            this.textBox1.CustomButton.Image = null;
            this.textBox1.CustomButton.Location = new System.Drawing.Point(340, 1);
            this.textBox1.CustomButton.Name = "";
            this.textBox1.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox1.CustomButton.TabIndex = 1;
            this.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox1.CustomButton.UseSelectable = true;
            this.textBox1.CustomButton.Visible = false;
            this.textBox1.Lines = new string[0];
            this.textBox1.Location = new System.Drawing.Point(7, 478);
            this.textBox1.MaxLength = 32767;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '\0';
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.SelectedText = "";
            this.textBox1.SelectionLength = 0;
            this.textBox1.SelectionStart = 0;
            this.textBox1.ShortcutsEnabled = true;
            this.textBox1.Size = new System.Drawing.Size(362, 23);
            this.textBox1.TabIndex = 8;
            this.textBox1.UseSelectable = true;
            this.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(143, 461);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(83, 19);
            this.metroLabel1.TabIndex = 7;
            this.metroLabel1.Text = "Törlés Oka(i)";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(50, 435);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(267, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Részletek Megtekintése";
            this.button3.UseSelectable = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Kiválasztott (Személy) Körözés Törlése";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.metroLabel2);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.Vehicles);
            this.groupBox2.Location = new System.Drawing.Point(399, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 507);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Körözött Járművek";
            // 
            // textBox2
            // 
            // 
            // 
            // 
            this.textBox2.CustomButton.Image = null;
            this.textBox2.CustomButton.Location = new System.Drawing.Point(315, 1);
            this.textBox2.CustomButton.Name = "";
            this.textBox2.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBox2.CustomButton.TabIndex = 1;
            this.textBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBox2.CustomButton.UseSelectable = true;
            this.textBox2.CustomButton.Visible = false;
            this.textBox2.Lines = new string[0];
            this.textBox2.Location = new System.Drawing.Point(6, 478);
            this.textBox2.MaxLength = 32767;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '\0';
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox2.SelectedText = "";
            this.textBox2.SelectionLength = 0;
            this.textBox2.SelectionStart = 0;
            this.textBox2.ShortcutsEnabled = true;
            this.textBox2.Size = new System.Drawing.Size(337, 23);
            this.textBox2.TabIndex = 8;
            this.textBox2.UseSelectable = true;
            this.textBox2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBox2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(130, 459);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(83, 19);
            this.metroLabel2.TabIndex = 7;
            this.metroLabel2.Text = "Törlés Oka(i)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(44, 436);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(267, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Részletek Megtekintése";
            this.button4.UseSelectable = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(44, 407);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(267, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Kiválasztott (Jármű) Körözés Törlése";
            this.button2.UseSelectable = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Vehicles
            // 
            this.Vehicles.FormattingEnabled = true;
            this.Vehicles.Location = new System.Drawing.Point(6, 20);
            this.Vehicles.Name = "Vehicles";
            this.Vehicles.Size = new System.Drawing.Size(337, 381);
            this.Vehicles.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(331, 572);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Archivum";
            this.button5.UseSelectable = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(399, 13);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(38, 40);
            this.metroProgressSpinner1.TabIndex = 3;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // Lista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 602);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Lista";
            this.Text = "Körözési Lista Személyek/Járművek";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox Persons;
        private System.Windows.Forms.ListBox Vehicles;
        private MetroFramework.Controls.MetroButton button3;
        private MetroFramework.Controls.MetroButton button1;
        private MetroFramework.Controls.MetroButton button4;
        private MetroFramework.Controls.MetroButton button2;
        private MetroFramework.Controls.MetroTextBox textBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox textBox2;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton button5;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}