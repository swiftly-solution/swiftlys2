
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBidirMsg_PredictionEventImpl : TypedProtobuf<CBidirMsg_PredictionEvent>, CBidirMsg_PredictionEvent
{
  public CBidirMsg_PredictionEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint EventId
  { get => Accessor.GetUInt32("event_id"); set => Accessor.SetUInt32("event_id", value); }


  public byte[] EventData
  { get => Accessor.GetBytes("event_data"); set => Accessor.SetBytes("event_data", value); }


  public uint SyncType
  { get => Accessor.GetUInt32("sync_type"); set => Accessor.SetUInt32("sync_type", value); }


  public uint SyncValUint32
  { get => Accessor.GetUInt32("sync_val_uint32"); set => Accessor.SetUInt32("sync_val_uint32", value); }

}
