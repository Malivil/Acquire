namespace Acquire.NetworkModels
{
    public class Disconnect : AcquireNetworkModel
    {
        /// <summary>
        /// The reason for the disconnection
        /// </summary>
        public string Message { get; set; }
    }
}
