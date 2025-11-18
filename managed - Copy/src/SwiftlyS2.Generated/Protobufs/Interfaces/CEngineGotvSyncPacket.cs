
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEngineGotvSyncPacket : ITypedProtobuf<CEngineGotvSyncPacket>
{
  static CEngineGotvSyncPacket ITypedProtobuf<CEngineGotvSyncPacket>.Wrap(nint handle, bool isManuallyAllocated) => new CEngineGotvSyncPacketImpl(handle, isManuallyAllocated);


  public ulong MatchId { get; set; }


  public uint InstanceId { get; set; }


  public uint Signupfragment { get; set; }


  public uint Currentfragment { get; set; }


  public float Tickrate { get; set; }


  public uint Tick { get; set; }


  public float Rtdelay { get; set; }


  public float Rcvage { get; set; }


  public float KeyframeInterval { get; set; }


  public uint Cdndelay { get; set; }

}
