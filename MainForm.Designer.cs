namespace ServerManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ServerStatus = new StatusStrip();
            ChattingServerLabel = new ToolStripStatusLabel();
            FrontServerLabel = new ToolStripStatusLabel();
            GameServerLabel = new ToolStripStatusLabel();
            GatewayServerLabel = new ToolStripStatusLabel();
            WorldServerLabel = new ToolStripStatusLabel();
            MySQLLabel = new ToolStripStatusLabel();
            CouchbaseLabel = new ToolStripStatusLabel();
            MemuraiLabel = new ToolStripStatusLabel();
            statusTimer = new System.Windows.Forms.Timer(components);
            ViewPanel = new Panel();
            WelcomeLabel = new Label();
            ButtonPanel = new Panel();
            PlayersButton = new Button();
            ServerStatus.SuspendLayout();
            ViewPanel.SuspendLayout();
            ButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // ServerStatus
            // 
            ServerStatus.Items.AddRange(new ToolStripItem[] { ChattingServerLabel, FrontServerLabel, GameServerLabel, GatewayServerLabel, WorldServerLabel, MySQLLabel, CouchbaseLabel, MemuraiLabel });
            ServerStatus.LayoutStyle = ToolStripLayoutStyle.Flow;
            ServerStatus.Location = new Point(0, 430);
            ServerStatus.Name = "ServerStatus";
            ServerStatus.Size = new Size(800, 20);
            ServerStatus.TabIndex = 0;
            // 
            // ChattingServerLabel
            // 
            ChattingServerLabel.Name = "ChattingServerLabel";
            ChattingServerLabel.Size = new Size(88, 15);
            ChattingServerLabel.Text = "Chatting Server";
            // 
            // FrontServerLabel
            // 
            FrontServerLabel.Name = "FrontServerLabel";
            FrontServerLabel.Size = new Size(70, 15);
            FrontServerLabel.Text = "Front Server";
            // 
            // GameServerLabel
            // 
            GameServerLabel.Name = "GameServerLabel";
            GameServerLabel.Size = new Size(73, 15);
            GameServerLabel.Text = "Game Server";
            // 
            // GatewayServerLabel
            // 
            GatewayServerLabel.Name = "GatewayServerLabel";
            GatewayServerLabel.Size = new Size(87, 15);
            GatewayServerLabel.Text = "Gateway Server";
            // 
            // WorldServerLabel
            // 
            WorldServerLabel.Name = "WorldServerLabel";
            WorldServerLabel.Size = new Size(74, 15);
            WorldServerLabel.Text = "World Server";
            // 
            // MySQLLabel
            // 
            MySQLLabel.Name = "MySQLLabel";
            MySQLLabel.Size = new Size(45, 15);
            MySQLLabel.Text = "MySQL";
            // 
            // CouchbaseLabel
            // 
            CouchbaseLabel.Name = "CouchbaseLabel";
            CouchbaseLabel.Size = new Size(66, 15);
            CouchbaseLabel.Text = "Couchbase";
            // 
            // MemuraiLabel
            // 
            MemuraiLabel.Name = "MemuraiLabel";
            MemuraiLabel.Size = new Size(55, 15);
            MemuraiLabel.Text = "Memurai";
            // 
            // statusTimer
            // 
            statusTimer.Interval = 2000;
            // 
            // ViewPanel
            // 
            ViewPanel.Controls.Add(WelcomeLabel);
            ViewPanel.Dock = DockStyle.Right;
            ViewPanel.Location = new Point(157, 0);
            ViewPanel.Name = "ViewPanel";
            ViewPanel.Size = new Size(643, 430);
            ViewPanel.TabIndex = 1;
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.AutoSize = true;
            WelcomeLabel.Dock = DockStyle.Fill;
            WelcomeLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            WelcomeLabel.Location = new Point(0, 0);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(625, 407);
            WelcomeLabel.TabIndex = 0;
            WelcomeLabel.Text = resources.GetString("WelcomeLabel.Text");
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ButtonPanel
            // 
            ButtonPanel.Controls.Add(PlayersButton);
            ButtonPanel.Dock = DockStyle.Left;
            ButtonPanel.Location = new Point(0, 0);
            ButtonPanel.Name = "ButtonPanel";
            ButtonPanel.Size = new Size(151, 430);
            ButtonPanel.TabIndex = 2;
            // 
            // PlayersButton
            // 
            PlayersButton.FlatStyle = FlatStyle.Flat;
            PlayersButton.Location = new Point(0, 0);
            PlayersButton.Name = "PlayersButton";
            PlayersButton.Size = new Size(151, 35);
            PlayersButton.TabIndex = 0;
            PlayersButton.Text = "Players";
            PlayersButton.UseVisualStyleBackColor = true;
            PlayersButton.Click += PlayersButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtonPanel);
            Controls.Add(ViewPanel);
            Controls.Add(ServerStatus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Topaz Mir 4 Server Manager";
            ServerStatus.ResumeLayout(false);
            ServerStatus.PerformLayout();
            ViewPanel.ResumeLayout(false);
            ViewPanel.PerformLayout();
            ButtonPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip ServerStatus;
        private ToolStripStatusLabel ChattingServerLabel;
        private ToolStripStatusLabel FrontServerLabel;
        private ToolStripStatusLabel GameServerLabel;
        private ToolStripStatusLabel GatewayServerLabel;
        private ToolStripStatusLabel WorldServerLabel;
        private ToolStripStatusLabel MySQLLabel;
        private ToolStripStatusLabel CouchbaseLabel;
        private ToolStripStatusLabel MemuraiLabel;
        private System.Windows.Forms.Timer statusTimer;
        private Panel ViewPanel;
        private Panel ButtonPanel;
        private Button PlayersButton;
        private Label WelcomeLabel;
    }
}
