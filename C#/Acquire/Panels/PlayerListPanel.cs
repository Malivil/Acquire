using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Acquire.Models.Interfaces;

namespace Acquire.Panels
{
    public partial class PlayerListPanel : UserControl
    {
        #region Constants

        private const int WIDTH = 58;
        private const int GAP = 2;

        #endregion

        public PlayerListPanel()
        {
            InitializeComponent();
        }

        #region Set Methods

        /// <summary>
        /// Clears out this player list and reconstructs it with the given list of players
        /// </summary>
        /// 
        /// <param name="players">The list of players to construct status panels for</param>
        public void SetPlayersList(List<IPlayer> players)
        {
            // Clear out what we have saved already
            Controls.Clear();

            // Get each player
            for (int i = 0; i < players.Count; i++)
            {
                // Create a new status panel
                PlayerStatusPanel panel = new PlayerStatusPanel(players[i])
                {
                    // Move it to the right place
                    Location = new Point(GAP, GAP + i * WIDTH),
                    // Add the border
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Highlight the current player
                if (i == 0)
                    panel.BackColor = Color.LightGray;

                // Add the panel to the list
                Controls.Add(panel);
            }
        }

        #endregion
    }
}
