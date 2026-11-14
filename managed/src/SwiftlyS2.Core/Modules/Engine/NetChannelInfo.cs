using SwiftlyS2.Shared.Engine;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Natives;

namespace SwiftlyS2.Core.Engine;

internal static class INetChannelInfoVTable
{
    public static Lazy<int> GetNameLazy = new(() => NativeOffsets.Fetch("CNetChan::GetName"));
    public static int GetNameOffset => GetNameLazy.Value;
    public static unsafe nint GetName( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetNameOffset);
        return ((delegate* unmanaged< nint, nint >)vFunction)(basePtr);
    }

    public static Lazy<int> GetAddressLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAddress"));
    public static int GetAddressOffset => GetAddressLazy.Value;
    public static unsafe nint GetAddress( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAddressOffset);
        return ((delegate* unmanaged< nint, nint >)vFunction)(basePtr);
    }

    public static Lazy<int> GetRemoteAddressLazy = new(() => NativeOffsets.Fetch("CNetChan::GetRemoteAddress"));
    public static int GetRemoteAddressOffset => GetRemoteAddressLazy.Value;
    public static unsafe NetworkAddress GetRemoteAddress( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRemoteAddressOffset);
        return *((delegate* unmanaged< nint, NetworkAddress* >)vFunction)(basePtr);
    }

    public static Lazy<int> GetTimeLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTime"));
    public static int GetTimeOffset => GetRemoteAddressLazy.Value;
    public static unsafe float GetTime( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRemoteAddressOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetTimeConnectedLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTimeConnected"));
    public static int GetTimeConnectedOffset => GetRemoteAddressLazy.Value;
    public static unsafe float GetTimeConnected( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRemoteAddressOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetDataRateLazy = new(() => NativeOffsets.Fetch("CNetChan::GetDataRate"));
    public static int GetDataRateOffset => GetRemoteAddressLazy.Value;
    public static unsafe int GetDataRate( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRemoteAddressOffset);
        return ((delegate* unmanaged< nint, int >)vFunction)(basePtr);
    }

    public static Lazy<int> IsLocalHostLazy = new(() => NativeOffsets.Fetch("CNetChan::IsLocalHost"));
    public static int IsLocalHostOffset => IsLocalHostLazy.Value;
    public static unsafe bool IsLocalHost( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsLocalHostOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsLoopbackLazy = new(() => NativeOffsets.Fetch("CNetChan::IsLoopback"));
    public static int IsLoopbackOffset => IsLoopbackLazy.Value;
    public static unsafe bool IsLoopback( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsLoopbackOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsTimingOutLazy = new(() => NativeOffsets.Fetch("CNetChan::IsTimingOut"));
    public static int IsTimingOutOffset => IsTimingOutLazy.Value;
    public static unsafe bool IsTimingOut( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsTimingOutOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> IsPlaybackLazy = new(() => NativeOffsets.Fetch("CNetChan::IsPlayback"));
    public static int IsPlaybackOffset => IsPlaybackLazy.Value;
    public static unsafe bool IsPlayback( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, IsPlaybackOffset);
        return ((delegate* unmanaged< nint, byte >)vFunction)(basePtr) == 1;
    }

    public static Lazy<int> GetAvgLatencyLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAvgLatency"));
    public static int GetAvgLatencyOffset => GetAvgLatencyLazy.Value;
    public static unsafe float GetAvgLatency( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAvgLatencyOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetEngineLatencyLazy = new(() => NativeOffsets.Fetch("CNetChan::GetEngineLatency"));
    public static int GetEngineLatencyOffset => GetEngineLatencyLazy.Value;
    public static unsafe float GetEngineLatency( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetEngineLatencyOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetAvgLossLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAvgLoss"));
    public static int GetAvgLossOffset => GetAvgLossLazy.Value;
    public static unsafe float GetAvgLoss( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAvgLossOffset);
        return ((delegate* unmanaged< nint, int, float >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetAvgChokeLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAvgChoke"));
    public static int GetAvgChokeOffset => GetAvgChokeLazy.Value;
    public static unsafe float GetAvgChoke( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAvgChokeOffset);
        return ((delegate* unmanaged< nint, int, float >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetPeakJitterLazy = new(() => NativeOffsets.Fetch("CNetChan::GetPeakJitter"));
    public static int GetPeakJitterOffset => GetPeakJitterLazy.Value;
    public static unsafe float GetPeakJitter( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetPeakJitterOffset);
        return ((delegate* unmanaged< nint, int, float >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetAvgDataLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAvgData"));
    public static int GetAvgDataOffset => GetAvgDataLazy.Value;
    public static unsafe float GetAvgData( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAvgDataOffset);
        return ((delegate* unmanaged< nint, int, float >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetAvgPacketsLazy = new(() => NativeOffsets.Fetch("CNetChan::GetAvgPackets"));
    public static int GetAvgPacketsOffset => GetAvgPacketsLazy.Value;
    public static unsafe float GetAvgPackets( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetAvgPacketsOffset);
        return ((delegate* unmanaged< nint, int, float >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetTotalDataLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTotalData"));
    public static int GetTotalDataOffset => GetTotalDataLazy.Value;
    public static unsafe ulong GetTotalData( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetTotalDataOffset);
        return ((delegate* unmanaged< nint, int, ulong >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetTotalPacketsLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTotalPackets"));
    public static int GetTotalPacketsOffset => GetTotalPacketsLazy.Value;
    public static unsafe int GetTotalPackets( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetTotalPacketsOffset);
        return ((delegate* unmanaged< nint, int, int >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetSequenceNrLazy = new(() => NativeOffsets.Fetch("CNetChan::GetSequenceNr"));
    public static int GetSequenceNrOffset => GetSequenceNrLazy.Value;
    public static unsafe int GetSequenceNr( nint basePtr, int flow )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetSequenceNrOffset);
        return ((delegate* unmanaged< nint, int, int >)vFunction)(basePtr, flow);
    }

    public static Lazy<int> GetTimeSinceLastReceivedLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTimeSinceLastReceived"));
    public static int GetTimeSinceLastReceivedOffset => GetTimeSinceLastReceivedLazy.Value;
    public static unsafe float GetTimeSinceLastReceived( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetTimeSinceLastReceivedOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> SetInterpolationAmountLazy = new(() => NativeOffsets.Fetch("CNetChan::SetInterpolationAmount"));
    public static int SetInterpolationAmountOffset => SetInterpolationAmountLazy.Value;
    public static unsafe void SetInterpolationAmount( nint basePtr, float flInterpolationAmount, float flUpdateRate )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetInterpolationAmountOffset);
        ((delegate* unmanaged< nint, float, float, void >)vFunction)(basePtr, flInterpolationAmount, flUpdateRate);
    }

    public static Lazy<int> SetNumPredictionErrorsLazy = new(() => NativeOffsets.Fetch("CNetChan::SetNumPredictionErrors"));
    public static int SetNumPredictionErrorsOffset => SetNumPredictionErrorsLazy.Value;
    public static unsafe void SetNumPredictionErrors( nint basePtr, int num )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetNumPredictionErrorsOffset);
        ((delegate* unmanaged< nint, int, void >)vFunction)(basePtr, num);
    }

    public static Lazy<int> SetShowNetMessagesLazy = new(() => NativeOffsets.Fetch("CNetChan::SetShowNetMessages"));
    public static int SetShowNetMessagesOffset => SetShowNetMessagesLazy.Value;
    public static unsafe void SetShowNetMessages( nint basePtr, bool show )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, SetShowNetMessagesOffset);
        ((delegate* unmanaged< nint, byte, void >)vFunction)(basePtr, (byte)(show ? 1 : 0));
    }

    public static Lazy<int> GetTimeoutSecondsLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTimeoutSeconds"));
    public static int GetTimeoutSecondsOffset => GetTimeoutSecondsLazy.Value;
    public static unsafe float GetTimeoutSeconds( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetTimeoutSecondsOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetTimeUntilTimeoutLazy = new(() => NativeOffsets.Fetch("CNetChan::GetTimeUntilTimeout"));
    public static int GetTimeUntilTimeoutOffset => GetTimeUntilTimeoutLazy.Value;
    public static unsafe float GetTimeUntilTimeout( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetTimeUntilTimeoutOffset);
        return ((delegate* unmanaged< nint, float >)vFunction)(basePtr);
    }

    public static Lazy<int> GetRemoteFramerateLazy = new(() => NativeOffsets.Fetch("CNetChan::GetRemoteFramerate"));
    public static int GetRemoteFramerateOffset => GetRemoteFramerateLazy.Value;
    public static unsafe (float FrameTime, float FrameTimeStdDeviation, float FrameStartTimeStdDeviation) GetRemoteFramerate( nint basePtr )
    {
        var vFunction = GameFunctions.GetVirtualFunction(basePtr, GetRemoteFramerateOffset);
        float frameTime = 0.0f, frameTimeStdDeviation = 0.0f, frameStartTimeStdDeviation = 0.0f;

        ((delegate* unmanaged< nint, float*, float*, float*, void >)vFunction)(basePtr, &frameTime, &frameTimeStdDeviation, &frameStartTimeStdDeviation);
        return (frameTime, frameTimeStdDeviation, frameStartTimeStdDeviation);
    }
}

internal class NetChannelInfo : INetChannelInfo
{
    internal nint Ptr { get; set; } = 0;

    internal void DangerouslySetPtr( nint ptr )
    {
        Ptr = ptr;
    }

    public string Name => StringAlloc.CreateCSharpString(INetChannelInfoVTable.GetName(Ptr));

    public string Address => StringAlloc.CreateCSharpString(INetChannelInfoVTable.GetAddress(Ptr));

    public NetworkAddress RemoteAddress => INetChannelInfoVTable.GetRemoteAddress(Ptr);

    public float Time => INetChannelInfoVTable.GetTime(Ptr);

    public float TimeConnected => INetChannelInfoVTable.GetTimeConnected(Ptr);

    public int DataRate => INetChannelInfoVTable.GetDataRate(Ptr);

    public bool IsLocalHost => INetChannelInfoVTable.IsLocalHost(Ptr);

    public bool IsLoopback => INetChannelInfoVTable.IsLoopback(Ptr);

    public bool IsTimingOut => INetChannelInfoVTable.IsTimingOut(Ptr);

    public bool IsPlayback => INetChannelInfoVTable.IsPlayback(Ptr);

    public float AvgLatency => INetChannelInfoVTable.GetAvgLatency(Ptr);

    public float EngineLatency => INetChannelInfoVTable.GetEngineLatency(Ptr);

    public float TimeSinceLastReceived => INetChannelInfoVTable.GetTimeSinceLastReceived(Ptr);

    public (float FrameTime, float FrameTimeStdDeviation, float FrameStartTimeStdDeviation) RemoteFramerate => INetChannelInfoVTable.GetRemoteFramerate(Ptr);

    public float TimeoutSeconds => INetChannelInfoVTable.GetTimeoutSeconds(Ptr);

    public float TimeUntilTimeout => INetChannelInfoVTable.GetTimeUntilTimeout(Ptr);

    public float GetAvgChoke( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetAvgChoke(Ptr, (int)flow);
    }

    public float GetPeakJitter( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetPeakJitter(Ptr, (int)flow);
    }

    public float GetAvgLoss( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetAvgLoss(Ptr, (int)flow);
    }

    public float GetAvgData( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetAvgData(Ptr, (int)flow);
    }

    public float GetAvgPackets( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetAvgPackets(Ptr, (int)flow);
    }

    public int GetSequenceNr( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetSequenceNr(Ptr, (int)flow);
    }

    public ulong GetTotalData( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetTotalData(Ptr, (int)flow);
    }

    public int GetTotalPackets( NetworkFlow flow )
    {
        return INetChannelInfoVTable.GetTotalPackets(Ptr, (int)flow);
    }

    public void SetInterpolationAmount( float InterpolationAmount, float UpdateRate )
    {
        INetChannelInfoVTable.SetInterpolationAmount(Ptr, InterpolationAmount, UpdateRate);
    }

    public void SetNumPredictionErrors( int num )
    {
        INetChannelInfoVTable.SetNumPredictionErrors(Ptr, num);
    }

    public void SetShowNetMessages( bool show )
    {
        INetChannelInfoVTable.SetShowNetMessages(Ptr, show);
    }
}