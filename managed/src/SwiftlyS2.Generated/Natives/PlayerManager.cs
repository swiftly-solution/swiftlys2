#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativePlayerManager
{

    private unsafe static delegate* unmanaged<int> _GetPlayerCount;

    public unsafe static int GetPlayerCount()
    {
        var ret = _GetPlayerCount();
        return ret;
    }

    private unsafe static delegate* unmanaged<int> _GetPlayerCap;

    public unsafe static int GetPlayerCap()
    {
        var ret = _GetPlayerCap();
        return ret;
    }

    private unsafe static delegate* unmanaged<int, byte*, int, void> _SendMessage;

    public unsafe static void SendMessage(int kind, string message, int duration)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var messageStr = new ScopedCString(message);
        fixed (byte* messageBufferPtr = messageStr)
        {
            _SendMessage(kind, messageBufferPtr, duration);
        }
    }

    private unsafe static delegate* unmanaged<int, byte, void> _ShouldBlockTransmitEntity;

    public unsafe static void ShouldBlockTransmitEntity(int entityidx, bool shouldBlockTransmit)
    {
        _ShouldBlockTransmitEntity(entityidx, shouldBlockTransmit ? (byte)1 : (byte)0);
    }

    private unsafe static delegate* unmanaged<void> _ClearAllBlockedTransmitEntity;

    public unsafe static void ClearAllBlockedTransmitEntity()
    {
        _ClearAllBlockedTransmitEntity();
    }
}