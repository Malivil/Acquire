using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class HostPlayer : Player, IHostPlayer
    {
        #region Public Variables

        public IPEndPoint Address { get; }

        #endregion

        public HostPlayer(string playerId, IPEndPoint address, string name) : base(name, PlayerType.Local, playerId, true)
        {
            Address = address;
        }
    }
}
