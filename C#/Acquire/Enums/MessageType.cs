namespace Acquire.Enums
{
    public enum MessageType
    {
        /// <summary>
        /// Unknown message
        /// </summary>
        Unknown,
        /// <summary>
        /// Initial connect
        /// </summary>
        Connect,
        /// <summary>
        /// Player disconnect
        /// </summary>
        Disconnect,
        /// <summary>
        /// Request of current player list
        /// </summary>
        PlayerListRequest,
        /// <summary>
        /// Response to <see cref="PlayerListRequest"/>
        /// </summary>
        PlayerListResponse,
        /// <summary>
        /// Player-typed message sent through the chat system
        /// </summary>
        PlayerMessage,
        /// <summary>
        /// Request from the server to rename a player (or multiple players)
        /// </summary>
        PlayerRename,
        /// <summary>
        /// Notification that it is a particular player's turn
        /// </summary>
        TurnNotification,
        /// <summary>
        /// Notification that the current turn is over
        /// </summary>
        TurnComplete,
        /// <summary>
        /// Notification of game start
        /// </summary>
        GameStart,
        /// <summary>
        /// Notification of game over
        /// </summary>
        GameOver
    }
}
