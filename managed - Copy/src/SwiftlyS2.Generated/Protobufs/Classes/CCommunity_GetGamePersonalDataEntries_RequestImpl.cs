
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCommunity_GetGamePersonalDataEntries_RequestImpl : TypedProtobuf<CCommunity_GetGamePersonalDataEntries_Request>, CCommunity_GetGamePersonalDataEntries_Request
{
  public CCommunity_GetGamePersonalDataEntries_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public string Type
  { get => Accessor.GetString("type"); set => Accessor.SetString("type", value); }


  public string ContinueToken
  { get => Accessor.GetString("continue_token"); set => Accessor.SetString("continue_token", value); }

}
