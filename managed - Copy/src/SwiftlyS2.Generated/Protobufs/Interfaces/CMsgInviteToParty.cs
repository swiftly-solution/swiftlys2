
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgInviteToParty : ITypedProtobuf<CMsgInviteToParty>
{
  static CMsgInviteToParty ITypedProtobuf<CMsgInviteToParty>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgInviteToPartyImpl(handle, isManuallyAllocated);


  public ulong SteamId { get; set; }


  public uint ClientVersion { get; set; }


  public uint TeamInvite { get; set; }

}
