using System.Net;
using Acquire.Enums;

namespace Acquire.NetworkModels
{
    public class NetworkMessage
    {
        #region Public Variables

        public string SourceAddress { get; }
        public MessageType MessageType { get; }
        public AcquireNetworkModel Data { get; }

        #endregion

        public NetworkMessage(AcquireNetworkModel data, IPEndPoint endpoint, MessageType type)
        {
            Data = data;
            SourceAddress = $"{endpoint.Address}:{endpoint.Port}";
            MessageType = type;
        }
    }
}
