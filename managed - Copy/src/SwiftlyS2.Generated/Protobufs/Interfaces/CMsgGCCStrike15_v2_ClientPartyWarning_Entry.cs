
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientPartyWarning_Entry : ITypedProtobuf<CMsgGCCStrike15_v2_ClientPartyWarning_Entry>
{
  static CMsgGCCStrike15_v2_ClientPartyWarning_Entry ITypedProtobuf<CMsgGCCStrike15_v2_ClientPartyWarning_Entry>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientPartyWarning_EntryImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public uint Warntype { get; set; }

}
