using System.Net;
using Acquire.Enums;

namespace Acquire.NetworkModels
{
    public class NetworkMessage
    {
        #region Public Variables

        public string SourceAddress { get; set; }
        public MessageType MessageType { get; set; }
        public AcquireNetworkModel Data { get; set; }

        #endregion

        public NetworkMessage() { }

        public NetworkMessage(AcquireNetworkModel data, IPEndPoint endpoint, MessageType type)
        {
            Data = data;
            SourceAddress = $"{endpoint.Address}:{endpoint.Port}";
            MessageType = type;
        }
    }
}
