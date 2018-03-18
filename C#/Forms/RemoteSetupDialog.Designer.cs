namespace Acquire.Forms
{
    partial class RemoteSetupDialog
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
            this.StatusBox = new System.Windows.Forms.ListBox();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.SavLogButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StatusBox
            // 
            this.StatusBox.FormattingEnabled = true;
            this.StatusBox.Location = new System.Drawing.Point(13, 13);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(639, 147);
            this.StatusBox.TabIndex = 0;
            // 
            // ContinueButton
            // 
            this.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ContinueButton.Enabled = false;
            this.ContinueButton.Location = new System.Drawing.Point(13, 167);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 1;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // SavLogButton
            // 
            this.SavLogButton.Location = new System.Drawing.Point(578, 167);
            this.SavLogButton.Name = "SavLogButton";
            this.SavLogButton.Size = new System.Drawing.Size(75, 23);
            this.SavLogButton.TabIndex = 2;
            this.SavLogButton.Text = "Save Log...";
            this.SavLogButton.UseVisualStyleBackColor = true;
            this.SavLogButton.Click += new System.EventHandler(this.SavLogButton_Click);
            // 
            // RemoteSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 197);
            this.Controls.Add(this.SavLogButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.StatusBox);
            this.Name = "RemoteSetupDialog";
            this.Text = "RemoteSetupDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox StatusBox;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button SavLogButton;
    }
}