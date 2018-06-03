using System.Collections.Generic;

namespace Acquire.NetworkModels
{
    public class PlayerRename : AcquireNetworkModel
    {
        public List<PlayerRenameItem> PlayerRenames { get; set; } = new List<PlayerRenameItem>();
    }
}
