using System.Collections.Generic;
using System.Diagnostics;
using Acquire.Components;
using Acquire.Enums;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class AiPlayer : Player, IAIPlayer
    {
        /// <summary>
        /// Creates a new Player with the AI type
        /// </summary>
        /// 
        /// <param name="name">The name to give this AI player</param>
        /// <param name="playerId">The unique id of this player.</param>
        public AiPlayer(string name, string playerId) : base(name, PlayerType.AI, playerId, false) { }

        #region Action Methods

        /// <summary>
        /// Makes the AI player take a turn
        /// 
        /// Places the first square it finds and then ends the turn
        /// </summary>
        public void TakeTurn()
        {
            // Attempt to place each square until it works
            foreach (Square square in Squares)
            {
                if (square.Place())
                {
                    break;
                }
            }

            // If we can end the game, do so
            if (Game.OwnerFrame.CanEndGame())
            {
                Game.EndGame();
                return;
            }
            // If we can't end the game, but we can end our turn do so
            if (Game.OwnerFrame.CanEndTurn())
            {
                Game.NextTurn();
                return;
            }

            Trace.Assert(true, $"AI Player ({Name}) is unable to make a move, end the game, or end their turn. This should not be possible");
        }

        /// <summary>
        /// Chooses a company to form when merging/creating and returns that company.
        /// </summary>
        /// 
        /// <param name="companies">The possible companies to form/expand</param>
        /// 
        /// <returns>The chosen company to form/expand</returns>
        public Company ChooseCompany(List<Company> companies) => companies[0];

        /// <summary>
        /// Handles the division of shares when merging two companies
        /// </summary>
        ///
        /// <param name="liveCompany">The company that will receive the merged results</param>
        /// <param name="deadCompany">The company that will be dissolved during the merge</param>
        public void HandleMerge(Company liveCompany, Company deadCompany)
        {
            SellShares(deadCompany, GetShares(deadCompany), true);
        }

        #endregion
    }
}