
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_ClientInfoImpl : NetMessage<CCLCMsg_ClientInfo>, CCLCMsg_ClientInfo
{
  public CCLCMsg_ClientInfoImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint SendTableCrc
  { get => Accessor.GetUInt32("send_table_crc"); set => Accessor.SetUInt32("send_table_crc", value); }


  public uint ServerCount
  { get => Accessor.GetUInt32("server_count"); set => Accessor.SetUInt32("server_count", value); }


  public bool IsHltv
  { get => Accessor.GetBool("is_hltv"); set => Accessor.SetBool("is_hltv", value); }


  public uint FriendsId
  { get => Accessor.GetUInt32("friends_id"); set => Accessor.SetUInt32("friends_id", value); }


  public string FriendsName
  { get => Accessor.GetString("friends_name"); set => Accessor.SetString("friends_name", value); }

}
