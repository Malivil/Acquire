using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;
using Newtonsoft.Json;

namespace Acquire.Models
{
    public class HostPlayer : Player, IHostPlayer
    {
        #region Public Variables

        /// <inheritdoc />
        [JsonIgnore]
        public IPEndPoint Endpoint => Utilities.ParseIPEndPoint(Address);
        /// <summary>
        /// The network location that this host is hosting on
        /// </summary>
        public string Address { get; }

        #endregion

        /// <summary>
        /// Creates an instance of a host player with the given parameters
        /// </summary>
        ///
        /// <param name="playerId">The unique id of this player.</param>
        /// <param name="address">The network address that this host is hosting on</param>
        /// <param name="name">The name of the player being created.</param>
        public HostPlayer(string playerId, string address, string name) : base(name, PlayerType.Local, playerId, true)
        {
            Address = address;
        }
    }
}
