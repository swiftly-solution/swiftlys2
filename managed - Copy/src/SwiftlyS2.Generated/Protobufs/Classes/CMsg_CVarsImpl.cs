
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsg_CVarsImpl : TypedProtobuf<CMsg_CVars>, CMsg_CVars
{
  public CMsg_CVarsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsg_CVars_CVar> Cvars
  { get => new ProtobufRepeatedFieldSubMessageType<CMsg_CVars_CVar>(Accessor, "cvars"); }

}
