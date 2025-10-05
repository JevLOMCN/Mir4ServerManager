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
            ServerStatus.SuspendLayout();
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ServerStatus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Topaz Mir 4 Server Manager";
            ServerStatus.ResumeLayout(false);
            ServerStatus.PerformLayout();
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
    }
}
