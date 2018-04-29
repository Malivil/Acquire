using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;
using Newtonsoft.Json;

namespace Acquire.Models
{
    public class HostPlayer : Player, IHostPlayer
    {
        #region Public Variables

        [JsonIgnore]
        public IPEndPoint Endpoint => Utilities.ParseIPEndPoint(Address);
        public string Address { get; }

        #endregion

        public HostPlayer(string playerId, string address, string name) : base(name, PlayerType.Local, playerId, true)
        {
            Address = address;
        }
    }
}
