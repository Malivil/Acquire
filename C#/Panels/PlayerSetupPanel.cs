using System;
using System.Windows.Forms;

namespace Acquire.Panels
{
    public partial class PlayerSetupPanel : UserControl
    {
        #region Constants

        private const string CONNECT = "Connect";
        private const string CONNECTED = "Connected";
        private const string RETRY = "Retry";
        private const string READY = "Ready";
        private const string UNREADY = "Un-Ready";

        #endregion

        /// <summary>
        /// Creates a new setup panel and defaults the dropboxes to their first entry
        /// </summary>
        public PlayerSetupPanel()
        {
            InitializeComponent();

            AINameBox.SelectedIndex = 0;
            TypeBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Connects to the remote player and returns the connection status.
        /// </summary>
        /// 
        /// <returns>The connection status.</returns>
        private string ConnectToRemote()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                ReadyButton.Enabled = false;
                return CONNECTED;
            }

            MessageBox.Show(@"Connection Failed: There is no network connection available.", @"No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return RETRY;
        }

        /// <summary>
        ///  Disconnects from remote player represented by this panel.
        /// </summary>
        private void DisconnectFromRemote() { }

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

            if (TypeBox.SelectedIndex != 2)
            {
                NameBox.Enabled = JoinBox.Checked;
                AINameBox.Enabled = false;
                AINameBox.Visible = false;
            }
            else
            {
                NameBox.Enabled = false;
                AINameBox.Enabled = JoinBox.Checked;
                AINameBox.Visible = true;
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
            if (ReadyButton.Text.Equals(CONNECTED))
            {
                DisconnectFromRemote();
            }

            ReadyButton.Enabled = JoinBox.Checked;
            ReadyButton.Text = READY;
            NameBox.Text = string.Empty;

            switch (TypeBox.SelectedIndex)
            {
                case 0: // Local Player
                    AINameBox.Enabled = false;
                    AINameBox.Visible = false;
                    NameBox.Visible = true;
                    NameBox.Enabled = true;
                    IPBox.Enabled = false;
                    break;
                case 1: // Remote Player
                    AINameBox.Enabled = false;
                    AINameBox.Visible = false;
                    NameBox.Visible = true;
                    NameBox.Enabled = false;
                    IPBox.Enabled = true;
                    NameBox.Text = GetRemoteName();
                    ReadyButton.Text = CONNECT;
                    break;
                case 2: // AI Player
                    AINameBox.Enabled = true;
                    AINameBox.Visible = true;
                    NameBox.Visible = false;
                    NameBox.Enabled = false;
                    IPBox.Enabled = false;
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
            if (!ReadyButton.Enabled)
            {
                return;
            }

            if (TypeBox.SelectedIndex == 0 && NameBox.Text.Length == 0)
            {
                MessageBox.Show(@"This player doesn't have a name yet.", @"Please enter a name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TypeBox.Enabled = !ReadyButton.Text.Equals(READY);
            NameLabel.Enabled = !ReadyButton.Text.Equals(READY);
            IPLabel.Enabled = !ReadyButton.Text.Equals(READY);
            TypeLabel.Enabled = !ReadyButton.Text.Equals(READY);

            if (TypeBox.SelectedIndex != 2)
            {
                if (TypeBox.SelectedIndex == 0)
                {
                    NameBox.Enabled = !ReadyButton.Text.Equals(READY);
                    IPBox.Enabled = false;
                }
                else
                {
                    NameBox.Enabled = false;
                    IPBox.Enabled = !ReadyButton.Text.Equals(READY);
                }

                AINameBox.Enabled = false;
                AINameBox.Visible = false;
            }
            else
            {
                NameBox.Enabled = false;
                IPBox.Enabled = false;
                AINameBox.Enabled = !ReadyButton.Text.Equals(READY);
                AINameBox.Visible = true;
            }

            if (TypeBox.SelectedIndex == 1)
            {
                ReadyButton.Text = ConnectToRemote();
                return;
            }

            ReadyButton.Text = ReadyButton.Text.Equals(READY) ? UNREADY : READY;
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
            // Local or Remote Player
            if (TypeBox.SelectedIndex != 2)
            {
                if (NameBox.Text.Length > 0)
                {
                    return NameBox.Text;
                }
                return TypeBox.SelectedIndex == 1 ? "Player" : "Remote Player";
            }

            // AI Player
            if (AINameBox.SelectedIndex > 0)
            {
                return AINameBox.Items[AINameBox.SelectedIndex].ToString();
            }
            return AINameBox.Items[new Random().Next(1, AINameBox.Items.Count - 1)].ToString();
        }

        /// <summary>
        /// Returns the type of the player represented by this panel
        /// </summary>
        /// 
        /// <returns>The type of the player represented by this panel</returns>
        public int GetPlayerType() => TypeBox.SelectedIndex + 1;

        /// <summary>
        /// Returns the IP address for the player represented by this panel.
        /// </summary>
        /// 
        /// <returns>The IP address for the player represented by this panel.</returns>
        public string GetAddress() => IPBox.Text;

        /// <summary>
        /// Fetches and returns the name of the remote player represented by this panel.
        /// </summary>
        /// 
        /// <returns>The name of the remote player represented by this panel.</returns>
        public string GetRemoteName() => "Remote";

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
