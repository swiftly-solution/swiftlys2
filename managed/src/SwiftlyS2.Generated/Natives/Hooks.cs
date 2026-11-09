#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeHooks
{

    private unsafe static delegate* unmanaged<nint> _AllocateHook;

    public unsafe static nint AllocateHook()
    {
        var ret = _AllocateHook();
        return ret;
    }

    private unsafe static delegate* unmanaged<nint> _AllocateMHook;

    public unsafe static nint AllocateMHook()
    {
        var ret = _AllocateMHook();
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, void> _DeallocateHook;

    public unsafe static void DeallocateHook(nint hook)
    {
        _DeallocateHook(hook);
    }

    private unsafe static delegate* unmanaged<nint, void> _DeallocateMHook;

    public unsafe static void DeallocateMHook(nint hook)
    {
        _DeallocateMHook(hook);
    }

    private unsafe static delegate* unmanaged<nint, nint, nint, void> _SetHook;

    /// <summary>
    /// the callback should receive the exact arguments as the function has, and to return the same amount of arguments
    /// </summary>
    public unsafe static void SetHook(nint hook, nint func, nint callback)
    {
        _SetHook(hook, func, callback);
    }

    private unsafe static delegate* unmanaged<nint, nint, nint, void> _SetMHook;

    /// <summary>
    /// the callback should receive `ref Context64`
    /// </summary>
    public unsafe static void SetMHook(nint hook, nint addr, nint callback)
    {
        _SetMHook(hook, addr, callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _EnableHook;

    public unsafe static void EnableHook(nint hook)
    {
        _EnableHook(hook);
    }

    private unsafe static delegate* unmanaged<nint, void> _EnableMHook;

    public unsafe static void EnableMHook(nint hook)
    {
        _EnableMHook(hook);
    }

    private unsafe static delegate* unmanaged<nint, void> _DisableMHook;

    public unsafe static void DisableMHook(nint hook)
    {
        _DisableMHook(hook);
    }

    private unsafe static delegate* unmanaged<nint, nint> _GetHookOriginal;

    public unsafe static nint GetHookOriginal(nint hook)
    {
        var ret = _GetHookOriginal(hook);
        return ret;
    }
}