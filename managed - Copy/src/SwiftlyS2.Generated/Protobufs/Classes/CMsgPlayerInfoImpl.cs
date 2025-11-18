
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgPlayerInfoImpl : TypedProtobuf<CMsgPlayerInfo>, CMsgPlayerInfo
{
  public CMsgPlayerInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public int Userid
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public bool Fakeplayer
  { get => Accessor.GetBool("fakeplayer"); set => Accessor.SetBool("fakeplayer", value); }


  public bool Ishltv
  { get => Accessor.GetBool("ishltv"); set => Accessor.SetBool("ishltv", value); }

}
