namespace Acquire.Components
{
    partial class CompanyStatusButton
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.SharesLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.CompanyImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyImage)).BeginInit();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(0, 0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(79, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "CompanyName";
            this.NameLabel.Click += new System.EventHandler(this.CompanyStatusButton_Click);
            this.NameLabel.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.NameLabel.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(0, 13);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(33, 13);
            this.SizeLabel.TabIndex = 1;
            this.SizeLabel.Text = "Size: ";
            this.SizeLabel.Click += new System.EventHandler(this.CompanyStatusButton_Click);
            this.SizeLabel.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.SizeLabel.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            // 
            // SharesLabel
            // 
            this.SharesLabel.AutoSize = true;
            this.SharesLabel.Location = new System.Drawing.Point(0, 26);
            this.SharesLabel.Name = "SharesLabel";
            this.SharesLabel.Size = new System.Drawing.Size(64, 13);
            this.SharesLabel.TabIndex = 2;
            this.SharesLabel.Text = "Shares Left:";
            this.SharesLabel.Click += new System.EventHandler(this.CompanyStatusButton_Click);
            this.SharesLabel.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.SharesLabel.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(0, 39);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(37, 13);
            this.PriceLabel.TabIndex = 3;
            this.PriceLabel.Text = "Price: ";
            this.PriceLabel.Click += new System.EventHandler(this.CompanyStatusButton_Click);
            this.PriceLabel.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.PriceLabel.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            // 
            // CompanyImage
            // 
            this.CompanyImage.Location = new System.Drawing.Point(3, 53);
            this.CompanyImage.Name = "CompanyImage";
            this.CompanyImage.Size = new System.Drawing.Size(100, 100);
            this.CompanyImage.TabIndex = 4;
            this.CompanyImage.TabStop = false;
            this.CompanyImage.Click += new System.EventHandler(this.CompanyStatusButton_Click);
            this.CompanyImage.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.CompanyImage.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            // 
            // CompanyStatusButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CompanyImage);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.SharesLabel);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.NameLabel);
            this.MaximumSize = new System.Drawing.Size(108, 154);
            this.MinimumSize = new System.Drawing.Size(108, 154);
            this.Name = "CompanyStatusButton";
            this.Size = new System.Drawing.Size(106, 152);
            this.MouseEnter += new System.EventHandler(this.CompanyStatusButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CompanyStatusButton_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.CompanyImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label SharesLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.PictureBox CompanyImage;
    }
}
