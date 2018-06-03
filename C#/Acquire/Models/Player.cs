using System;
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

        /// <summary>
        /// This player's name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// This player's Unique ID
        /// </summary>
        public string PlayerId { get; }

        /// <summary>
        /// The type of player this is
        /// </summary>
        public PlayerType Type { get; }

        /// <summary>
        /// Whether this player is the host of the game
        /// </summary>
        public bool IsHost { get; }

        /// <summary>
        /// The amount of money this player has
        /// </summary>
        public int Money { get; private set; } = 6000;

        /// <summary>
        /// The number of buys this player has left in this turn
        /// </summary>
        public int NumBuysLeft { get; set; } = 3;

        /// <summary>
        /// List of all the squares this player has
        /// </summary>
        public List<Square> Squares { get; set; } = new List<Square>();

        #endregion

        /// <summary>
        /// Creates a player with the given name <paramref name="name"/> and <paramref name="type"/>
        /// <para>
        /// Initilizes majority/minority holdings to false, and company shares to 0
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

        /// <summary>
        /// Buys one share of the given company
        /// </summary>
        /// 
        /// <param name="company">The company from which to buy a share</param>
        /// <param name="openingShare">Whether or not the share being bought is an opening share</param>
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
                            LogMaster.DynamicLog(Name + " bought " + LogMaster.DynamicContent(Name + company.Name + "Buy", LogMaster.STANDARD_NUPROG, 1, "Buy") + " " + LogMaster.DynamicContent(Name + company.Name + "Shares", LogMaster.SHARES, 1, LogMaster.SHARES_TYPE) + " of " + company.Name + ".");
                        }
                        else
                        {
                            LogMaster.Log(Name + " was given one free share of " + company.Name);
                        }

                        // Update the company's holders with this player
                        company.UpdateHolders(this);

                        Game.OwnerFrame.UpdatePlayerList();
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
        public void SellShare(Company company, bool isMerging, int companySize)
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

                    string dynamicSold = LogMaster.DynamicContent(Name + company.Name + "Sold", LogMaster.STANDARD_NUPROG, 1, "Sold");
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

        /// <summary>
        /// Sells multiple shares of the given company
        /// </summary>
        ///
        /// <param name="company">The company from which to sell <paramref name="numShares"/> shares</param>
        /// <param name="numShares">The number of shares being sold</param>
        /// <param name="isMerging">Whether or not this company is merging with another one</param>
        ///
        public void SellShares(Company company, int numShares, bool isMerging)
        {
            for (int i = 0; i < numShares; i++)
            {
                SellShare(company, isMerging, company.Size);
            }
        }

        /// <summary>
        /// Trades the amount of shares of <paramref name="deadCompany"/> given by <paramref name="numShares"/> to shares of <paramref name="liveCompany"/>
        /// </summary>
        /// 
        /// <param name="deadCompany">The company being destroyed whose shares are being traded</param>
        /// <param name="liveCompany">The company to which the old shares are being traded</param>
        /// <param name="numShares">The amount of shares to be traded</param>
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
                deadCompany.Shares = deadCompany.Shares + numShares * 2;
                liveCompany.Shares = liveCompany.Shares - numShares;
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
                deadCompany.Shares = deadCompany.Shares + sharesTraded;
                liveCompany.Shares = liveCompany.Shares - sharesTraded / 2;
            }
        }

        /// <summary>
        /// Gives the amount designated by <paramref name="money"/> to the player
        /// </summary>
        /// 
        /// <param name="money">The amount of money to give the player</param>
        public void GiveMoney(int money)
        {
            Money += money;
        }

        /// <summary>
        /// Prints out the status of this company to the console
        /// </summary>
        public void PrintStatus()
        {
            Console.WriteLine($@"Name: {Name} money: ${Money}");
            Console.WriteLine(@"Number of shares in each company:");

            foreach (Company company in Game.Companies)
            {
                Console.WriteLine($@"{company.Name}: {GetShares(company)}");
            }
        }

        /// <summary>
        /// Removes all unusable squares from the player's hand
        /// </summary>
        public void RemoveDeadSquares()
        {
            Squares.RemoveAll(s => s.State == SquareState.Dead);
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the amount of shares the player has in the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <returns>The amount of shares the player has in the company named <paramref name="companyName"/></returns>
        public int GetShares(string companyName) => shares[companyName.ToLower()];

        /// <summary>
        /// Returns the amount of shares the player has in the <paramref name="company"/>
        /// </summary>
        /// 
        /// <returns>The amount of shares the player has in the <paramref name="company"/></returns>
        public int GetShares(Company company) => GetShares(company.Name);

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets the amount of shares this player has in the company named <paramref name="companyName"/> to <paramref name="numShares"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company whose shares we are updating</param>
        /// <param name="numShares">The amount of shares to be stored for the given company</param>
        public void SetShares(string companyName, int numShares)
        {
            shares[companyName.ToLower()] = numShares;
        }

        /// <summary>
        /// Sets the amount of shares this player has in the company <paramref name="company"/> to <paramref name="numShares"/>
        /// </summary>
        /// 
        /// <param name="company">The company whose shares we are updating</param>
        /// <param name="numShares">The amount of shares to be stored for the given company</param>
        public void SetShares(Company company, int numShares)
        {
            SetShares(company.Name, numShares);
        }

        /// <summary>
        /// Sets whether or not this player is a majority holder for the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for whom we are updating majority holder status</param>
        /// <param name="isPlayerMajorityHolder">Whether or not this player is a majority holder</param>
        public void SetMajorityHolder(string companyName, bool isPlayerMajorityHolder)
        {
            isMajorityHolder[companyName.ToLower()] = isPlayerMajorityHolder;
        }

        /// <summary>
        /// Sets whether or not this player is a majority holder for the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for whom we are updating majority holder status</param>
        /// <param name="isPlayerMajorityHolder">Whether or not this player is a majority holder</param>
        public void SetMajorityHolder(Company company, bool isPlayerMajorityHolder)
        {
            SetMajorityHolder(company.Name, isPlayerMajorityHolder);
        }

        /// <summary>
        /// Sets whether or not this player is a minority holder for the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for whom we are updating minority holder status</param>
        /// <param name="isPlayerMinorityHolder">Whether or not this player is a minority holder</param>
        public void SetMinorityHolder(string companyName, bool isPlayerMinorityHolder)
        {
            isMinorityHolder[companyName.ToLower()] = isPlayerMinorityHolder;
        }

        /// <summary>
        /// Sets whether or not this player is a minority holder for the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for whom we are updating minority holder status</param>
        /// <param name="isPlayerMinorityHolder">Whether or not this player is a minority holder</param>
        public void SetMinorityHolder(Company company, bool isPlayerMinorityHolder)
        {
            SetMinorityHolder(company.Name, isPlayerMinorityHolder);
        }

        #endregion

        #region Boolean Methods

        /// <summary>
        /// Returns whether or not the pPlayer can place a square
        /// </summary>
        /// 
        /// <returns>Whether or not the pPlayer can place a square</returns>
        /// 
        public bool CanPlaceSquare()
        {
            if (canPlaceSquare && !Squares.Any())
            {
                canPlaceSquare = false;
            }

            return canPlaceSquare;
        }

        /// <summary>
        /// Sets whether or not the player can place a square to <paramref name="canPlayerPlaceSquare"/>
        /// </summary>
        /// 
        /// <param name="canPlayerPlaceSquare">Whether or not the player can place a square</param>
        public void CanPlaceSquare(bool canPlayerPlaceSquare)
        {
            canPlaceSquare = canPlayerPlaceSquare;
        }

        /// <summary>
        /// Returns whether or not this player has the given <paramref name="square"/>
        /// </summary>
        /// 
        /// <param name="square">The square being searched for</param>
        /// 
        /// <returns>Whether or not this player has the given square</returns>
        public bool HasSquare(Square square) => Squares.Contains(square);

        /// <summary>
        /// Returns whether or not the pPlayer is a majority holder of the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a majority holder of the company named <paramref name="companyName"/></returns>
        public bool IsMajorityHolder(string companyName) => isMajorityHolder[companyName.ToLower()];

        /// <summary>
        /// Returns whether or not the pPlayer is a majority holder of the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a majority holder of the company <paramref name="company"/></returns>
        public bool IsMajorityHolder(Company company) => IsMajorityHolder(company.Name);

        /// <summary>
        /// Returns whether or not the pPlayer is a minority holder of the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for which we are finding if this player is a minority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a minority holder of the company named <paramref name="companyName"/></returns>
        public bool IsMinorityHolder(string companyName) => isMinorityHolder[companyName.ToLower()];

        /// <summary>
        /// Returns whether or not the pPlayer is a minority holder of the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a minority holder of the company <paramref name="company"/></returns>
        public bool IsMinorityHolder(Company company) => IsMinorityHolder(company.Name);

        #endregion
    }
}
