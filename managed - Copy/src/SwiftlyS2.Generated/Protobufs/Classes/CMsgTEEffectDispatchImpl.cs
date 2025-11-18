
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEEffectDispatchImpl : NetMessage<CMsgTEEffectDispatch>, CMsgTEEffectDispatch
{
  public CMsgTEEffectDispatchImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgEffectData Effectdata
  { get => new CMsgEffectDataImpl(NativeNetMessages.GetNestedMessage(Address, "effectdata"), false); }

}
