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
            components = new System.ComponentModel.Container();
            this.PlayersListDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersListDataGrid)).BeginInit();
            this.SuspendLayout();
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
