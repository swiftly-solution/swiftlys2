
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCHInviteUserToLobbyImpl : TypedProtobuf<CMsgGCHInviteUserToLobby>, CMsgGCHInviteUserToLobby
{
  public CMsgGCHInviteUserToLobbyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public ulong SteamidInvited
  { get => Accessor.GetUInt64("steamid_invited"); set => Accessor.SetUInt64("steamid_invited", value); }


  public ulong SteamidLobby
  { get => Accessor.GetUInt64("steamid_lobby"); set => Accessor.SetUInt64("steamid_lobby", value); }

}
