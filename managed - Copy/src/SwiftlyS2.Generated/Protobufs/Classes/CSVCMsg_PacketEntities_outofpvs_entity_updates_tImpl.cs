
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PacketEntities_outofpvs_entity_updates_tImpl : TypedProtobuf<CSVCMsg_PacketEntities_outofpvs_entity_updates_t>, CSVCMsg_PacketEntities_outofpvs_entity_updates_t
{
  public CSVCMsg_PacketEntities_outofpvs_entity_updates_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Count
  { get => Accessor.GetInt32("count"); set => Accessor.SetInt32("count", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
