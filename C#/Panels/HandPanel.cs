using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Acquire.Components;

namespace Acquire.Panels
{
    public partial class HandPanel : UserControl
    {
        #region Constants

        private const int HEIGHT = 42;
        private const int GAP = 2;

        #endregion

        public HandPanel()
        {
            InitializeComponent();
        }

        #region Set Methods

        /// <summary>
        /// Clears out all the squares and updates the panel with the new hand
        /// </summary>
        /// 
        /// <param name="squares">List of squares to add to the panel</param>
        public void SetHand(List<Square> squares)
        {
            // Clear out what we have already
            SquarePanel.Controls.Clear();

            for (int i = 0; i < squares.Count; i++)
            {
                // Create a new square
                Square square = new Square(squares[i].ToString())
                {
                    // Move it to the right place
                    Location = new Point(GAP + HEIGHT * i, GAP)
                };
                // Add it to the panel
                SquarePanel.Controls.Add(square);
            }

            // Update the status label
            SquaresLeftLabel.Text = $@"Number of squares left in bag: {GridPanel.SquareBag.Count}";
        }

        #endregion
    }
}
