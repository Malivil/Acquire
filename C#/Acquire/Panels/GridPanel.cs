using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Acquire.Components;
using Acquire.Enums;
using Acquire.Forms;
using Acquire.Models;
using Acquire.Models.Interfaces;

namespace Acquire.Panels
{
    public partial class GridPanel : UserControl
    {
        #region Constants

        private const int WIDTH = 40;
        private const int HEIGHT = 37;
        private const int GAP = 2;

        #endregion

        #region Private Member Variables

        // All of the squares in the grid
        private static Square[,] Squares { get; } = new Square[9, 12];

        #endregion

        #region Public Member Variables

        /// <summary>
        /// All the companies that have been formed so far
        /// </summary>
        public static List<Company> ActiveCompanies { get; } = new List<Company>();

        /// <summary>
        /// The squares that are left to give to players
        /// </summary>
        public static List<Square> SquareBag { get; } = new List<Square>();

        #endregion

        /// <summary>
        /// Sets up the GridPanel. Creates all 9x12 squares and places them on the board
        /// </summary>
        public GridPanel()
        {
            InitializeComponent();

            // The range of letters used to label the squares
            const string alphabet = "ABCDEFGHI";

            // Create and place all 9x12 squares
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    // Create a new square with the right letter and number
                    Squares[i, j] = new Square(alphabet[i] + (j + 1).ToString())
                    {
                        // 2 pixel gap on all sides + width of 40 and height of 37
                        Location = new Point(GetSquareLocationFromSize(WIDTH, i), GetSquareLocationFromSize(HEIGHT, j))
                    };

                    // Add the new square to the panel
                    Controls.Add(Squares[i, j]);

                    // Add each square to the bag
                    if (!SquareBag.Contains(Squares[i, j]))
                        SquareBag.Add(Squares[i, j]);
                }
            }

            // For each square
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    // Find the squares to the left
                    Square left = i != 0 ? Squares[i - 1, j] : null;

                    // Right
                    Square right = i != 8 ? Squares[i + 1, j] : null;

                    // Top
                    Square top = j != 0 ? Squares[i, j - 1] : null;

                    // And bottom
                    Square bottom = j != 11 ? Squares[i, j + 1] : null;

                    // And set them as the current square's bounds
                    Squares[i, j].SetBounds(left, right, top, bottom);
                }
            }

            // Shuffles the bag
            ShuffleBag();
        }

        #region Action Methods

        /// <summary>
        /// Shuffles the bag of squares using two randomly generated numbers 100 times
        /// </summary>
        public void ShuffleBag()
        {
            // Don't shuffle an empty bag or one with just 1 square
            if (SquareBag.Count > 1)
            {
                // Our random number generator
                Random random = new Random();
                // Get the first two random numbers
                int n = random.Next(SquareBag.Count);
                int k = random.Next(SquareBag.Count);

                // Do this 100 times
                for (int i = 0; i < 100; i++)
                {
                    // Get the current square at n
                    Square temp = SquareBag[n];
                    // Switch the squares at n and k
                    SquareBag[n] = SquareBag[k];
                    // Put the n square where k was
                    SquareBag[k] = temp;

                    // Get new random numbers
                    n = random.Next(SquareBag.Count);
                    k = random.Next(SquareBag.Count);
                }
            }
        }

        /// <summary>
        /// Forces every square to check its bounds
        /// </summary>
        public static void CheckSquares()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Squares[i, j].CheckSquare();
                }
            }
        }

        /// <summary>
        /// Places the given square on the board. Handles unplaceable squares,
        /// forming new companies, growing existing companies, and merging multiple compnaies together.
        /// 
        /// Returns null if the square was not able to be places, an arbitrary value otherwise
        /// </summary>
        /// 
        /// <param name="square">The square attempting to be placed</param>
        /// 
        /// <returns>Null if the square was not able to be places, an arbitrary value otherwise</returns>
        public static object PlaceSquare(Square square)
        {
            // Only place a square if it is placeable
            if (square.CanBePlaced)
            {
                // Can't place a dead square
                if (square.GetState() != Square.STATE_DEAD)
                {
                    // If there are no other squares around, just put this one down and be done with it
                    if ((square.LeftSquare == null || square.LeftSquare.GetState() < Square.STATE_PLACED) &&
                        (square.RightSquare == null || square.RightSquare.GetState() < Square.STATE_PLACED) &&
                        (square.TopSquare == null || square.TopSquare.GetState() < Square.STATE_PLACED) &&
                        (square.BottomSquare == null || square.BottomSquare.GetState() < Square.STATE_PLACED))
                    {
                        // Update the state
                        square.SetState(Square.STATE_PLACED);
                        // Update the log
                        LogMaster.Log($"{Game.CurrentPlayer.Name} placed a square at {square}.");

                        // Return random non-null value to show success
                        return 1;
                    }

                    // List of surrounding viable squares
                    List<Square> squares = new List<Square>();

                    // Only add the squares if they exist and they are placed or are part of a company
                    if (square.LeftSquare != null && (square.LeftSquare.IsPlaced || square.LeftSquare.IsCompany))
                    {
                        squares.Add(square.LeftSquare);
                    }

                    if (square.RightSquare != null && (square.RightSquare.IsPlaced || square.RightSquare.IsCompany))
                    {
                        squares.Add(square.RightSquare);
                    }

                    if (square.TopSquare != null && (square.TopSquare.IsPlaced || square.TopSquare.IsCompany))
                    {
                        squares.Add(square.TopSquare);
                    }

                    if (square.BottomSquare != null && (square.BottomSquare.IsPlaced || square.BottomSquare.IsCompany))
                    {
                        squares.Add(square.BottomSquare);
                    }

                    // Merge the list of squares with the placed square
                    return Merge(squares, square);
                }

                LogMaster.Log("Two or more of the companies you are trying to merge are safe.");
                return null;
            }

            return 1;
        }

        /// <summary>
        /// Gives a square to the given player
        /// </summary>
        /// 
        /// <param name="player">The player who is to be given more squares</param>
        public void TakeSquare(IPlayer player)
        {
            // Get the squares that the player has already
            List<Square> squares = player.Squares;

            // Make sure they have 10 at all times
            while (squares.Count < 10)
            {
                // Only give them squares if there are some to give
                if (SquareBag.Count != 0)
                {
                    // Don't give unplaceable squares
                    if (SquareBag[0].CanBePlaced)
                    {
                        squares.Add(SquareBag[0]);
                    }
                    // Remove this square from the list
                    SquareBag.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

            // Update the player's list of squares
            player.Squares = squares;
            // Shuffle the bag
            ShuffleBag();
        }

        /// <summary>
        /// Updates the display to show which squares a player has and can place
        /// </summary>
        /// 
        /// <param name="player">The player whose placeable squares are going to be displayed</param>
        public void UpdateHeldSquares(IPlayer player)
        {
            // Update the hand panel too
            Game.OwnerFrame.GetHandPanel().SetHand(player.Squares);

            // Go through each square
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    // If the player has this square
                    if (player.Squares.Contains(Squares[i, j]))
                    {
                        // Put the border on it
                        Squares[i, j].BorderStyle = BorderStyle.Fixed3D;
                        // If it is placeable, change it's state
                        if (Squares[i, j].CanBePlaced)
                        {
                            Squares[i, j].SetState(Square.STATE_OPTION);
                        }
                    }
                    else
                    {
                        // Otherwise turn off the border
                        Squares[i, j].BorderStyle = BorderStyle.None;
                        // And reset the state to open
                        if (Squares[i, j].CanBePlaced)
                        {
                            Squares[i, j].SetState(Square.STATE_OPEN);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ends the game by disabling all squares
        /// </summary>
        public void EndGame()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Squares[i, j].CanBePlaced = false;
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Handles the merging of a list of squares with a new square.
        /// Handles forming new companies, growing existing companies, and merging multiple compnaies together.
        /// 
        /// Returns null if the square was not able to be places, an arbitrary value otherwise
        /// </summary>
        /// 
        /// <param name="squares">The list of squares being merged with <paramref name="instigator"/></param>
        /// <param name="instigator">The square that was placed, causing all this merging business</param>
        /// 
        /// <returns>Null if the square was not able to be places, an arbitrary value otherwise</returns>
        private static Company Merge(IList<Square> squares, Square instigator)
        {
            // List of possible companies
            List<Company> options = new List<Company>();
            // List of destroyed companies
            List<Company> destroyedCompanies = new List<Company>();
            // List of companies that were JUST destroyed
            List<Company> newlyDead = new List<Company>();
            // The newly formed company, if any
            Company newCompany = null;
            // Whether the company being formed is new, or just merging with another one
            bool isNewCompany = false;

            // While we still ahve squares to look at
            while (squares.Count != 0)
            {
                // Get the first one
                Square square = squares[0];
                // Remove it from the list
                squares.RemoveAt(0);

                // If it is a company
                if (!square.IsCompany)
                {
                    continue;
                }

                // And we haven't formed a new company yet
                if (newCompany == null)
                {
                    // The new company will be the company in this square
                    newCompany = square.GetCompany();
                    // Get rid of the old options
                    options.Clear();
                    // Add this company to the list of options and newly dead list
                    options.Add(newCompany);
                    newlyDead.Add(newCompany);
                }
                // If we already have a company, but the one in this square is bigger
                else if (square.GetCompany().Size > newCompany.Size)
                {
                    // Then store the one in this square
                    newCompany = square.GetCompany();
                    // Add the options we already had to companies who will die
                    destroyedCompanies.AddRange(options);
                    // Clear the options
                    options.Clear();
                    // Add the new company to the options and newly dead list
                    options.Add(newCompany);
                    newlyDead.Add(newCompany);
                }
                // If the company in this square is smaller than the current company
                else if (square.GetCompany().Size < newCompany.Size)
                {
                    // It will die
                    newlyDead.Add(square.GetCompany());
                    destroyedCompanies.Add(square.GetCompany());
                }
                // If they are the same size
                else if (square.GetCompany().Size == newCompany.Size)
                {
                    // Add both to the list of options if they aren't there already
                    if (!options.Contains(newCompany))
                    {
                        options.Add(newCompany);
                    }

                    if (!options.Contains(square.GetCompany()))
                    {
                        options.Add(square.GetCompany());
                    }

                    // And to the newly dead list too
                    newlyDead.Add(newCompany);
                    newlyDead.Add(square.GetCompany());
                }
            }

            // If we have no options
            if (options.Count == 0)
            {
                // And we have no dead companies
                if (Game.GetDeadCompanies().Count == 0)
                {
                    // Then we have no more to make
                    LogMaster.Log("This square can't be placed because no new companies can be created.");
                    return null;
                }

                // Otherwise we just add the dead companies
                options.AddRange(Game.GetDeadCompanies());
                // And we'll be forming a whole new company
                isNewCompany = true;
            }

            // If there's only one option, that's the one to go for
            if (options.Count == 1)
            {
                newCompany = options[0];
            }
            // If the player is an AI, let them choose a company without the GUI
            else if (Game.CurrentPlayer.Type == PlayerType.AI)
            {
                newCompany = ((AiPlayer)Game.CurrentPlayer).ChooseCompany(options);
            }
            else
            {
                // Otherwise pop up a dialog to let the user choose
                ChooseCompanyDialog csbDialog = new ChooseCompanyDialog(options);
                csbDialog.ShowDialog();

                // If they don't choose, don't put the square down
                if (csbDialog.ChosenCompany != null)
                {
                    newCompany = csbDialog.ChosenCompany;
                }
                else
                {
                    return null;
                }
            }

            // If companies are being destroyed
            if (newlyDead.Count > 0)
            {
                // Get each one that isn't the one we're already creating
                foreach (Company company in newlyDead.Where(c => !c.GetName().Equals(newCompany.GetName())))
                {
                    // Get each player that has shares in this company
                    foreach (IPlayer player in Game.Players.Where(p => p.GetShares(company) > 0))
                    {
                        // To check if they have any shares in the company
                        // If so, let them decide what to do with them
                        if (player.Type != PlayerType.AI)
                        {
                            new MergeDialog(newCompany, company, player).ShowDialog();
                        }
                        // If it's an AI, let them handle it without the GUI
                        else
                        {
                            ((AiPlayer)player).HandleMerge(newCompany, company);
                        }
                    }

                    // Log who is being destroyed
                    LogMaster.Log($"{Game.CurrentPlayer.Name} has destroyed {company.GetName()} to expand {newCompany.GetName()}.");
                }
            }

            // Log where the square is placed, now that it is certain
            LogMaster.Log($"{Game.CurrentPlayer.Name} placed a square at {instigator}.");

            // If this isn't a new company, add the options
            if (!isNewCompany)
            {
                destroyedCompanies.AddRange(options);
            }

            // Remove this company, though
            destroyedCompanies.Remove(newCompany);

            // Reset each dead company
            foreach (Company destroyedCompany in destroyedCompanies)
            {
                destroyedCompany.IsPlaced = false;
                destroyedCompany.Size = 0;
            }

            // Lead the squares with this company
            return Link(instigator, newCompany);
        }

        /// <summary>
        /// Recursively links every square touching this one with the given company
        /// 
        /// Returns the company being linked
        /// </summary>
        /// 
        /// <param name="instigator">The square instigating this linking</param>
        /// <param name="newCompany">The company taking over</param>
        /// 
        /// <returns>The company being linked</returns>
        private static Company Link(Square instigator, Company newCompany)
        {
            // Add this square to the company
            newCompany.Add(instigator);
            // Increment the size of the company
            newCompany.Size++;
            // Redraw the frame
            Game.OwnerFrame.Refresh();
            // Get the current player
            IPlayer player = Game.CurrentPlayer;
            // If this company is in the active list, it isn't new
            bool isNew = !ActiveCompanies.Any(c => c.GetName().Equals(newCompany.GetName()));

            // If it is new, add it to the list of active companies
            if (isNew)
            {
                ActiveCompanies.Add(newCompany);
            }

            // If this company isn't marked as placed
            if (!newCompany.IsPlaced)
            {
                // Log that the company is being formed
                LogMaster.Log($"{player.Name} has formed the {newCompany.GetName()} company.");
                // And give the player the amount of money they need to get 1 "free" share
                player.GiveMoney(newCompany.GetPrice());
                player.BuyShare(newCompany, true);
                // Mark the company as placed
                newCompany.IsPlaced = true;
            }

            // Check if the game is enable
            Game.EndGameCheck();
            // Set the company associated with this square to this company
            instigator.SetCompany(newCompany);

            // Check the boundary squares. If there is a square that needs to be taken over, take it over
            if (instigator.LeftSquare != null && instigator.LeftSquare.GetCompany() != newCompany && instigator.LeftSquare.GetState() > Square.STATE_OPTION)
            {
                newCompany = Link(instigator.LeftSquare, newCompany);
            }

            if (instigator.RightSquare != null && instigator.RightSquare.GetCompany() != newCompany && instigator.RightSquare.GetState() > Square.STATE_OPTION)
            {
                newCompany = Link(instigator.RightSquare, newCompany);
            }

            if (instigator.TopSquare != null && instigator.TopSquare.GetCompany() != newCompany && instigator.TopSquare.GetState() > Square.STATE_OPTION)
            {
                newCompany = Link(instigator.TopSquare, newCompany);
            }

            if (instigator.BottomSquare != null && instigator.BottomSquare.GetCompany() != newCompany && instigator.BottomSquare.GetState() > Square.STATE_OPTION)
            {
                newCompany = Link(instigator.BottomSquare, newCompany);
            }

            return newCompany;
        }

        /// <summary>
        /// Gets the adjusted size of a square, given the <paramref name="size"/> and <paramref name="multiplier"/>
        /// </summary>
        ///
        /// <param name="size">The initial size of the square</param>
        /// <param name="multiplier">The muliplier indicating the relative grid position</param>
        ///
        /// <returns>The adjusted position based on the <paramref name="size"/> and <paramref name="multiplier"/></returns>
        private static int GetSquareLocationFromSize(int size, int multiplier)
            => GAP + (size + GAP) * multiplier;

        #endregion
    }
}
