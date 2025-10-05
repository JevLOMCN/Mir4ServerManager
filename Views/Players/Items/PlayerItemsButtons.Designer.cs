namespace ServerManager.Views.Players.Items
{
    partial class PlayerItemsButtons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AddItemButton = new Button();
            DeleteItemButton = new Button();
            SuspendLayout();
            // 
            // AddItemButton
            // 
            AddItemButton.FlatStyle = FlatStyle.Flat;
            AddItemButton.Location = new Point(0, 32);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(151, 35);
            AddItemButton.TabIndex = 3;
            AddItemButton.Text = "Add Item";
            AddItemButton.UseVisualStyleBackColor = true;
            // 
            // DeleteItemButton
            // 
            DeleteItemButton.FlatStyle = FlatStyle.Flat;
            DeleteItemButton.Location = new Point(0, 0);
            DeleteItemButton.Name = "DeleteItemButton";
            DeleteItemButton.Size = new Size(151, 35);
            DeleteItemButton.TabIndex = 2;
            DeleteItemButton.Text = "Delete Item";
            DeleteItemButton.UseVisualStyleBackColor = true;
            // 
            // PlayerItemsButtons
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(AddItemButton);
            Controls.Add(DeleteItemButton);
            Name = "PlayerItemsButtons";
            Size = new Size(151, 430);
            ResumeLayout(false);
        }

        #endregion

        private Button AddItemButton;
        private Button DeleteItemButton;
    }
}
