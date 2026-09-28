namespace SwiftlyS2.Shared.SteamAPI;

public static class Packsize
{
    #if WINDOWS
    public const int value = 8;
    #else
    public const int value = 4;
    #endif
}