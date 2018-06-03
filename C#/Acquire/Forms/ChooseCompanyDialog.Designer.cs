namespace Acquire.Forms
{
    partial class ChooseCompanyDialog
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CompanyBox = new System.Windows.Forms.ComboBox();
            this.FormLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(64, 64);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(146, 64);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CompanyBox
            // 
            this.CompanyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CompanyBox.FormattingEnabled = true;
            this.CompanyBox.Location = new System.Drawing.Point(81, 35);
            this.CompanyBox.Name = "CompanyBox";
            this.CompanyBox.Size = new System.Drawing.Size(121, 21);
            this.CompanyBox.TabIndex = 2;
            // 
            // FormLabel
            // 
            this.FormLabel.AutoSize = true;
            this.FormLabel.Location = new System.Drawing.Point(24, 13);
            this.FormLabel.Name = "FormLabel";
            this.FormLabel.Size = new System.Drawing.Size(236, 13);
            this.FormLabel.TabIndex = 3;
            this.FormLabel.Text = "Which Company would you like to form/expand?";
            // 
            // ChooseCompanyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 102);
            this.ControlBox = false;
            this.Controls.Add(this.FormLabel);
            this.Controls.Add(this.CompanyBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 140);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 140);
            this.Name = "ChooseCompanyDialog";
            this.Text = "Forming/Expanding a Company";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox CompanyBox;
        private System.Windows.Forms.Label FormLabel;
    }
}