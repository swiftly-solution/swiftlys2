
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPartyWarningImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPartyWarning>, CMsgGCCStrike15_v2_ClientPartyWarning
{
  public CMsgGCCStrike15_v2_ClientPartyWarningImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_ClientPartyWarning_Entry> Entries
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_ClientPartyWarning_Entry>(Accessor, "entries"); }

}
