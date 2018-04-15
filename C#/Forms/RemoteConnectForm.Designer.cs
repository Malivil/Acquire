namespace Acquire.Forms
{
    partial class RemoteConnectForm
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
            this.RemoteStatusBox = new System.Windows.Forms.ListBox();
            this.CancelConnectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RemoteStatusBox
            // 
            this.RemoteStatusBox.FormattingEnabled = true;
            this.RemoteStatusBox.Location = new System.Drawing.Point(13, 13);
            this.RemoteStatusBox.Name = "RemoteStatusBox";
            this.RemoteStatusBox.Size = new System.Drawing.Size(395, 173);
            this.RemoteStatusBox.TabIndex = 0;
            // 
            // CancelConnectButton
            // 
            this.CancelConnectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelConnectButton.Location = new System.Drawing.Point(13, 193);
            this.CancelConnectButton.Name = "CancelConnectButton";
            this.CancelConnectButton.Size = new System.Drawing.Size(75, 23);
            this.CancelConnectButton.TabIndex = 1;
            this.CancelConnectButton.Text = "Cancel";
            this.CancelConnectButton.UseVisualStyleBackColor = true;
            this.CancelConnectButton.Click += new System.EventHandler(this.CancelConnectButton_Click);
            // 
            // RemoteConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 221);
            this.Controls.Add(this.CancelConnectButton);
            this.Controls.Add(this.RemoteStatusBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 260);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 260);
            this.Name = "RemoteConnectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Initializing Remote Connection...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox RemoteStatusBox;
        private System.Windows.Forms.Button CancelConnectButton;
    }
}