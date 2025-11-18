
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgConnectionStatusImpl : TypedProtobuf<CMsgConnectionStatus>, CMsgConnectionStatus
{
  public CMsgConnectionStatusImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public GCConnectionStatus Status
  { get => (GCConnectionStatus)Accessor.GetInt32("status"); set => Accessor.SetInt32("status", (int)value); }


  public uint ClientSessionNeed
  { get => Accessor.GetUInt32("client_session_need"); set => Accessor.SetUInt32("client_session_need", value); }


  public int QueuePosition
  { get => Accessor.GetInt32("queue_position"); set => Accessor.SetInt32("queue_position", value); }


  public int QueueSize
  { get => Accessor.GetInt32("queue_size"); set => Accessor.SetInt32("queue_size", value); }


  public int WaitSeconds
  { get => Accessor.GetInt32("wait_seconds"); set => Accessor.SetInt32("wait_seconds", value); }


  public int EstimatedWaitSecondsRemaining
  { get => Accessor.GetInt32("estimated_wait_seconds_remaining"); set => Accessor.SetInt32("estimated_wait_seconds_remaining", value); }

}
