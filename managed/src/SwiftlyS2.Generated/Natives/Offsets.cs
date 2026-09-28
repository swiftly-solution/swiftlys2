#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeOffsets
{

    private unsafe static delegate* unmanaged<byte*, byte> _Exists;

    public unsafe static bool Exists(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            var ret = _Exists(nameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, int> _Fetch;

    public unsafe static int Fetch(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            var ret = _Fetch(nameBufferPtr);
            return ret;
        }
    }
}