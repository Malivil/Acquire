namespace Acquire.Forms
{
    partial class BuyShareForm
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
            this.HowManyLabel = new System.Windows.Forms.Label();
            this.BuyButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SharesComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // HowManyLabel
            // 
            this.HowManyLabel.AutoSize = true;
            this.HowManyLabel.Location = new System.Drawing.Point(13, 13);
            this.HowManyLabel.Name = "HowManyLabel";
            this.HowManyLabel.Size = new System.Drawing.Size(298, 13);
            this.HowManyLabel.TabIndex = 0;
            this.HowManyLabel.Text = "How many shares of COMPANYNAME would you like to buy?";
            // 
            // BuyButton
            // 
            this.BuyButton.Location = new System.Drawing.Point(84, 85);
            this.BuyButton.Name = "BuyButton";
            this.BuyButton.Size = new System.Drawing.Size(75, 23);
            this.BuyButton.TabIndex = 1;
            this.BuyButton.Text = "Buy";
            this.BuyButton.UseVisualStyleBackColor = true;
            this.BuyButton.Click += new System.EventHandler(this.BuyButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(166, 85);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SharesComboBox
            // 
            this.SharesComboBox.FormattingEnabled = true;
            this.SharesComboBox.Location = new System.Drawing.Point(102, 46);
            this.SharesComboBox.Name = "SharesComboBox";
            this.SharesComboBox.Size = new System.Drawing.Size(121, 21);
            this.SharesComboBox.TabIndex = 3;
            // 
            // BuyShareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 120);
            this.ControlBox = false;
            this.Controls.Add(this.SharesComboBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.BuyButton);
            this.Controls.Add(this.HowManyLabel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 158);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 158);
            this.Name = "BuyShareForm";
            this.Text = "BuyShareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label HowManyLabel;
        private System.Windows.Forms.Button BuyButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox SharesComboBox;
    }
}