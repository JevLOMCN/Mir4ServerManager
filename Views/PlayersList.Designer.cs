using System.Drawing;
using System.Windows.Forms;

namespace ServerManager.Forms
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
            components = new System.ComponentModel.Container();
            this.PlayersListDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersListDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayersListDataGrid
            // 
            this.PlayersListDataGrid.Name = "PlayersListDataGrid";
            this.PlayersListDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayersListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlayersListDataGrid.Location = new System.Drawing.Point(0, 0);
            this.PlayersListDataGrid.Size = new System.Drawing.Size(700, 450);
            this.PlayersListDataGrid.TabIndex = 0;
            // 
            // PlayersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PlayersListDataGrid);
            this.Name = "PlayersList";
            this.Size = new System.Drawing.Size(700, 450);
            ((System.ComponentModel.ISupportInitialize)(this.PlayersListDataGrid)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}
