using System.Collections.Generic;
using System.Drawing;
using Acquire.Components;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class Company : List<Square>
    {
        #region Private Member Variables

        // The quality of the company
        private readonly int quality;
        // Whether the company is safe
        private bool isSafe;

        // A list of the company's majority and minority holders
        private readonly List<IPlayer> majorityHolders;
        private readonly List<IPlayer> minorityHolders;

        #endregion

        #region Public Variables

        /// <summary>
        /// Name of the company
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The company ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Whether the company has been placed
        /// </summary>
        public bool IsPlaced { get; set; }

        /// <summary>
        /// Whether the company is safe
        /// </summary>
        public bool IsSafe
        {
            get => isSafe;
            set
            {
                // Only log this company as being safe if it is just being turned safe
                if (value && !isSafe)
                {
                    LogMaster.Log($"{Name} is now safe.");
                }

                isSafe = value;
            }
        }

        /// <summary>
        /// The company's icon
        /// </summary>
        public Image ImageIcon { get; }

        /// <summary>
        /// The size of the company
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The number of shares available for this company
        /// </summary>
        public int Shares { get; set; } = 30;

        /// <summary>
        /// The square color that represents this company
        /// </summary>
        public Color Color { get; }

        #endregion

        /// <summary>
        /// Creates a new instance of Company with no Majority or Minority holders
        /// having the given name name, ID <paramref name="id"/> and quality <paramref name="quality"/>.
        /// </summary>
        /// 
        /// <param name="name">The name to be given to this new company</param>
        /// <param name="id">The ID # of this company</param>
        /// <param name="quality">The quality of this company</param>
        /// <param name="imageIcon">The icon to associate with this company</param>
        /// <param name="companyColor">The square color that represents this company</param>
        public Company(string name, int id, int quality, Image imageIcon, Color companyColor)
        {
            this.quality = quality;
            Name = name;
            Id = id;
            Color = companyColor;

            ImageIcon = imageIcon;
            majorityHolders = new List<IPlayer>();
            minorityHolders = new List<IPlayer>();
        }

        #region Action Methods

        /// <summary>
        /// Buys a single share of this company and returns the price.
        /// </summary>
        /// 
        /// <returns>The price of the share bought.</returns>
        public int BuyShare()
        {
            // Can only buy a share if we have some
            if (Shares > 0)
            {
                Shares--;
                return GetPrice();
            }

            return 0;
        }

        /// <summary>
        /// Sells a single share of this company at the price the stock is worth
        /// when the size is the given <paramref name="sellSize"/>. (Useful for selling a merging company,
        /// as it maintains the price even though the company is already effectively dissolved)
        /// </summary>
        /// 
        /// <param name="sellSize">The size that the company was before its shares were sold off.</param>
        /// 
        /// <returns>The amount of money the share is sold for.</returns>
        public int SellShare(int sellSize)
        {
            int currentSize = Size;
            Size = sellSize;

            Shares++;

            int price = GetPrice();
            Size = currentSize;

            return price;
        }

        /// <summary>
        /// Gives all holders (both Majority and Minority) their due bonuses
        /// </summary>
        /// 
        /// <param name="size">The size at which to give the holder bonus</param>
        public void GiveHolderBonus(int size)
        {
            // Store the current size so that the sell size can be used
            int currentSize = size;
            // Use the sell size for now
            Size = size;

            // Give all majority holders their bonus
            foreach (IPlayer player in majorityHolders)
            {
                player.GiveMoney(GetPrice() * 10 / majorityHolders.Count);
            }
            // Give all minority holders their bonus
            foreach (IPlayer player in minorityHolders)
            {
                player.GiveMoney(GetPrice() * 5 / majorityHolders.Count);
            }

            // Reset the size
            Size = currentSize;
            // Reset the majority and minority holders
            majorityHolders.Clear();
            minorityHolders.Clear();
        }

        /// <summary>
        /// Updates the list of majority and minority holders
        /// </summary>
        /// 
        /// <param name="currentPlayer">The current Player, potentially added to the list of holders if they qualify and are not in the list already.</param>
        /// <param name="logChanges">Whether to log the changes in majority and minority leaders</param>
        public void UpdateHolders(Player currentPlayer, bool logChanges = false)
        {
            // Store the current majority holders as "old"
            List<IPlayer> oldMajorityHolders = new List<IPlayer>(majorityHolders);
            // Store the current minority holders as "old"
            List<IPlayer> oldMinorityHolders = new List<IPlayer>(minorityHolders);
            // Add the majority and minority holders to the list of all of them
            List<IPlayer> majorityAndMinorityHolders = new List<IPlayer>(majorityHolders);
            majorityAndMinorityHolders.AddRange(minorityHolders);

            // I this pPlayer isn't already in there, add them now.
            if (!majorityAndMinorityHolders.Contains(currentPlayer))
            {
                majorityAndMinorityHolders.Add(currentPlayer);
            }

            // Set the majority and minority holder status for every pPlayer to false
            foreach (IPlayer player in majorityAndMinorityHolders)
            {
                player.SetMajorityHolder(Name, false);
                player.SetMinorityHolder(Name, false);
            }

            // Clear the current majority and minority holders
            majorityHolders.Clear();
            minorityHolders.Clear();

            // For each pPlayer being considered...
            foreach (IPlayer player in majorityAndMinorityHolders)
            {
                // Set the current majority to the lead holder if we have one
                int currentMajShares = majorityHolders.Count != 0 ? majorityHolders[0].GetShares(Name) : 0;

                // Set the current majority to the lead holder if we have one
                int currentMinShares = minorityHolders.Count != 0 ? minorityHolders[0].GetShares(Name) : 0;

                // If the pPlayer qualifies for a majority share...
                if (player.GetShares(Name) >= currentMajShares)
                {
                    // If the pPlayer beats out the current leader
                    if (player.GetShares(Name) > currentMajShares)
                    {
                        // Reset both lists
                        minorityHolders.Clear();
                        // And add the majority leaders to the minority leader list
                        minorityHolders.AddRange(majorityHolders);
                        majorityHolders.Clear();
                    }

                    // If the majority leader list doesn't contain this pPlayer, add it.
                    if (!majorityHolders.Contains(player))
                    {
                        majorityHolders.Add(player);
                    }
                }
                // If the pPlayer qualifies for a minority share
                else if (player.GetShares(Name) >= currentMinShares && player.GetShares(Name) < currentMajShares)
                {
                    // If the pPlayer beats out the current minority leader, clear the minority leaders
                    if (player.GetShares(Name) > currentMinShares)
                    {
                        minorityHolders.Clear();
                    }

                    // Add this pPlayer if they aren't in the list already
                    if (!minorityHolders.Contains(player))
                    {
                        minorityHolders.Add(player);
                    }
                }
            }

            // If there are no minority leaders, the majority leaders become minority leaders too
            if (minorityHolders.Count == 0 && majorityHolders.Count != 0)
            {
                minorityHolders.AddRange(majorityHolders);
            }

            // Make sure each majority leader knows they are one
            foreach (IPlayer player in majorityHolders)
            {
                player.SetMajorityHolder(Name, true);

                if (logChanges && !oldMajorityHolders.Contains(player))
                {
                    LogMaster.Log(player.Name + " is now a majority leader of " + Name + ".");
                }
            }

            // Make sure each minority leader knows they are one
            foreach (IPlayer player in minorityHolders)
            {
                player.SetMinorityHolder(Name, true);

                if (logChanges && !oldMinorityHolders.Contains(player))
                {
                    LogMaster.Log(player.Name + " is now a minority leader of " + Name + ".");
                }
            }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Calculates and returns the company's current price
        /// </summary>
        /// 
        /// <returns>The company's current price as calculated</returns>
        public int GetPrice()
        {
            // 0 is the default price
            int price = 0;

            // Calculate the price depending on the size
            if (Size > 0 && Size < 6)
            {
                price = 100 * (Size + quality);
            }
            else if (Size >= 6 && Size <= 10)
            {
                price = 600 + quality * 100;
            }
            else if (Size >= 11 && Size <= 20)
            {
                price = 700 + quality * 100;
            }
            else if (Size >= 21 && Size <= 30)
            {
                price = 800 + quality * 100;
            }
            else if (Size >= 31 && Size <= 40)
            {
                price = 900 + quality * 100;
            }
            else if (Size >= 41)
            {
                price = 1000 + quality * 100;
            }

            return price;
        }

        /// <summary>
        /// Calculates and returns the company's price based on the given <paramref name="size"/>
        /// </summary>
        /// 
        /// <param name="size">The size at which to price this company</param>
        /// 
        /// <returns>The company's current price as calculated</returns>
        public int GetPrice(int size)
        {
            int currentSize = Size;

            Size = size;
            int price = GetPrice();
            Size = currentSize;

            return price;
        }

        #endregion
    }
}