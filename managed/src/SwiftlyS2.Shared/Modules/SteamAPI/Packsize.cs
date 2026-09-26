using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

public static class Packsize
{
    #if WINDOWS
    public const int value = 8;
    #else
    public const int value = 4;
    #endif

    [StructLayout(LayoutKind.Sequential, Pack = value)]
    public struct ValvePackingSentinel_t
    {
        public uint m_u32;
        public ulong m_u64;
        public ushort m_u16;
        public double m_d;
    };
}