namespace ServerManager.Views
{
    partial class PlayersItemsButtons
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
            ItemsButton = new Button();
            SkillsButton = new Button();
            SuspendLayout();
            // 
            // ItemsButton
            // 
            ItemsButton.FlatStyle = FlatStyle.Flat;
            ItemsButton.Location = new Point(0, 0);
            ItemsButton.Name = "ItemsButton";
            ItemsButton.Size = new Size(151, 35);
            ItemsButton.TabIndex = 0;
            ItemsButton.Text = "Items";
            ItemsButton.UseVisualStyleBackColor = true;
            // 
            // SkillsButton
            // 
            SkillsButton.FlatStyle = FlatStyle.Flat;
            SkillsButton.Location = new Point(0, 32);
            SkillsButton.Name = "SkillsButton";
            SkillsButton.Size = new Size(151, 35);
            SkillsButton.TabIndex = 1;
            SkillsButton.Text = "Skills";
            SkillsButton.UseVisualStyleBackColor = true;
            // 
            // PlayersButtons
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SkillsButton);
            Controls.Add(ItemsButton);
            Name = "PlayersButtons";
            Size = new Size(151, 430);
            ResumeLayout(false);
        }

        #endregion

        private Button ItemsButton;
        private Button SkillsButton;
    }
}
