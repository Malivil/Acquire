using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Acquire.Forms;
using Acquire.Models;
using Acquire.Models.Interfaces;
using Acquire.Panels;

namespace Acquire.Frames
{
    public partial class AcquireFrame : Form
    {
        #region Private Member Varibles

        // Whether or not we are restarting the game
        private bool restarting;

        #endregion

        /// <summary>
        /// Creates a new Acquire game using the list of players provided
        /// </summary>
        /// 
        /// <param name="players">Thye list of players for this game</param>
        public AcquireFrame(List<IPlayer> players)
        {
            InitializeComponent();

            // Set up the LogMaster to use this text box
            LogMaster.SetListBox(LogBox);

            // Initialize the game using these players, these companies, and this frame
            Game.Initialize(players, this);
            // Set up the list of players
            PlayerList.SetPlayersList(players);

            // Initialize the company status buttons
            // Luxor
            LuxorStatusButton.Company = Game.Companies.Single(c => c.Name == "Luxor");
            LuxorStatusButton.ImageIcon = LuxorStatusButton.Company.ImageIcon;
            // Tower
            TowerStatusButton.Company = Game.Companies.Single(c => c.Name == "Tower");
            TowerStatusButton.ImageIcon = TowerStatusButton.Company.ImageIcon;
            // Festival
            FestivalStatusButton.Company = Game.Companies.Single(c => c.Name == "Festival");
            FestivalStatusButton.ImageIcon = FestivalStatusButton.Company.ImageIcon;
            // Worldwide
            WorldwideStatusButton.Company = Game.Companies.Single(c => c.Name == "Worldwide");
            WorldwideStatusButton.ImageIcon = WorldwideStatusButton.Company.ImageIcon;
            // American
            AmericanStatusButton.Company = Game.Companies.Single(c => c.Name == "American");
            AmericanStatusButton.ImageIcon = AmericanStatusButton.Company.ImageIcon;
            // Continental
            ContinentalStatusButton.Company = Game.Companies.Single(c => c.Name == "Continental");
            ContinentalStatusButton.ImageIcon = ContinentalStatusButton.Company.ImageIcon;
            // Imperial
            ImperialStatusButton.Company = Game.Companies.Single(c => c.Name == "Imperial");
            ImperialStatusButton.ImageIcon = ImperialStatusButton.Company.ImageIcon;

            // Load up the logo image
            LogoBox.Image = Image.FromFile(@"Images/Acquire_Logo.png", true);
        }

        #region Event Handlers

        /// <summary>
        /// Sets the log box to the last line when the frame is painted.
        /// (Only really useful when AI players play eachother as this isn't painted untl the game is already over)
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void AcquireFrame_Paint(object sender, PaintEventArgs args)
        {
            LogBox.TopIndex = LogBox.Items.Count - LogBox.Height / LogBox.ItemHeight;
        }

        /// <summary>
        /// Handles the closing of this window
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void AcquireFrame_FormClosing(object sender, FormClosingEventArgs args)
        {
            // If windows is shutting down, close it.
            if (args.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            // If we are restarting, just go
            if (restarting)
            {
                restarting = false;
                return;
            }

            // Otherwise only close it if the user wants to.
            if (MessageBox.Show(@"Are you sure you want to quit?", @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                args.Cancel = true;
            }
        }

        /// <summary>
        /// Handles when the "New Game" button is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void NewGameMenuItem_Click(object sender, EventArgs args)
        {
            if (MessageBox.Show(@"Are you sure you want to start a new game?", @"Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                restarting = true;
                Application.Restart();
            }
        }

        /// <summary>
        /// Handles saving the game log as a text file
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void SaveGameLogMenuItem_Click(object sender, EventArgs args)
        {
            Utilities.SaveLogFile(LogBox);
        }

        /// <summary>
        /// Show the help frame when the button is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void HelpToolItem_Click(object sender, EventArgs args)
        {
            new HelpFrame().ShowDialog();
        }

        /// <summary>
        /// Show the about frame when the button is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void AboutMenuItem_Click(object sender, EventArgs args)
        {
            new AboutFrame().ShowDialog();
        }

        /// <summary>
        /// Quit the game when the button is clicked
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void QuitMenuItem_Click(object sender, EventArgs args)
        {
            Close();
        }

        /// <summary>
        /// Handle ending the current player's turn
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void EndTurnButton_Click(object sender, EventArgs args)
        {
            // If they have stocks to buy, make sure they know that
            if (Game.CurrentPlayer.NumBuysLeft > 0 && Game.HasActiveCompanies())
            {
                if (MessageBox.Show(@"You can still buy shares, do you really want to end your turn?", @"End turn?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Game.NextTurn();
                }
            }
            else
            {
                Game.NextTurn();
            }
        }

        /// <summary>
        /// Handles ending the game
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void EndGameButton_Click(object sender, EventArgs args)
        {
            Game.EndGame();
        }

        /// <summary>
        /// Handles sending the current text in the text box to the game log
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void SendButton_Click(object sender, EventArgs args)
        {
            // Only send something if there is something to send
            if (SendBox.Text.Length > 0)
            {
                LogMaster.Log($"{Game.CurrentPlayer.Name}: {SendBox.Text}");
                SendBox.Clear();
            }
        }

        /// <summary>
        /// Handles sending the current text in the text box to the game log
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void SendBox_KeyPress(object sender, KeyPressEventArgs args)
        {
            // Only send if there is something to send and the user pressed enter
            if (args.KeyChar == (char)Keys.Enter && SendBox.Text.Length > 0)
            {
                LogMaster.Log($"{Game.CurrentPlayer.Name}: {SendBox.Text}");
                SendBox.Clear();
            }
        }

        /// <summary>
        /// Handles buying shares of Tower
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void TowerStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, TowerStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of Luxor
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void LuxorStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, LuxorStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of American
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void AmericanStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, AmericanStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of Festival
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void FestivalStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, FestivalStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of Worldwide
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void WorldwideStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, WorldwideStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of Continental
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void ContinentalStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, ContinentalStatusButton.Company);
        }

        /// <summary>
        /// Handles buying shares of Imperial
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void ImperialStatusButton_Click(object sender, EventArgs args)
        {
            BuyShare(Game.CurrentPlayer, ImperialStatusButton.Company);
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Opens the BuyShare form for <paramref name="player"/> to buy shares of <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="player">The player buying shares</param>
        /// <param name="company">The company to buy shares from</param>
        private void BuyShare(IPlayer player, Company company)
        {
            // Can't buy shares if the game is over
            if (!Game.IsGameOver)
            {
                // Can't buy shares until you've placed a square
                if (!player.CanPlaceSquare())
                {
                    // Can't buy shares in a company that doesn't exist
                    if (company.IsPlaced)
                    {
                        // Can't buy more than 3 shares per turn
                        if (player.NumBuysLeft > 0)
                        {
                            // Buy the shares
                            new BuyShareForm(player, company).ShowDialog(this);
                            // Update the player's cash
                            UpdatePlayerList();
                            // Redraw
                            Refresh();
                        }
                        else
                        {
                            LogMaster.Log("You have already bought your 3 stock for this turn.");
                        }
                    }
                    else
                    {
                        LogMaster.Log("That company is not active.");
                    }
                }
                else
                {
                    LogMaster.Log("Place a square first!");
                }
            }
            else
            {
                LogMaster.Log("The game is already over. You cannot buy more stock.");
            }
        }

        /// <summary>
        /// Updates the list of players
        /// </summary>
        public void UpdatePlayerList()
        {
            PlayerList.SetPlayersList(Game.Players);
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the Hand Panel in this frame.
        /// </summary>
        /// 
        /// <returns>The Hand Panel in this frame.</returns>
        public HandPanel GetHandPanel() => CardHandPanel;

        /// <summary>
        /// Returns the Grid Panel in this frame.
        /// </summary>
        /// 
        /// <returns>The Grid Panel in this frame.</returns>
        public GridPanel GetGridPanel() => SquareGridPanel;

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets the End Game button's enabled state to the given <paramref name="enable"/>
        /// </summary>
        /// 
        /// <param name="enable">Whether or not to enable to End Game button</param>
        public void SetEndGameButtonEnabled(bool enable)
        {
            EndGameButton.Enabled = enable;
        }

        /// <summary>
        /// Sets the End Turn button's enabled state to the given <paramref name="enable"/>
        /// </summary>
        /// 
        /// <param name="enable">Whether or not to enable to End Turn button</param>
        public void SetEndTurnButtonEnabled(bool enable)
        {
            EndTurnButton.Enabled = enable;
            if (enable)
            {
                EndTurnButton.Focus();
            }
        }

        #endregion

        #region Boolean Methods

        /// <summary>
        /// Returns whether or not the player can end the turn. Used for AI players
        /// </summary>
        /// 
        /// <returns>Whether or not the player can end the turn.</returns>
        public bool CanEndTurn() => EndTurnButton.Enabled;

        /// <summary>
        /// Returns whether or not the player can end the game. Used for AI players
        /// </summary>
        /// 
        /// <returns>Whether or not the player can end the game.</returns>
        public bool CanEndGame() => EndGameButton.Enabled;

        #endregion
    }
}
