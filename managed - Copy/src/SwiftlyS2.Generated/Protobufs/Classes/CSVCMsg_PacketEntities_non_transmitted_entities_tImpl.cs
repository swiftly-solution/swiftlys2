
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PacketEntities_non_transmitted_entities_tImpl : TypedProtobuf<CSVCMsg_PacketEntities_non_transmitted_entities_t>, CSVCMsg_PacketEntities_non_transmitted_entities_t
{
  public CSVCMsg_PacketEntities_non_transmitted_entities_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int HeaderCount
  { get => Accessor.GetInt32("header_count"); set => Accessor.SetInt32("header_count", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
