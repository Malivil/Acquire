using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;
using Newtonsoft.Json;

namespace Acquire.Models
{
    public class RemotePlayer : Player, IRemotePlayer
    {
        #region Public Variables

        [JsonIgnore]
        public IPEndPoint Endpoint => Utilities.ParseIPEndPoint(Address);
        public string Address { get; }

        #endregion

        public RemotePlayer(string playerId, string address, bool isHost, string name = "Remote Player") : base(name, PlayerType.Remote, playerId, isHost)
        {
            Address = address;
        }
    }
}
