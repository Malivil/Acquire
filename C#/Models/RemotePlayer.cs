namespace Acquire.Models
{
    public class RemotePlayer : Player
    {
        #region Private Member Variables

        private readonly string address;

        #endregion

        public RemotePlayer(string playerId, string address, bool isHost) : base("Remote Player", REMOTE_PLAYER, playerId, isHost)
        {
            this.address = address;
        }
    }
}
