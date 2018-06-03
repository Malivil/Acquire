namespace Acquire.Panels
{
    partial class PlayerSetupPanel
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
            this.ReadyButton = new System.Windows.Forms.Button();
            this.JoinBox = new System.Windows.Forms.CheckBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.TypeBox = new System.Windows.Forms.ComboBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.AINameBox = new System.Windows.Forms.ComboBox();
            this.IsHostBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ReadyButton
            // 
            this.ReadyButton.Enabled = false;
            this.ReadyButton.Location = new System.Drawing.Point(10, 62);
            this.ReadyButton.Name = "ReadyButton";
            this.ReadyButton.Size = new System.Drawing.Size(75, 23);
            this.ReadyButton.TabIndex = 0;
            this.ReadyButton.Text = "Ready";
            this.ReadyButton.UseVisualStyleBackColor = true;
            this.ReadyButton.Click += new System.EventHandler(this.ReadyButton_Click);
            // 
            // JoinBox
            // 
            this.JoinBox.AutoSize = true;
            this.JoinBox.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinBox.Location = new System.Drawing.Point(10, 7);
            this.JoinBox.Name = "JoinBox";
            this.JoinBox.Size = new System.Drawing.Size(115, 22);
            this.JoinBox.TabIndex = 1;
            this.JoinBox.Text = "Join the Game";
            this.JoinBox.UseVisualStyleBackColor = true;
            this.JoinBox.CheckedChanged += new System.EventHandler(this.JoinBox_CheckedChanged);
            // 
            // NameBox
            // 
            this.NameBox.Enabled = false;
            this.NameBox.Location = new System.Drawing.Point(287, 7);
            this.NameBox.MaxLength = 15;
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(100, 20);
            this.NameBox.TabIndex = 2;
            this.NameBox.WordWrap = false;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Enabled = false;
            this.NameLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(158, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(47, 18);
            this.NameLabel.TabIndex = 3;
            this.NameLabel.Text = "Name:";
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Enabled = false;
            this.TypeLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeLabel.Location = new System.Drawing.Point(158, 38);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(42, 18);
            this.TypeLabel.TabIndex = 4;
            this.TypeLabel.Text = "Type:";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Enabled = false;
            this.IPLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel.Location = new System.Drawing.Point(158, 67);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(107, 18);
            this.IPLabel.TabIndex = 5;
            this.IPLabel.Text = "IP Address/Port:";
            this.IPLabel.Visible = false;
            // 
            // TypeBox
            // 
            this.TypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeBox.Enabled = false;
            this.TypeBox.FormattingEnabled = true;
            this.TypeBox.Items.AddRange(new object[] {
            "Local Player",
            "Remote Player",
            "AI Player"});
            this.TypeBox.Location = new System.Drawing.Point(287, 36);
            this.TypeBox.Name = "TypeBox";
            this.TypeBox.Size = new System.Drawing.Size(121, 21);
            this.TypeBox.TabIndex = 6;
            this.TypeBox.SelectedIndexChanged += new System.EventHandler(this.TypeBox_SelectedIndexChanged);
            // 
            // IPBox
            // 
            this.IPBox.AllowDrop = true;
            this.IPBox.Enabled = false;
            this.IPBox.Location = new System.Drawing.Point(287, 67);
            this.IPBox.MaxLength = 20;
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(100, 20);
            this.IPBox.TabIndex = 7;
            this.IPBox.Text = "127.0.0.1:80";
            this.IPBox.Visible = false;
            this.IPBox.WordWrap = false;
            // 
            // AINameBox
            // 
            this.AINameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AINameBox.Enabled = false;
            this.AINameBox.FormattingEnabled = true;
            this.AINameBox.Items.AddRange(new object[] {
            "Random",
            "Spartan 117",
            "Cortana",
            "Shepard",
            "Kerrigan",
            "Jim Raynor",
            "Star Fox",
            "Link",
            "Zelda",
            "Andariel",
            "Bowser",
            "Mario",
            "Luigi",
            "Kirby",
            "Gir",
            "Zim",
            "Obi-wan",
            "Darth Vader",
            "Han Solo",
            "Skywalker",
            "Leia",
            "Bastila Shan",
            "Scott Pilgrim",
            "Loki",
            "Thor",
            "Freeman",
            "Sheldon",
            "Duke Nukem",
            "Sonic"});
            this.AINameBox.Location = new System.Drawing.Point(287, 7);
            this.AINameBox.Name = "AINameBox";
            this.AINameBox.Size = new System.Drawing.Size(121, 21);
            this.AINameBox.TabIndex = 8;
            this.AINameBox.Visible = false;
            // 
            // IsHostBox
            // 
            this.IsHostBox.AutoSize = true;
            this.IsHostBox.Enabled = false;
            this.IsHostBox.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsHostBox.Location = new System.Drawing.Point(10, 34);
            this.IsHostBox.Name = "IsHostBox";
            this.IsHostBox.Size = new System.Drawing.Size(74, 22);
            this.IsHostBox.TabIndex = 9;
            this.IsHostBox.Text = "Is Host?";
            this.IsHostBox.UseVisualStyleBackColor = true;
            this.IsHostBox.CheckedChanged += new System.EventHandler(this.IsHostBox_CheckedChanged);
            // 
            // PlayerSetupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.IsHostBox);
            this.Controls.Add(this.AINameBox);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.TypeBox);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.JoinBox);
            this.Controls.Add(this.ReadyButton);
            this.MaximumSize = new System.Drawing.Size(416, 94);
            this.MinimumSize = new System.Drawing.Size(416, 94);
            this.Name = "PlayerSetupPanel";
            this.Size = new System.Drawing.Size(414, 92);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadyButton;
        private System.Windows.Forms.CheckBox JoinBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.ComboBox TypeBox;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.ComboBox AINameBox;
        private System.Windows.Forms.CheckBox IsHostBox;
    }
}
