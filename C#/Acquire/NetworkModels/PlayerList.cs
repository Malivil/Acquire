using System.Collections.Generic;
using System.Linq;
using Acquire.Enums;
using Acquire.Models;

namespace Acquire.NetworkModels
{
    public class PlayerList : AcquireNetworkModel
    {
        /// <summary>
        /// The list of players being sent
        /// </summary>
        public List<Player> Players { get; set; } = new List<Player>();
        /// <summary>
        /// How many players there are
        /// </summary>
        public int PlayerCount => Players?.Count ?? 0;
        /// <summary>
        /// How many players there are with the <see cref="PlayerType.Local"/> type
        /// </summary>
        public int LocalCount => Players?.Count(p => p.Type == PlayerType.Local) ?? 0;
        /// <summary>
        /// How many players there are with the <see cref="PlayerType.AI"/> type
        /// </summary>
        public int AICount => Players?.Count(p => p.Type == PlayerType.AI) ?? 0;
        /// <summary>
        /// How many players there are with the <see cref="PlayerType.Remote"/> type
        /// </summary>
        public int RemoteCount => Players?.Count(p => p.Type == PlayerType.Remote) ?? 0;
    }
}
