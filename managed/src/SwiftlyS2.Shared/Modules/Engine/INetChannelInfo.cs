using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Engine;

public enum NetworkFlow : int
{
    Outgoing = 0,
    Incoming = 1,
    Max = 2
}

public interface INetChannelInfo
{
    public string Name { get; }
    public string Address { get; }

    public NetworkAddress RemoteAddress { get; }

    public float Time { get; }
    public float TimeConnected { get; }
    public int DataRate { get; }
    public bool IsLocalHost { get; }
    public bool IsLoopback { get; }
    public bool IsTimingOut { get; }
    public bool IsPlayback { get; }
    public float AvgLatency { get; }
    public float EngineLatency { get; }

    public float GetAvgLoss( NetworkFlow flow );
    public float GetAvgChoke( NetworkFlow flow );
    public float GetPeakJitter( NetworkFlow flow );
    public float GetAvgData( NetworkFlow flow );
    public float GetAvgPackets( NetworkFlow flow );
    public ulong GetTotalData( NetworkFlow flow );
    public int GetTotalPackets( NetworkFlow flow );
    public int GetSequenceNr( NetworkFlow flow );

    public float TimeSinceLastReceived { get; }

    public (float FrameTime, float FrameTimeStdDeviation, float FrameStartTimeStdDeviation) RemoteFramerate { get; }

    public float TimeoutSeconds { get; }
    public float TimeUntilTimeout { get; }

    public void SetInterpolationAmount( float InterpolationAmount, float UpdateRate );
    public void SetNumPredictionErrors( int num );
    public void SetShowNetMessages( bool show );
}