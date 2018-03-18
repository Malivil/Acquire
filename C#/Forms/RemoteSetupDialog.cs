using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Acquire.Models;

namespace Acquire.Forms
{
    public partial class RemoteSetupDialog : Form
    {
        #region Private Variables

        // The error message which is preventing the game from continuing, if any
        private string errorMessage;

        #endregion

        public RemoteSetupDialog(IEnumerable<Player> players)
        {
            InitializeComponent();

            ConnectToPlayers(players);
        }

        #region Get Methods

        public string GetErrorMessage() => errorMessage;

        #endregion

        #region Event Handlers

        private void ContinueButton_Click(object sender, EventArgs args)
        {
            Close();
        }

        private void SavLogButton_Click(object sender, EventArgs e)
        {
            Utilities.SaveLogFile(StatusBox);
        }

        #endregion

        #region Helper Methods

        private void ConnectToPlayers(IEnumerable<Player> players)
        {
            Player host = players.First(p => p.IsHost);

            StatusBox.Items.Add("Initializing connections...");
            StatusBox.Items.Add($"{host.Name} is the host");

            // TODO: Figure out this logic

            ContinueButton.Enabled = true;
        }

        #endregion
    }
}
