#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeMemoryHelpers
{

    private unsafe static delegate* unmanaged<byte*, nint> _FetchInterfaceByName;

    /// <summary>
    /// supports both internal interface system, but also valve interface system
    /// </summary>
    public unsafe static nint FetchInterfaceByName(string ifaceName)
    {
        using var ifaceNameStr = new ScopedCString(ifaceName);
        fixed (byte* ifaceNameBufferPtr = ifaceNameStr)
        {
            var ret = _FetchInterfaceByName(ifaceNameBufferPtr);
            return ret;
        }
    }
}