using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;
using Newtonsoft.Json;

namespace Acquire.Models
{
    public class RemotePlayer : Player, IRemotePlayer
    {
        #region Public Variables

        /// <inheritdoc />
        [JsonIgnore]
        public IPEndPoint Endpoint => Utilities.ParseIPEndPoint(Address);
        /// <inheritdoc />
        public string Address { get; }

        #endregion

        /// <summary>
        /// Creates an instance of a remote player with the given parameters
        /// </summary>
        ///
        /// <param name="playerId">The unique id of this player.</param>
        /// <param name="address">The network address that this player is connecting from</param>
        /// <param name="isHost">Whether this player is the host of the game.</param>
        /// <param name="name">The name of the player being created.</param>
        public RemotePlayer(string playerId, string address, bool isHost, string name = "Remote Player") : base(name, PlayerType.Remote, playerId, isHost)
        {
            Address = address;
        }
    }
}
