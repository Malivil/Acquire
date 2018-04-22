using System.Net;
using Acquire.Enums;
using Acquire.Models.Interfaces;

namespace Acquire.Models
{
    public class RemotePlayer : Player, IRemotePlayer
    {
        #region Public Variables

        public IPEndPoint Address { get; }

        #endregion

        public RemotePlayer(string playerId, IPEndPoint address, bool isHost, string name = "Remote Player") : base(name, PlayerType.Remote, playerId, isHost)
        {
            Address = address;
        }
    }
}
