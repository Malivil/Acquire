using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Acquire.Enums;
using Acquire.Frames;
using Acquire.Models.Interfaces;
using Acquire.Panels;

namespace Acquire.Models
{
    public class Game
    {
        #region Public Member Variables

        /// <summary>
        /// The player whose turn it is now
        /// </summary>
        public static IPlayer CurrentPlayer { get; private set; }

        /// <summary>
        /// The list of all players
        /// </summary>
        public static List<IPlayer> Players { get; private set; }

        /// <summary>
        /// The active <see cref="AcquireFrame"/>
        /// </summary>
        public static AcquireFrame OwnerFrame { get; private set; }

        /// <summary>
        /// Whether or not this game is over
        /// </summary>
        public static bool IsGameOver { get; private set; }

        /// <summary>
        /// Create all of the companies and put them in a list
        /// </summary>
        public static List<Company> Companies = new List<Company>
        {
            new Company("Tower", 5, 0, Image.FromFile(@"Images/tow_icon.GIF", true)),
            new Company("Luxor", 4, 0, Image.FromFile(@"Images/lux_icon.GIF", true)),
            new Company("American", 8, 2, Image.FromFile(@"Images/amer_icon.GIF", true)),
            new Company("Festival", 6, 1, Image.FromFile(@"Images/fest_icon.GIF", true)),
            new Company("Worldwide", 7, 1, Image.FromFile(@"Images/wor_icon.GIF", true)),
            new Company("Continental", 9, 2, Image.FromFile(@"Images/cont_icon.GIF", true)),
            new Company("Imperial", 10, 2, Image.FromFile(@"Images/imp_icon.GIF", true))
        };

        #endregion

        #region Action Methods

        /// <summary>
        /// Initializes this Game with the given playesr, companies, and AcquireFrame
        /// </summary>
        /// 
        /// <param name="players">The players playing this Game</param>
        /// <param name="frame">The AcquireFrame used in this Game</param>
        public static void Initialize(List<IPlayer> players, AcquireFrame frame)
        {
            // Store the provided variables
            Players = players;
            OwnerFrame = frame;

            // Give the players squares
            foreach (IPlayer pPlayer in Players)
            {
                OwnerFrame.GetGridPanel().TakeSquare(pPlayer);
            }

            // Store the current player
            CurrentPlayer = Players[0];
            // Display their squares
            OwnerFrame.GetGridPanel().UpdateHeldSquares(CurrentPlayer);
            // Output the initial log lines
            LogMaster.Log("Welcome to Acquire!");
            LogMaster.Log("---------------------------------");
            LogMaster.Log("It is " + CurrentPlayer.Name + "'s turn first.");

            // If this is an AI player, get them started
            if (CurrentPlayer.Type == PlayerType.AI)
            {
                ((AiPlayer)CurrentPlayer).TakeTurn();
            }
        }

        /// <summary>
        /// Checks whether or not the turn is endable.
        /// Enables or disables the end turn button as necessary
        /// </summary>
        public static void EndTurnCheck()
        {
            if (CurrentPlayer.CanPlaceSquare() && CurrentPlayer.Squares.Count > 0)
            {
                OwnerFrame.SetEndTurnButtonEnabled(false);
            }
            else if (!IsGameOver)
            {
                OwnerFrame.SetEndTurnButtonEnabled(true);
            }
        }

        /// <summary>
        /// Checks whether or not the game is endable.
        /// Enables/disables the end game button as necessary
        /// </summary>
        public static void EndGameCheck()
        {
            // Get the companies that exist
            List<Company> activeCompanies = GridPanel.ActiveCompanies;
            // Whether or not we can end the game
            bool canEnd = false;
            // How many safe companies there are
            int safeCompanies = 0;

            // Check every company
            foreach (Company company in activeCompanies)
            {
                // If the company is safe, let it know
                if (company.Size > 10)
                {
                    company.IsSafe(true);
                    safeCompanies++;
                }

                // If the company is over size 40, the game can end
                if (company.Size > 40)
                {
                    canEnd = true;
                }
            }

            // If we haven't already found a way to end the game, and we only have 1 company
            // and it is safe, the rules say the game is endable at this point
            if (!canEnd && safeCompanies == activeCompanies.Count)
            {
                canEnd = true;
            }

            // If we still can't end, check the squares
            if (!canEnd)
            {
                // If we have no more to give out
                if (GridPanel.SquareBag.Count == 0)
                {
                    // We can probably end
                    canEnd = true;

                    // Unless a player still has squares left
                    for (int i = 0; i < Players.Count && canEnd; i++)
                    {
                        // Don't wait for AI players to place all their squares... 'cause they are dumb
                        if (Players[i].Type != PlayerType.AI && Players[i].Squares.Count > 0)
                        {
                            canEnd = false;
                        }
                    }
                }
            }

            // Update the end game button status
            if (canEnd && !IsGameOver)
            {
                OwnerFrame.SetEndGameButtonEnabled(true);
            }
            else
            {
                OwnerFrame.SetEndGameButtonEnabled(false);
            }
        }

        /// <summary>
        /// Handles ending the game
        /// </summary>
        public static void EndGame()
        {
            foreach (IPlayer player in Players)
            {
                foreach (Company company in Companies)
                {
                    if (player.GetShares(company) > 0)
                    {
                        player.SellShares(company, player.GetShares(company), true);
                    }
                }
            }

            // Store the first player as both highest and lowest
            IPlayer highest = Players[0];
            IPlayer lowest = Players[0];

            // Find the lowest and highest money players
            foreach (IPlayer player in Players)
            {
                if (player.Money > highest.Money)
                {
                    highest = player;
                }
                if (player.Money < lowest.Money)
                {
                    lowest = player;
                }
            }

            // If the amount is the same, there is no winnder
            IPlayer winner = highest.Money == lowest.Money ? null : highest;

            // Let the users know it's all over
            if (winner != null)
            {
                LogMaster.Log(winner.Name + " has won the game with $" + winner.Money + "!");
                IsGameOver = true;
            }
            else if (!IsGameOver)
            {
                LogMaster.Log("There was a tie! All players have $" + Players[0].Money + "!");
                IsGameOver = true;
            }

            // Don't let anything else happen
            OwnerFrame.GetGridPanel().EndGame();
            OwnerFrame.SetEndTurnButtonEnabled(false);
            OwnerFrame.SetEndGameButtonEnabled(false);
        }

        /// <summary>
        /// Sets everything up for the next turn and switches to the next player
        /// </summary>
        public static void NextTurn()
        {
            // Remove the unplaceable squares
            CurrentPlayer.RemoveDeadSquares();
            // Reset the player's ability to place squares
            CurrentPlayer.CanPlaceSquare(true);
            // Make the place take new squares
            OwnerFrame.GetGridPanel().TakeSquare(CurrentPlayer);

            // Switch to the next player
            Players.Add(Players[0]);
            Players.RemoveAt(0);
            CurrentPlayer = Players[0];

            // Update the player display
            OwnerFrame.UpdatePlayerList();
            // Update the square highlighting
            OwnerFrame.GetGridPanel().UpdateHeldSquares(CurrentPlayer);
            // Reset the amount of stock this player can buy
            CurrentPlayer.NumBuysLeft = 3;

            // Reset the end turn button
            EndTurnCheck();
            // Clear the current dynamic content
            LogMaster.ClearDynamicContent(true, true);
            // Tell the users whose turn it is
            LogMaster.Log("It is now " + CurrentPlayer.Name + "'s turn.");

            // If this is an AI player, make them take their turn
            if (CurrentPlayer.Type == PlayerType.AI)
            {
                ((AiPlayer)CurrentPlayer).TakeTurn();
            }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Gets the list of companies that have not been placed
        /// </summary>
        ///
        /// <returns>The list of companies that have not been placed</returns>
        public static List<Company> GetDeadCompanies()
        {
            List<Company> deadCompanies = new List<Company>();
            foreach (Company company in Companies)
            {
                if (!company.IsPlaced)
                {
                    deadCompanies.Add(company);
                }
            }

            return deadCompanies;
        }

        #endregion

        #region Boolean Methods

        /// <summary>
        /// Returns whether any companies have been placed
        /// </summary>
        ///
        /// <returns>True if a company has been placed, false otherwise</returns>
        public static bool HasActiveCompanies() => Companies.Any(c => c.IsPlaced);

        #endregion
    }
}
