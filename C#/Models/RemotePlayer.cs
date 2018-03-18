namespace Acquire.Models
{
    public class RemotePlayer : Player
    {
        #region Private Member Variables

        private readonly string address;

        #endregion

        public RemotePlayer(string name, string playerId, string address) : base(name, REMOTE_PLAYER, playerId)
        {
            this.address = address;
        }
    }
}
