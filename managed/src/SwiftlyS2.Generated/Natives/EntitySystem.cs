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

    private unsafe static delegate* unmanaged<byte*, nint> _CreateEntityByName;

    public unsafe static nint CreateEntityByName(string name)
    {
        return StringAlloc.CreateCString(name, nameBufferPtr =>
        {
            var ret = _CreateEntityByName((byte*)nameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint, nint, nint, int, void> _AcceptInput;

    public unsafe static void AcceptInput(nint entity, string input, nint activator, nint caller, nint value, int outputID)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        StringAlloc.CreateCString(input, inputBufferPtr =>
        {
            _AcceptInput(entity, (byte*)inputBufferPtr, activator, caller, value, outputID);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint, nint, nint, float, void> _AddEntityIOEvent;

    public unsafe static void AddEntityIOEvent(nint entity, string input, nint activator, nint caller, nint value, float delay)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        StringAlloc.CreateCString(input, inputBufferPtr =>
        {
            _AddEntityIOEvent(entity, (byte*)inputBufferPtr, activator, caller, value, delay);
        });
    }

    private unsafe static delegate* unmanaged<nint> _GetEntitySystem;

    public unsafe static nint GetEntitySystem()
    {
        var ret = _GetEntitySystem();
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, byte*, nint, ulong> _HookEntityOutput;

    /// <summary>
    /// CEntityIOOutput*, string outputName, CEntityInstance* activator, CEntityInstance* caller, float delay -> int (HookResult)
    /// </summary>
    public unsafe static ulong HookEntityOutput(string className, string outputName, nint callback)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            return StringAlloc.CreateCString(outputName, outputNameBufferPtr =>
            {
                var ret = _HookEntityOutput((byte*)classNameBufferPtr, (byte*)outputNameBufferPtr, callback);
                return ret;
            });
        });
    }

    private unsafe static delegate* unmanaged<ulong, void> _UnhookEntityOutput;

    public unsafe static void UnhookEntityOutput(ulong hookid)
    {
        _UnhookEntityOutput(hookid);
    }

    private unsafe static delegate* unmanaged<byte> _IsValid;

    public unsafe static bool IsValid()
    {
        var ret = _IsValid();
        return ret == 1;
    }
}