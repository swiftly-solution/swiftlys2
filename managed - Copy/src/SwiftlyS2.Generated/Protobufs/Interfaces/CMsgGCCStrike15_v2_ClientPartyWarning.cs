
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientPartyWarning : ITypedProtobuf<CMsgGCCStrike15_v2_ClientPartyWarning>
{
  static CMsgGCCStrike15_v2_ClientPartyWarning ITypedProtobuf<CMsgGCCStrike15_v2_ClientPartyWarning>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientPartyWarningImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_ClientPartyWarning_Entry> Entries { get; }

}
