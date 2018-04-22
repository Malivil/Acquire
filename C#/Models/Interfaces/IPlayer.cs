using System.Collections.Generic;
using Acquire.Components;
using Acquire.Enums;

namespace Acquire.Models.Interfaces
{
    public interface IPlayer
    {
        #region Member Variables

        /// <summary>
        /// This player's name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This player's Unique ID
        /// </summary>
        string PlayerId { get; }

        /// <summary>
        /// The type of player this is
        /// </summary>
        PlayerType Type { get; }

        /// <summary>
        /// Whether this player is the host of the game
        /// </summary>
        bool IsHost { get; }

        /// <summary>
        /// The amount of money this player has
        /// </summary>
        int Money { get; }

        /// <summary>
        /// The number of buys this player has left in this turn
        /// </summary>
        int NumBuysLeft { get; set; }

        /// <summary>
        /// List of all the squares this player has
        /// </summary>
        List<Square> Squares { get; set; }

        #endregion

        #region Action Methods

        /// <summary>
        /// Buys one share of the given company
        /// </summary>
        /// 
        /// <param name="company">The company from which to buy a share</param>
        /// <param name="openingShare">Whether or not the share being bought is an opening share</param>
        void BuyShare(Company company, bool openingShare);

        /// <summary>
        /// Sells a single share of the given company
        /// </summary>
        /// 
        /// <param name="company">The company from which to sell a single share</param>
        /// <param name="isMerging">Whether or not this company is merging with another one</param>
        /// <param name="companySize">The size of the company at the time of selling</param>
        void SellShare(Company company, bool isMerging, int companySize);

        /// <summary>
        /// Sells multiple shares of the given company
        /// </summary>
        ///
        /// <param name="company">The company from which to sell <paramref name="numShares"/> shares</param>
        /// <param name="numShares">The number of shares being sold</param>
        /// <param name="isMerging">Whether or not this company is merging with another one</param>
        ///
        void SellShares(Company company, int numShares, bool isMerging);

        /// <summary>
        /// Trades the amount of shares of <paramref name="deadCompany"/> given by <paramref name="numShares"/> to shares of <paramref name="liveCompany"/>
        /// </summary>
        /// 
        /// <param name="deadCompany">The company being destroyed whose shares are being traded</param>
        /// <param name="liveCompany">The company to which the old shares are being traded</param>
        /// <param name="numShares">The amount of shares to be traded</param>
        void TradeShares(Company deadCompany, Company liveCompany, int numShares);

        /// <summary>
        /// Gives the amount designated by <paramref name="money"/> to the player
        /// </summary>
        /// 
        /// <param name="money">The amount of money to give the player</param>
        void GiveMoney(int money);

        /// <summary>
        /// Prints out the status of this company to the console
        /// </summary>
        void PrintStatus();

        /// <summary>
        /// Removes all unusable squares from the player's hand
        /// </summary>
        void RemoveDeadSquares();

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the amount of shares the player has in the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <returns>The amount of shares the player has in the company named <paramref name="companyName"/></returns>
        int GetShares(string companyName);

        /// <summary>
        /// Returns the amount of shares the player has in the <paramref name="company"/>
        /// </summary>
        /// 
        /// <returns>The amount of shares the player has in the <paramref name="company"/></returns>
        int GetShares(Company company);

        #endregion

        #region Set Methods

        /// <summary>
        /// Sets the amount of shares this player has in the company named <paramref name="companyName"/> to <paramref name="numShares"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company whose shares we are updating</param>
        /// <param name="numShares">The amount of shares to be stored for the given company</param>
        void SetShares(string companyName, int numShares);

        /// <summary>
        /// Sets the amount of shares this player has in the company <paramref name="company"/> to <paramref name="numShares"/>
        /// </summary>
        /// 
        /// <param name="company">The company whose shares we are updating</param>
        /// <param name="numShares">The amount of shares to be stored for the given company</param>
        void SetShares(Company company, int numShares);

        /// <summary>
        /// Sets whether or not this player is a majority holder for the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for whom we are updating majority holder status</param>
        /// <param name="isPlayerMajorityHolder">Whether or not this player is a majority holder</param>
        void SetMajorityHolder(string companyName, bool isPlayerMajorityHolder);

        /// <summary>
        /// Sets whether or not this player is a majority holder for the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for whom we are updating majority holder status</param>
        /// <param name="isPlayerMajorityHolder">Whether or not this player is a majority holder</param>
        void SetMajorityHolder(Company company, bool isPlayerMajorityHolder);

        /// <summary>
        /// Sets whether or not this player is a minority holder for the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for whom we are updating minority holder status</param>
        /// <param name="isPlayerMinorityHolder">Whether or not this player is a minority holder</param>
        void SetMinorityHolder(string companyName, bool isPlayerMinorityHolder);

        /// <summary>
        /// Sets whether or not this player is a minority holder for the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for whom we are updating minority holder status</param>
        /// <param name="isPlayerMinorityHolder">Whether or not this player is a minority holder</param>
        void SetMinorityHolder(Company company, bool isPlayerMinorityHolder);

        #endregion

        #region Boolean Methods

        /// <summary>
        /// Returns whether or not the pPlayer can place a square
        /// </summary>
        /// 
        /// <returns>Whether or not the pPlayer can place a square</returns>
        /// 
        bool CanPlaceSquare();

        /// <summary>
        /// Sets whether or not the player can place a square to <paramref name="canPlayerPlaceSquare"/>
        /// </summary>
        /// 
        /// <param name="canPlayerPlaceSquare">Whether or not the player can place a square</param>
        void CanPlaceSquare(bool canPlayerPlaceSquare);

        /// <summary>
        /// Returns whether or not this player has the given <paramref name="square"/>
        /// </summary>
        /// 
        /// <param name="square">The square being searched for</param>
        /// 
        /// <returns>Whether or not this player has the given square</returns>
        bool HasSquare(Square square);

        /// <summary>
        /// Returns whether or not the pPlayer is a majority holder of the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a majority holder of the company named <paramref name="companyName"/></returns>
        bool IsMajorityHolder(string companyName);

        /// <summary>
        /// Returns whether or not the pPlayer is a majority holder of the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a majority holder of the company <paramref name="company"/></returns>
        bool IsMajorityHolder(Company company);

        /// <summary>
        /// Returns whether or not the pPlayer is a minority holder of the company named <paramref name="companyName"/>
        /// </summary>
        /// 
        /// <param name="companyName">The name of the company for which we are finding if this player is a minority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a minority holder of the company named <paramref name="companyName"/></returns>
        bool IsMinorityHolder(string companyName);

        /// <summary>
        /// Returns whether or not the pPlayer is a minority holder of the company <paramref name="company"/>
        /// </summary>
        /// 
        /// <param name="company">The company for which we are finding if this player is a majority holder</param>
        /// 
        /// <returns>Whether or not the pPlayer is a minority holder of the company <paramref name="company"/></returns>
        bool IsMinorityHolder(Company company);

        #endregion
    }
}
