using System;
using System.Windows.Forms;
using Acquire.Models;
using Acquire.Models.Interfaces;

namespace Acquire.Forms
{
    public partial class BuyShareForm : Form
    {
        #region Private Member Variables

        // The player using this form to buy a share
        private readonly IPlayer player;

        // The company from which the player is buying a share
        private readonly Company company;

        #endregion

        /// <summary>
        /// Creates a new BuyShareForm so that player can buy shares of <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="player">The player buying a share</param>
        /// <param name="company">The company that shares are being bought of</param>
        public BuyShareForm(IPlayer player, Company company)
        {
            InitializeComponent();

            // Store the player
            this.player = player;
            // Store the company
            this.company = company;

            // Update the label with the company's name
            HowManyLabel.Text = $@"How many shares of {company.GetName()} would you like to buy?";
            Text = $@"Buying shares of {company.GetName()}";

            // Add 0 as a default
            SharesComboBox.Items.Add(0);
            // Make the 0 default
            SharesComboBox.SelectedIndex = 0;

            // Add the amount of shares a player can buy based on how many more shares they can buy this turn
            for (int i = 1; i <= player.NumBuysLeft; i++)
            {
                // Make sure the player has enough money to buy these shares... and the company has them to sell
                if (player.Money >= company.GetPrice(company.Size + i) * i && company.Shares >= i)
                {
                    SharesComboBox.Items.Add(i);
                }
            }
        }

        #region Event Handlers

        /// <summary>
        /// Closes the window when "Cancel" is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending this event</param>
        /// <param name="args">The arguments for this event</param>
        private void CancelButton_Click(object sender, EventArgs args)
        {
            Close();
        }

        /// <summary>
        /// Performs the buy and then closes the window
        /// </summary>
        /// 
        /// <param name="sender">The object sending this event</param>
        /// <param name="args">The arguments for this event</param>
        private void BuyButton_Click(object sender, EventArgs args)
        {
            // Get the amount the player entered in the box
            int amount = int.Parse(SharesComboBox.Text);
            // Make sure that they are only buying the amount they are allowed to
            if (amount > player.NumBuysLeft)
            {
                // Ask the player if it is ok they are only buying the smaller amount
                DialogResult result = MessageBox.Show($@"You are only allowed to buy {player.NumBuysLeft} more shares. Buy this many?", @"Not enough buys left", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // If not yes, close the window and do nothing
                if (result != DialogResult.Yes)
                {
                    Close();
                    return;
                }

                amount = player.NumBuysLeft;
            }

            // Buy each share
            for (int i = 1; i <= amount; i++)
            {
                player.BuyShare(company, false);
                player.NumBuysLeft = player.NumBuysLeft - 1;
            }

            // Close the window
            Close();
        }

        #endregion
    }
}
