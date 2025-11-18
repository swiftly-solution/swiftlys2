
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgInviteToPartyImpl : TypedProtobuf<CMsgInviteToParty>, CMsgInviteToParty
{
  public CMsgInviteToPartyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong SteamId
  { get => Accessor.GetUInt64("steam_id"); set => Accessor.SetUInt64("steam_id", value); }


  public uint ClientVersion
  { get => Accessor.GetUInt32("client_version"); set => Accessor.SetUInt32("client_version", value); }


  public uint TeamInvite
  { get => Accessor.GetUInt32("team_invite"); set => Accessor.SetUInt32("team_invite", value); }

}
