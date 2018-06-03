namespace Acquire.Enums
{
    /// <summary>
    /// The state of the squares either on the board or in the player's hand
    /// </summary>
    public enum SquareState
    {
        /// <summary>
        /// Square has not been placed and is not going to be placeable. Generally due to company size safety rules
        /// </summary>
        Dead = 0,
        /// <summary>
        /// Square has not been placed but is not currently placeable
        /// </summary>
        Open = 1,
        /// <summary>
        /// Square has not been placed and is currently placeable
        /// </summary>
        Placeable = 2,
        /// <summary>
        /// Square has already been placed on the board
        /// </summary>
        Placed = 3
    }
}
