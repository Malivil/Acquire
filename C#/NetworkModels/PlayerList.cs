using System.Collections.Generic;
using System.Linq;
using Acquire.Enums;
using Acquire.Models;

namespace Acquire.NetworkModels
{
    public class PlayerList : AcquireNetworkModel
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public int PlayerCount => Players?.Count ?? 0;
        public int LocalCount => Players?.Count(p => p.Type == PlayerType.Local) ?? 0;
        public int AICount => Players?.Count(p => p.Type == PlayerType.AI) ?? 0;
        public int RemoteCount => Players?.Count(p => p.Type == PlayerType.Remote) ?? 0;
    }
}
