using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

public static class Packsize
{
    public const int value = 4;

    public static bool Test()
    {
        var sentinelSize = Marshal.SizeOf(typeof(ValvePackingSentinel_t));
        var subscribedFilesSize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
        return sentinelSize == 24 && subscribedFilesSize == (1 + 1 + 1 + 50 + 100) * 4;
    }

    [StructLayout(LayoutKind.Sequential, Pack = value)]
    private struct ValvePackingSentinel_t
    {
        private readonly uint m_u32;
        private readonly ulong m_u64;
        private readonly ushort m_u16;
        private readonly double m_d;
    };
}