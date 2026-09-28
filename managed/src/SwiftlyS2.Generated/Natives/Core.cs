#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCore
{

    private unsafe static delegate* unmanaged<byte> _PluginManualLoadState;

    public unsafe static bool PluginManualLoadState()
    {
        var ret = _PluginManualLoadState();
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int*, byte*> _PluginLoadOrder;

    public unsafe static string PluginLoadOrder()
    {
        var length = 0;
        var returnedPtr = _PluginLoadOrder(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int> _GetProfilerLevelByDefault;

    public unsafe static int GetProfilerLevelByDefault()
    {
        return _GetProfilerLevelByDefault();
    }

    private unsafe static delegate* unmanaged<byte> _IsMainThread;

    public unsafe static bool IsMainThread()
    {
        var ret = _IsMainThread();
        return ret == 1;
    }
}