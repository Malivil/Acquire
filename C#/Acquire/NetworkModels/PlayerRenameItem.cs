namespace Acquire.NetworkModels
{
    public class PlayerRenameItem
    {
        /// <summary>
        /// The unique ID belonging to the player being renamed
        /// </summary>
        public string PlayerId { get; set; }
        /// <summary>
        /// The original name to change
        /// </summary>
        public string OriginalName { get; set; }
        /// <summary>
        /// The new name to give the player
        /// </summary>
        public string NewName { get; set; }
    }
}
