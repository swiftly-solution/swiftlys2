
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameInfoImpl : TypedProtobuf<CGameInfo>, CGameInfo
{
  public CGameInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CGameInfo_CDotaGameInfo Dota
  { get => new CGameInfo_CDotaGameInfoImpl(NativeNetMessages.GetNestedMessage(Address, "dota"), false); }


  public CGameInfo_CCSGameInfo Cs
  { get => new CGameInfo_CCSGameInfoImpl(NativeNetMessages.GetNestedMessage(Address, "cs"), false); }

}
