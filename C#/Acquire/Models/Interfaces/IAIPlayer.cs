using System.Collections.Generic;

namespace Acquire.Models.Interfaces
{
    public interface IAIPlayer : IPlayer
    {
        #region Action Methods

        /// <summary>
        /// Makes the AI player take a turn
        /// 
        /// Places the first square it finds and then ends the turn
        /// </summary>
        void TakeTurn();

        /// <summary>
        /// Chooses a company to form when merging/creating and returns that company.
        /// </summary>
        /// 
        /// <param name="companies">The possible companies to form/expand</param>
        /// 
        /// <returns>The chosen company to form/expand</returns>
        Company ChooseCompany(List<Company> companies);

        /// <summary>
        /// Handles the division of shares when merging two companies
        /// </summary>
        ///
        /// <param name="liveCompany">The company that will receive the merged results</param>
        /// <param name="deadCompany">The company that will be dissolved during the merge</param>
        void HandleMerge(Company liveCompany, Company deadCompany);

        #endregion
    }
}
