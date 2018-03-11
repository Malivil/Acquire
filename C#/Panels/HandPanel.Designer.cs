namespace Acquire.Panels
{
    partial class HandPanel
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
            this.SquaresLeftLabel = new System.Windows.Forms.Label();
            this.SquarePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // SquaresLeftLabel
            // 
            this.SquaresLeftLabel.AutoSize = true;
            this.SquaresLeftLabel.Location = new System.Drawing.Point(437, 14);
            this.SquaresLeftLabel.Name = "SquaresLeftLabel";
            this.SquaresLeftLabel.Size = new System.Drawing.Size(154, 13);
            this.SquaresLeftLabel.TabIndex = 0;
            this.SquaresLeftLabel.Text = "Number of squares left in bag:  ";
            // 
            // SquarePanel
            // 
            this.SquarePanel.Location = new System.Drawing.Point(0, 0);
            this.SquarePanel.Name = "SquarePanel";
            this.SquarePanel.Size = new System.Drawing.Size(431, 39);
            this.SquarePanel.TabIndex = 1;
            // 
            // HandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SquarePanel);
            this.Controls.Add(this.SquaresLeftLabel);
            this.MaximumSize = new System.Drawing.Size(617, 39);
            this.MinimumSize = new System.Drawing.Size(617, 39);
            this.Name = "HandPanel";
            this.Size = new System.Drawing.Size(617, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SquaresLeftLabel;
        private System.Windows.Forms.Panel SquarePanel;
    }
}
