using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using Acquire.Enums;
using Acquire.Models;

namespace Acquire.Panels
{
    public partial class PlayerSetupPanel : UserControl
    {
        #region Constants

        private const string READY = "Ready";
        private const string UNREADY = "Un-Ready";
        private const string REMOTE_PLAYER = "Remote Player";

        #endregion

        #region Public Member Variables

        /// <summary>
        /// This player's Unique ID
        /// </summary>
        public string PlayerId = Guid.NewGuid().ToString("N");

        #endregion

        #region Events

        public delegate bool OnPlayerHostStatusChanged(PlayerSetupPanel sender, bool isHost);
        [Browsable(true)]
        public event OnPlayerHostStatusChanged PlayerHostStatusChanged;

        #endregion

        /// <summary>
        /// Creates a new setup panel and defaults the dropboxes to their first entry
        /// </summary>
        public PlayerSetupPanel()
        {
            InitializeComponent();

            AINameBox.SelectedIndex = 0;
            TypeBox.SelectedIndex = (int)PlayerType.Local;
        }

        #region Event Handlers

        /// <summary>
        /// Enabkes and disables the needed components when the join box is checked.
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void JoinBox_CheckedChanged(object sender, EventArgs args)
        {
            TypeBox.Enabled = JoinBox.Checked;
            ReadyButton.Enabled = JoinBox.Checked;
            NameLabel.Enabled = JoinBox.Checked;
            IPLabel.Enabled = JoinBox.Checked;
            TypeLabel.Enabled = JoinBox.Checked;

            if (GetPlayerType() == PlayerType.AI)
            {
                NameBox.Enabled = false;
                AINameBox.Enabled = JoinBox.Checked;
                AINameBox.Visible = true;
            }
            else
            {
                NameBox.Enabled = JoinBox.Checked;
                AINameBox.Enabled = false;
                AINameBox.Visible = false;
                IsHostBox.Enabled = JoinBox.Checked;

                if (GetPlayerType() == PlayerType.Remote)
                {
                    IPBox.Enabled = JoinBox.Checked;
                }
            }

            if (!JoinBox.Checked)
            {
                ReadyButton.Text = READY;
                IsHostBox.Checked = false;
            }
        }

        /// <summary>
        /// Enables and disables the necessary componenets depending on what type of player is chosen.
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void TypeBox_SelectedIndexChanged(object sender, EventArgs args)
        {
            ReadyButton.Enabled = JoinBox.Checked;
            ReadyButton.Text = READY;
            NameBox.Text = string.Empty;
            NameBox.Enabled = false;
            NameBox.Visible = true;
            AINameBox.Enabled = AINameBox.Visible = false;
            IPBox.Visible = IPBox.Enabled = IPLabel.Visible = JoinBox.Checked && IsHostBox.Checked;
            IsHostBox.Enabled = JoinBox.Checked;
            IsHostBox.Visible = true;

            switch (GetPlayerType())
            {
                case PlayerType.Local:
                    NameBox.Enabled = JoinBox.Checked;
                    break;
                case PlayerType.Remote:
                    NameBox.Text = REMOTE_PLAYER;
                    break;
                case PlayerType.AI:
                    AINameBox.Enabled = JoinBox.Checked;
                    AINameBox.Visible = true;
                    NameBox.Visible = false;
                    IsHostBox.Enabled = IsHostBox.Visible = IsHostBox.Checked = false;
                    IPBox.Visible = IPBox.Enabled = IPLabel.Visible = false;
                    break;
            }

            // Toggle the check mark to refresh the state of the host player in the setup frame
            // This fixes switching between Local and Remote not updating the status of things like the "Begin Listening" button
            if (IsHostBox.Checked)
            {
                IsHostBox.Checked = false;
                IsHostBox.Checked = true;
            }

            // TODO: Only allow 1 remote player. Rename to "Remote Host" in dropdown. Force IsHost to be checked when selecting it
            // TODO: If the local player is the host, don't allow any remote players to be added this way
        }

        /// <summary>
        /// Checks the validity of entered information and disables additional input while ready
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void ReadyButton_Click(object sender, EventArgs args)
        {
            if (GetPlayerType() == PlayerType.Local && string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show(@"This player doesn't have a name yet.", @"Please enter a name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IsHost() && GetAddressEndPoint() == null)
            {
                MessageBox.Show(@"Host player has an invalid IP and/or port entered", @"Please enter a valid IP and port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TypeBox.Enabled = IsReady();
            NameLabel.Enabled = IsReady();
            IPLabel.Enabled = IsReady();
            TypeLabel.Enabled = IsReady();

            // If this is an AI
            if (GetPlayerType() == PlayerType.AI)
            {
                AINameBox.Enabled = IsReady();
                AINameBox.Visible = true;
                NameBox.Enabled = false;
                IPBox.Enabled = false;
            }
            else
            {
                AINameBox.Enabled = false;
                AINameBox.Visible = false;
                IsHostBox.Enabled = IsReady();

                // If this is a local user
                if (GetPlayerType() == PlayerType.Local)
                {
                    NameBox.Enabled = IsReady();
                    IPBox.Enabled = false;
                }
                // If this is a remote user
                else
                {
                    NameBox.Enabled = false;
                    IPBox.Enabled = IsReady();
                }
            }

            ReadyButton.Text = IsReady() ? READY : UNREADY;
        }

        /// <summary>
        /// Lets all listeners know when this player's host status has changed. Used for validation.
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void IsHostBox_CheckedChanged(object sender, EventArgs args)
        {
            if (!(PlayerHostStatusChanged?.Invoke(this, IsHostBox.Checked) ?? true))
            {
                MessageBox.Show(@"There is already a host for this game.", @"Game already has host", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsHostBox.Checked = false;
            }

            // We only need to enter the IP/Port of the host user
            IPBox.Visible = IPBox.Enabled = IPLabel.Visible = JoinBox.Checked && IsHostBox.Checked;
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the name of this player.
        /// 
        /// If it is an AI and it is set to random name, a random name is returned
        /// </summary>
        /// 
        /// <returns>The name of this player.</returns>
        public string GetName()
        {
            // AI Player
            if (GetPlayerType() == PlayerType.AI)
            {
                // Either get the chosen name or a random one
                int aiIndex = AINameBox.SelectedIndex > 0 ? AINameBox.SelectedIndex : new Random().Next(1, AINameBox.Items.Count - 1);
                return AINameBox.Items[aiIndex].ToString();
            }

            // Local or Remote Player
            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                return NameBox.Text;
            }
            return GetPlayerType() == PlayerType.Local ? "Player" : REMOTE_PLAYER;
        }

        /// <summary>
        /// Returns the type of the player represented by this panel
        /// </summary>
        /// 
        /// <returns>The type of the player represented by this panel</returns>
        public PlayerType GetPlayerType() => (PlayerType)TypeBox.SelectedIndex;

        /// <summary>
        /// Returns the IP address for the player represented by this panel.
        /// </summary>
        /// 
        /// <returns>The IP address for the player represented by this panel.</returns>
        public string GetAddress() => IPBox.Text;

        /// <summary>
        /// Returns the endpoint for the player represented by this panel.
        /// </summary>
        ///
        /// <returns>The endpoint for the player represented by this panel.</returns>
        public IPEndPoint GetAddressEndPoint() => Utilities.ParseIPEndPoint(GetAddress());

        /// <summary>
        /// Gets a new <see cref="Player"/>-derived object for the player represented by this panel
        /// </summary>
        ///
        /// <param name="nameOverride">The name to override this player's name with. Used when creating unique names during setup</param>
        ///
        /// <returns>A new <see cref="Player"/>-derived object for the player represented by this panel</returns>
        public Player GetPlayer(string nameOverride = null)
        {
            if (GetPlayerType() == PlayerType.Local)
            {
                return new Player(nameOverride ?? GetName(), PlayerType.Local, PlayerId, IsHost());
            }

            if (GetPlayerType() == PlayerType.AI)
            {
                return new AiPlayer(nameOverride ?? GetName(), PlayerId);
            }

            return new RemotePlayer(PlayerId, GetAddress(), IsHost());
        }

        #endregion

        #region Boolean Methods

        /// <summary>
        /// Returns whether or not the player represented by this panel is joining the game
        /// </summary>
        /// 
        /// <returns>Whether or not the player represented by this panel is joining the game</returns>
        public bool IsJoining() => JoinBox.Checked;

        /// <summary>
        /// Returns whether or not the player represented by this panel is ready
        /// </summary>
        /// 
        /// <returns>Whether or not the player represented by this panel is ready</returns>
        public bool IsReady() => ReadyButton.Text.Equals(UNREADY);

        /// <summary>
        /// Returns whether or not the player represented by this panel is the host
        /// </summary>
        ///
        /// <returns>Whether or not the player represented by this panel is the host</returns>
        public bool IsHost() => IsHostBox.Checked;

        #endregion
    }
}
