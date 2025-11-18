
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoAnimationHeaderImpl : TypedProtobuf<CDemoAnimationHeader>, CDemoAnimationHeader
{
  public CDemoAnimationHeaderImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EntityId
  { get => Accessor.GetInt32("entity_id"); set => Accessor.SetInt32("entity_id", value); }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
