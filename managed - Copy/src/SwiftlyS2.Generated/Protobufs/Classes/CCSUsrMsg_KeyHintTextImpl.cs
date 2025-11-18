
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_KeyHintTextImpl : NetMessage<CCSUsrMsg_KeyHintText>, CCSUsrMsg_KeyHintText
{
  public CCSUsrMsg_KeyHintTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldValueType<string> Messages
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "messages"); }

}
