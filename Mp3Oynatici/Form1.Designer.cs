namespace Mp3Player
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGecenSure = new System.Windows.Forms.Label();
            this.lblToplamSure = new System.Windows.Forms.Label();
            this.graphic = new Guna.UI2.WinForms.Guna2Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBackground2 = new System.Windows.Forms.Label();
            this.lblBackground1 = new System.Windows.Forms.Label();
            this.lblExport = new System.Windows.Forms.Label();
            this.lblImport = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.Volume = new Guna.UI2.WinForms.Guna2TrackBar();
            this.guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.formatToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toMP3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toWAVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toAACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toOGGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblBluetooth = new System.Windows.Forms.Label();
            this.lblChangeMicrophone = new System.Windows.Forms.Label();
            this.btnMicStop = new System.Windows.Forms.Button();
            this.btnMicPlay = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnPlay = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnVolume = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMicrophone = new System.Windows.Forms.Button();
            this.btnBluetooth = new System.Windows.Forms.Button();
            this.btnBackground = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.guna2ContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGecenSure
            // 
            this.lblGecenSure.AutoSize = true;
            this.lblGecenSure.Location = new System.Drawing.Point(3, 3);
            this.lblGecenSure.Name = "lblGecenSure";
            this.lblGecenSure.Size = new System.Drawing.Size(49, 13);
            this.lblGecenSure.TabIndex = 8;
            this.lblGecenSure.Text = "00:00:00";
            // 
            // lblToplamSure
            // 
            this.lblToplamSure.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblToplamSure.AutoSize = true;
            this.lblToplamSure.Location = new System.Drawing.Point(899, 3);
            this.lblToplamSure.Name = "lblToplamSure";
            this.lblToplamSure.Size = new System.Drawing.Size(49, 13);
            this.lblToplamSure.TabIndex = 9;
            this.lblToplamSure.Text = "00:00:00";
            // 
            // graphic
            // 
            this.graphic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.graphic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graphic.Location = new System.Drawing.Point(0, 422);
            this.graphic.Name = "graphic";
            this.graphic.Size = new System.Drawing.Size(950, 99);
            this.graphic.TabIndex = 17;
            this.graphic.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            this.graphic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graphic_MouseClick);
            this.graphic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.guna2Panel1_MouseDown);
            this.graphic.MouseEnter += new System.EventHandler(this.guna2Panel1_MouseEnter);
            this.graphic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.guna2Panel1_MouseMove);
            this.graphic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.guna2Panel1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(250)))), ((int)(((byte)(244)))));
            this.panel1.Controls.Add(this.lblChangeMicrophone);
            this.panel1.Controls.Add(this.btnMicrophone);
            this.panel1.Controls.Add(this.lblBluetooth);
            this.panel1.Controls.Add(this.lblBackground2);
            this.panel1.Controls.Add(this.btnBluetooth);
            this.panel1.Controls.Add(this.lblBackground1);
            this.panel1.Controls.Add(this.btnBackground);
            this.panel1.Controls.Add(this.lblExport);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.lblImport);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 423);
            this.panel1.TabIndex = 21;
            // 
            // lblBackground2
            // 
            this.lblBackground2.AutoSize = true;
            this.lblBackground2.Location = new System.Drawing.Point(36, 238);
            this.lblBackground2.Name = "lblBackground2";
            this.lblBackground2.Size = new System.Drawing.Size(65, 13);
            this.lblBackground2.TabIndex = 35;
            this.lblBackground2.Text = "Background";
            // 
            // lblBackground1
            // 
            this.lblBackground1.AutoSize = true;
            this.lblBackground1.Location = new System.Drawing.Point(45, 225);
            this.lblBackground1.Name = "lblBackground1";
            this.lblBackground1.Size = new System.Drawing.Size(47, 13);
            this.lblBackground1.TabIndex = 34;
            this.lblBackground1.Text = "Remove";
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Location = new System.Drawing.Point(52, 159);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(37, 13);
            this.lblExport.TabIndex = 32;
            this.lblExport.Text = "Export";
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Location = new System.Drawing.Point(52, 85);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(36, 13);
            this.lblImport.TabIndex = 26;
            this.lblImport.Text = "Import";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel2.Controls.Add(this.lblFileName);
            this.panel2.Controls.Add(this.btnMicStop);
            this.panel2.Controls.Add(this.btnMicPlay);
            this.panel2.Controls.Add(this.btnForward);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.btnVolume);
            this.panel2.Controls.Add(this.btnBackward);
            this.panel2.Controls.Add(this.btnRestart);
            this.panel2.Controls.Add(this.Volume);
            this.panel2.Controls.Add(this.lblGecenSure);
            this.panel2.Controls.Add(this.lblToplamSure);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 521);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 69);
            this.panel2.TabIndex = 22;
            // 
            // lblFileName
            // 
            this.lblFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(59, 21);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 24);
            this.lblFileName.TabIndex = 25;
            // 
            // Volume
            // 
            this.Volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Volume.Location = new System.Drawing.Point(731, 15);
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(195, 33);
            this.Volume.Style = Guna.UI2.WinForms.Enums.TrackBarStyle.Metro;
            this.Volume.TabIndex = 23;
            this.Volume.ThumbColor = System.Drawing.Color.DarkSlateGray;
            this.Volume.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Volume_Scroll);
            // 
            // guna2ContextMenuStrip1
            // 
            this.guna2ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatToolStrip,
            this.audioToolStrip});
            this.guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            this.guna2ContextMenuStrip1.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            this.guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.guna2ContextMenuStrip1.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.guna2ContextMenuStrip1.Size = new System.Drawing.Size(174, 48);
            // 
            // formatToolStrip
            // 
            this.formatToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toMP3ToolStripMenuItem,
            this.toWAVToolStripMenuItem,
            this.toAACToolStripMenuItem,
            this.toOGGToolStripMenuItem});
            this.formatToolStrip.Name = "formatToolStrip";
            this.formatToolStrip.Size = new System.Drawing.Size(173, 22);
            this.formatToolStrip.Text = "Change file format";
            // 
            // toMP3ToolStripMenuItem
            // 
            this.toMP3ToolStripMenuItem.Name = "toMP3ToolStripMenuItem";
            this.toMP3ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.toMP3ToolStripMenuItem.Text = "to MP3";
            this.toMP3ToolStripMenuItem.Click += new System.EventHandler(this.toMp3_Click);
            // 
            // toWAVToolStripMenuItem
            // 
            this.toWAVToolStripMenuItem.Name = "toWAVToolStripMenuItem";
            this.toWAVToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.toWAVToolStripMenuItem.Text = "to WAV";
            this.toWAVToolStripMenuItem.Click += new System.EventHandler(this.toWav_Click);
            // 
            // toAACToolStripMenuItem
            // 
            this.toAACToolStripMenuItem.Name = "toAACToolStripMenuItem";
            this.toAACToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.toAACToolStripMenuItem.Text = "to AAC";
            this.toAACToolStripMenuItem.Click += new System.EventHandler(this.toAac_Click);
            // 
            // toOGGToolStripMenuItem
            // 
            this.toOGGToolStripMenuItem.Name = "toOGGToolStripMenuItem";
            this.toOGGToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.toOGGToolStripMenuItem.Text = "to OGG";
            this.toOGGToolStripMenuItem.Click += new System.EventHandler(this.toOgg_Click);
            // 
            // audioToolStrip
            // 
            this.audioToolStrip.Name = "audioToolStrip";
            this.audioToolStrip.Size = new System.Drawing.Size(173, 22);
            this.audioToolStrip.Text = "Change Decibels";
            this.audioToolStrip.Click += new System.EventHandler(this.audioToolStrip_Click);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Red;
            this.guna2ControlBox1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(911, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(39, 29);
            this.guna2ControlBox1.TabIndex = 23;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(872, 0);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(39, 29);
            this.guna2ControlBox2.TabIndex = 24;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(833, 0);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(39, 29);
            this.guna2ControlBox3.TabIndex = 25;
            // 
            // lblBluetooth
            // 
            this.lblBluetooth.AutoSize = true;
            this.lblBluetooth.Location = new System.Drawing.Point(45, 85);
            this.lblBluetooth.Name = "lblBluetooth";
            this.lblBluetooth.Size = new System.Drawing.Size(52, 13);
            this.lblBluetooth.TabIndex = 37;
            this.lblBluetooth.Text = "Bluetooth";
            // 
            // lblChangeMicrophone
            // 
            this.lblChangeMicrophone.AutoSize = true;
            this.lblChangeMicrophone.Location = new System.Drawing.Point(38, 310);
            this.lblChangeMicrophone.Name = "lblChangeMicrophone";
            this.lblChangeMicrophone.Size = new System.Drawing.Size(63, 13);
            this.lblChangeMicrophone.TabIndex = 39;
            this.lblChangeMicrophone.Text = "Microphone";
            // 
            // btnMicStop
            // 
            this.btnMicStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMicStop.BackColor = System.Drawing.Color.Transparent;
            this.btnMicStop.FlatAppearance.BorderSize = 0;
            this.btnMicStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnMicStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnMicStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMicStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMicStop.Image = global::Mp3Oynatici.Properties.Resources.icons8_stop_25;
            this.btnMicStop.Location = new System.Drawing.Point(362, 15);
            this.btnMicStop.Name = "btnMicStop";
            this.btnMicStop.Size = new System.Drawing.Size(37, 32);
            this.btnMicStop.TabIndex = 27;
            this.btnMicStop.UseVisualStyleBackColor = false;
            this.btnMicStop.Click += new System.EventHandler(this.btnMicStop_Click);
            // 
            // btnMicPlay
            // 
            this.btnMicPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnMicPlay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMicPlay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMicPlay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMicPlay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMicPlay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnMicPlay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMicPlay.ForeColor = System.Drawing.Color.White;
            this.btnMicPlay.Image = global::Mp3Oynatici.Properties.Resources.icons8_play_25;
            this.btnMicPlay.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnMicPlay.Location = new System.Drawing.Point(429, 11);
            this.btnMicPlay.Name = "btnMicPlay";
            this.btnMicPlay.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnMicPlay.Size = new System.Drawing.Size(41, 41);
            this.btnMicPlay.TabIndex = 27;
            this.btnMicPlay.Click += new System.EventHandler(this.btnMicPlay_Click);
            // 
            // btnForward
            // 
            this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnForward.BackColor = System.Drawing.Color.Transparent;
            this.btnForward.FlatAppearance.BorderSize = 0;
            this.btnForward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnForward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnForward.Image = global::Mp3Oynatici.Properties.Resources.icons8_fast_forward_25;
            this.btnForward.Location = new System.Drawing.Point(498, 15);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(37, 32);
            this.btnForward.TabIndex = 14;
            this.btnForward.UseVisualStyleBackColor = false;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnPlay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPlay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPlay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPlay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPlay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPlay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Image = global::Mp3Oynatici.Properties.Resources.icons8_play_25;
            this.btnPlay.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnPlay.Location = new System.Drawing.Point(429, 11);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnPlay.Size = new System.Drawing.Size(41, 41);
            this.btnPlay.TabIndex = 26;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnVolume
            // 
            this.btnVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolume.BackColor = System.Drawing.Color.Transparent;
            this.btnVolume.FlatAppearance.BorderSize = 0;
            this.btnVolume.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnVolume.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnVolume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolume.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnVolume.Image = global::Mp3Oynatici.Properties.Resources.icons8_voice_25;
            this.btnVolume.Location = new System.Drawing.Point(691, 15);
            this.btnVolume.Name = "btnVolume";
            this.btnVolume.Size = new System.Drawing.Size(37, 32);
            this.btnVolume.TabIndex = 24;
            this.btnVolume.UseVisualStyleBackColor = false;
            this.btnVolume.Click += new System.EventHandler(this.btnVolume_Click);
            this.btnVolume.Paint += new System.Windows.Forms.PaintEventHandler(this.btnVolume_Paint);
            // 
            // btnBackward
            // 
            this.btnBackward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnBackward.BackColor = System.Drawing.Color.Transparent;
            this.btnBackward.FlatAppearance.BorderSize = 0;
            this.btnBackward.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnBackward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackward.Image = global::Mp3Oynatici.Properties.Resources.icons8_rewind_25;
            this.btnBackward.Location = new System.Drawing.Point(298, 15);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(37, 32);
            this.btnBackward.TabIndex = 15;
            this.btnBackward.UseVisualStyleBackColor = false;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnRestart.BackColor = System.Drawing.Color.Transparent;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SkyBlue;
            this.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRestart.Image = global::Mp3Oynatici.Properties.Resources.icons8_restart_25;
            this.btnRestart.Location = new System.Drawing.Point(362, 15);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(37, 32);
            this.btnRestart.TabIndex = 20;
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnMicrophone
            // 
            this.btnMicrophone.BackColor = System.Drawing.Color.Transparent;
            this.btnMicrophone.FlatAppearance.BorderSize = 0;
            this.btnMicrophone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(244)))));
            this.btnMicrophone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.btnMicrophone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMicrophone.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMicrophone.Image = global::Mp3Oynatici.Properties.Resources.icons8_microphone_30;
            this.btnMicrophone.Location = new System.Drawing.Point(44, 264);
            this.btnMicrophone.Name = "btnMicrophone";
            this.btnMicrophone.Size = new System.Drawing.Size(48, 43);
            this.btnMicrophone.TabIndex = 38;
            this.btnMicrophone.UseVisualStyleBackColor = false;
            this.btnMicrophone.Click += new System.EventHandler(this.btnMicrophone_Click);
            // 
            // btnBluetooth
            // 
            this.btnBluetooth.BackColor = System.Drawing.Color.Transparent;
            this.btnBluetooth.FlatAppearance.BorderSize = 0;
            this.btnBluetooth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(244)))));
            this.btnBluetooth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.btnBluetooth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBluetooth.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBluetooth.Image = global::Mp3Oynatici.Properties.Resources.icons8_bluetooth_25;
            this.btnBluetooth.Location = new System.Drawing.Point(46, 39);
            this.btnBluetooth.Name = "btnBluetooth";
            this.btnBluetooth.Size = new System.Drawing.Size(48, 43);
            this.btnBluetooth.TabIndex = 36;
            this.btnBluetooth.UseVisualStyleBackColor = false;
            // 
            // btnBackground
            // 
            this.btnBackground.BackColor = System.Drawing.Color.Transparent;
            this.btnBackground.FlatAppearance.BorderSize = 0;
            this.btnBackground.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(244)))));
            this.btnBackground.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.btnBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackground.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackground.Image = global::Mp3Oynatici.Properties.Resources.icons8_remove_25;
            this.btnBackground.Location = new System.Drawing.Point(44, 185);
            this.btnBackground.Name = "btnBackground";
            this.btnBackground.Size = new System.Drawing.Size(48, 43);
            this.btnBackground.TabIndex = 33;
            this.btnBackground.UseVisualStyleBackColor = false;
            this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(244)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Image = global::Mp3Oynatici.Properties.Resources.icons8_convert_35;
            this.btnExport.Location = new System.Drawing.Point(46, 113);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(48, 43);
            this.btnExport.TabIndex = 31;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(244)))));
            this.btnOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(240)))), ((int)(((byte)(244)))));
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.Image = global::Mp3Oynatici.Properties.Resources.icons8_opened_folder_35;
            this.btnOpen.Location = new System.Drawing.Point(46, 39);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(48, 43);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(950, 590);
            this.Controls.Add(this.guna2ControlBox3);
            this.Controls.Add(this.guna2ControlBox2);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.graphic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(950, 440);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mp3 Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.guna2ContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblGecenSure;
        private System.Windows.Forms.Label lblToplamSure;
        internal System.Windows.Forms.Button btnForward;
        internal System.Windows.Forms.Button btnBackward;
        private Guna.UI2.WinForms.Guna2Panel graphic;
        internal System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2TrackBar Volume;
        internal System.Windows.Forms.Button btnVolume;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.Button btnExport;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem formatToolStrip;
        private System.Windows.Forms.ToolStripMenuItem toMP3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toWAVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toAACToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toOGGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioToolStrip;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2CircleButton btnPlay;
        private System.Windows.Forms.Button btnBackground;
        private System.Windows.Forms.Label lblBackground1;
        private System.Windows.Forms.Label lblBackground2;
        internal System.Windows.Forms.Button btnMicStop;
        private Guna.UI2.WinForms.Guna2CircleButton btnMicPlay;
        private System.Windows.Forms.Button btnBluetooth;
        private System.Windows.Forms.Label lblBluetooth;
        private System.Windows.Forms.Label lblChangeMicrophone;
        private System.Windows.Forms.Button btnMicrophone;
    }
}

