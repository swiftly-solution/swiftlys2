
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_ClassInfoImpl : NetMessage<CSVCMsg_ClassInfo>, CSVCMsg_ClassInfo
{
  public CSVCMsg_ClassInfoImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool CreateOnClient
  { get => Accessor.GetBool("create_on_client"); set => Accessor.SetBool("create_on_client", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_ClassInfo_class_t> Classes
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_ClassInfo_class_t>(Accessor, "classes"); }

}
