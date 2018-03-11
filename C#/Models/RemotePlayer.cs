namespace Acquire.Models
{
    public class RemotePlayer : Player
    {
        #region Private Member Variables

        private readonly string address;

        #endregion

        public RemotePlayer(string name, string address) :
            base(name, REMOTE_PLAYER)
        {
            this.address = address;
        }
    }
}
