using Acquire.Panels;

namespace Acquire.Frames
{
    partial class AcquireSetupFrame
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
            this.StartButton = new System.Windows.Forms.Button();
            this.PlayerSetupPanel8 = new PlayerSetupPanel();
            this.PlayerSetupPanel7 = new PlayerSetupPanel();
            this.PlayerSetupPanel6 = new PlayerSetupPanel();
            this.PlayerSetupPanel5 = new PlayerSetupPanel();
            this.PlayerSetupPanel4 = new PlayerSetupPanel();
            this.PlayerSetupPanel3 = new PlayerSetupPanel();
            this.PlayerSetupPanel2 = new PlayerSetupPanel();
            this.PlayerSetupPanel1 = new PlayerSetupPanel();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 426);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 8;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // PlayerSetupPanel8
            // 
            this.PlayerSetupPanel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel8.Location = new System.Drawing.Point(436, 318);
            this.PlayerSetupPanel8.Name = "PlayerSetupPanel8";
            this.PlayerSetupPanel8.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel8.TabIndex = 7;
            // 
            // PlayerSetupPanel7
            // 
            this.PlayerSetupPanel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel7.Location = new System.Drawing.Point(12, 318);
            this.PlayerSetupPanel7.Name = "PlayerSetupPanel7";
            this.PlayerSetupPanel7.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel7.TabIndex = 6;
            // 
            // PlayerSetupPanel6
            // 
            this.PlayerSetupPanel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel6.Location = new System.Drawing.Point(436, 216);
            this.PlayerSetupPanel6.Name = "PlayerSetupPanel6";
            this.PlayerSetupPanel6.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel6.TabIndex = 5;
            // 
            // PlayerSetupPanel5
            // 
            this.PlayerSetupPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel5.Location = new System.Drawing.Point(12, 216);
            this.PlayerSetupPanel5.Name = "PlayerSetupPanel5";
            this.PlayerSetupPanel5.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel5.TabIndex = 4;
            // 
            // PlayerSetupPanel4
            // 
            this.PlayerSetupPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel4.Location = new System.Drawing.Point(436, 114);
            this.PlayerSetupPanel4.Name = "PlayerSetupPanel4";
            this.PlayerSetupPanel4.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel4.TabIndex = 3;
            // 
            // PlayerSetupPanel3
            // 
            this.PlayerSetupPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel3.Location = new System.Drawing.Point(12, 114);
            this.PlayerSetupPanel3.Name = "PlayerSetupPanel3";
            this.PlayerSetupPanel3.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel3.TabIndex = 2;
            // 
            // PlayerSetupPanel2
            // 
            this.PlayerSetupPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel2.Location = new System.Drawing.Point(436, 12);
            this.PlayerSetupPanel2.Name = "PlayerSetupPanel2";
            this.PlayerSetupPanel2.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel2.TabIndex = 1;
            // 
            // PlayerSetupPanel1
            // 
            this.PlayerSetupPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerSetupPanel1.Location = new System.Drawing.Point(12, 12);
            this.PlayerSetupPanel1.Name = "PlayerSetupPanel1";
            this.PlayerSetupPanel1.Size = new System.Drawing.Size(418, 96);
            this.PlayerSetupPanel1.TabIndex = 0;
            // 
            // AcquireSetupFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 458);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PlayerSetupPanel8);
            this.Controls.Add(this.PlayerSetupPanel7);
            this.Controls.Add(this.PlayerSetupPanel6);
            this.Controls.Add(this.PlayerSetupPanel5);
            this.Controls.Add(this.PlayerSetupPanel4);
            this.Controls.Add(this.PlayerSetupPanel3);
            this.Controls.Add(this.PlayerSetupPanel2);
            this.Controls.Add(this.PlayerSetupPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(884, 496);
            this.MinimumSize = new System.Drawing.Size(884, 496);
            this.Name = "AcquireSetupFrame";
            this.Text = "New Game of Acquire in C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AcquireSetupFrame_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private PlayerSetupPanel PlayerSetupPanel1;
        private PlayerSetupPanel PlayerSetupPanel2;
        private PlayerSetupPanel PlayerSetupPanel3;
        private PlayerSetupPanel PlayerSetupPanel4;
        private PlayerSetupPanel PlayerSetupPanel5;
        private PlayerSetupPanel PlayerSetupPanel6;
        private PlayerSetupPanel PlayerSetupPanel7;
        private PlayerSetupPanel PlayerSetupPanel8;
        private System.Windows.Forms.Button StartButton;
    }
}