
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgPartyInviteResponseImpl : TypedProtobuf<CMsgPartyInviteResponse>, CMsgPartyInviteResponse
{
  public CMsgPartyInviteResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong PartyId
  { get => Accessor.GetUInt64("party_id"); set => Accessor.SetUInt64("party_id", value); }


  public bool Accept
  { get => Accessor.GetBool("accept"); set => Accessor.SetBool("accept", value); }


  public uint ClientVersion
  { get => Accessor.GetUInt32("client_version"); set => Accessor.SetUInt32("client_version", value); }


  public uint TeamInvite
  { get => Accessor.GetUInt32("team_invite"); set => Accessor.SetUInt32("team_invite", value); }

}
