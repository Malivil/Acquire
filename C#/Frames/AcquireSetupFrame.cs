﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Acquire.Models;
using Acquire.Panels;

namespace Acquire.Frames
{
    public partial class AcquireSetupFrame : Form
    {
        #region Public Variables

        /// <summary>
        /// The list of players generated by this setup frame
        /// </summary>
        public List<Player> Players { get; } = new List<Player>();

        /// <summary>
        /// Whether or not we are starting the game
        /// </summary>
        public bool IsStarting { get; private set; }

        #endregion

        #region Private Member Variables

        // What the host of this game's name is
        private string hostPlayerId;

        // Whether this game already has a host
        private bool hasHost => !string.IsNullOrWhiteSpace(hostPlayerId);

        #endregion

        /// <summary>
        /// Make a new frame and initialize all the components
        /// </summary>
        public AcquireSetupFrame()
        {
            InitializeComponent();
        }

        #region Event Handlers

        /// <summary>
        /// Gathers all the players from the PlayerSetupPanels, checks that they are valid, and starts the game
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void StartButton_Click(object sender, EventArgs args)
        {
            // How many AI players we have
            int aiPlayers = 0;
            // How many remote players we have
            int remotePlayers = 0;

            // Create a list of the PSPs to make it easier and quicker to run the same code on each
            IEnumerable<PlayerSetupPanel> setupPanels = Controls.OfType<PlayerSetupPanel>();
            // Clear the current list of players
            Players.Clear();
            // The list of current player names (to eliminate duplicates)
            List<string> playerNames = new List<string>();

            // For each panel...
            foreach (PlayerSetupPanel panel in setupPanels)
            {
                // If we are not joining, continue
                if (!panel.IsJoining())
                {
                    continue;
                }

                // Check if the player is ready
                if (!panel.IsReady())
                {
                    // Tell the user who isn't ready
                    if (panel.GetPlayerType() != Player.AI_PLAYER)
                    {
                        MessageBox.Show($@"{panel.GetName()} is not ready yet.", @"Player not ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(@"AI Player is not set to ready yet.", @"AI not set to ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                // TODO: Figure out how to handle this for remote players. Maybe have it as part of the handshake and force a remote player to append a number then?

                // Get the current name
                string name = panel.GetName();

                // Make sure there are no duplicates
                if (playerNames.Contains(name))
                {
                    // Find a number to add to the end to make it unique
                    for (int i = 2; i <= 9; i++)
                    {
                        if (!playerNames.Contains($"{name} ({i})"))
                        {
                            name += $" ({i})";
                            break;
                        }
                    }
                }

                // Add this name to the list of names used
                playerNames.Add(name);

                // Create the player of the correct type
                if (panel.GetPlayerType() == Player.LOCAL_PLAYER)
                {
                    Players.Add(new Player(name, Player.LOCAL_PLAYER, panel.PlayerId));
                }
                else if (panel.GetPlayerType() == Player.REMOTE_PLAYER)
                {
                    remotePlayers++;
                    Players.Add(new RemotePlayer(name, panel.PlayerId, panel.GetAddress()));
                }
                else
                {
                    aiPlayers++;
                    Players.Add(new AiPlayer(name, panel.PlayerId));
                }
            }

            // Only continue if we have at least one player
            if (Players.Count > 1)
            {
                // Make sure the user knows that there are only AI players
                if (aiPlayers == Players.Count)
                {
                    if (MessageBox.Show(@"There are no human players, is this ok?", @"No human players?", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                // Make sure we have a host if we have remote players
                if (remotePlayers > 0 && !hasHost)
                {
                    MessageBox.Show(@"There are remote players listed but no host selected. Please select a host", @"No host selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IsStarting = true;
                Close();
            }
            else
            {
                MessageBox.Show(@"There are not enough players to start the game.", @"Not enough players", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the form closing
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void AcquireSetupFrame_FormClosing(object sender, FormClosingEventArgs args)
        {
            // If windows is shutting down, close it.
            if (args.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            // If we are trying to start
            if (IsStarting)
            {
                // Make sure the user really wants it
                if (MessageBox.Show(@"Are you sure you want to start?", @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    args.Cancel = true;
                    IsStarting = false;
                }
            }
            // Otherwise only close it if the user wants to.
            else if (MessageBox.Show(@"Are you sure you want to quit?", @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                args.Cancel = true;
                IsStarting = false;
            }
        }

        /// <summary>
        /// Handles event where a player is either claiming or releasing hosting rights for this game
        /// </summary>
        ///
        /// <param name="sender">The panel sending the event</param>
        /// <param name="isHost">Whether the sending panel is trying to claim host of this game</param>
        ///
        /// <returns>True if the panel successfully claimed or released hosting rights, false otherwise</returns>
        private bool PlayerSetupPanel_PlayerHostStatusChanged(PlayerSetupPanel sender, bool isHost)
        {
            if (hasHost && isHost)
            {
                return false;
            }

            if (isHost)
            {
                hostPlayerId = sender.PlayerId;
            }
            else if (hostPlayerId != sender.PlayerId)
            {
                hostPlayerId = null;
            }

            return true;
        }

        #endregion
    }
}
