
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_PacketEntities_outofpvs_entity_updates_t : ITypedProtobuf<CSVCMsg_PacketEntities_outofpvs_entity_updates_t>
{
  static CSVCMsg_PacketEntities_outofpvs_entity_updates_t ITypedProtobuf<CSVCMsg_PacketEntities_outofpvs_entity_updates_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PacketEntities_outofpvs_entity_updates_tImpl(handle, isManuallyAllocated);


  public int Count { get; set; }


  public byte[] Data { get; set; }

}
