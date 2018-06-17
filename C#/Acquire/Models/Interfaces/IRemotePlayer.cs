using System.Net;

namespace Acquire.Models.Interfaces
{
    public interface IRemotePlayer : IPlayer
    {
        /// <summary>
        /// An <see cref="IPEndPoint"/> parsed from <see cref="Address"/>
        /// </summary>
        IPEndPoint Endpoint { get; }
        /// <summary>
        /// The network location that this remote player is connecting from
        /// </summary>
        string Address { get; }
    }
}
