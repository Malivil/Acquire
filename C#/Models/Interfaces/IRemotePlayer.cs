using System.Net;

namespace Acquire.Models.Interfaces
{
    public interface IRemotePlayer : IPlayer
    {
        IPEndPoint Address { get; }
    }
}
