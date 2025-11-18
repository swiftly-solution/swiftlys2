
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoStringTables_items_t : ITypedProtobuf<CDemoStringTables_items_t>
{
  static CDemoStringTables_items_t ITypedProtobuf<CDemoStringTables_items_t>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoStringTables_items_tImpl(handle, isManuallyAllocated);


  public string Str { get; set; }


  public byte[] Data { get; set; }

}
