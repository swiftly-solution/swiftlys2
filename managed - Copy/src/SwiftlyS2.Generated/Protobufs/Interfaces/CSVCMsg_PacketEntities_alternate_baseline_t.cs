
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_PacketEntities_alternate_baseline_t : ITypedProtobuf<CSVCMsg_PacketEntities_alternate_baseline_t>
{
  static CSVCMsg_PacketEntities_alternate_baseline_t ITypedProtobuf<CSVCMsg_PacketEntities_alternate_baseline_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PacketEntities_alternate_baseline_tImpl(handle, isManuallyAllocated);


  public int EntityIndex { get; set; }


  public int BaselineIndex { get; set; }

}
