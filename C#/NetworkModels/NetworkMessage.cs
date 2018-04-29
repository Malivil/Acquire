using Acquire.Enums;

namespace Acquire.NetworkModels
{
    public class NetworkMessage
    {
        #region Public Variables

        public MessageType MessageType { get; set; }
        public AcquireNetworkModel Data { get; set; }

        #endregion

        public NetworkMessage() { }

        public NetworkMessage(AcquireNetworkModel data, MessageType type)
        {
            Data = data;
            MessageType = type;
        }
    }
}
