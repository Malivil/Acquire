using System;
using System.Windows.Forms;
using Acquire.Models;
using Acquire.Models.Interfaces;

namespace Acquire.Forms
{
    public partial class MergeDialog : Form
    {
        #region Private Member Variables

        // The company that is taking over
        private readonly Company liveCompany;

        // The company that is being destroyed
        private readonly Company deadCompany;

        // How many shares there are to deal with
        private readonly int shares;

        // The player that is dealing with shares
        private readonly IPlayer player;

        // Whether or not either of the three drop boxes is being changed on purpose
        private bool isTradeChanged = true;
        private bool isKeepChanged = true;
        private bool isSellChanged = true;

        #endregion

        /// <summary>
        /// Creates a new dialog to manage the distribution of shares
        /// </summary>
        /// 
        /// <param name="liveCompany">The company taking over</param>
        /// <param name="deadCompany">The copany being eaten</param>
        /// <param name="player">The player that is dealing with shares</param>
        public MergeDialog(Company liveCompany, Company deadCompany, IPlayer player)
        {
            InitializeComponent();

            // Store the live company
            this.liveCompany = liveCompany;
            // Store the dead company
            this.deadCompany = deadCompany;
            // Store how many shares the player has
            shares = player.GetShares(deadCompany);
            // Store the player
            this.player = player;

            // Update the label
            SharesLabel.Text = $@"{player.Name}, you have {shares} shares of {deadCompany.Name}.";

            // Add all the trade possibilities
            for (int i = 0; i <= shares; i += 2)
            {
                TradeBox.Items.Add(i);
            }

            // Add all the keep and sell possibilities
            for (int i = 0; i <= shares; i++)
            {
                KeepBox.Items.Add(i);
                SellBox.Items.Add(i);
            }

            // Select the first one by default for all
            TradeBox.SelectedItem = 0;
            KeepBox.SelectedItem = 0;
            SellBox.SelectedItem = 0;
        }

        #region Event Handlers

        /// <summary>
        /// Handles the adjusting of the dropdown boxes based on which ones are already selected
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void TradeBox_SelectedIndexChanged(object sender, EventArgs args)
        {
            // If this was a deliberate change, ignore it
            if (isTradeChanged)
            {
                isTradeChanged = false;
                return;
            }

            // Store the current selections
            int keepSelected = KeepBox.SelectedIndex;
            int sellSelected = SellBox.SelectedIndex;
            // Clear out the lists
            KeepBox.Items.Clear();
            SellBox.Items.Clear();

            // Add all viable options
            for (int i = 0; i <= shares - TradeBox.SelectedIndex * 2 - sellSelected; i++)
            {
                KeepBox.Items.Add(i);
            }
            for (int i = 0; i <= shares - TradeBox.SelectedIndex * 2 - keepSelected; i++)
            {
                SellBox.Items.Add(i);
            }

            // Reset the selected indeces but set the forced booleans first so we don't infinite loop events
            isKeepChanged = true;
            KeepBox.SelectedIndex = keepSelected;
            isSellChanged = true;
            SellBox.SelectedIndex = sellSelected;
        }

        /// <summary>
        /// Handles the adjusting of the dropdown boxes based on which ones are already selected
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void KeepBox_SelectedIndexChanged(object sender, EventArgs args)
        {
            // If this was a deliberate change, ignore it
            if (isKeepChanged)
            {
                isKeepChanged = false;
                return;
            }

            // Store the current selections
            int tradeSelected = TradeBox.SelectedIndex;
            int sellSelected = SellBox.SelectedIndex;
            // Clear out the lists
            TradeBox.Items.Clear();
            SellBox.Items.Clear();

            // Add all viable options
            for (int i = 0; i <= shares - KeepBox.SelectedIndex - sellSelected; i += 2)
            {
                TradeBox.Items.Add(i);
            }
            for (int i = 0; i <= shares - KeepBox.SelectedIndex - tradeSelected * 2; i++)
            {
                SellBox.Items.Add(i);
            }

            // Reset the selected indeces but set the forced booleans first so we don't infinite loop events
            isTradeChanged = true;
            TradeBox.SelectedIndex = tradeSelected;
            isSellChanged = true;
            SellBox.SelectedIndex = sellSelected;
        }

        /// <summary>
        /// Handles the adjusting of the dropdown boxes based on which ones are already selected
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void SellBox_SelectedIndexChanged(object sender, EventArgs args)
        {
            // If this was a deliberate change, ignore it
            if (isSellChanged)
            {
                isSellChanged = false;
                return;
            }

            // Store the current selections
            int tradeSelected = TradeBox.SelectedIndex;
            int keepSelected = KeepBox.SelectedIndex;
            // Clear out the lists
            TradeBox.Items.Clear();
            KeepBox.Items.Clear();

            // Add all viable options
            for (int i = 0; i <= shares - SellBox.SelectedIndex - keepSelected; i += 2)
            {
                TradeBox.Items.Add(i);
            }
            for (int i = 0; i <= shares - SellBox.SelectedIndex - tradeSelected * 2; i++)
            {
                KeepBox.Items.Add(i);
            }

            // Reset the selected indeces but set the forced booleans first so we don't infinite loop events
            isTradeChanged = true;
            TradeBox.SelectedIndex = tradeSelected;
            isKeepChanged = true;
            KeepBox.SelectedIndex = keepSelected;
        }

        /// <summary>
        /// Handles the selling and trading when OK is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void OKButton_Click(object sender, EventArgs args)
        {
            // The total amount of shares being processed
            int totalShares = 0;

            // Only add the index if something was selected (-1 was showing up sometimes o.O)
            if (KeepBox.SelectedIndex > 0)
            {
                totalShares += KeepBox.SelectedIndex;
            }
            if (SellBox.SelectedIndex > 0)
            {
                totalShares += SellBox.SelectedIndex;
            }
            if (TradeBox.SelectedIndex > 0)
            {
                totalShares += TradeBox.SelectedIndex * 2;
            }

            bool shouldContinue = true;
            int remainingShares = shares - totalShares;
            // If this isn't all of the shares, make sure that the user knows.
            if (remainingShares > 0)
            {
                shouldContinue = MessageBox.Show($@"You still have {remainingShares} shares left.\nWould you like to just keep them?", @"Keep remaining shares?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            // If the user said yes (or it doesn't matter)
            if (shouldContinue)
            {
                // Handle the selling and trading
                MergeShares(SellBox.SelectedIndex, TradeBox.SelectedIndex * 2);
                // Then close the window
                Close();
            }
        }

        /// <summary>
        /// Handles when the user clicks the Cancel button and they haven't used up their shares
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void CancelButton_Click(object sender, EventArgs args)
        {
            if (MessageBox.Show(@"You still have shares left.\nWould you like to just keep them?", @"Keep remaining shares?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Handles the selling and trading of shares
        /// </summary>
        /// 
        /// <param name="soldShares">The number of shares sold</param>
        /// <param name="tradedShares">The number of shares to trade</param>
        private void MergeShares(int soldShares, int tradedShares)
        {
            // Give the holders of the dead company their bonus before we break it down
            deadCompany.GiveHolderBonus(deadCompany.Size);

            // Sell the shares
            if (soldShares > 0)
            {
                player.SellShares(deadCompany, soldShares, true);
            }
            // Trade the shares
            if (tradedShares > 0)
            {
                player.TradeShares(deadCompany, liveCompany, tradedShares);
            }
        }

        #endregion
    }
}
