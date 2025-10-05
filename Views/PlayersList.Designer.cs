using System.Drawing;
using System.Windows.Forms;

namespace ServerManager.Views
{
    partial class PlayersList : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView PlayersListDataGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            PlayersListDataGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)PlayersListDataGrid).BeginInit();
            SuspendLayout();
            // 
            // PlayersListDataGrid
            // 
            PlayersListDataGrid.Dock = DockStyle.Fill;
            PlayersListDataGrid.Location = new Point(0, 0);
            PlayersListDataGrid.Name = "PlayersListDataGrid";
            PlayersListDataGrid.Size = new Size(700, 450);
            PlayersListDataGrid.TabIndex = 0;
            // 
            // PlayersList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PlayersListDataGrid);
            Name = "PlayersList";
            Size = new Size(700, 450);
            ((System.ComponentModel.ISupportInitialize)PlayersListDataGrid).EndInit();
            ResumeLayout(false);
        }
        #endregion
    }
}
