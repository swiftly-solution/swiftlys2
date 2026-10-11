#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEntitySystem
{

    private unsafe static delegate* unmanaged<nint> _GetEntitySystem;

    public unsafe static nint GetEntitySystem()
    {
        var ret = _GetEntitySystem();
        return ret;
    }

    private unsafe static delegate* unmanaged<byte> _IsValid;

    public unsafe static bool IsValid()
    {
        var ret = _IsValid();
        return ret == 1;
    }
}