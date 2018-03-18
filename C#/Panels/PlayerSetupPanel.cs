using System;
using System.ComponentModel;
using System.Windows.Forms;
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
            TypeBox.SelectedIndex = Player.LOCAL_PLAYER;
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

            if (GetPlayerType() == Player.AI_PLAYER)
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

                if (GetPlayerType() == Player.REMOTE_PLAYER)
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
            NameBox.Visible = true;
            AINameBox.Enabled = false;
            AINameBox.Visible = false;
            IPBox.Enabled = JoinBox.Checked;
            IPBox.Visible = false;
            IPLabel.Visible = false;
            IsHostBox.Enabled = JoinBox.Checked;
            IsHostBox.Visible = true;

            switch (GetPlayerType())
            {
                case Player.LOCAL_PLAYER:
                    NameBox.Enabled = JoinBox.Checked;
                    break;
                case Player.REMOTE_PLAYER:
                    NameBox.Enabled = false;
                    IPBox.Enabled = JoinBox.Checked;
                    IPBox.Visible = true;
                    IPLabel.Visible = true;
                    NameBox.Text = REMOTE_PLAYER;
                    break;
                case Player.AI_PLAYER:
                    AINameBox.Enabled = JoinBox.Checked;
                    AINameBox.Visible = true;
                    NameBox.Visible = false;
                    NameBox.Enabled = false;
                    IsHostBox.Enabled = false;
                    IsHostBox.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Checks the validity of entered information and disables additional input while ready
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void ReadyButton_Click(object sender, EventArgs args)
        {
            if (GetPlayerType() == Player.LOCAL_PLAYER && string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show(@"This player doesn't have a name yet.", @"Please enter a name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TypeBox.Enabled = IsReady();
            NameLabel.Enabled = IsReady();
            IPLabel.Enabled = IsReady();
            TypeLabel.Enabled = IsReady();

            // If this is an AI
            if (GetPlayerType() == Player.AI_PLAYER)
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
                if (GetPlayerType() == Player.LOCAL_PLAYER)
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
            if (GetPlayerType() == Player.AI_PLAYER)
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
            return GetPlayerType() == Player.LOCAL_PLAYER ? "Player" : REMOTE_PLAYER;
        }

        /// <summary>
        /// Returns the type of the player represented by this panel
        /// </summary>
        /// 
        /// <returns>The type of the player represented by this panel</returns>
        public int GetPlayerType() => TypeBox.SelectedIndex;

        /// <summary>
        /// Returns the IP address for the player represented by this panel.
        /// </summary>
        /// 
        /// <returns>The IP address for the player represented by this panel.</returns>
        public string GetAddress() => IPBox.Text;

        /// <summary>
        /// Gets a new <see cref="Player"/>-derived object for the player represented by this panel
        /// </summary>
        ///
        /// <param name="nameOverride">The name to override this player's name with. Used when creating unique names during setup</param>
        ///
        /// <returns>A new <see cref="Player"/>-derived object for the player represented by this panel</returns>
        public Player GetPlayer(string nameOverride = null)
        {
            if (GetPlayerType() == Player.LOCAL_PLAYER)
            {
                return new Player(nameOverride ?? GetName(), Player.LOCAL_PLAYER, PlayerId, IsHost());
            }

            if (GetPlayerType() == Player.AI_PLAYER)
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
