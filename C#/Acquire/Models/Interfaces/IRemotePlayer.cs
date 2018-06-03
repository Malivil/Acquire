using System.Net;

namespace Acquire.Models.Interfaces
{
    public interface IRemotePlayer : IPlayer
    {
        IPEndPoint Endpoint { get; }
        string Address { get; }
    }
}
