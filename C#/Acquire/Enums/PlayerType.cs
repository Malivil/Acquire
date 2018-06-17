namespace Acquire.Enums
{
    public enum PlayerType
    {
        /// <summary>
        /// Player on this computer providing input directly rather than over a network
        /// </summary>
        Local = 0,
        /// <summary>
        /// Player remotely connected to this computer either as the host or a client of this or another host
        /// </summary>
        Remote = 1,
        /// <summary>
        /// Artificial player using some pre-coded logic to perform moves
        /// </summary>
        AI = 2
    }
}
