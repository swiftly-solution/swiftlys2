
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCUpdateSQLKeyValue : ITypedProtobuf<CMsgGCToGCUpdateSQLKeyValue>
{
  static CMsgGCToGCUpdateSQLKeyValue ITypedProtobuf<CMsgGCToGCUpdateSQLKeyValue>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCUpdateSQLKeyValueImpl(handle, isManuallyAllocated);


  public string KeyName { get; set; }

}
