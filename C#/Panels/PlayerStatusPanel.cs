using System;
using System.Windows.Forms;
using Acquire.Models;

namespace Acquire.Panels
{
    public partial class PlayerStatusPanel : UserControl
    {
        #region Private Member Variables

        // The player associated with this panel
        private readonly Player player;

        #endregion

        /// <summary>
        /// Creates a new PlayerStatusPanel for the given <paramref name="player"/>
        /// </summary>
        /// 
        /// <param name="player">The player to associate with this panel</param>
        public PlayerStatusPanel(Player player)
        {
            InitializeComponent();

            // Store this player
            this.player = player;
            // Update the name label
            NameLabel.Text = player.Name;
            // Update the money label
            MoneyLabel.Text = @"$" + player.Money;
        }

        #region Event Handlers

        /// <summary>
        /// Sets up the tooltip for thsi panel
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void PlayerStatusPanel_Load(object sender, EventArgs args)
        {
            // Construct the tooltip string
            string toolTipText = string.Empty;
            foreach (Company company in Game.Companies)
            {
                // Put each company, the amount of stock the player has
                toolTipText += $"{company.GetName()}: {player.GetShares(company.GetName())} ";
                // And whether or not they are a holder of some type
                if (player.IsMajorityHolder(company))
                {
                    toolTipText += "(+)";
                }
                if (player.IsMinorityHolder(company))
                {
                    toolTipText += "(-)";
                }
                toolTipText += "\n";
            }

            // Set the tooltip title to this player's name
            ToolTip toolTip = new ToolTip
            {
                ToolTipTitle = $"{player.Name}'s Stocks"
            };

            // Make sure each component has the tooltip
            toolTip.SetToolTip(this, toolTipText);
            toolTip.SetToolTip(MoneyLabel, toolTipText);
            toolTip.SetToolTip(NameLabel, toolTipText);
        }

        #endregion
    }
}
