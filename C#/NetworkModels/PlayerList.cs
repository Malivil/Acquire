using System.Collections.Generic;
using Acquire.Models;

namespace Acquire.NetworkModels
{
    public class PlayerList : AcquireNetworkModel
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public int PlayerCount => Players?.Count ?? 0;
    }
}
