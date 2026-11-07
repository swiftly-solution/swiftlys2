namespace SwiftlyS2.Shared.Players;

public enum MessageType : byte
{
    Notify = 1,
    Console,
    Chat,
    Center,
    // Prefixes the message with `\x04` / (ChatColors.Green)
    ChatEOT,
    Alert,
    CenterHTML = 100
};
