using Acquire.Enums;

namespace Acquire.NetworkModels
{
    public class NetworkMessage
    {
        #region Public Variables

        /// <summary>
        /// What type of data this message represents
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// The data to send along with this message
        /// </summary>
        public AcquireNetworkModel Data { get; set; }

        #endregion

        /// <summary>
        /// Creates an empty message instance. Required for serialization
        /// </summary>
        public NetworkMessage() { }

        /// <summary>
        /// Creates a network message with the given parameters
        /// </summary>
        ///
        /// <param name="data">The data to send along with this message</param>
        /// <param name="type">What type of data this message represents</param>
        public NetworkMessage(AcquireNetworkModel data, MessageType type)
        {
            Data = data;
            MessageType = type;
        }
    }
}
