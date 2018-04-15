using System.Net;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class HostPlayer : Player, IHostPlayer
    {
        #region Public Variables

        public IPEndPoint Address { get; }

        #endregion

        public HostPlayer(string playerId, IPEndPoint address, string name) : base(name, LOCAL_PLAYER, playerId, true)
        {
            Address = address;
        }
    }
}
