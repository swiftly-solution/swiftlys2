#pragma warning disable CS0649
#pragma warning disable CS0169

namespace SwiftlyS2.Core.Natives;

internal static class NativeVoiceManager
{
    private static readonly unsafe delegate* unmanaged< int, int, int, void > _SetClientListenOverride;

    public unsafe static void SetClientListenOverride( int playerid, int targetid, int listenOverride )
    {
        _SetClientListenOverride(playerid, targetid, listenOverride);
    }

    private static readonly unsafe delegate* unmanaged< int, int, int > _GetClientListenOverride;

    public unsafe static int GetClientListenOverride( int playerid, int targetid )
    {
        var ret = _GetClientListenOverride(playerid, targetid);
        return ret;
    }

    private static readonly unsafe delegate* unmanaged< int, int, void > _SetClientVoiceFlags;

    public unsafe static void SetClientVoiceFlags( int playerid, int flags )
    {
        _SetClientVoiceFlags(playerid, flags);
    }

    private static readonly unsafe delegate* unmanaged< int, int > _GetClientVoiceFlags;

    public unsafe static int GetClientVoiceFlags( int playerid )
    {
        var ret = _GetClientVoiceFlags(playerid);
        return ret;
    }
}