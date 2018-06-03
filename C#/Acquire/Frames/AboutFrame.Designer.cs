namespace Acquire.Frames
{
    partial class AboutFrame
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
            this.LogoBox = new System.Windows.Forms.PictureBox();
            this.DesignedLabel = new System.Windows.Forms.Label();
            this.NamesLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel1 = new System.Windows.Forms.Label();
            this.CopyrightLabel2 = new System.Windows.Forms.Label();
            this.CopyrightLabel3 = new System.Windows.Forms.Label();
            this.AcquireLabel = new System.Windows.Forms.Label();
            this.AcquireLabel2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LogoBox
            // 
            this.LogoBox.Location = new System.Drawing.Point(149, 12);
            this.LogoBox.Name = "LogoBox";
            this.LogoBox.Size = new System.Drawing.Size(154, 104);
            this.LogoBox.TabIndex = 16;
            this.LogoBox.TabStop = false;
            // 
            // DesignedLabel
            // 
            this.DesignedLabel.AutoSize = true;
            this.DesignedLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesignedLabel.Location = new System.Drawing.Point(84, 119);
            this.DesignedLabel.Name = "DesignedLabel";
            this.DesignedLabel.Size = new System.Drawing.Size(303, 18);
            this.DesignedLabel.TabIndex = 17;
            this.DesignedLabel.Text = "\"Acquire in Java\" code designed and written by";
            // 
            // NamesLabel
            // 
            this.NamesLabel.AutoSize = true;
            this.NamesLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamesLabel.Location = new System.Drawing.Point(84, 137);
            this.NamesLabel.Name = "NamesLabel";
            this.NamesLabel.Size = new System.Drawing.Size(315, 18);
            this.NamesLabel.TabIndex = 18;
            this.NamesLabel.Text = "Colin Hansen, Michael Lebson, and Myles Alyward";
            // 
            // CopyrightLabel1
            // 
            this.CopyrightLabel1.AutoSize = true;
            this.CopyrightLabel1.Location = new System.Drawing.Point(65, 250);
            this.CopyrightLabel1.Name = "CopyrightLabel1";
            this.CopyrightLabel1.Size = new System.Drawing.Size(290, 13);
            this.CopyrightLabel1.TabIndex = 19;
            this.CopyrightLabel1.Text = "Acquire originally designed and created by 3M Games, 1962";
            // 
            // CopyrightLabel2
            // 
            this.CopyrightLabel2.AutoSize = true;
            this.CopyrightLabel2.Location = new System.Drawing.Point(65, 263);
            this.CopyrightLabel2.Name = "CopyrightLabel2";
            this.CopyrightLabel2.Size = new System.Drawing.Size(323, 13);
            this.CopyrightLabel2.TabIndex = 20;
            this.CopyrightLabel2.Text = "Java and the Java Logo are trademarks or registered trademarks of";
            // 
            // CopyrightLabel3
            // 
            this.CopyrightLabel3.AutoSize = true;
            this.CopyrightLabel3.Location = new System.Drawing.Point(65, 276);
            this.CopyrightLabel3.Name = "CopyrightLabel3";
            this.CopyrightLabel3.Size = new System.Drawing.Size(288, 13);
            this.CopyrightLabel3.TabIndex = 21;
            this.CopyrightLabel3.Text = "Oracle Corporation in the United States and other countries.";
            // 
            // AcquireLabel
            // 
            this.AcquireLabel.AutoSize = true;
            this.AcquireLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcquireLabel.Location = new System.Drawing.Point(84, 177);
            this.AcquireLabel.Name = "AcquireLabel";
            this.AcquireLabel.Size = new System.Drawing.Size(307, 18);
            this.AcquireLabel.TabIndex = 22;
            this.AcquireLabel.Text = "\"Acquire in C#\" written by Michael Lebson using";
            // 
            // AcquireLabel2
            // 
            this.AcquireLabel2.AutoSize = true;
            this.AcquireLabel2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcquireLabel2.Location = new System.Drawing.Point(84, 195);
            this.AcquireLabel2.Name = "AcquireLabel2";
            this.AcquireLabel2.Size = new System.Drawing.Size(215, 18);
            this.AcquireLabel2.TabIndex = 23;
            this.AcquireLabel2.Text = "code from the above contributors";
            // 
            // AboutFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 298);
            this.Controls.Add(this.AcquireLabel2);
            this.Controls.Add(this.AcquireLabel);
            this.Controls.Add(this.CopyrightLabel3);
            this.Controls.Add(this.CopyrightLabel2);
            this.Controls.Add(this.CopyrightLabel1);
            this.Controls.Add(this.NamesLabel);
            this.Controls.Add(this.DesignedLabel);
            this.Controls.Add(this.LogoBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(469, 336);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(469, 336);
            this.Name = "AboutFrame";
            this.Text = "About Acquire in C#";
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox LogoBox;
        private System.Windows.Forms.Label DesignedLabel;
        private System.Windows.Forms.Label NamesLabel;
        private System.Windows.Forms.Label CopyrightLabel1;
        private System.Windows.Forms.Label CopyrightLabel2;
        private System.Windows.Forms.Label CopyrightLabel3;
        private System.Windows.Forms.Label AcquireLabel;
        private System.Windows.Forms.Label AcquireLabel2;

    }
}