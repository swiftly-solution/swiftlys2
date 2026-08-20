#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEntitySystem
{

    private unsafe static delegate* unmanaged<nint, nint, void> _Spawn;

    public unsafe static void Spawn(nint entity, nint keyvalues)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _Spawn(entity, keyvalues);
    }

    private unsafe static delegate* unmanaged<nint, void> _Despawn;

    public unsafe static void Despawn(nint entity)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _Despawn(entity);
    }

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