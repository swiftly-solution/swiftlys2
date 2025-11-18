
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_ClassInfo_class_tImpl : TypedProtobuf<CSVCMsg_ClassInfo_class_t>, CSVCMsg_ClassInfo_class_t
{
  public CSVCMsg_ClassInfo_class_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ClassId
  { get => Accessor.GetInt32("class_id"); set => Accessor.SetInt32("class_id", value); }


  public string ClassName
  { get => Accessor.GetString("class_name"); set => Accessor.SetString("class_name", value); }

}
