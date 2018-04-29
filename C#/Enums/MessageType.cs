namespace Acquire.Enums
{
    public enum MessageType
    {
        Unknown,
        Connect,
        PlayerListRequest,
        PlayerListResponse,
        PlayerMessage,
        TurnNotification,
        TurnComplete,
        GameStart,
        GameOver
    }
}
