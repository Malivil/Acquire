namespace Acquire.Forms
{
    partial class MergeDialog
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
            this.WhatLabel = new System.Windows.Forms.Label();
            this.SharesLabel = new System.Windows.Forms.Label();
            this.TradeLabel = new System.Windows.Forms.Label();
            this.KeepLabel = new System.Windows.Forms.Label();
            this.SellLlabel = new System.Windows.Forms.Label();
            this.TradeBox = new System.Windows.Forms.ComboBox();
            this.KeepBox = new System.Windows.Forms.ComboBox();
            this.SellBox = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WhatLabel
            // 
            this.WhatLabel.AutoSize = true;
            this.WhatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhatLabel.Location = new System.Drawing.Point(39, 12);
            this.WhatLabel.Name = "WhatLabel";
            this.WhatLabel.Size = new System.Drawing.Size(430, 20);
            this.WhatLabel.TabIndex = 0;
            this.WhatLabel.Text = "What would you like to do with your shares of this company?";
            // 
            // SharesLabel
            // 
            this.SharesLabel.AutoSize = true;
            this.SharesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SharesLabel.Location = new System.Drawing.Point(136, 36);
            this.SharesLabel.Name = "SharesLabel";
            this.SharesLabel.Size = new System.Drawing.Size(237, 15);
            this.SharesLabel.TabIndex = 1;
            this.SharesLabel.Text = "NAME, you have # shares in of COMPANY.";
            // 
            // TradeLabel
            // 
            this.TradeLabel.AutoSize = true;
            this.TradeLabel.Location = new System.Drawing.Point(101, 77);
            this.TradeLabel.Name = "TradeLabel";
            this.TradeLabel.Size = new System.Drawing.Size(224, 13);
            this.TradeLabel.TabIndex = 2;
            this.TradeLabel.Text = "How many shares would you like to trade 2:1?";
            // 
            // KeepLabel
            // 
            this.KeepLabel.AutoSize = true;
            this.KeepLabel.Location = new System.Drawing.Point(101, 108);
            this.KeepLabel.Name = "KeepLabel";
            this.KeepLabel.Size = new System.Drawing.Size(206, 13);
            this.KeepLabel.TabIndex = 3;
            this.KeepLabel.Text = "How many shares would you like to keep?";
            // 
            // SellLlabel
            // 
            this.SellLlabel.AutoSize = true;
            this.SellLlabel.Location = new System.Drawing.Point(101, 138);
            this.SellLlabel.Name = "SellLlabel";
            this.SellLlabel.Size = new System.Drawing.Size(197, 13);
            this.SellLlabel.TabIndex = 4;
            this.SellLlabel.Text = "How many shares would you like to sell?";
            // 
            // TradeBox
            // 
            this.TradeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TradeBox.FormattingEnabled = true;
            this.TradeBox.Location = new System.Drawing.Point(354, 77);
            this.TradeBox.Name = "TradeBox";
            this.TradeBox.Size = new System.Drawing.Size(53, 21);
            this.TradeBox.TabIndex = 5;
            this.TradeBox.SelectedIndexChanged += new System.EventHandler(this.TradeBox_SelectedIndexChanged);
            // 
            // KeepBox
            // 
            this.KeepBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeepBox.FormattingEnabled = true;
            this.KeepBox.Location = new System.Drawing.Point(354, 108);
            this.KeepBox.Name = "KeepBox";
            this.KeepBox.Size = new System.Drawing.Size(53, 21);
            this.KeepBox.TabIndex = 6;
            this.KeepBox.SelectedIndexChanged += new System.EventHandler(this.KeepBox_SelectedIndexChanged);
            // 
            // SellBox
            // 
            this.SellBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SellBox.FormattingEnabled = true;
            this.SellBox.Location = new System.Drawing.Point(354, 138);
            this.SellBox.Name = "SellBox";
            this.SellBox.Size = new System.Drawing.Size(53, 21);
            this.SellBox.TabIndex = 7;
            this.SellBox.SelectedIndexChanged += new System.EventHandler(this.SellBox_SelectedIndexChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(176, 177);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(258, 176);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 9;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // MergeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 209);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.SellBox);
            this.Controls.Add(this.KeepBox);
            this.Controls.Add(this.TradeBox);
            this.Controls.Add(this.SellLlabel);
            this.Controls.Add(this.KeepLabel);
            this.Controls.Add(this.TradeLabel);
            this.Controls.Add(this.SharesLabel);
            this.Controls.Add(this.WhatLabel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(524, 247);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(524, 247);
            this.Name = "MergeDialog";
            this.Text = "What about the shares?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WhatLabel;
        private System.Windows.Forms.Label SharesLabel;
        private System.Windows.Forms.Label TradeLabel;
        private System.Windows.Forms.Label KeepLabel;
        private System.Windows.Forms.Label SellLlabel;
        private System.Windows.Forms.ComboBox TradeBox;
        private System.Windows.Forms.ComboBox KeepBox;
        private System.Windows.Forms.ComboBox SellBox;
        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}