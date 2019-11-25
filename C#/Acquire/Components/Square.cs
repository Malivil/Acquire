using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Acquire.Enums;
using Acquire.Models;
using Acquire.Panels;

namespace Acquire.Components
{
    public partial class Square : UserControl
    {
        #region Public Member Variables

        /// <summary>
        /// Whether or not this square has been placed
        /// </summary>
        public bool IsPlaced { get; private set; }
        /// <summary>
        /// Whether or not this square represents a company
        /// </summary>
        public bool IsCompany { get; private set; }
        /// <summary>
        /// Whether or not this square can been placed
        /// </summary>
        public bool CanBePlaced { get; set; } = true;

        /// <summary>
        /// The state this square is in
        /// </summary>
        public SquareState State { get; private set; }

        /// <summary>
        /// The company associated with this square
        /// </summary>
        public Company Company { get; private set; }

        /// <summary>
        /// The square to the left
        /// </summary>
        public Square LeftSquare { get; private set; }
        /// <summary>
        /// The square to the right
        /// </summary>
        public Square RightSquare { get; private set; }
        /// <summary>
        /// The square to the top
        /// </summary>
        public Square TopSquare { get; private set; }
        /// <summary>
        /// The square to the bottom
        /// </summary>
        public Square BottomSquare { get; private set; }

        #endregion

        #region Private Member Variables

        // This square's ID
        private readonly string id;

        #endregion

        /// <summary>
        /// Creates a new Square with ID id
        /// Initializes the square to SquareState.Open
        /// </summary>
        /// 
        /// <param name="id">The ID to assign this Square</param>
        public Square(string id)
        {
            InitializeComponent();

            this.id = id;
            SetState(SquareState.Open);
        }

        #region Event Handlers

        /// <summary>
        /// Paints the square ID
        /// </summary>
        /// 
        /// <param name="pArgs">The arguments for this Paint event</param>
        protected override void OnPaint(PaintEventArgs pArgs)
        {
            IDLabel.Text = id;
        }

        /// <summary>
        /// Handles when a square is clicked
        /// </summary>
        /// 
        /// <param name="oSender">The object sending the event</param>
        /// <param name="eArgs">The arguments sent</param>
        private void Square_Click(object oSender, EventArgs eArgs)
        {
            Place();
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Sets references to the squares that surround this one
        /// </summary>
        ///
        /// <param name="left">The square to the left</param>
        /// <param name="right">The square to the right</param>
        /// <param name="top">The square to the top</param>
        /// <param name="bottom">The square to the bottom</param>
        public void SetBounds(Square left, Square right, Square top, Square bottom)
        {
            LeftSquare = left;
            RightSquare = right;
            TopSquare = top;
            BottomSquare = bottom;
        }

        /// <summary>
        /// Returns this square's ID
        /// </summary>
        /// 
        /// <returns>This square's ID</returns>
        public override string ToString() => id;

        /// <summary>
        /// Checks whether or not this square is placeable
        /// </summary>
        public void CheckSquare()
        {
            // List of the companies that surround this square.
            List<Company> liSurroundingComps = new List<Company>();

            // If the left square is a safe company, add it to the list
            if (LeftSquare?.Company?.IsSafe == true)
            {
                liSurroundingComps.Add(LeftSquare.Company);
            }

            // If the right square is a safe company and it isn't in the list already, add it to the list
            if (RightSquare?.Company?.IsSafe == true && !liSurroundingComps.Contains(RightSquare.Company))
            {
                liSurroundingComps.Add(RightSquare.Company);
            }

            // If the top square is a safe company and it isn't in the list already, add it to the list
            if (TopSquare?.Company?.IsSafe == true && !liSurroundingComps.Contains(TopSquare.Company))
            {
                liSurroundingComps.Add(TopSquare.Company);
            }

            // If the bottom square is a safe company and it isn't in the list already, add it to the list
            if (BottomSquare?.Company?.IsSafe == true && !liSurroundingComps.Contains(BottomSquare.Company))
            {
                liSurroundingComps.Add(BottomSquare.Company);
            }

            // If there are at least two safe companies around this square, it is never placeable
            if (liSurroundingComps.Count >= 2)
            {
                SetState(SquareState.Dead);
            }
        }

        /// <summary>
        /// Places this square
        /// </summary>
        /// 
        /// <returns>True if it is successfully placed, false otherwise</returns>
        public bool Place()
        {
            // Can't buy shares if the game is over
            if (!Game.IsGameOver)
            {
                // Only place a square if the player is allowed to
                if (Game.CurrentPlayer.CanPlaceSquare())
                {
                    // Only place a square if the player has it
                    if (Game.CurrentPlayer.HasSquare(this))
                    {
                        // Only place a square if this square can be placed
                        if (CanBePlaced)
                        {
                            // Try to place a square
                            if (GridPanel.PlaceSquare(this))
                            {
                                // If it worked, stop the player from placing more squares
                                Game.CurrentPlayer.CanPlaceSquare(false);
                                // Remove this square from the player's hand
                                Game.CurrentPlayer.Squares.Remove(this);
                                // Update the hand panel to show the player what he has
                                Game.SetHand(Game.CurrentPlayer.Squares);

                                // This square can't be placed because it already has been.
                                CanBePlaced = false;
                                // Check if the game is endable
                                Game.EndTurnCheck();
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            // If the game is over, that's why the player can't place
                            if (Game.IsGameOver)
                            {
                                LogMaster.Log("You cannot place a square, the game is over");
                                return false;
                            }
                            // Otherwise they don't have the square

                            LogMaster.Log("Cannot place a square there.");
                            return false;
                        }
                    }
                    else
                    {
                        LogMaster.Log("You don't have that square to place.");
                        return false;
                    }
                }
                else
                {
                    LogMaster.Log("You have already placed a square this turn.");
                    return false;
                }
            }
            else
            {
                LogMaster.Log("The game is already over. You cannot place any more squares.");
                return false;
            }

            // Check and update all the squares
            GridPanel.CheckSquares();
            return true;
        }

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets this squares state to <paramref name="state"/> and updates the color and boolean status
        /// </summary>
        /// 
        /// <param name="state">The state to set this square to</param>
        public void SetState(SquareState state)
        {
            State = state;

            switch (state)
            {
                case SquareState.Dead:
                    BackColor = Color.White;
                    CanBePlaced = false;
                    IsPlaced = true;
                    IsCompany = false;
                    break;
                case SquareState.Open:
                    BackColor = Color.DarkGray;
                    IsPlaced = false;
                    IsCompany = false;
                    CanBePlaced = true;
                    break;
                case SquareState.Placeable:
                    BackColor = Color.Gray;
                    IsCompany = false;
                    IsPlaced = false;
                    CanBePlaced = true;
                    break;
                case SquareState.Placed:
                    BackColor = Color.Black;
                    IsCompany = false;
                    IsPlaced = true;
                    break;
            }
        }

        /// <summary>
        /// Sets the company associated with this Square to <paramref name="company"/>. Also updates the current state
        /// </summary>
        /// 
        /// <param name="company">The company to associate with this Square</param>
        public void SetCompany(Company company)
        {
            Company = company;
            State = (SquareState)company.Id;
            BackColor = company.Color;
            IsCompany = true;
            IsPlaced = false;
        }

        #endregion
    }
}