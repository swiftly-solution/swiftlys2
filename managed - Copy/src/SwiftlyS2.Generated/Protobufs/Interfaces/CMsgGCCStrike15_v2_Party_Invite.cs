
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Party_Invite : ITypedProtobuf<CMsgGCCStrike15_v2_Party_Invite>
{
  static CMsgGCCStrike15_v2_Party_Invite ITypedProtobuf<CMsgGCCStrike15_v2_Party_Invite>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Party_InviteImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public uint Lobbyid { get; set; }

}
