
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_HintTextImpl : NetMessage<CCSUsrMsg_HintText>, CCSUsrMsg_HintText
{
  public CCSUsrMsg_HintTextImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Message
  { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }

}
