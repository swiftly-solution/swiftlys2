
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEngineGotvSyncPacketImpl : TypedProtobuf<CEngineGotvSyncPacket>, CEngineGotvSyncPacket
{
  public CEngineGotvSyncPacketImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public uint InstanceId
  { get => Accessor.GetUInt32("instance_id"); set => Accessor.SetUInt32("instance_id", value); }


  public uint Signupfragment
  { get => Accessor.GetUInt32("signupfragment"); set => Accessor.SetUInt32("signupfragment", value); }


  public uint Currentfragment
  { get => Accessor.GetUInt32("currentfragment"); set => Accessor.SetUInt32("currentfragment", value); }


  public float Tickrate
  { get => Accessor.GetFloat("tickrate"); set => Accessor.SetFloat("tickrate", value); }


  public uint Tick
  { get => Accessor.GetUInt32("tick"); set => Accessor.SetUInt32("tick", value); }


  public float Rtdelay
  { get => Accessor.GetFloat("rtdelay"); set => Accessor.SetFloat("rtdelay", value); }


  public float Rcvage
  { get => Accessor.GetFloat("rcvage"); set => Accessor.SetFloat("rcvage", value); }


  public float KeyframeInterval
  { get => Accessor.GetFloat("keyframe_interval"); set => Accessor.SetFloat("keyframe_interval", value); }


  public uint Cdndelay
  { get => Accessor.GetUInt32("cdndelay"); set => Accessor.SetUInt32("cdndelay", value); }

}
