
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoAnimationDataImpl : TypedProtobuf<CDemoAnimationData>, CDemoAnimationData
{
  public CDemoAnimationDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EntityId
  { get => Accessor.GetInt32("entity_id"); set => Accessor.SetInt32("entity_id", value); }


  public int StartTick
  { get => Accessor.GetInt32("start_tick"); set => Accessor.SetInt32("start_tick", value); }


  public int EndTick
  { get => Accessor.GetInt32("end_tick"); set => Accessor.SetInt32("end_tick", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }


  public long DataChecksum
  { get => Accessor.GetInt64("data_checksum"); set => Accessor.SetInt64("data_checksum", value); }

}
