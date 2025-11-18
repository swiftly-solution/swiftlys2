
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoClassInfo_class_tImpl : TypedProtobuf<CDemoClassInfo_class_t>, CDemoClassInfo_class_t
{
  public CDemoClassInfo_class_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ClassId
  { get => Accessor.GetInt32("class_id"); set => Accessor.SetInt32("class_id", value); }


  public string NetworkName
  { get => Accessor.GetString("network_name"); set => Accessor.SetString("network_name", value); }


  public string TableName
  { get => Accessor.GetString("table_name"); set => Accessor.SetString("table_name", value); }

}
