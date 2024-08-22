namespace Mp3Oynatici
{
    partial class decibelForm
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
            this.trackDecibel = new Guna.UI2.WinForms.Guna2TrackBar();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblDecibel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnDecibel = new Guna.UI2.WinForms.Guna2CircleButton();
            this.SuspendLayout();
            // 
            // trackDecibel
            // 
            this.trackDecibel.Location = new System.Drawing.Point(76, 83);
            this.trackDecibel.Maximum = 20;
            this.trackDecibel.Name = "trackDecibel";
            this.trackDecibel.Size = new System.Drawing.Size(267, 23);
            this.trackDecibel.Style = Guna.UI2.WinForms.Enums.TrackBarStyle.Metro;
            this.trackDecibel.TabIndex = 0;
            this.trackDecibel.ThumbColor = System.Drawing.Color.DimGray;
            this.trackDecibel.Value = 10;
            this.trackDecibel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trackDecibel_Scroll);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.guna2ControlBox1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(367, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(39, 33);
            this.guna2ControlBox1.TabIndex = 1;
            // 
            // lblDecibel
            // 
            this.lblDecibel.BackColor = System.Drawing.Color.Transparent;
            this.lblDecibel.Location = new System.Drawing.Point(199, 111);
            this.lblDecibel.Name = "lblDecibel";
            this.lblDecibel.Size = new System.Drawing.Size(14, 15);
            this.lblDecibel.TabIndex = 2;
            this.lblDecibel.Text = "x1";
            // 
            // btnDecibel
            // 
            this.btnDecibel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDecibel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDecibel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDecibel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDecibel.FillColor = System.Drawing.Color.Gray;
            this.btnDecibel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDecibel.ForeColor = System.Drawing.Color.White;
            this.btnDecibel.Location = new System.Drawing.Point(176, 140);
            this.btnDecibel.Name = "btnDecibel";
            this.btnDecibel.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDecibel.Size = new System.Drawing.Size(69, 61);
            this.btnDecibel.TabIndex = 3;
            this.btnDecibel.Text = "Gönder";
            this.btnDecibel.Click += new System.EventHandler(this.btnDecibel_Click);
            // 
            // decibelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(406, 270);
            this.Controls.Add(this.btnDecibel);
            this.Controls.Add(this.lblDecibel);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.trackDecibel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "decibelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "decibelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TrackBar trackDecibel;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDecibel;
        private Guna.UI2.WinForms.Guna2CircleButton btnDecibel;
    }
}