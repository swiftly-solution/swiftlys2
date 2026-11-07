namespace SwiftlyS2.Shared.Events;

public enum ClientKind
{
    Player = 0,
    Bot = 1,
    HLTV = 2,
    [Obsolete("Please use ClientKind.HLTV instead")]
    Unknown = HLTV
}