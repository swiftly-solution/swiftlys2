using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Engine;

namespace SwiftlyS2.Core.Engine;

internal static class INetChannelVTable
{
    public static Lazy<int> CanPacketLazy = new(() => NativeOffsets.Fetch("CNetChan::CanPacket"));
    public static int CanPacketOffset => CanPacketLazy.Value;
    public static unsafe bool CanPacket( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, CanPacketOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsOverflowedLazy = new(() => NativeOffsets.Fetch("CNetChan::IsOverflowed"));
    public static int IsOverflowedOffset => IsOverflowedLazy.Value;
    public static unsafe bool IsOverflowed( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsOverflowedOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> HasPendingReliableDataLazy = new(() => NativeOffsets.Fetch("CNetChan::HasPendingReliableData"));
    public static int HasPendingReliableDataOffset => HasPendingReliableDataLazy.Value;
    public static unsafe bool HasPendingReliableData( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, HasPendingReliableDataOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsTimedOutLazy = new(() => NativeOffsets.Fetch("CNetChan::IsTimedOut"));
    public static int IsTimedOutOffset => IsTimedOutLazy.Value;
    public static unsafe bool IsTimedOut( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsTimedOutOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsSuppressingTransmitLazy = new(() => NativeOffsets.Fetch("CNetChan::IsSuppressingTransmit"));
    public static int IsSuppressingTransmitOffset => IsSuppressingTransmitLazy.Value;
    public static unsafe bool IsSuppressingTransmit( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsSuppressingTransmitOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> SetMinDataRateLazy = new(() => NativeOffsets.Fetch("CNetChan::SetMinDataRate"));
    public static int SetMinDataRateOffset => SetMinDataRateLazy.Value;
    public static unsafe void SetMinDataRate( nint basePtr, int rate )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetMinDataRateOffset);
        ((delegate* unmanaged< nint, int, void >)vFunction)(basePtr, rate);
    }

    public static Lazy<int> SetMaxDataRateLazy = new(() => NativeOffsets.Fetch("CNetChan::SetMaxDataRate"));
    public static int SetMaxDataRateOffset => SetMaxDataRateLazy.Value;
    public static unsafe void SetMaxDataRate( nint basePtr, int rate )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetMaxDataRateOffset);
        ((delegate* unmanaged< nint, int, void >)vFunction)(basePtr, rate);
    }

    public static Lazy<int> SetTimeoutLazy = new(() => NativeOffsets.Fetch("CNetChan::SetTimeout"));
    public static int SetTimeoutOffset => SetTimeoutLazy.Value;
    public static unsafe void SetTimeout( nint basePtr, float seconds, bool bForceExact )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetTimeoutOffset);
        ((delegate* unmanaged< nint, float, byte, void >)vFunction)(basePtr, seconds, (byte)(bForceExact ? 1 : 0));
    }

    public static Lazy<int> SuppressTransmitLazy = new(() => NativeOffsets.Fetch("CNetChan::SuppressTransmit"));
    public static int SuppressTransmitOffset => SuppressTransmitLazy.Value;
    public static unsafe void SuppressTransmit( nint basePtr, bool suppress )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SuppressTransmitOffset);
        ((delegate* unmanaged< nint, byte, void >)vFunction)(basePtr, (byte)(suppress ? 1 : 0));
    }
}

internal class NetChannel : NetChannelInfo, INetChannel
{
    public bool CanPacket => INetChannelVTable.CanPacket(Ptr);

    public bool IsOverflowed => INetChannelVTable.IsOverflowed(Ptr);

    public bool HasPendingReliableData => INetChannelVTable.HasPendingReliableData(Ptr);

    public bool IsTimedOut => INetChannelVTable.IsTimedOut(Ptr);

    public bool IsSuppressingTransmit => INetChannelVTable.IsSuppressingTransmit(Ptr);

    public void SetMaxDataRate( int rate )
    {
        INetChannelVTable.SetMaxDataRate(Ptr, rate);
    }

    public void SetMinDataRate( int rate )
    {
        INetChannelVTable.SetMinDataRate(Ptr, rate);
    }

    public void SetTimeout( float seconds, bool forceExact = false )
    {
        INetChannelVTable.SetTimeout(Ptr, seconds, forceExact);
    }

    public void SuppressTransmit( bool suppress )
    {
        INetChannelVTable.SuppressTransmit(Ptr, suppress);
    }
}