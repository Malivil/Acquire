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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Company ChooseCompany(List<Company> companies) => companies[0];

        /// <inheritdoc />
        public void HandleMerge(Company liveCompany, Company deadCompany)
        {
            SellShares(deadCompany, GetShares(deadCompany), true);
        }

        #endregion
    }
}