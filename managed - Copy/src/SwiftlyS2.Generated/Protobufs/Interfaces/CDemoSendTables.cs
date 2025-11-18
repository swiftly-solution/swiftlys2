
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoSendTables : ITypedProtobuf<CDemoSendTables>
{
  static CDemoSendTables ITypedProtobuf<CDemoSendTables>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoSendTablesImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }

}
