namespace SDRSharp.LimeSDR
{
  public  partial  class LimeSDRControllerDialog
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
            this.close = new System.Windows.Forms.Button();
            this.samplerateComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gainBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.gainDB = new System.Windows.Forms.Label();
            this.rx0 = new System.Windows.Forms.RadioButton();
            this.rx1 = new System.Windows.Forms.RadioButton();
            this.ant_h = new System.Windows.Forms.RadioButton();
            this.ant_l = new System.Windows.Forms.RadioButton();
            this.ant_w = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.gainBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.Location = new System.Drawing.Point(143, 265);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 0;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // samplerateComboBox
            // 
            this.samplerateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.samplerateComboBox.FormattingEnabled = true;
            this.samplerateComboBox.Location = new System.Drawing.Point(19, 39);
            this.samplerateComboBox.Name = "samplerateComboBox";
            this.samplerateComboBox.Size = new System.Drawing.Size(199, 20);
            this.samplerateComboBox.TabIndex = 1;
            this.samplerateComboBox.SelectedIndexChanged += new System.EventHandler(this.samplerateComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sample Rate";
            // 
            // gainBar
            // 
            this.gainBar.Location = new System.Drawing.Point(19, 102);
            this.gainBar.Maximum = 70;
            this.gainBar.Name = "gainBar";
            this.gainBar.Size = new System.Drawing.Size(199, 45);
            this.gainBar.TabIndex = 3;
            this.gainBar.Scroll += new System.EventHandler(this.gainBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gain";
            // 
            // gainDB
            // 
            this.gainDB.AutoSize = true;
            this.gainDB.Location = new System.Drawing.Point(189, 76);
            this.gainDB.Name = "gainDB";
            this.gainDB.Size = new System.Drawing.Size(17, 12);
            this.gainDB.TabIndex = 5;
            this.gainDB.Text = "db";
            // 
            // rx0
            // 
            this.rx0.AutoSize = true;
            this.rx0.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rx0.Location = new System.Drawing.Point(6, 20);
            this.rx0.Name = "rx0";
            this.rx0.Size = new System.Drawing.Size(41, 16);
            this.rx0.TabIndex = 7;
            this.rx0.TabStop = true;
            this.rx0.Text = "RX0";
            this.rx0.UseVisualStyleBackColor = true;
            this.rx0.CheckedChanged += new System.EventHandler(this.rx0_CheckedChanged);
            // 
            // rx1
            // 
            this.rx1.AutoSize = true;
            this.rx1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rx1.Location = new System.Drawing.Point(6, 42);
            this.rx1.Name = "rx1";
            this.rx1.Size = new System.Drawing.Size(41, 16);
            this.rx1.TabIndex = 8;
            this.rx1.TabStop = true;
            this.rx1.Text = "RX1";
            this.rx1.UseVisualStyleBackColor = true;
            this.rx1.CheckedChanged += new System.EventHandler(this.rx1_CheckedChanged);
            // 
            // ant_h
            // 
            this.ant_h.AutoSize = true;
            this.ant_h.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ant_h.Location = new System.Drawing.Point(6, 20);
            this.ant_h.Name = "ant_h";
            this.ant_h.Size = new System.Drawing.Size(53, 16);
            this.ant_h.TabIndex = 10;
            this.ant_h.TabStop = true;
            this.ant_h.Text = "LNA_H";
            this.ant_h.UseVisualStyleBackColor = true;
            this.ant_h.CheckedChanged += new System.EventHandler(this.ant_h_CheckedChanged);
            // 
            // ant_l
            // 
            this.ant_l.AutoSize = true;
            this.ant_l.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ant_l.Location = new System.Drawing.Point(6, 42);
            this.ant_l.Name = "ant_l";
            this.ant_l.Size = new System.Drawing.Size(53, 16);
            this.ant_l.TabIndex = 11;
            this.ant_l.TabStop = true;
            this.ant_l.Text = "LNA_L";
            this.ant_l.UseVisualStyleBackColor = true;
            this.ant_l.CheckedChanged += new System.EventHandler(this.ant_l_CheckedChanged);
            // 
            // ant_w
            // 
            this.ant_w.AutoSize = true;
            this.ant_w.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ant_w.Location = new System.Drawing.Point(6, 64);
            this.ant_w.Name = "ant_w";
            this.ant_w.Size = new System.Drawing.Size(53, 16);
            this.ant_w.TabIndex = 12;
            this.ant_w.TabStop = true;
            this.ant_w.Text = "LNA_W";
            this.ant_w.UseVisualStyleBackColor = true;
            this.ant_w.CheckedChanged += new System.EventHandler(this.ant_w_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rx0);
            this.groupBox1.Controls.Add(this.rx1);
            this.groupBox1.Location = new System.Drawing.Point(21, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 86);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ant_h);
            this.groupBox2.Controls.Add(this.ant_l);
            this.groupBox2.Controls.Add(this.ant_w);
            this.groupBox2.Location = new System.Drawing.Point(114, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 86);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Antenna";
            // 
            // LimeSDRControllerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.close;
            this.ClientSize = new System.Drawing.Size(241, 300);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gainDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gainBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.samplerateComboBox);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LimeSDRControllerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LimeSDR Controller test version";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.LimeSDRControllerDialog_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LimeSDRControllerDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gainBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ComboBox samplerateComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar gainBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label gainDB;
        private System.Windows.Forms.RadioButton rx0;
        private System.Windows.Forms.RadioButton rx1;
        private System.Windows.Forms.RadioButton ant_h;
        private System.Windows.Forms.RadioButton ant_l;
        private System.Windows.Forms.RadioButton ant_w;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}