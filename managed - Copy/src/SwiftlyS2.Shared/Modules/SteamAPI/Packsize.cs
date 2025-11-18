using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace SwiftlyS2.Shared.SteamAPI;

public static class Packsize
{
    public const int value = 4;

    public static bool Test()
    {
        int sentinelSize = Marshal.SizeOf(typeof(ValvePackingSentinel_t));
        int subscribedFilesSize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
        if (sentinelSize != 24 || subscribedFilesSize != (1 + 1 + 1 + 50 + 100) * 4)
            return false;
        return true;
    }

    [StructLayout(LayoutKind.Sequential, Pack = value)]
    struct ValvePackingSentinel_t
    {
        uint m_u32;
        ulong m_u64;
        ushort m_u16;
        double m_d;
    };
}