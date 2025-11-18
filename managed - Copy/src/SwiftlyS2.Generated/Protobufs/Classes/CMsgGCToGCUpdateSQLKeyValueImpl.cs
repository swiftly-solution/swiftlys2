
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCUpdateSQLKeyValueImpl : TypedProtobuf<CMsgGCToGCUpdateSQLKeyValue>, CMsgGCToGCUpdateSQLKeyValue
{
  public CMsgGCToGCUpdateSQLKeyValueImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string KeyName
  { get => Accessor.GetString("key_name"); set => Accessor.SetString("key_name", value); }

}
