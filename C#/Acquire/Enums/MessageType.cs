namespace Acquire.Enums
{
    public enum MessageType
    {
        Unknown,
        Connect,
        Disconnect,
        PlayerListRequest,
        PlayerListResponse,
        PlayerMessage,
        PlayerRename,
        TurnNotification,
        TurnComplete,
        GameStart,
        GameOver
    }
}
