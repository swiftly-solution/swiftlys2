namespace SwiftlyS2.Shared.Players;

public enum MessageType: byte
{
    Notify = 1,
    Console,
    Chat,
    Center,
    ChatEOT,
    Alert,
    CenterHTML = 100
};