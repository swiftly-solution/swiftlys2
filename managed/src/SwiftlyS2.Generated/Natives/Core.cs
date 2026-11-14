#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCore
{

    private unsafe static delegate* unmanaged<byte> _IsMainThread;

    public unsafe static bool IsMainThread()
    {
        var ret = _IsMainThread();
        return ret == 1;
    }
}