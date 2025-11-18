
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PacketEntities_alternate_baseline_tImpl : TypedProtobuf<CSVCMsg_PacketEntities_alternate_baseline_t>, CSVCMsg_PacketEntities_alternate_baseline_t
{
  public CSVCMsg_PacketEntities_alternate_baseline_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EntityIndex
  { get => Accessor.GetInt32("entity_index"); set => Accessor.SetInt32("entity_index", value); }


  public int BaselineIndex
  { get => Accessor.GetInt32("baseline_index"); set => Accessor.SetInt32("baseline_index", value); }

}
