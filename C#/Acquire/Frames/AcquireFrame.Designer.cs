using Acquire.Components;
using Acquire.Panels;

namespace Acquire.Frames
{
    partial class AcquireFrame
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveGameLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.QuitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogBox = new System.Windows.Forms.ListBox();
            this.SendBox = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.EndTurnButton = new System.Windows.Forms.Button();
            this.EndGameButton = new System.Windows.Forms.Button();
            this.LogoBox = new System.Windows.Forms.PictureBox();
            this.PlayerList = new PlayerListPanel();
            this.CardHandPanel = new HandPanel();
            this.SquareGridPanel = new GridPanel();
            this.ImperialStatusButton = new CompanyStatusButton();
            this.ContinentalStatusButton = new CompanyStatusButton();
            this.WorldwideStatusButton = new CompanyStatusButton();
            this.FestivalStatusButton = new CompanyStatusButton();
            this.AmericanStatusButton = new CompanyStatusButton();
            this.LuxorStatusButton = new CompanyStatusButton();
            this.TowerStatusButton = new CompanyStatusButton();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.HelpMenu});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(622, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewGameMenuItem,
            this.SaveGameLogMenuItem,
            this.toolStripSeparator1,
            this.QuitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // NewGameMenuItem
            // 
            this.NewGameMenuItem.Name = "NewGameMenuItem";
            this.NewGameMenuItem.Size = new System.Drawing.Size(164, 22);
            this.NewGameMenuItem.Text = "New Game";
            this.NewGameMenuItem.Click += new System.EventHandler(this.NewGameMenuItem_Click);
            // 
            // SaveGameLogMenuItem
            // 
            this.SaveGameLogMenuItem.Name = "SaveGameLogMenuItem";
            this.SaveGameLogMenuItem.Size = new System.Drawing.Size(164, 22);
            this.SaveGameLogMenuItem.Text = "Save Game Log...";
            this.SaveGameLogMenuItem.Click += new System.EventHandler(this.SaveGameLogMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // QuitMenuItem
            // 
            this.QuitMenuItem.Name = "QuitMenuItem";
            this.QuitMenuItem.Size = new System.Drawing.Size(164, 22);
            this.QuitMenuItem.Text = "Quit";
            this.QuitMenuItem.Click += new System.EventHandler(this.QuitMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpToolItem,
            this.AboutMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(44, 20);
            this.HelpMenu.Text = "Help";
            // 
            // HelpToolItem
            // 
            this.HelpToolItem.Name = "HelpToolItem";
            this.HelpToolItem.Size = new System.Drawing.Size(107, 22);
            this.HelpToolItem.Text = "Help";
            this.HelpToolItem.Click += new System.EventHandler(this.HelpToolItem_Click);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.ShortcutKeyDisplayString = "";
            this.AboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.AboutMenuItem.Text = "About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // LogBox
            // 
            this.LogBox.FormattingEnabled = true;
            this.LogBox.Location = new System.Drawing.Point(2, 708);
            this.LogBox.Name = "LogBox";
            this.LogBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.LogBox.Size = new System.Drawing.Size(617, 95);
            this.LogBox.TabIndex = 8;
            this.LogBox.TabStop = false;
            // 
            // SendBox
            // 
            this.SendBox.Location = new System.Drawing.Point(2, 806);
            this.SendBox.MaxLength = 256;
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(562, 20);
            this.SendBox.TabIndex = 11;
            this.SendBox.TabStop = false;
            this.SendBox.WordWrap = false;
            this.SendBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SendBox_KeyPress);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(564, 805);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(56, 23);
            this.SendButton.TabIndex = 12;
            this.SendButton.TabStop = false;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // EndTurnButton
            // 
            this.EndTurnButton.Enabled = false;
            this.EndTurnButton.Location = new System.Drawing.Point(460, 617);
            this.EndTurnButton.Name = "EndTurnButton";
            this.EndTurnButton.Size = new System.Drawing.Size(155, 23);
            this.EndTurnButton.TabIndex = 13;
            this.EndTurnButton.Text = "End Turn";
            this.EndTurnButton.UseVisualStyleBackColor = true;
            this.EndTurnButton.Click += new System.EventHandler(this.EndTurnButton_Click);
            // 
            // EndGameButton
            // 
            this.EndGameButton.Enabled = false;
            this.EndGameButton.Location = new System.Drawing.Point(460, 641);
            this.EndGameButton.Name = "EndGameButton";
            this.EndGameButton.Size = new System.Drawing.Size(155, 23);
            this.EndGameButton.TabIndex = 14;
            this.EndGameButton.Text = "End Game";
            this.EndGameButton.UseVisualStyleBackColor = true;
            this.EndGameButton.Click += new System.EventHandler(this.EndGameButton_Click);
            // 
            // LogoBox
            // 
            this.LogoBox.Location = new System.Drawing.Point(461, 507);
            this.LogoBox.Name = "LogoBox";
            this.LogoBox.Size = new System.Drawing.Size(154, 104);
            this.LogoBox.TabIndex = 15;
            this.LogoBox.TabStop = false;
            // 
            // PlayerList
            // 
            this.PlayerList.BackColor = System.Drawing.SystemColors.Control;
            this.PlayerList.Location = new System.Drawing.Point(502, 32);
            this.PlayerList.MaximumSize = new System.Drawing.Size(115, 466);
            this.PlayerList.MinimumSize = new System.Drawing.Size(115, 466);
            this.PlayerList.Name = "PlayerList";
            this.PlayerList.Size = new System.Drawing.Size(115, 466);
            this.PlayerList.TabIndex = 16;
            // 
            // CardHandPanel
            // 
            this.CardHandPanel.BackColor = System.Drawing.SystemColors.Control;
            this.CardHandPanel.Location = new System.Drawing.Point(2, 666);
            this.CardHandPanel.MaximumSize = new System.Drawing.Size(617, 39);
            this.CardHandPanel.MinimumSize = new System.Drawing.Size(617, 39);
            this.CardHandPanel.Name = "CardHandPanel";
            this.CardHandPanel.Size = new System.Drawing.Size(617, 39);
            this.CardHandPanel.TabIndex = 10;
            this.CardHandPanel.TabStop = false;
            // 
            // SquareGridPanel
            // 
            this.SquareGridPanel.BackColor = System.Drawing.Color.White;
            this.SquareGridPanel.Location = new System.Drawing.Point(119, 30);
            this.SquareGridPanel.MaximumSize = new System.Drawing.Size(380, 470);
            this.SquareGridPanel.MinimumSize = new System.Drawing.Size(380, 470);
            this.SquareGridPanel.Name = "SquareGridPanel";
            this.SquareGridPanel.Size = new System.Drawing.Size(380, 470);
            this.SquareGridPanel.TabIndex = 9;
            this.SquareGridPanel.TabStop = false;
            // 
            // ImperialStatusButton
            // 
            this.ImperialStatusButton.BackColor = System.Drawing.Color.White;
            this.ImperialStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImperialStatusButton.Location = new System.Drawing.Point(344, 507);
            this.ImperialStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.ImperialStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.ImperialStatusButton.Name = "ImperialStatusButton";
            this.ImperialStatusButton.Size = new System.Drawing.Size(108, 154);
            this.ImperialStatusButton.TabIndex = 7;
            this.ImperialStatusButton.TabStop = false;
            this.ImperialStatusButton.Click += new System.EventHandler(this.ImperialStatusButton_Click);
            // 
            // ContinentalStatusButton
            // 
            this.ContinentalStatusButton.BackColor = System.Drawing.Color.White;
            this.ContinentalStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContinentalStatusButton.Location = new System.Drawing.Point(230, 507);
            this.ContinentalStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.ContinentalStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.ContinentalStatusButton.Name = "ContinentalStatusButton";
            this.ContinentalStatusButton.Size = new System.Drawing.Size(108, 154);
            this.ContinentalStatusButton.TabIndex = 6;
            this.ContinentalStatusButton.TabStop = false;
            this.ContinentalStatusButton.Click += new System.EventHandler(this.ContinentalStatusButton_Click);
            // 
            // WorldwideStatusButton
            // 
            this.WorldwideStatusButton.BackColor = System.Drawing.Color.White;
            this.WorldwideStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WorldwideStatusButton.Location = new System.Drawing.Point(116, 507);
            this.WorldwideStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.WorldwideStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.WorldwideStatusButton.Name = "WorldwideStatusButton";
            this.WorldwideStatusButton.Size = new System.Drawing.Size(108, 154);
            this.WorldwideStatusButton.TabIndex = 5;
            this.WorldwideStatusButton.TabStop = false;
            this.WorldwideStatusButton.Click += new System.EventHandler(this.WorldwideStatusButton_Click);
            // 
            // FestivalStatusButton
            // 
            this.FestivalStatusButton.BackColor = System.Drawing.Color.White;
            this.FestivalStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FestivalStatusButton.Location = new System.Drawing.Point(2, 507);
            this.FestivalStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.FestivalStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.FestivalStatusButton.Name = "FestivalStatusButton";
            this.FestivalStatusButton.Size = new System.Drawing.Size(108, 154);
            this.FestivalStatusButton.TabIndex = 4;
            this.FestivalStatusButton.TabStop = false;
            this.FestivalStatusButton.Click += new System.EventHandler(this.FestivalStatusButton_Click);
            // 
            // AmericanStatusButton
            // 
            this.AmericanStatusButton.BackColor = System.Drawing.Color.White;
            this.AmericanStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AmericanStatusButton.Location = new System.Drawing.Point(2, 347);
            this.AmericanStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.AmericanStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.AmericanStatusButton.Name = "AmericanStatusButton";
            this.AmericanStatusButton.Size = new System.Drawing.Size(108, 154);
            this.AmericanStatusButton.TabIndex = 3;
            this.AmericanStatusButton.TabStop = false;
            this.AmericanStatusButton.Click += new System.EventHandler(this.AmericanStatusButton_Click);
            // 
            // LuxorStatusButton
            // 
            this.LuxorStatusButton.BackColor = System.Drawing.Color.White;
            this.LuxorStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LuxorStatusButton.Location = new System.Drawing.Point(2, 187);
            this.LuxorStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.LuxorStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.LuxorStatusButton.Name = "LuxorStatusButton";
            this.LuxorStatusButton.Size = new System.Drawing.Size(108, 154);
            this.LuxorStatusButton.TabIndex = 2;
            this.LuxorStatusButton.TabStop = false;
            this.LuxorStatusButton.Click += new System.EventHandler(this.LuxorStatusButton_Click);
            // 
            // TowerStatusButton
            // 
            this.TowerStatusButton.BackColor = System.Drawing.Color.White;
            this.TowerStatusButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TowerStatusButton.Location = new System.Drawing.Point(2, 27);
            this.TowerStatusButton.MaximumSize = new System.Drawing.Size(108, 154);
            this.TowerStatusButton.MinimumSize = new System.Drawing.Size(108, 154);
            this.TowerStatusButton.Name = "TowerStatusButton";
            this.TowerStatusButton.Size = new System.Drawing.Size(108, 154);
            this.TowerStatusButton.TabIndex = 1;
            this.TowerStatusButton.TabStop = false;
            this.TowerStatusButton.Click += new System.EventHandler(this.TowerStatusButton_Click);
            // 
            // AcquireFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 829);
            this.Controls.Add(this.PlayerList);
            this.Controls.Add(this.LogoBox);
            this.Controls.Add(this.EndGameButton);
            this.Controls.Add(this.EndTurnButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.SendBox);
            this.Controls.Add(this.CardHandPanel);
            this.Controls.Add(this.SquareGridPanel);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.ImperialStatusButton);
            this.Controls.Add(this.ContinentalStatusButton);
            this.Controls.Add(this.WorldwideStatusButton);
            this.Controls.Add(this.FestivalStatusButton);
            this.Controls.Add(this.AmericanStatusButton);
            this.Controls.Add(this.LuxorStatusButton);
            this.Controls.Add(this.TowerStatusButton);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(638, 867);
            this.MinimumSize = new System.Drawing.Size(638, 867);
            this.Name = "AcquireFrame";
            this.Text = "Acquire in C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AcquireFrame_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AcquireFrame_Paint);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem NewGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveGameLogMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem QuitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpToolItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private CompanyStatusButton TowerStatusButton;
        private CompanyStatusButton LuxorStatusButton;
        private CompanyStatusButton AmericanStatusButton;
        private CompanyStatusButton FestivalStatusButton;
        private CompanyStatusButton WorldwideStatusButton;
        private CompanyStatusButton ContinentalStatusButton;
        private CompanyStatusButton ImperialStatusButton;
        private System.Windows.Forms.ListBox LogBox;
        private GridPanel SquareGridPanel;
        private HandPanel CardHandPanel;
        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button EndTurnButton;
        private System.Windows.Forms.Button EndGameButton;
        private System.Windows.Forms.PictureBox LogoBox;
        private PlayerListPanel PlayerList;
    }
}

