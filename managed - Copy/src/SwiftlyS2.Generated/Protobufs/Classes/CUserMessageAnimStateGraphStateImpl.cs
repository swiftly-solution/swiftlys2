
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageAnimStateGraphStateImpl : TypedProtobuf<CUserMessageAnimStateGraphState>, CUserMessageAnimStateGraphState
{
  public CUserMessageAnimStateGraphStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EntityIndex
  { get => Accessor.GetInt32("entity_index"); set => Accessor.SetInt32("entity_index", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
