
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCHInviteUserToLobby : ITypedProtobuf<CMsgGCHInviteUserToLobby>
{
  static CMsgGCHInviteUserToLobby ITypedProtobuf<CMsgGCHInviteUserToLobby>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCHInviteUserToLobbyImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public uint Appid { get; set; }


  public ulong SteamidInvited { get; set; }


  public ulong SteamidLobby { get; set; }

}
