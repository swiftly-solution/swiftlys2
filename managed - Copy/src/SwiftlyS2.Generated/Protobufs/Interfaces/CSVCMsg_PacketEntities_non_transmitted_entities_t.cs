
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_PacketEntities_non_transmitted_entities_t : ITypedProtobuf<CSVCMsg_PacketEntities_non_transmitted_entities_t>
{
  static CSVCMsg_PacketEntities_non_transmitted_entities_t ITypedProtobuf<CSVCMsg_PacketEntities_non_transmitted_entities_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PacketEntities_non_transmitted_entities_tImpl(handle, isManuallyAllocated);


  public int HeaderCount { get; set; }


  public byte[] Data { get; set; }

}
