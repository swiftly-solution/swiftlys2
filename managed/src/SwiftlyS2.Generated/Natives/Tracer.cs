#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeTracer
{

    private unsafe static delegate* unmanaged<void> _StartProfiling;

    public unsafe static void StartProfiling()
    {
        _StartProfiling();
    }

    private unsafe static delegate* unmanaged<void> _StopProfiling;

    public unsafe static void StopProfiling()
    {
        _StopProfiling();
    }

    private unsafe static delegate* unmanaged<byte*, byte*, void> _CustomBegin;

    public unsafe static void CustomBegin(string name, string source)
    {
        using var nameStr = new ScopedCString(name);
        using var sourceStr = new ScopedCString(source);
        fixed (byte* nameBufferPtr = nameStr)
        {
            fixed (byte* sourceBufferPtr = sourceStr)
            {
                _CustomBegin(nameBufferPtr, sourceBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, void> _CustomEnd;

    public unsafe static void CustomEnd(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            _CustomEnd(nameBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<byte, void> _SetObjectTracking;

    public unsafe static void SetObjectTracking(bool value)
    {
        _SetObjectTracking(value ? (byte)1 : (byte)0);
    }

    private unsafe static delegate* unmanaged<byte> _IsObjectTracking;

    public unsafe static bool IsObjectTracking()
    {
        var ret = _IsObjectTracking();
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<byte*, byte> _SaveProfile;

    public unsafe static bool SaveProfile(string path)
    {
        using var pathStr = new ScopedCString(path);
        fixed (byte* pathBufferPtr = pathStr)
        {
            var ret = _SaveProfile(pathBufferPtr);
            return ret == 1;
        }
    }
}