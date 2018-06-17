using System.Collections.Generic;

namespace Acquire.NetworkModels
{
    public class PlayerRename : AcquireNetworkModel
    {
        /// <summary>
        /// The list of players to rename and their information
        /// </summary>
        public List<PlayerRenameItem> PlayerRenames { get; set; } = new List<PlayerRenameItem>();
    }
}
