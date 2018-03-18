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

            // If this is an AI
            if (TypeBox.SelectedIndex == Player.AI_PLAYER)
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
            }

            if (!JoinBox.Checked)
            {
                ReadyButton.Text = READY;
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
            IPBox.Enabled = false;
            IPBox.Visible = false;
            IPLabel.Visible = false;
            IsHost.Enabled = true;
            IsHost.Visible = true;

            switch (TypeBox.SelectedIndex)
            {
                case Player.LOCAL_PLAYER:
                    NameBox.Enabled = true;
                    break;
                case Player.REMOTE_PLAYER:
                    NameBox.Enabled = false;
                    IPBox.Enabled = true;
                    IPBox.Visible = true;
                    IPLabel.Visible = true;
                    NameBox.Text = REMOTE_PLAYER;
                    break;
                case Player.AI_PLAYER:
                    AINameBox.Enabled = true;
                    AINameBox.Visible = true;
                    NameBox.Visible = false;
                    NameBox.Enabled = false;
                    IsHost.Enabled = false;
                    IsHost.Visible = false;
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
            if (TypeBox.SelectedIndex == Player.LOCAL_PLAYER && string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show(@"This player doesn't have a name yet.", @"Please enter a name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TypeBox.Enabled = !ReadyButton.Text.Equals(READY);
            NameLabel.Enabled = !ReadyButton.Text.Equals(READY);
            IPLabel.Enabled = !ReadyButton.Text.Equals(READY);
            TypeLabel.Enabled = !ReadyButton.Text.Equals(READY);

            // If this is an AI
            if (TypeBox.SelectedIndex == Player.AI_PLAYER)
            {
                AINameBox.Enabled = !ReadyButton.Text.Equals(READY);
                AINameBox.Visible = true;
                NameBox.Enabled = false;
                IPBox.Enabled = false;
            }
            else
            {
                AINameBox.Enabled = false;
                AINameBox.Visible = false;

                // If this is a local user
                if (TypeBox.SelectedIndex == Player.LOCAL_PLAYER)
                {
                    NameBox.Enabled = !ReadyButton.Text.Equals(READY);
                    IPBox.Enabled = false;
                }
                // If this is a remote user
                else
                {
                    NameBox.Enabled = false;
                    IPBox.Enabled = !ReadyButton.Text.Equals(READY);
                }
            }

            ReadyButton.Text = ReadyButton.Text.Equals(READY) ? UNREADY : READY;
        }

        /// <summary>
        /// Lets all listeners know when this player's host status has changed. Used for validation.
        /// </summary>
        /// 
        /// <param name="sender">The object sending the event</param>
        /// <param name="args">The arguments sent</param>
        private void chkIsHost_CheckedChanged(object sender, EventArgs args)
        {
            if (!(PlayerHostStatusChanged?.Invoke(this, IsHost.Checked) ?? true))
            {
                MessageBox.Show(@"There is already a host for this game.", @"Game already has host", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IsHost.Checked = false;
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
            if (TypeBox.SelectedIndex == Player.AI_PLAYER)
            {
                // Either get the chosen name or a random one if one wasn't chosen
                int aiIndex = AINameBox.SelectedIndex > 0 ? AINameBox.SelectedIndex : new Random().Next(1, AINameBox.Items.Count - 1);
                return AINameBox.Items[aiIndex].ToString();
            }

            // Local or Remote Player
            if (!string.IsNullOrWhiteSpace(NameBox.Text))
            {
                return NameBox.Text;
            }
            return TypeBox.SelectedIndex == Player.LOCAL_PLAYER ? "Player" : REMOTE_PLAYER;
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

        #endregion
    }
}
