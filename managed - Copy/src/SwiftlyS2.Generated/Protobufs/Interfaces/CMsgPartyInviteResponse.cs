
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgPartyInviteResponse : ITypedProtobuf<CMsgPartyInviteResponse>
{
  static CMsgPartyInviteResponse ITypedProtobuf<CMsgPartyInviteResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgPartyInviteResponseImpl(handle, isManuallyAllocated);


  public ulong PartyId { get; set; }


  public bool Accept { get; set; }


  public uint ClientVersion { get; set; }


  public uint TeamInvite { get; set; }

}
