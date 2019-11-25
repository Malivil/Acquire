using System.Collections.Generic;
using System.Linq;
using Acquire.Components;
using Acquire.Enums;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class Player : IPlayer
    {
        #region Private Member Variables

        // Whether this player can place a square or not
        private bool canPlaceSquare = true;

        // Dictionary to store whether or not this player is a majority holder in each company.
        private readonly Dictionary<string, bool> isMajorityHolder = new Dictionary<string, bool>();

        // Dictionary to store whether or not this player is a majority holder in each company.
        private readonly Dictionary<string, bool> isMinorityHolder = new Dictionary<string, bool>();

        // Dictionary to store how many shares of each company a player has
        private readonly Dictionary<string, int> shares = new Dictionary<string, int>();

        #endregion

        #region Public Member Variables

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string PlayerId { get; }

        /// <inheritdoc />
        public PlayerType Type { get; }

        /// <inheritdoc />
        public bool IsHost { get; }

        /// <inheritdoc />
        public int Money { get; private set; } = 6000;

        /// <inheritdoc />
        public int NumBuysLeft { get; set; } = 3;

        /// <inheritdoc />
        public List<Square> Squares { get; set; } = new List<Square>();

        #endregion

        /// <summary>
        /// Creates a player with the given name <paramref name="name"/> and <paramref name="type"/>
        /// <para>
        /// Initializes majority/minority holdings to false, and company shares to 0
        /// </para>
        /// </summary>
        /// 
        /// <param name="name">The name of the player being created.</param>
        /// <param name="type">The type of this player.</param>
        /// <param name="playerId">The unique id of this player.</param>
        /// <param name="isHost">Whether this player is the host of the game.</param>
        public Player(string name, PlayerType type, string playerId, bool isHost)
        {
            Name = name;
            Type = type;
            PlayerId = playerId;
            IsHost = isHost;

            // Initialize all of the companies
            foreach (Company company in Game.Companies)
            {
                string companyName = company.Name.ToLowerInvariant();
                isMajorityHolder[companyName] = false;
                isMinorityHolder[companyName] = false;
                shares[companyName] = 0;
            }
        }

        #region Action Methods

        /// <inheritdoc />
        public void BuyShare(Company company, bool openingShare)
        {
            // Can only buy a share if we've placed a square or its the opening share
            if (!canPlaceSquare || openingShare)
            {
                // Company has to have shares to buy them...
                if (company.Shares > 0)
                {
                    // Gotta have the money to pay too
                    if (company.GetPrice() <= Money)
                    {
                        // Subtract the cost
                        Money -= company.BuyShare();

                        // Give the pPlayer the share
                        SetShares(company, GetShares(company) + 1);

                        if (!openingShare)
                        {
                            LogMaster.DynamicLog(Name + " bought " + LogMaster.DynamicContent(Name + company.Name + "Buy", LogMaster.STANDARD_NUM_PROGRESSION, 1, "Buy") + " " + LogMaster.DynamicContent(Name + company.Name + "Shares", LogMaster.SHARES, 1, LogMaster.SHARES_TYPE) + " of " + company.Name + ".");
                        }
                        else
                        {
                            LogMaster.Log(Name + " was given one free share of " + company.Name);
                        }

                        // Update the company's holders with this player
                        company.UpdateHolders(this);

                        Game.UpdatePlayerList();
                    }
                    else
                    {
                        LogMaster.Log("Not enough money to buy a share.");
                    }
                }
                else
                {
                    LogMaster.Log("Not enough shares to buy.");
                }
            }
            else
            {
                LogMaster.Log("Place a square first!");
            }
        }

        /// <summary>
        /// Sells a single share of the given company
        /// </summary>
        /// 
        /// <param name="company">The company from which to sell a single share</param>
        /// <param name="isMerging">Whether or not this company is merging with another one</param>
        /// <param name="companySize">The size of the company at the time of selling</param>
        private void SellShare(Company company, bool isMerging, int companySize)
        {
            // Can only sell a share if a square has been placed or a company is merging
            if (!canPlaceSquare || isMerging)
            {
                // Can't sell a share we don't have
                if (GetShares(company) > 0)
                {
                    // Give the pPlayer their money
                    int sellPrice = company.SellShare(companySize);
                    Money += sellPrice;

                    string dynamicSold = LogMaster.DynamicContent(Name + company.Name + "Sold", LogMaster.STANDARD_NUM_PROGRESSION, 1, "Sold");
                    string dynamicShares = LogMaster.DynamicContent(Name + company.Name + "Shares", LogMaster.SHARES, 1, LogMaster.SHARES_TYPE);
                    LogMaster.DynamicLog($"{Name} sold {dynamicSold} {dynamicShares} of {company.Name} for {sellPrice:C0}.");

                    // Subtract one share from the pPlayer
                    SetShares(company, GetShares(company) - 1);
                }
                else
                {
                    LogMaster.Log($"You don't have any shares of {company.Name} to sell.");
                }
            }
            else
            {
                LogMaster.Log("Place a square first!");
            }
        }

        /// <inheritdoc />
        public void SellShares(Company company, int numShares, bool isMerging)
        {
            for (int i = 0; i < numShares; i++)
            {
                SellShare(company, isMerging, company.Size);
            }
        }

        /// <inheritdoc />
        public void TradeShares(Company deadCompany, Company liveCompany, int numShares)
        {
            // If there are enough shares in the live company to trade all the shares
            if (GetShares(deadCompany) / 2 > liveCompany.Shares)
            {
                LogMaster.Log($"{Name} traded {numShares * 2} shares of {deadCompany.Name} for {numShares} shares of {liveCompany.Name}");

                // Trade the shares
                SetShares(liveCompany, GetShares(liveCompany) + numShares);
                SetShares(deadCompany, GetShares(deadCompany) - numShares * 2);

                // Update the company sizes
                deadCompany.Shares += numShares * 2;
                liveCompany.Shares -= numShares;
            }
            // Otherwise...
            else
            {
                LogMaster.Log($"{Name} traded {GetShares(deadCompany)} shares of {deadCompany.Name} for {GetShares(deadCompany) / 2} shares of {liveCompany.Name}");

                int sharesTraded = GetShares(deadCompany);

                // Trade the shares we can
                SetShares(liveCompany, GetShares(liveCompany) + sharesTraded / 2);
                SetShares(deadCompany, 0);

                // Update the company sizes
                deadCompany.Shares += sharesTraded;
                liveCompany.Shares -= sharesTraded / 2;
            }
        }

        /// <inheritdoc />
        public void GiveMoney(int money)
        {
            Money += money;
        }

        /// <inheritdoc />
        public void RemoveDeadSquares()
        {
            Squares.RemoveAll(s => s.State == SquareState.Dead);
        }

        #endregion

        #region Get Methods

        /// <inheritdoc />
        public int GetShares(string companyName) => shares[companyName.ToLower()];

        /// <inheritdoc />
        public int GetShares(Company company) => GetShares(company.Name);

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets the amount of shares this player has in the company <paramref name="company"/> to <paramref name="numShares"/>
        /// </summary>
        /// 
        /// <param name="company">The company whose shares we are updating</param>
        /// <param name="numShares">The amount of shares to be stored for the given company</param>
        private void SetShares(Company company, int numShares)
        {
            shares[company.Name.ToLower()] = numShares;
        }

        /// <inheritdoc />
        public void SetMajorityHolder(string companyName, bool isPlayerMajorityHolder)
        {
            isMajorityHolder[companyName.ToLower()] = isPlayerMajorityHolder;
        }

        /// <inheritdoc />
        public void SetMinorityHolder(string companyName, bool isPlayerMinorityHolder)
        {
            isMinorityHolder[companyName.ToLower()] = isPlayerMinorityHolder;
        }

        /// <inheritdoc />
        public void CanPlaceSquare(bool canPlayerPlaceSquare)
        {
            canPlaceSquare = canPlayerPlaceSquare;
        }

        #endregion

        #region Boolean Methods

        /// <inheritdoc />
        public bool CanPlaceSquare()
        {
            if (canPlaceSquare && !Squares.Any())
            {
                canPlaceSquare = false;
            }

            return canPlaceSquare;
        }

        /// <inheritdoc />
        public bool HasSquare(Square square) => Squares.Contains(square);

        /// <inheritdoc />
        public bool IsMajorityHolder(Company company) => isMajorityHolder[company.Name.ToLower()];

        /// <inheritdoc />
        public bool IsMinorityHolder(Company company) => isMinorityHolder[company.Name.ToLower()];

        #endregion
    }
}
